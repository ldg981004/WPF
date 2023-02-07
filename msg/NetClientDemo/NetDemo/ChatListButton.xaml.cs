using NetDemo.Network;
using PacketLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// ChatListButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatListButton : UserControl, INetworkEventListener
    {
        public ChatListButton()
        {
            InitializeComponent();
            NetworkProxy.Get.RegisterEventListener(this);
            Unloaded += (s, e) =>
            {
                NetworkProxy.Get.UnregisterEventListener(this);
            };

        }

        private void chat_list_btn_Click(object sender, RoutedEventArgs e)
        {
            List<string> chat_info = Regex.Split(chat_list_btn.Content.ToString(), "\r\n").ToList();
            // 버튼의 content를 엔터 기준으로 나눠서 리스트 형태로 변환

            int cs_chatroom = (int)PacketID.CS_ChatRoom;

            List<string> info_list = new List<string>()
            {
                cs_chatroom.ToString(),
                chat_info[0].ToString(),    // 채팅창 고유 번호
                chat_info[1].ToString(),    // 채팅창 이름
                chat_info[2].ToString(),    // 채팅창 멤버
                chat_info[3].ToString()     // 채팅창 방장
                // 버튼 content에 써있는 애들을 서버로 전송
            };

            NetworkProxy.Get.SendMessage(info_list);

        }

        public void OnEventReceived(string msg)
        {
            //throw new NotImplementedException();

            AddLogMessage(msg);
        }

        public void AddLogMessage(string msg)
        {

        }

    }
}
