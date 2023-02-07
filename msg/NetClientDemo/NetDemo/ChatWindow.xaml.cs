using NetDemo.Network;
using PacketLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace NetDemo

/// <summary>
/// ChatWindow.xaml에 대한 상호 작용 논리
/// </summary>
{ public partial class ChatWindow : UserControl, INetworkEventListener
    {
        string room_num = "";
        string room_name = "";
        string member = "";
        string maker = "";
        public ChatWindow()
        {
            InitializeComponent();

            NetworkProxy.Get.RegisterEventListener(this);
            // 채팅방에 관련된 내용을 가져옴

            int cs_previous = (int)PacketID.CS_Previous;
            int cs_maker = (int)PacketID.CS_Maker;

            List<string> pre_list = new List<string>()
            {
                cs_previous.ToString(),
                "Previous_Record"
            };
            NetworkProxy.Get.SendMessage(pre_list);

            List<string> maker = new List<string>()
            {
                cs_maker.ToString(),
                "Who_is_maker"
            };
            NetworkProxy.Get.SendMessage(maker);

            Unloaded += (s, e) =>
            {
                NetworkProxy.Get.UnregisterEventListener(this);
            };

        }

        public void OnEventReceived(string msg)
        {
            //throw new NotImplementedException();

            AddLogMessage(msg);
        }

        public void AddLogMessage(string msg)
        {
            List<string> server_messages = msg.Split(' ').ToList();

            string head = server_messages[0].ToString();

            int sc_chatroomclick = (int)PacketID.SC_ChatRoomClick;
            int sc_who_are_you = (int)PacketID.SC_Who_are_you;
            int sc_prechat = (int)PacketID.SC_PreChat;
            int sc_outok = (int)PacketID.SC_OutOk;
            int sc_sendchat = (int)PacketID.SC_SendChat;
            int sc_makeranduser = (int)PacketID.SC_MakerAndUser;
            int sc_invitemember = (int)PacketID.SC_InviteMember;
            int sc_expulsionmember = (int)PacketID.SC_ExpulsionMember;

            if (head == sc_chatroomclick.ToString())
            {
                who.Content = server_messages[2].ToString();
                maker = server_messages.Last().ToString();
                room_num = server_messages[3].ToString();
                room_name = server_messages[4].ToString();

                Label lb_maker = new Label();
                lb_maker.Content = "방장 : " + maker;
                lb_maker.VerticalAlignment = VerticalAlignment.Center;
                lb_maker.HorizontalContentAlignment = HorizontalAlignment.Center;
                lb_maker.FontWeight = FontWeights.Bold;
                lb_maker.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                maker_grid.Children.Add(lb_maker);
                
                
                for(int i = 5; i < server_messages.Count-1; i++)
                {
                    member = member + server_messages[i].ToString() + " ";
                }

                lb_mem.Content =  member;
                member = "";

                if(who.Content.ToString() == maker)
                {

                    Button btn_remove = new Button();
                    btn_remove.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                    btn_remove.Height = 25;
                    btn_remove.Width = 75;
                    btn_remove.FontWeight = FontWeights.Bold;
                    btn_remove.FontSize = 15;
                    btn_remove.Click += btn_remove_click;
                    btn_remove.Content = "강퇴하기";
                    btn_remove_grid.Children.Add(btn_remove);
                }
            }
            else if (head == sc_who_are_you.ToString())
            {
                int i_am = (int)PacketID.CS_I_am;

                List<string> i_am_list = new List<string>()
                {
                    i_am.ToString(),
                    room_num.ToString(),
                };
                NetworkProxy.Get.SendMessage(i_am_list);
            }

            else if (head == sc_invitemember.ToString())
            {
                server_messages.RemoveAll(s => s == "");

                if(room_num == server_messages[1])
                {
                    string member = "";

                    for(int i = 2; i < server_messages.Count; i++)
                    {
                        member = member + server_messages[i] + " ";
                    }

                    lb_mem.Content = member;
                }
            }
            
            else if (server_messages.Contains(sc_prechat.ToString()))
            {
                {
                    Label textlb = new Label();

                    byte[] bytedecode = Convert.FromBase64String(server_messages[2]);
                    string decodestring = Encoding.UTF8.GetString(bytedecode);

                    textlb.Content = server_messages[1] + " : " + decodestring + " - " + server_messages[3];
                    message_panel.Children.Add(textlb);
                }
            }

            else if (head == sc_sendchat.ToString())
            {
                if (room_num == server_messages[2])
                {
                    Label textlabel = new Label();

                    byte[] bytedecode = Convert.FromBase64String(server_messages[6]);
                    string decodestring = Encoding.UTF8.GetString(bytedecode);

                    textlabel.Content = server_messages[1] + " : " + decodestring + " - " + server_messages[3].ToString();
                    message_panel.Children.Add(textlabel);
                }
                
            }

            else if (head == sc_makeranduser.ToString())
            {
                if(room_num == server_messages[1])
                {
                    server_messages.RemoveAll(s => s == "");

                    maker_grid.Children.Clear();
                    Label lb_maker = new Label();
                    lb_maker.Content = "방장 : " + server_messages[2];
                    lb_maker.VerticalAlignment = VerticalAlignment.Center;
                    lb_maker.HorizontalContentAlignment = HorizontalAlignment.Center;
                    lb_maker.FontWeight = FontWeights.Bold;
                    lb_maker.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    maker_grid.Children.Add(lb_maker);

                    string member = "";

                    for(int i = 3; i < server_messages.Count; i++)
                    {
                        member = member + server_messages[i] + " ";
                    }

                    lb_mem.Content = member;

                }
            }

            else if (head == sc_expulsionmember.ToString())
            {
                if (room_num == server_messages[1])
                {
                    server_messages.RemoveAll(s => s == "");

                    string member = "";

                    for (int i = 2; i < server_messages.Count; i++)
                    {
                        member = member + server_messages[i] + " ";
                    }

                    lb_mem.Content = member;

                }
            }

            else if (head == sc_outok.ToString())
            {
                int cs_chatroomlist = (int)PacketID.CS_ChatroomList;

                List<string> return_list = new List<string>()
                {
                    cs_chatroomlist.ToString(),
                    who.Content.ToString(),
                };

                int cs_homebtn = (int)PacketID.CS_HomeBtn;
                List<string> homebtnList = new List<string>()
                {
                    cs_homebtn.ToString(),
                    who.Content.ToString()
                };
                NetworkProxy.Get.SendMessage(homebtnList);

                MessageBox.Show("나가기 완료");

                chat_window_grid.Children.Clear();
                chat_window_grid.RowDefinitions.Clear();
                chat_window_grid.ColumnDefinitions.Clear();
                chat_window_grid.Children.Add(new MainUser());
            }

        }

        private void submit_button_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(tb_mes.Text))
            {
                MessageBox.Show("메시지를 입력하세요.", "ERROR");
                tb_mes.Text = null;
            }

            else if (lb_mem.Content.ToString().Contains(who.Content.ToString()) == false)
            {
                MessageBox.Show("방에서 강퇴 당했습니다.", "ERROR");
            }

            else
            {
                int cs_sendchat = (int)PacketID.CS_SendChat;

                List<string> content = new List<string>()
                {
                    cs_sendchat.ToString(),
                    who.Content.ToString(),
                    room_name,
                    lb_mem.Content.ToString().Replace(" ", string.Empty),
                    tb_mes.Text.ToString().Replace(" ", string.Empty),
                    room_num.ToString(),
                };

                NetworkProxy.Get.SendMessage(content);

                tb_mes.Text = null;
            }
        }

        private void out_button_Click(object sender, RoutedEventArgs e)
        {
            int cs_outroom = (int)PacketID.CS_OutRoom;

            List<string> out_room = new List<string>()
            {
                cs_outroom.ToString(),
                who.Content.ToString(),
                room_num,
                "Out"
            };
            NetworkProxy.Get.SendMessage(out_room);

        }

        private void invite_button_Click(object sender, RoutedEventArgs e)
        {
            int cs_chatmember = (int)PacketID.CS_ChatMember;

            List<string> chatmember = new List<string>()
            {
                cs_chatmember.ToString(),
                room_num.ToString(),
                who.Content.ToString()
            };
            NetworkProxy.Get.SendMessage(chatmember);

            int cs_alluser = (int)PacketID.CS_AllUser;

            List<string> all_user = new List<string>()
            {
                cs_alluser.ToString(),
                "See all user"

            };
            NetworkProxy.Get.SendMessage(all_user);

            chat_window_grid.Children.Clear();
            chat_window_grid.RowDefinitions.Clear();
            chat_window_grid.Children.Add(new ChatRoomInvite());
        }

        private void btn_remove_click(object sender, EventArgs e)
        {
            int cs_removeuser = (int)PacketID.CS_RemoveUser;

            List<string> remove_info = new List<string>()
            {
                cs_removeuser.ToString(),
                room_num.ToString(),
                who.Content.ToString()
            };
            NetworkProxy.Get.SendMessage(remove_info);

            chat_window_grid.Children.Clear();
            chat_window_grid.RowDefinitions.Clear();
            chat_window_grid.Children.Add(new ChatRoomExpulsion());
        }
    }
}
