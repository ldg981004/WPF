using CommonLib.Network;
using NetDemo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace NetDemo.Network
{
    internal interface INetworkEventListener // interface는 다중 상속 가능 => 멤버 변수가 없는 경우
    { 
        void OnEventReceived(string msg);
    }

    internal class NetworkProxy : SingletonBase<NetworkProxy> // internal은 어셈블리 단위로 접근 가능
    {
        private DispatcherTimer _netTimer = null;
        private SimpleNetClient _netClient = null;
        private bool _isConnected = false;
        private bool _isConnecting = false;
        // 네트워크 기능 4가지는 NetWorkProxy에서 기능을 가짐

        private List<INetworkEventListener> _eventListeners = new List<INetworkEventListener>();

        public void RegisterEventListener(INetworkEventListener listener)
        {
            if (null == listener)
                return;

            _eventListeners.Add(listener);
        }
        public void UnregisterEventListener(INetworkEventListener listener)
        {
            if (null == listener)
                return;

            _eventListeners.Remove(listener);
        }

        protected override void OnInit() // 부모 클래스(SIngletonBase에 있는 OnInit 메소드를 재정의)
        {
            if (null == _netClient) // _netClient가 null일 경우
            {
                _netClient = new SimpleNetClient(); // 새로운 객체를 생성
                _netClient.InitClient();

                //register event handlers
                _netClient.OnConnected += OnConnected_Recv;
                _netClient.OnDisconnected += OnDisconnected_Recv;
                _netClient.OnError += OnError_Recv;
                _netClient.OnMessage += OnMessage_Recv;
            }

            if (null == _netTimer)
            {
                _netTimer = new DispatcherTimer();
                _netTimer.Tick += NetworkTimer_Tick;
                // polling every 200ms
                _netTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                _netTimer.Start();
            }
        }

        public bool ConnectToServer(string ip, int port) // 서버에 연결
        {
            if (string.IsNullOrWhiteSpace(ip) // 포트번호 체크
                || 0 > port || ushort.MaxValue <= port)
                return false;

            if (_isConnected)
            // 이미 연결이 되어있으면 false
            {
                List<string> list = new List<string>()
                {
                    "Already-Connect"
                };
                SendMessage(list);
                return false;
            } 

            _netClient.ConnectToServer(ip, port);
            return true;
        }

        public void SendMessage(List<string> cmsgArr)
            // 연결이 되어있으면 msg를 보냄
        {
            if (!_isConnected)
                return;

            StringBuilder csb = new StringBuilder();

            foreach(string cmsg in cmsgArr)
            {
                csb.Append(cmsg);
                csb.Append(" ");
            }
            cmsgArr.RemoveAll(s => s == "");
            // Sendmessage를 할 때 리스트의 공백을 제거하고 보냄

            _netClient.SendMessage(csb.ToString());
        }

        public void TerminateProxy()
        {
            if (null != _netTimer)
            {
                _netTimer.Stop();
                _netTimer = null;
            }

            if (null != _netClient)
            {
                _netClient.TerminateClient();
                _netClient = null;
            }
        }


        private void OnConnected_Recv(string msg)
        {
            _isConnected = true;
            _isConnecting = false;
        }
        private void OnDisconnected_Recv(string msg)
        {
            _isConnecting = false;
            _isConnected = false;
        }
        private void OnError_Recv(string msg)
        {
            var em = new ErrorMessage(msg);
            switch (em.ErrorType)
            {
                case 1:
                    {
                        _isConnecting = false;
                        _isConnected = false;
                    }
                    break;
                default:
                    break;
            }

        }
        private void OnMessage_Recv(string msg)
        {
            foreach (var l in _eventListeners.ToList())
            {
                if (null == l)
                    continue;

                l.OnEventReceived(msg);
            }
        }
        private void NetworkTimer_Tick(object sender, EventArgs e)
        {
            if (null != _netClient)
            {
                _netClient.UpdateClient();
            }
        }
    }
}
