using NetDemo.Network;
using PacketLib;
using System;
using System.Collections.Generic;
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
    /// ChatRoomInvite.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatRoomInvite : UserControl, INetworkEventListener
    {
        string room_num = "";
        string user = "";
        public ChatRoomInvite()
        {
            InitializeComponent();
            Unloaded += (s, e) =>
            {
                NetworkProxy.Get.UnregisterEventListener(this);
            };

            NetworkProxy.Get.RegisterEventListener(this);            
        }


        public void AddLogMessage(string msg)
        {
            List<string> server_messages = msg.Split(' ').ToList();

            string head = server_messages[0].ToString();

            int sc_currentmember = (int)PacketID.SC_CurrentMember;
            int sc_alluserlist = (int)PacketID.SC_AllUserList;
            int sc_invitesuc = (int)PacketID.SC_InviteSuc;
            int sc_invitefail = (int)PacketID.SC_InviteFail;

            if(head == sc_currentmember.ToString())
            {
                server_messages.RemoveAll(s => s == "");

                for(int i = 3; i <server_messages.Count; i++)
                {
                    Label lb_current_user = new Label();
                    lb_current_user.Content = server_messages[i].ToString();

                    mem_panel.Children.Add(lb_current_user);
                }
                room_num = server_messages[1];
                user = server_messages[2];
            }

            else if(head == sc_alluserlist.ToString())
            {
                for (int i = 1; i < server_messages.Count; i++)
                {
                    CheckBox chat_box = new CheckBox();
                    chat_box.Width = 150;
                    chat_box.Content = server_messages[i].ToString();

                    user_panel.Children.Add(chat_box);
                }
            }
            else if (head == sc_invitesuc.ToString())
            {
                MessageBox.Show("초대 성공", "Success");

                int cs_homebtn = (int)PacketID.CS_HomeBtn;
                List<string> homebtnList = new List<string>()
                {
                    cs_homebtn.ToString(),
                    user
                };
                NetworkProxy.Get.SendMessage(homebtnList);

                invite_grid.Children.Clear();
                invite_grid.RowDefinitions.Clear();
                invite_grid.ColumnDefinitions.Clear();
                invite_grid.Children.Add(new MainUser());
            }

            else if (head == sc_invitefail.ToString())
            {
                MessageBox.Show("이미 채팅방에 참여","초대 실패");
            }

        }
        public void OnEventReceived(string msg)
        {
            AddLogMessage(msg);
        }

        private void invite_button_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";

            int cs_chatjoin = (int)PacketID.CS_ChatJoin;

            List<string> join_user = new List<string>();


            Button btn = sender as Button;
            if(btn.Name == "invite_button")
            {
                IEnumerable<CheckBox> ChkBoxes = from checkbox in this.user_panel.Children.OfType<CheckBox>()
                                                 select checkbox;

                join_user.Add(cs_chatjoin.ToString());
                join_user.Add(room_num);

                foreach (CheckBox chk in ChkBoxes)
                {
                    if (chk.IsChecked == true)
                    {
                        msg = msg + chk.Content.ToString();
                        join_user.Add(chk.Content.ToString());
                    }
                }
                if (msg == "")
                {
                    MessageBox.Show("초대할 사용자를 선택해 주세요.", "ERROR");
                }
                else
                {
                    NetworkProxy.Get.SendMessage(join_user);
                }
               
            }
        }
    }
}
