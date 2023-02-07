using CommonLib.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Security.Cryptography;
using NetDemo.Network;
using PacketLib;

namespace NetDemo
{
    /// <summary>
    /// SignUp.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUp : UserControl, INetworkEventListener
    {
       
        public List<Sign> users = new List<Sign>();

        public SignUp()
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

            if (server_messages[0] == PacketID.SC_DupId.ToString())
            {
                byte[] bytedecode = Convert.FromBase64String(server_messages[1]);
                string decodestring = Encoding.UTF8.GetString(bytedecode);
                // msg의 두번째 항목을 utf8로 디코딩하고

                MessageBox.Show(decodestring, "ERROR");
                // 메시지 박스를 띄움 => 아이디중복, ERROR
            }

            else if (server_messages[0] == PacketID.SC_SignUpResult.ToString())
            {
                MessageBox.Show(msg, "회원가입 성공");
                NetworkProxy.Get.UnregisterEventListener(this);
                SignBoard.Children.Clear();
                SignBoard.Children.Add(new Login());

                // 서버에서 보낸 메시지가 Success Sign-Up일 경우
                // 로그인 페이지로 이동
            }
        }

        public void OnEventReceived(string msg)
        {
            AddLogMessage(msg);
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            // 회원가입 버튼 클릭 이벤트

            SHA256Managed sha256Managed = new SHA256Managed();
            // SHA 256 암호화 사용
            byte[] encryptBytes1 = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(tb_password.Password));
            byte[] encryptBytes2 = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(tb_repass.Password));
            // 비빌번호와 비빌번호 확인 항목을 sha256 방법으로 byte 형식으로 암호화

            String password = Convert.ToBase64String(encryptBytes1);
            String repass = Convert.ToBase64String(encryptBytes2);
            // byte로 암호화된 항목을 string으로 변환

            users.Add(new Sign() 
            {
                Name = tb_name.Text,
                Id = tb_id.Text,
                Password = password,
                Repass = repass,
                Birth = tb_birth.Text,
                Phone = tb_phone.Text,
                //Sign 클래스에 맞는 항목에 textbox값들을 대입
            });

            if ((tb_name.Text != "" && tb_id.Text != "" 
                && tb_password.Password != "" && tb_repass.Password != "" 
                && tb_birth.Text != "" && tb_phone.Text != "") 
                && (tb_password.Password == tb_repass.Password)
                && tb_id.Text.Contains("@") == true)
                // textbox값이 비어있는지 조건 채크
            {
                List<string> sign_list = new List<string>()
                {
                    PacketID.CS_ReqSignUp.ToString(),
                    users.Last().Name,
                    users.Last().Id,
                    users.Last().Password,
                    users.Last().Repass,
                    users.Last().Birth,
                    users.Last().Phone

                    // 비어있지 않으면 수행
                    // users의 [0]번을 접근하게 되면 맨 처음 입력한 값을 계속 보냄
                    // 리스트는 밑으로 추가되기 때문에 Last값 접근
                };
                NetworkProxy.Get.SendMessage(sign_list);
                // NetworkProxy의 SendMessage에서 정의한 형식으로 Message를 보냄
            }
            else if(tb_password.Password != tb_repass.Password)
            {
                MessageBox.Show("비밀번호 오류", "PASSWORD-ERROR");
                // 비빌번호와 비밀번호 확인 값이 다른 경우
            }
            else if(tb_id.Text.Contains("@") == false)
            {
                MessageBox.Show("아이디 형식 오류\r\n이메일 형식으로 입력", "ID-ERROR");
                // 아이디에 @가 없으면 형식이 잘못되어있나고 나옴
            }
            else
            {
                MessageBox.Show("비어있는 항목 오류", "NULL-ERROR");
                // 항목을 모두 작성하지 않은 경우
            }
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            SignUp_grid.Children.Clear();
            SignUp_grid.Children.Add(new Login());
            // 로그인 버튼을 클릭하면 단순히 로그인 페이지로 이동
        }
    }
    public class Sign
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string Repass { get; set; }
        public string Birth { get; set; }
        public string Phone { get; set; }
    }

}
