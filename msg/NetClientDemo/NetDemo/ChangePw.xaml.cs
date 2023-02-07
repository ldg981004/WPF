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
    /// ChangePw.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChangePw : UserControl, INetworkEventListener
    {
        string user = "";
        public ChangePw()
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

            int sc_changepassword = (int)PacketID.SC_ChangePassword;
            int sc_changepasswordfail = (int)PacketID.SC_ChangePasswordFail;
            int sc_changepasswordsuccess = (int)PacketID.SC_ChangePasswordSuccess;

            if(head == sc_changepassword.ToString())
            {
                user = server_messages[1].ToString();
                // message[1]은 현재 유저임
            }

            else if(head == sc_changepasswordfail.ToString())
            {
                MessageBox.Show("현재 비밀번호 틀림", "ERROR");
                // 비밀번호 변경 실패
            }

            else if (head == sc_changepasswordsuccess.ToString())
            {
                MessageBox.Show("비밀번호 변경완료", "SUCCESS");
                // 비밀번호 변경 성공
            }
        }
            public void OnEventReceived(string msg)
            {
                AddLogMessage(msg);
            }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            int cs_changepasswordinfo = (int)PacketID.CS_ChangePasswordInfo;

            List<string> info = new List<string>()
            {
                cs_changepasswordinfo.ToString(),   // 패킷 아이디
                user,                               // 현재 로그인중인 유저
                cur_pass.Password.ToString(),       // 현재 비밀번호
                change_pass.Password.ToString(),    // 변경할 비밀번호
                re_pass.Password.ToString()         // 변경할 비밀번호 확인을 서버로 보내줌
            };

            if(change_pass.Password.ToString() == re_pass.Password.ToString() && change_pass.Password.ToString().Count() >= 4)
            {
                NetworkProxy.Get.SendMessage(info);
            }
            else if (change_pass.Password.ToString() != re_pass.Password.ToString())
            {
                MessageBox.Show("비밀번호가 다름", "ERROR");
                // 바뀔 비밀번호와 비밀 번호 확인이 다른 경우
            }
            else if (change_pass.Password.ToString().Count() < 4)
            {
                MessageBox.Show("최소 4글자 이상 입력", "ERROR");
                // 최소 4글자 이상의 조건을 충족시키지 못한 경우
            }

        }
    }
}
