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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetDemo
{
    /// <summary>
    /// ChatRoomList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatRoomList : UserControl, INetworkEventListener
    {
        string member = "";
        string us = "";
  
        public ChatRoomList()
        {
            InitializeComponent();            
            NetworkProxy.Get.RegisterEventListener(this);
            //chat_panel.Children.Add(new ChatListButton());
            Unloaded += (s, e) =>
            {
                NetworkProxy.Get.UnregisterEventListener(this);
            };
        }
        public void AddLogMessage(string msg)
        {
            List<string> server_messages = msg.Split(' ').ToList();

            int sc_chatinfo = (int)PacketID.SC_ChatInfo;
            int sc_chatroominfo = (int)PacketID.SC_ChatRoomInfo;
            int sc_dbnull = (int)PacketID.SC_DbNull;

            /*for(int i = 0; i < server_messages.Count - 1; i++)
            {*/
                if (server_messages.Contains(sc_chatinfo.ToString()))
                    // ChatRoom의 ChatRoomList에서 보냄
                {
                    server_messages.RemoveAll(s => s == "");
                    // server_messages의 모든 공란을 제거

                    ChatListButton btn = new ChatListButton();
                    // ChatListButton이라는 UserControl을 만들어 놓고 사용
                    us = server_messages[1].ToString();
                    // us는 현재 로그인 한 사람
                    for (int k = 4; k < server_messages.Count - 2; k++)
                    {
                        member = member + server_messages[k].ToString() + " ";
                    }

                    // btn의 chat_list_btn의 이름을 지정
                    // 버튼 클릭 이벤트는 ChatListButton에 만들어놓음

                    byte[] bytedecode = Convert.FromBase64String(server_messages.Last());
                    string decodestring = Encoding.UTF8.GetString(bytedecode);

                    btn.chat_list_btn.Content = server_messages[2].ToString() + "\r\n" +              //채팅창 고유 번호
                                                server_messages[3].ToString() + "\r\n" +              // 채팅창 이름
                                                member + "\r\n" +                                     // 채팅창 멤버
                                                server_messages[server_messages.Count - 2] + "\r\n" + // 채팅창 방장
                                                decodestring;                                         // 마지막 채팅 메시지
                    
                    chat_panel.Children.Add(btn);
                    member = "";

                }
            //}
            lb_us.Content = us + "의 채팅방";
            // 맨 위에 label에 현재 로그인한 사람의 채팅방이라고 표시

            if (server_messages[0] == sc_dbnull.ToString())
            {
                string name = server_messages[2];
                lb_us.Content = name + "의 채팅방";
                MessageBox.Show("비어있음", "Empty");
                // DB_Null인 경우 비어있다고 메시지 박스를 띄움
            }

            if (server_messages[0].Contains(sc_chatroominfo.ToString()))
            {
                server_messages.RemoveAll(s => s == "");
                string user_info = msg.Replace(sc_chatroominfo.ToString(), us);

                int cs_chatroomclick = (int)PacketID.CS_ChatRoomClick;

                List<string> clicklist = new List<string>()
                {
                    cs_chatroomclick.ToString(),
                    user_info
                };

                NetworkProxy.Get.SendMessage(clicklist);
                chat_grid.Children.Clear();
                chat_grid.Children.Add(new ChatWindow());
            }
        }

        public void OnEventReceived(string msg)
        {
            AddLogMessage(msg);
        }
        
    }
}
