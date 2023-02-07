using CommonLib.Network;
using NetDemo.Network;
using PacketLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace NetDemo
{
    /// <summary>
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : UserControl, INetworkEventListener
    {
        private string _user = null;
        public MainPage()
        {
            InitializeComponent();
            NetworkProxy.Get.RegisterEventListener(this);

            Unloaded += (s, e) =>
            {
                NetworkProxy.Get.UnregisterEventListener(this);
            };

        }

        public void AddLogMessage(string msg)
        {
            List<string> server_messages = msg.Split(' ').ToList();
            string head = server_messages[0].ToString();

            int cs_user = (int)PacketID.CS_User;
            int sc_loginresult = (int)PacketID.SC_LoginResult;
            int sc_logoutuser = (int)PacketID.SC_LogOutUser;
            
            if (head == sc_loginresult.ToString())
                //PacketID가 loginresult(이름, 아이디 생년월일)인 경우
            {
                byte[] bytedecode = Convert.FromBase64String(server_messages[1]);
                string decodestring = Encoding.UTF8.GetString(bytedecode);
                lb_name.Content = "이름 : " + decodestring;
                lb_id.Content = "아이디 : " + server_messages[2];
                _user = server_messages[2];
                // _user는 메시지의 사용자 아이디
                lb_birth.Content = "생년월일 : " + server_messages[3];
                // 로그인한 사용자의 개인 정보를 상단에 표시

                List<string> user = new List<string>()
                {
                    cs_user.ToString(),
                    _user
                };
                // list user를 만들어서 지금 사용자가 누구인지 서버로 보냄

                NetworkProxy.Get.SendMessage(user);
                NetworkProxy.Get.UnregisterEventListener(this);
                User_Grid.Children.Clear();
                User_Grid.Children.Add(new MainUser());
                // Usre_Grid에 Mainuer를 추가
            }

            else if (head == sc_logoutuser.ToString())
            {
                if (server_messages[1] == _user)
                {
                    MessageBox.Show("다른 PC에서 접속");
                }
            }
        }

        public void OnEventReceived(string msg)
        {
            AddLogMessage(msg);
        }

        private void log_out_Click(object sender, RoutedEventArgs e)
            // 로그아웃 버튼 클릭 에벤트
        {
            int cs_logout = (int)PacketID.CS_LogOut;
            
            List<string> log_out_user = new List<string>()
            {
                cs_logout.ToString(),
                _user
            };
            // log_out_user list를 만들어서 패킷 아이디와
            // 누가 로그아웃 하는지를 서버로 보냄
            NetworkProxy.Get.SendMessage(log_out_user);
            main_grid.Children.Clear();
            main_grid.RowDefinitions.Clear();
            main_grid.ColumnDefinitions.Clear();
            main_grid.Children.Add(new Login());

            // 로그아웃 버튼 클릭 시 로그인 화면으로 다시 돌아감
        }

        private void Chat_Button_Click(object sender, RoutedEventArgs e)
        {
            // 채팅버튼 (아래 가운데 채팅 모양 버튼) 클릭 이벤트

            int cs_chatroomlist = (int)PacketID.CS_ChatroomList;

            List<string> chatroomList = new List<string>()
            {
                cs_chatroomlist.ToString(),
                _user
            };
            // 패킷 아이디와 누구의 채팅방 리스트 인지를 서버로 보냄
            NetworkProxy.Get.SendMessage(chatroomList);
            NetworkProxy.Get.UnregisterEventListener(this);
            User_Grid.Children.Clear();
            User_Grid.ColumnDefinitions.Clear();
            
            User_Grid.Children.Add(new ChatRoomList());
            // 채팅 리스트 화면으로 이동
            
        }

        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            // 홈버튼(아래 왼쪽) 클릭 이벤트

            int cs_homebtn = (int)PacketID.CS_HomeBtn;
            List<string> homebtnList = new List<string>()
            {
                cs_homebtn.ToString(),
                _user
            };
            NetworkProxy.Get.SendMessage(homebtnList);
            // 홈버튼을 눌렀다고 서버로 메시지를 보냄

            //MessageBox.Show("사용자 : " + _user, "현재 사용자");
            User_Grid.Children.Clear();
            User_Grid.RowDefinitions.Clear();
            User_Grid.ColumnDefinitions.Clear();
            User_Grid.Children.Add(new MainUser());
            // Mainuser 페이지를 추가 => 채팅방 만들기 / 현재 누가 로그인 되어 있는지
        }

        private void Setting_Button_Click(object sender, RoutedEventArgs e)
        {
            // 비밀번호 변경 버튼
            int cs_changepassword = (int)PacketID.CS_ChangePassword;
            List<string> changepasswordList = new List<string>()
            {
                cs_changepassword.ToString(),
                _user
                // 패킷 아이디와 현재 로그인한 사람이 누구인지를 서버로 보냄
            };
            NetworkProxy.Get.SendMessage(changepasswordList);

            User_Grid.Children.Clear();
            User_Grid.RowDefinitions.Clear();
            User_Grid.ColumnDefinitions.Clear();
            User_Grid.Children.Add(new ChangePw());
            // 현재 그리드를 비밀번호 변경 그리드 화면으로 변경
        }
    }
}
