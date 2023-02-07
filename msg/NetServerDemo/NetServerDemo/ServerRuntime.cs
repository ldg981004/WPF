using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CommonLib.Loggers;
using CommonLib.Network;
using CommonLib.Runtime;
using NetServerDemo.ChatLogic;
using NLog;
using PacketLib;
using static NetServerDemo.Model;

namespace NetServerDemo
{
    class ServerRuntime : RuntimeFramework
    {
       
        private SimpleNetServer? _server = null;
        string u = "";

        protected override bool OnInitialized()
        {
            if (null != _server)
                return false;

            _server = NetServer.MakeSimpleServer("127.0.0.1", 6767) as SimpleNetServer;
            if (null == _server)
                return false;

            _server.OnOpen += OnOpen_Recv;
            _server.OnClose += OnClose_Recv;
            _server.OnMessage += OnMessage_Recv;
            _server.OnError += OnError_Recv;

            _server.StartServer();
            return true;
        }

        private List<ConnectionID> _connectedUsers = new List<ConnectionID>();
        // ������ ������ ����ڸ� ��Ƴ��� ����Ʈ

        private List<string> _onlineuser = new List<string>();
        // �α��ε� ����ڸ� ��Ƴ��� ����Ʈ

        private List<string> uid = new List<string>();

        private Dictionary<string, ChatUser> _idUserLookup = new Dictionary<string, ChatUser>();
        // �α��� �� User-ID�� �����ϴ� ��ųʸ�

        private Dictionary<ConnectionID, ChatUser> _connUserLookup = new Dictionary<ConnectionID, ChatUser>();
        // ������ ������ Connection-ID�� �����ϴ� ��ųʸ�

        private Dictionary<int, ChatRoom> _chatroomLookup = new Dictionary<int, ChatRoom>();

        private bool LoginUser(string userId, ConnectionID connId) // �α��� ó��
        {
            if (string.IsNullOrWhiteSpace(userId)) // userId(Email)�� null�̸� false
                return false;

            if (!connId.IsValid) // Connection Id�� ��ȿ���� ������ false
                return false;

            if (_idUserLookup.ContainsKey(userId) ||
                _connUserLookup.ContainsKey(connId))
            // �̹� �α����� �Ǿ��ִ� ���
            {
                return false;
            }

            ChatUser cu = new ChatUser(userId, connId); // ChatUser�� �ϳ� ������
            if (null == cu)                             // cu�� null�̸� false
                return false;

            _idUserLookup.Add(userId, cu);              // ����� ���� user��ųʸ��� �α����� id�� cu�� ����
            _connUserLookup.Add(connId, cu);            // ����� ���� conn��ųʸ��� ������ connid�� cu�� ����
            return true;
        }

        private void LogoutUser(string userId)          // �α׾ƿ�(userid)
        {
            if (string.IsNullOrWhiteSpace(userId))      // userId�� null�̰ų� ������ ��� return
                return;

            if (!_idUserLookup.ContainsKey(userId))     // id ��ųʸ��� userId�� ������ return
                return;

            ChatUser chatUser = _idUserLookup[userId];
            if (null == chatUser)
                return;

            _idUserLookup.Remove(userId);               // userid�� id ��ųʸ����� ����
            _connUserLookup.Remove(chatUser.ConnId);    // connid�� conn ��ųʸ����� ����
        }
        private void LogoutUser(ConnectionID connId)    // �α׾ƿ�(connId)
        {
            if (!connId.IsValid)                        // connid�� ��ȿ���� ������ return
                return;

            if (!_connUserLookup.ContainsKey(connId))   // connid ��ųʸ��� connid�� ������ return
                return;

            ChatUser cu = _connUserLookup[connId];      // cu�� ����
            if (null == cu)
                return;

            _connUserLookup.Remove(connId);
            _idUserLookup.Remove(cu.UserId);
        }

        // �α��� �� ���¿��� �α׾ƿ��� �ϸ� ������ ������ ������ => conn ��ųʸ����� remove

        public void SendMessage(ConnectionID connId, PacketID packetId, List<string> msgArr)    // �޽����� ������ connid, packetid(����), msgArr�� ����
        {
            if (!connId.IsValid)                    // connid�� ��ȿ���� ������ return
                return;

            StringBuilder sb = new StringBuilder(); // StringBuilder sb�� ����

            sb.Append(((int)packetId).ToString());  // sb�� ó���� packetid(����)

            foreach (string msg in msgArr)           // 
            {
                sb.Append(" ");                     // packetid ������ ����
                sb.Append(msg);                     // �� ������ msgArr�� �׸��� 0������
                                                    // ����� �����ư��鼭 ���
            }
            _server!.SendMessage(connId, sb.ToString());
            //sb.Append()
        }

        #region network event
        private void OnOpen_Recv(ConnectionID id, string msg)
        {
            NLogger.Get.Log(NLog.LogLevel.Info, msg);
            _connectedUsers.Add(id);
        }
        private void OnClose_Recv(ConnectionID id, string msg)
        {
            NLogger.Get.Log(NLog.LogLevel.Info, msg);
            _connectedUsers.Remove(id);

            LogoutUser(id);
        }
        private void OnError_Recv(ConnectionID id, string msg)
        {
            NLogger.Get.Log(NLog.LogLevel.Info, msg);
        }
        private void OnMessage_Recv(ConnectionID id, string msg)
        {
            NLogger.Get.Log(NLog.LogLevel.Info, msg);

            List<string> message = msg.Split(' ').ToList();
            //message ����Ʈ�� ���� �޽����� ������ ����Ʈ ���·� �߰�

            var msgId = message[0];
            // msgId�� �׻� message�� ù��° �׸�

            if (string.IsNullOrWhiteSpace(msgId))
                return;
            // message ����Ʈ�� ù ��° �׸�(���)�� null�̰ų� �����̸� return

            if (!int.TryParse(msgId, out int resId))
                return;
            // msgId�� ������ �ƴϸ� return
            // msgId�� int�� ���� resId�� ����

            PacketID packetId = (PacketID)resId;
            // packetId�� msgId�� int�� ���� resId�� ������ �� ģ���� ����

            using var dbdb = new EFContext();
            // dbdb�� EFContext�� ����
            {
                var qu = dbdb.User_TBLs!.ToList();
                // dbdb�� User_TBLs�� List ���·� ������
                switch (packetId)
                {
                    #region SignUp
                    case PacketID.CS_ReqSignUp: //Ŭ���̾�Ʈ->���� packetId�� CS_ReqSignUp(1)�̸�
                        {
                            User_TBL ut = new User_TBL();
                            // User_TBL�� ut�� ����

                            List<string> idcheck = new List<string>();
                            // string ������ id�� äũ�ϴ� ����Ʈ�� ����

                            foreach (var a in qu)
                            {
                                idcheck.Add(a.User_Id!);
                                //qu(User_TBLs�� List ���·� ������)�� �ִ�
                                //User_Id�� ����� ���� idcheck list�� �߰�
                            }

                            if (idcheck.Contains(message[2]))   // idcheck ����Ʈ�� message[2](ȸ������ �� ���̵�)
                                                                // �ִ°�� ���̵� �ߺ� �޽��� ����
                            {
                                Console.WriteLine("���̵� �ߺ�");
                                // �ֿܼ� ���̵� �ߺ��̶�� �α׸� ����

                                byte[] byteEncode = Encoding.UTF8.GetBytes("���̵��ߺ�");
                                string encodingstring = Convert.ToBase64String(byteEncode);
                                // "���̵��ߺ�" �̶�� ���ڸ� utf8�� ���ڵ��� �ؼ� ������ 

                                List<string> id_dup = new List<string>()
                                {
                                    encodingstring
                                };
                                SendMessage(id, PacketID.SC_DupId, id_dup);
                                // PacketID.SC_DupID(4)�� ���̵��ߺ��̶�� string�� ������
                            }
                            else
                            // ���̵� �ߺ��� �ƴϸ�
                            {
                                ut.User_Name = message[1];  //User_TBL�� Name�� �޽��� 1��
                                ut.User_Id = message[2];    //User_TBL�� Id�� �޽��� 2��
                                ut.User_Pw = message[3];    //User_TBL�� Pw�� �޽��� 3��
                                ut.User_Birth = message[5]; //User_TBL�� Birth�� �޽��� 4��
                                ut.User_Phone = message[6]; //User_TBL�� Phone�� �޽��� 5��

                                dbdb.Add(ut);
                                dbdb.SaveChanges();

                                Console.WriteLine("ȸ������ ����");

                                byte[] byteEncode = Encoding.UTF8.GetBytes("ȸ�����Լ���");
                                string encodingstring = Convert.ToBase64String(byteEncode);

                                List<string> signup_suc = new List<string>()
                                {
                                    encodingstring
                                };
                                SendMessage(id, PacketID.SC_SignUpResult, signup_suc);
                                // PacketID.SC_SignUpResult(5)�� ȸ�����Լ����̶�� string�� ������
                            }
                        }
                        break;
                    #endregion

                    #region Login
                    case PacketID.CS_ReqLogin: //Ŭ���̾�Ʈ->���� packetId�� CS_ReqLogin(2)�̸�
                        {

                            u = message[1];
                            // �α����� ������� ���̵� u�� ����

                            foreach (var a in qu)
                            {
                                uid.Add(a.User_Id!);
                            }
                            // User_Id�� �������� uid ����Ʈ�� ����

                            if (uid.Contains(message[1]) == false)
                            // message[1], �� u�� ȸ������ �� ��ü ���̵� ������
                            {
                                Console.WriteLine("���Ե��� ���� ���̵�");
                                List<string> login_fail = new List<string>()
                                {
                                    "���Ե���-����-���̵�"
                                };
                                SendMessage(id, PacketID.SC_NoId, login_fail);
                                // "���Ե��� ���� ���̵�"�� Ŭ���̾�Ʈ�� ����(6)
                            }

                            else if (uid.Contains(message[1]))
                            // uid�� (message[1] == u)�� ������
                            {
                                var same_id = dbdb.User_TBLs!.Where(u => u.User_Id == message[1]).ToList();
                                // User_TBLs DB���� ���̵� message[1]�� ���� ���� list ���·� ������

                                if (same_id[0].User_Pw == message[2])
                                // ������ list�� ù��°�� User_Pw�� (message[2] == �α��� �� Pw)�� ������
                                {
                                    if (_onlineuser.Contains(same_id[0].User_Id!))
                                    // �α��� �� ����ڸ� �����ϴ� ����Ʈ�� User_Id�� ������
                                    {
                                        Console.WriteLine("�̹� �α��� �� �����");

                                        byte[] byteEncode = Encoding.UTF8.GetBytes("�α������ξ��̵�");
                                        string encodingstring = Convert.ToBase64String(byteEncode);

                                        List<string> already_login = new List<string>()
                                        {
                                            "�α��� ���� ���̵�"
                                        };
                                        SendMessage(id, PacketID.SC_AlreadyLogin, already_login);

                                        List<string> strings = new List<string>()
                                        {
                                            same_id[0].User_Id!
                                        };

                                        foreach (var conn in _connectedUsers)
                                        {
                                            SendMessage(id, PacketID.SC_LogOutUser, strings);
                                        }

                                        _onlineuser.Remove(same_id[0].User_Id!);
                                        // �α��� ����� ����Ʈ���� �ش� ���̵� ����

                                        Console.WriteLine("�α׾ƿ� �Ϸ�");
                                        List<string> Logout_Suc = new List<string>()
                                        {
                                            "�α׾ƿ�-�Ϸ�"
                                        };
                                        LogoutUser(id);
                                        LogoutUser(same_id[0].User_Id!);
                                        SendMessage(id, PacketID.SC_LogoutSuc, Logout_Suc);
                                        // �α׾ƿ� �Ϸ�Ǿ��ٴ� �޽����� Ŭ���̾�Ʈ�� ����
                                    }
                                    else // �α��� ����Ʈ�� �ش� ���̵� ������ �α��� ����
                                    {
                                        Console.WriteLine("�α��� ����");
                                        u = same_id[0].User_Id!;
                                        // �����(u)�� ���� �α��� �� ���
                                        _onlineuser.Add(same_id[0].User_Id!);
                                        // �α��� ����Ʈ�� �ش� ���̵� ����
                                        List<string> Login_Suc = new List<string>()
                                        {
                                            "�α��� ����",
                                        };
                                        LoginUser(u, id);
                                        SendMessage(id, PacketID.SC_LoginSuc, Login_Suc);
                                        // �α����� �����ߴٰ� �޽����� Ŭ���̾�Ʈ���� ����

                                        List<string> ulist = new List<string>()
                                        {
                                            u
                                        };
                                        foreach(var conn in _connectedUsers)
                                        {
                                            SendMessage(conn, PacketID.SC_LoginUserList, ulist);
                                        }

                                        byte[] byteEncode = Encoding.UTF8.GetBytes(same_id[0].User_Name!);
                                        string encodingstring = Convert.ToBase64String(byteEncode);
                                        // User_Name�� utf8�� ���ڵ�

                                        List<string> user_info_list = new List<string>()
                                        {
                                            encodingstring,         // User�� �̸�
                                            same_id[0].User_Id!,    // User�� ���̵�
                                            same_id[0].User_Birth!  // User�� ��������� ����
                                        };
                                        SendMessage(id, PacketID.SC_LoginResult, user_info_list);
                                        // �α��� ���� �� �α��� ����� Ŭ���̾�Ʈ���� ����
                                    }
                                }
                                else
                                // ������ list�� ù��°�� User_Pw�� (message[2] == �α��� �� Pw)�� �ٸ��� �α��� ����
                                {
                                    Console.WriteLine("�н����� �ٸ�");

                                    List<string> password_incorect = new List<string>()
                                    {
                                       "�н����� �ٸ�"
                                    };
                                    SendMessage(id, PacketID.SC_PwInCor, password_incorect);
                                    // ��Ŷ ID(�н����尡 �ٸ��ٰ�)�� Ŭ���̾�Ʈ�� ����
                                }
                            }
                        }
                        break;
                    #endregion

                    case PacketID.CS_Logined:
                        // MainUser���� ��û
                        {
                            var login = uid.Where(x => _onlineuser.Count(s => x.Contains(s)) != 0).ToList();
                            // user_id�� ��ü ����ִ� ����Ʈ�� �α��ε� ����ڸ� �ִ� ����Ʈ�� ���ؼ�
                            // ���� ���̸� ����Ʈ ���·� ���

                            string login_user = "";

                            foreach (var l in login)
                            {
                                login_user = login_user + l + " ";
                                // string login_user�� �α��ε� ����ڸ� ��Ƴ��� login�� �ϳ��� ����
                                Console.WriteLine(l);
                            }
                            List<string> logined_user = new List<string>()
                            {
                                login_user
                            };
                            SendMessage(id, PacketID.SC_LoginedUser, logined_user);
                            // �α��� �Ǿ��ִ� ����ڵ��� ����Ʈ ���·� Ŭ���̾�Ʈ�� ����
                        }
                        break;

                    case PacketID.CS_AllUserList:
                        // MainUser���� ��û
                        {
                            string idlist = "";
                            // ����� ���̵� ��ü�� idlist�� ��Ƽ� Ŭ���̾�Ʈ�� ����
                            foreach (var u in uid)
                            {
                                idlist = idlist + u + " ";
                            }
                            List<string> allidlist = new List<string>()
                            {
                                idlist,
                                u
                            };
                            SendMessage(id, PacketID.SC_AllUser, allidlist);

                            uid.Clear();
                            // uid�� �ʱ�ȭ ������
                        }
                        break;

                    case PacketID.CS_User:
                        // �� ��� message[0] : cs_user.Tostring(), message[1] : _user(����� => MainPage.xaml�� ����)
                    {
                        List<string> user = new List<string>()
                        {
                            message[1]
                        };
                        SendMessage(id, PacketID.SC_User, user);
                            // MainUser�� ���� ����ڰ� �������� ����
                    }
                    break;

                    case PacketID.CS_HomeBtn:
                        // Ȩ��ư Ŭ�� �̺�Ʈ
                        {
                            using var db = new EFContext();
                            {
                                var q = db.User_TBLs!.ToList();
                                // User_TBLs�� ����Ʈ ���·� ������

                                List<string> alluser = new List<string>();
                                alluser.Add(u);

                                foreach(var a in q)
                                {
                                    alluser.Add(a.User_Id!);
                                }
                                // alluser�� ��� ȸ�����Ե� id�� ����

                                alluser.Add(message[1]);

                                SendMessage(id, PacketID.SC_HomeBtnAlluser, alluser);
                                SendMessage(id, PacketID.SC_HomeBtnLogined, _onlineuser);
                                // ��� ȸ�����Ե� �������ִ� ����Ʈ��
                                // ���� �α��� �Ǿ��ִ� ������ �ִ� ����Ʈ�� Ŭ���̾�Ʈ�� ������
                            }
                        }
                        break;

                    case PacketID.CS_ChangePassword:
                        {
                            List<string> change_password_list = new List<string>();
                            string user = message[1];
                            // message[1]�� ���� �α��� ���� �������

                            change_password_list.Add(user);

                            SendMessage(id, PacketID.SC_ChangePassword, change_password_list);
                            // ���� ���� �α��� ���̶�� ChangePW�� ������
                        }
                        break;

                    case PacketID.CS_ChangePasswordInfo:
                        {
                            using var db = new EFContext();
                            {
                                var q = db.User_TBLs!.Where(d => d.User_Id == message[1]).ToList();
                                // message[1]�� ���� �α��� ���� ������ ���̵���
                                // User ���̺��� �� ���̵�� ���� ���� ����Ʈ�� ������

                                SHA256Managed sha256Managed = new SHA256Managed();

                                byte[] encryptBytes1 = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(message[2]));
                                string cur_pass = Convert.ToBase64String(encryptBytes1);

                                byte[] encryptBytes2 = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(message[3]));
                                string change_pass = Convert.ToBase64String(encryptBytes2);

                                // ���� ��й�ȣ�� �ٲ� ��й�ȣ ��ȣȭ ����

                                if(cur_pass != q[0].User_Pw)
                                    // �޽��� ���� ���� �н����带 ��ȣȭ�� ���¿��� DB�� ��ȣȭ ��й�ȣ�� ��
                                {
                                    List<string> incorrect_pass = new List<string>()
                                    {
                                        "Password Incorrect"
                                    };
                                    SendMessage(id, PacketID.SC_ChangePasswordFail, incorrect_pass);
                                    // ���� ��й�ȣ�� DB�� �ִ� ��й�ȣ�� �ٸ� ��� Password Incoreect�� ������
                                }
                                else if(cur_pass == q[0].User_Pw)
                                    // ���� ��й�ȣ��  DB�� ������ ���
                                {
                                    q[0].User_Pw = change_pass;
                                    // �ٲ� ��й�ȣ�� ��ȣȭ �ؼ� DB�� �ٽ� ����
                                    db.SaveChanges();

                                    List<string> change_password_suc = new List<string>()
                                    {
                                        "Change_Success"
                                    };
                                    SendMessage(id, PacketID.SC_ChangePasswordSuccess, change_password_suc);
                                    // ��й�ȣ ������ �� �Ǿ��ٰ� �޽����� ����
                                }
                            }
                        }
                        break;

                    case PacketID.CS_ChatroomCreate:
                        {
                            // MainUser���� ��û
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.OnCreateChatRoom(message);
                            // �� ��� message => Packet ID, �����, ä��â ���
                        }
                        break;

                    case PacketID.CS_ChatroomList:
                        {
                            // MainPage���� ��û => �ϴ� �߾� ä�� ��� ��ư
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.ChatRoomList(id, message);
                        }
                        break;

                    case PacketID.CS_ChatRoom:
                        {
                            // ChatListButton���� ��û
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.ChatRoomInfo(id, message);
                        }
                        break;

                    case PacketID.CS_ChatRoomClick:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.ChatRoomClick(id, message);
                        }
                        break;

                    case PacketID.CS_Previous:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.Who(id, message);
                        }
                        break;

                    case PacketID.CS_I_am:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.Record(id,message);
                        }
                        break;

                    case PacketID.CS_OutRoom:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.OutRoom(id, message);
                        }
                        break;

                    case PacketID.CS_ChatMember:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.CureentMem(id, message);
                        }
                        break;
                    case PacketID.CS_AllUser:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.AllUser(id, message);
                        }
                        break;

                    case PacketID.CS_ChatJoin:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.JoinUser(id, message);
                        }
                        break;

                    case PacketID.CS_BackChat:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.Record(id, message);
                            chatroom.BackChat(id, message);
                        }
                        break;

                    case PacketID.CS_InviteBack:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.ChatRoomList(id, message);
                        }
                        break;

                    case PacketID.CS_SendChat:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.BroadcastAll(message);
                        }
                        break;

                    case PacketID.CS_RemoveUser:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.RemoveUser(id, message);
                        }
                        break;

                    case PacketID.CS_MemberExpulsion:
                        {
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.ExpulsionUser(id, message);
                        }
                        break;

                    case PacketID.CS_LogOut:
                        {
                            _onlineuser.Remove(message[1]);
                            // �α��� �Ǿ��ִ� ����Ʈ���� message[1](���� �α׾ƿ��� ��û�ߴ���)�� ����
                            // => MainPage.xaml�� log_out_click �̺�Ʈ���� ���� 
                            LogoutUser(id);
                            LogoutUser(message[1]);
                            Console.WriteLine(message[1] + " " + "Log-Out");
                            List<string> Logout_Suc = new List<string>()
                            {
                                "LogOut-Success"
                            };
                            SendMessage(id, PacketID.SC_LogoutSuc, Logout_Suc);

                            foreach(var conn in _connectedUsers)
                            {
                                SendMessage(conn, PacketID.SC_LogOutList,_onlineuser);
                            }

                            // �α׾ƿ� ó��
                        }
                        break;

                    default:
                        break;
                }
            } 
        }
        #endregion

        protected override void OnTerminated()
        {
            if (null != _server)
            {
                _server.TerminateServer();
                _server = null;
            }
        }

        protected override void OnTickEvent(long elapsedTime)
        {
            if (null != _server)
            {
                _server.UpdateServer();
            }
        }
    }
}