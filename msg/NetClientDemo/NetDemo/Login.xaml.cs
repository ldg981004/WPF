using CommonLib.Network;
using NetDemo.Network;
using PacketLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : UserControl, INetworkEventListener
    {
        public Login() 
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
            string head = server_messages[0].ToString();        // head는 항상 서버 메시지의 첫번째 항목

            int sc_noid = (int)PacketID.SC_NoId;                // 회원가입되지 않은 아이디로 로그인한 경우
            int sc_alreadylogin = (int)PacketID.SC_AlreadyLogin;// 이미 로그인 되어있는 경우
            int sc_logoutsuc = (int)PacketID.SC_LogoutSuc;      // 로그아웃 완료 된 경우
            int sc_loginsuc = (int)PacketID.SC_LoginSuc;        // 로그인이 성공한 경우
            int sc_pwincor = (int)PacketID.SC_PwInCor;          // 비밀번호가 올바르지 않은 경우


            if (head == sc_noid.ToString())
            {
                MessageBox.Show("가입되지 않은 아이디", "ERROR");
            }
            else if (head == sc_alreadylogin.ToString())
            {
                MessageBox.Show("이미 로그인 중인 아이디", "ERROR");
            }
            else if (head == sc_logoutsuc.ToString())
            {
                MessageBox.Show("로그아웃 완료", "Success");
            }
            else if (head == sc_loginsuc.ToString())
            {
                MessageBox.Show("로그인 성공", "Success");
                login_grid.Children.Clear();
                login_grid.Children.Add(new MainPage());
                // 로그인 성공시 MainPage로 이동
            }
            else if (head == sc_pwincor.ToString())
            {
                MessageBox.Show("비밀번호 오류", "ERROR");
            }            
        }

        public void OnEventReceived(string msg)
        {
            AddLogMessage(msg);
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            // 로그인 버튼 클릭 이벤트

            SHA256Managed sha256Managed = new SHA256Managed();

            byte[] encryptBytes1 = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(tb_pw.Password));
            String password = Convert.ToBase64String(encryptBytes1);
            // 페스워드를 sha256 암호화 알고리즘을 통해 암호화

            int cs_reqlogin = (int)PacketID.CS_ReqLogin;

            List<string> login_list = new List<string>()
            {
                cs_reqlogin.ToString(),
                tb_id.Text,
                password
            };
            // login_list에 패킷ID, 유저 ID, 유저 비밀번호를 담음

            if(tb_id.Text == "" || tb_pw.Password == "")
            {
                MessageBox.Show("아이디 또는 비밀번호를 입력해주세요", "ERROR");
                // 아이디 또는 비밀번호가 비어있는 경우
            }
            else
            {
                NetworkProxy.Get.SendMessage(login_list);
                // 서버로 login_list를 전달
            }
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            login_grid.Children.Clear();
            login_grid.Children.Add(new SignUp());
            // 회원가입 버튼 클릭 시 단순히 회원가입 화면으로 이동
        }
    }
}
