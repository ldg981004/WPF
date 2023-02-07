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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetDemo
{
    /// <summary>
    /// ChatRoomExpulsion.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatRoomExpulsion : UserControl, INetworkEventListener
    {
        string num = "";
        string user = "";
        public ChatRoomExpulsion()
        {
            InitializeComponent();
            NetworkProxy.Get.RegisterEventListener(this);

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

            int sc_removemember = (int)PacketID.SC_RemoveMember;
            int sc_expusionresult = (int)PacketID.SC_ExpusionResult;

            if (head == sc_removemember.ToString())
            {
                string who = server_messages.Last();
                user = who;
                //[0] Packet 아이디, [1] chatroom-num [2]~[last-1] 맴버, [last] 방장

                num = server_messages[1];

                server_messages.RemoveAll(s => s == "");

                for (int i = 2; i < server_messages.Count - 1; i++)
                {
                    CheckBox chat_box = new CheckBox();
                    chat_box.Width = 150;
                    chat_box.Content = server_messages[i].ToString();
                    if (chat_box.Content.ToString() == who)
                    {
                        chat_box.IsEnabled = false;
                    }

                    remove_panel.Children.Add(chat_box);
                }
            }

            else if (head == sc_expusionresult.ToString())
            {
                MessageBox.Show("강퇴 완료", "Success");

                int cs_homebtn = (int)PacketID.CS_HomeBtn;
                List<string> homebtnList = new List<string>()
                {
                    cs_homebtn.ToString(),
                    user
                };
                NetworkProxy.Get.SendMessage(homebtnList);

                expulsion_grid.Children.Clear();
                expulsion_grid.RowDefinitions.Clear();
                expulsion_grid.ColumnDefinitions.Clear();
                expulsion_grid.Children.Add(new MainUser());
                //NetworkProxy.Get.UnregisterEventListener(this);
            }
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            int sc_memberexpulsion = (int)PacketID.CS_MemberExpulsion;
            Button btn = sender as Button;
            string _msg = "";

            if(btn.Name == "btn_remove")
            {
                IEnumerable<CheckBox> ChkBoxes = from checkbox in this.remove_panel.Children.OfType<CheckBox>()
                                                 select checkbox;

                foreach (CheckBox chk in ChkBoxes)
                {
                    if (chk.IsChecked == true)
                    {
                        _msg = _msg + chk.Content.ToString() + " ";
                    }
                }
                if (_msg == "")
                {
                    MessageBox.Show("사용자를 선택해 주세요", "선택 오류");
                }
                else
                {
                    List<string> member_expulsion = new List<string>()
                {
                    sc_memberexpulsion.ToString(),
                    num,
                    _msg
                };
                    NetworkProxy.Get.SendMessage(member_expulsion);
                } 
            }
        }
    }
}
