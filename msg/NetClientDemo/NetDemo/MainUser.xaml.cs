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
    /// MainUser.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainUser : UserControl, INetworkEventListener
    {
        private static string _user;
        public MainUser()
        {
            InitializeComponent();
            NetworkProxy.Get.RegisterEventListener(this);

            Unloaded += (s, e) =>
            {
                NetworkProxy.Get.UnregisterEventListener(this);
            };

            int cs_logined = (int)PacketID.CS_Logined;
            int cs_alluserlist = (int)PacketID.CS_AllUserList;

            List<string> loginedlist = new List<string>()
            {
                cs_logined.ToString()
            };

            List<string> alluserlist = new List<string>()
            {
                cs_alluserlist.ToString()
            };

            NetworkProxy.Get.SendMessage(loginedlist);
            NetworkProxy.Get.SendMessage(alluserlist);
            
            // MainUser 페이지가 초기화 되자 마자
            // 전체 사용자와 현재 로그인된 사용자 정보를 서버에 요청
        }

        public void OnEventReceived(string msg)
        {
            AddLogMessage(msg);
        }

        private void AddLogMessage(string msg)
        {
            List<string> server_messages = msg.Split(' ').ToList();
            string head = server_messages[0].ToString();
            // 서버에서 클라이언트로 보낸 메시지의 첫번째는 패킷 아이디 == head

            int sc_logineduser = (int)PacketID.SC_LoginedUser;
            int sc_alluser = (int)PacketID.SC_AllUser;
            int sc_user = (int)PacketID.SC_User;
            int sc_homebtnalluser = (int)PacketID.SC_HomeBtnAlluser;
            int sc_homebtnlogined = (int)PacketID.SC_HomeBtnLogined;
            int sc_logoutlist = (int)PacketID.SC_LogOutList;
            int sc_loginuserlist = (int)PacketID.SC_LoginUserList;

            string login_user = "";

            if (head == sc_logineduser.ToString())
                // head가 sc_logineduser인 경우
            {
                for (int i = 1; i < server_messages.Count - 1; i++)
                {
                    login_user = login_user + server_messages[i].ToString();
                    Label lb = new Label();
                    lb.Content = server_messages[i].ToString() + " " + "온라인 상태";
                    login_user_panel.Children.Add(lb);
                    // 로그인된 사용자를 label로 표시해서 login_user_panel에 추가
                }
            }

            else if (head == sc_loginuserlist.ToString())
            {
                Label lb = new Label();
                lb.Content = server_messages[1] + " " + "온라인 상태" ;
                login_user_panel.Children.Add(lb);
            }
            
            else if (head == sc_user.ToString())
            // head가 sc_logineduser인 경우
            {
                _user = server_messages[1];
                // _user는 현재 로그인 한 사용자
            }

            else if (head == sc_alluser.ToString())
            // head가 sc_alluer인 경우
            {
                for (int i = 1; i < server_messages.Count() - 2; i++)
                {
                    CheckBox chat_box = new CheckBox();
                    chat_box.Width = 150;
                    chat_box.Content = server_messages[i].ToString(); 
                    //check box를 만들어서 전체 사용자를 나타냄
                    if (chat_box.Content.ToString() == server_messages.Last())
                    {
                        chat_box.IsEnabled = false;
                    }
                    // check box의 내용이 _user(자기 자신)와 같으면 체크 불가
                    // 본인은 어차피 채팅방에 들어감

                    user_panel.Children.Add(chat_box);
                }
            }

            else if (head == sc_homebtnalluser.ToString())
            // head가 sc_homebtnalluser인 경우
            {
                for (int i = 2; i < server_messages.Count() - 1; i++)
                {
                    CheckBox chat_box = new CheckBox();
                    chat_box.Width = 150;
                    chat_box.Content = server_messages[i].ToString();
                    if (chat_box.Content.ToString() == server_messages.Last())
                    {
                        chat_box.IsEnabled = false;
                    }
                    user_panel.Children.Add(chat_box);
                    // 홈버튼을 눌렀을때 서버에서 모든 회원가입된 유저를 리스트 형태로 보내줌
                    // 이 리스트를 checkbox 형태로 만듦
                }
            }

            else if (head == sc_homebtnlogined.ToString())
            // head가 sc_homebtnlogined인 경우
            {
                for (int i = 1; i < server_messages.Count; i++)
                {
                    login_user = login_user + server_messages[i].ToString();
                    Label lb = new Label();
                    lb.Content = server_messages[i].ToString() + " " + "온라인 상태";
                    login_user_panel.Children.Add(lb);
                }
                // 홈버튼을 눌렀을때 로그인한 사용자를 보여줌
            }
             
            else if (head == sc_logoutlist.ToString())
            {
                login_user_panel.Children.Clear();
                for (int i = 1; i < server_messages.Count; i++)
                {
                    login_user = login_user + server_messages[i].ToString();
                    Label lb = new Label();
                    lb.Content = server_messages[i].ToString() + " " + "온라인 상태";
                    login_user_panel.Children.Add(lb);
                }


            }

            
        }

        private void invite_Click(object sender, RoutedEventArgs e)
            // 초대하기 버튼 클릭 이벤트
        {
            int cs_chatroomcreate = (int)PacketID.CS_ChatroomCreate;
            Button btn = sender as Button;
            // button btn 생성
            string _msg = "";

            if (btn.Name == "invite") // btn의 이름이 invite이고 _msg가 null이 아니면
            {
                IEnumerable<CheckBox> ChkBoxes = from checkbox in this.user_panel.Children.OfType<CheckBox>()
                                                 select checkbox;
                
                // user_panel에 추가된 체크 박스들 중에서

                foreach (CheckBox chk in ChkBoxes)
                {
                    if (chk.IsChecked == true)
                    {
                        _msg = _msg + chk.Content.ToString() + " ";
                    }
                    // 채크 박스가 채크가 되어 있는 경우 _msg에 채크 박스의 이름(User_id)를 더함
                }
                if (_msg == "")
                {
                    MessageBox.Show("사용자를 선택해 주세요", "선택 오류");
                    // 채크박스가 아무것도 채크되어있지 않는 경우

                }
                else
                {
                    List<string> chatroomCreate = new List<string>()
                {
                    cs_chatroomcreate.ToString(),
                    _user,
                    _msg
                    // 서버로 패킷 아이디, 누가 만들었는지, 멤버는 누구인지 정보를 보냄
                };
                    NetworkProxy.Get.SendMessage(chatroomCreate);
                    MessageBox.Show("채팅방 생성 완료");
                } 
            }
        }
    }
}
