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
        // 서버에 접속한 사용자를 담아놓는 리스트

        private List<string> _onlineuser = new List<string>();
        // 로그인된 사용자를 담아놓는 리스트

        private List<string> uid = new List<string>();

        private Dictionary<string, ChatUser> _idUserLookup = new Dictionary<string, ChatUser>();
        // 로그인 한 User-ID를 저장하는 딕셔너리

        private Dictionary<ConnectionID, ChatUser> _connUserLookup = new Dictionary<ConnectionID, ChatUser>();
        // 서버에 접속한 Connection-ID를 저장하는 딕셔너리

        private Dictionary<int, ChatRoom> _chatroomLookup = new Dictionary<int, ChatRoom>();

        private bool LoginUser(string userId, ConnectionID connId) // 로그인 처리
        {
            if (string.IsNullOrWhiteSpace(userId)) // userId(Email)이 null이면 false
                return false;

            if (!connId.IsValid) // Connection Id가 유효하지 않으면 false
                return false;

            if (_idUserLookup.ContainsKey(userId) ||
                _connUserLookup.ContainsKey(connId))
            // 이미 로그인이 되어있는 경우
            {
                return false;
            }

            ChatUser cu = new ChatUser(userId, connId); // ChatUser를 하나 생성함
            if (null == cu)                             // cu가 null이면 false
                return false;

            _idUserLookup.Add(userId, cu);              // 만들어 놓은 user딕셔너리에 로그인한 id와 cu를 더함
            _connUserLookup.Add(connId, cu);            // 만들어 놓은 conn딕셔너리에 접속한 connid와 cu를 더함
            return true;
        }

        private void LogoutUser(string userId)          // 로그아웃(userid)
        {
            if (string.IsNullOrWhiteSpace(userId))      // userId가 null이거나 공백인 경우 return
                return;

            if (!_idUserLookup.ContainsKey(userId))     // id 딕셔너리에 userId가 없으면 return
                return;

            ChatUser chatUser = _idUserLookup[userId];
            if (null == chatUser)
                return;

            _idUserLookup.Remove(userId);               // userid를 id 딕셔너리에서 삭제
            _connUserLookup.Remove(chatUser.ConnId);    // connid를 conn 딕셔너리에서 삭제
        }
        private void LogoutUser(ConnectionID connId)    // 로그아웃(connId)
        {
            if (!connId.IsValid)                        // connid가 유효하지 않으면 return
                return;

            if (!_connUserLookup.ContainsKey(connId))   // connid 딕셔너리에 connid가 없으면 return
                return;

            ChatUser cu = _connUserLookup[connId];      // cu를 생성
            if (null == cu)
                return;

            _connUserLookup.Remove(connId);
            _idUserLookup.Remove(cu.UserId);
        }

        // 로그인 한 상태에서 로그아웃을 하면 서버와 연결이 끊어짐 => conn 딕셔너리에서 remove

        public void SendMessage(ConnectionID connId, PacketID packetId, List<string> msgArr)    // 메시지를 보낼때 connid, packetid(숫자), msgArr를 보냄
        {
            if (!connId.IsValid)                    // connid가 유효하지 않으면 return
                return;

            StringBuilder sb = new StringBuilder(); // StringBuilder sb를 생성

            sb.Append(((int)packetId).ToString());  // sb의 처음은 packetid(숫자)

            foreach (string msg in msgArr)           // 
            {
                sb.Append(" ");                     // packetid 다음은 공백
                sb.Append(msg);                     // 그 다음은 msgArr의 항목을 0번부터
                                                    // 공백과 번갈아가면서 출력
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
            //message 리스트에 받은 메시지를 나눠서 리스트 형태로 추가

            var msgId = message[0];
            // msgId는 항상 message의 첫번째 항목

            if (string.IsNullOrWhiteSpace(msgId))
                return;
            // message 리스트의 첫 번째 항목(헤더)가 null이거나 공백이면 return

            if (!int.TryParse(msgId, out int resId))
                return;
            // msgId가 정수가 아니면 return
            // msgId를 int형 변수 resId에 저장

            PacketID packetId = (PacketID)resId;
            // packetId는 msgId를 int형 변수 resId에 저장한 그 친구로 설정

            using var dbdb = new EFContext();
            // dbdb를 EFContext로 생성
            {
                var qu = dbdb.User_TBLs!.ToList();
                // dbdb의 User_TBLs를 List 형태로 가져옴
                switch (packetId)
                {
                    #region SignUp
                    case PacketID.CS_ReqSignUp: //클라이언트->서버 packetId가 CS_ReqSignUp(1)이면
                        {
                            User_TBL ut = new User_TBL();
                            // User_TBL의 ut를 생성

                            List<string> idcheck = new List<string>();
                            // string 형태의 id를 채크하는 리스트를 만듦

                            foreach (var a in qu)
                            {
                                idcheck.Add(a.User_Id!);
                                //qu(User_TBLs를 List 형태로 가져옴)에 있는
                                //User_Id를 만들어 놓은 idcheck list에 추가
                            }

                            if (idcheck.Contains(message[2]))   // idcheck 리스트에 message[2](회원가입 할 아이디)
                                                                // 있는경우 아이디 중복 메시지 보냄
                            {
                                Console.WriteLine("아이디 중복");
                                // 콘솔에 아이디 중복이라고 로그를 찍음

                                byte[] byteEncode = Encoding.UTF8.GetBytes("아이디중복");
                                string encodingstring = Convert.ToBase64String(byteEncode);
                                // "아이디중복" 이라는 글자를 utf8로 인코딩을 해서 보내줌 

                                List<string> id_dup = new List<string>()
                                {
                                    encodingstring
                                };
                                SendMessage(id, PacketID.SC_DupId, id_dup);
                                // PacketID.SC_DupID(4)과 아이디중복이라는 string을 보내줌
                            }
                            else
                            // 아이디 중복이 아니면
                            {
                                ut.User_Name = message[1];  //User_TBL의 Name은 메시지 1번
                                ut.User_Id = message[2];    //User_TBL의 Id는 메시지 2번
                                ut.User_Pw = message[3];    //User_TBL의 Pw는 메시지 3번
                                ut.User_Birth = message[5]; //User_TBL의 Birth는 메시지 4번
                                ut.User_Phone = message[6]; //User_TBL의 Phone은 메시지 5번

                                dbdb.Add(ut);
                                dbdb.SaveChanges();

                                Console.WriteLine("회원가입 성공");

                                byte[] byteEncode = Encoding.UTF8.GetBytes("회원가입성공");
                                string encodingstring = Convert.ToBase64String(byteEncode);

                                List<string> signup_suc = new List<string>()
                                {
                                    encodingstring
                                };
                                SendMessage(id, PacketID.SC_SignUpResult, signup_suc);
                                // PacketID.SC_SignUpResult(5)과 회원가입성공이라는 string을 보내줌
                            }
                        }
                        break;
                    #endregion

                    #region Login
                    case PacketID.CS_ReqLogin: //클라이언트->서버 packetId가 CS_ReqLogin(2)이면
                        {

                            u = message[1];
                            // 로그인한 사용자의 아이디를 u로 선언

                            foreach (var a in qu)
                            {
                                uid.Add(a.User_Id!);
                            }
                            // User_Id를 만들어놓은 uid 리스트에 담음

                            if (uid.Contains(message[1]) == false)
                            // message[1], 즉 u가 회원가입 된 전체 아이디에 없으면
                            {
                                Console.WriteLine("가입되지 않은 아이디");
                                List<string> login_fail = new List<string>()
                                {
                                    "가입되지-않은-아이디"
                                };
                                SendMessage(id, PacketID.SC_NoId, login_fail);
                                // "가입되지 않은 아이디"를 클라이언트로 보냄(6)
                            }

                            else if (uid.Contains(message[1]))
                            // uid에 (message[1] == u)가 있으면
                            {
                                var same_id = dbdb.User_TBLs!.Where(u => u.User_Id == message[1]).ToList();
                                // User_TBLs DB에서 아이디가 message[1]과 같은 행을 list 형태로 가져옴

                                if (same_id[0].User_Pw == message[2])
                                // 가져온 list의 첫번째의 User_Pw가 (message[2] == 로그인 시 Pw)와 같으면
                                {
                                    if (_onlineuser.Contains(same_id[0].User_Id!))
                                    // 로그인 된 사용자를 저장하는 리스트에 User_Id가 있으면
                                    {
                                        Console.WriteLine("이미 로그인 된 사용자");

                                        byte[] byteEncode = Encoding.UTF8.GetBytes("로그인중인아이디");
                                        string encodingstring = Convert.ToBase64String(byteEncode);

                                        List<string> already_login = new List<string>()
                                        {
                                            "로그인 중인 아이디"
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
                                        // 로그인 사용자 리스트에서 해당 아이디를 삭제

                                        Console.WriteLine("로그아웃 완료");
                                        List<string> Logout_Suc = new List<string>()
                                        {
                                            "로그아웃-완료"
                                        };
                                        LogoutUser(id);
                                        LogoutUser(same_id[0].User_Id!);
                                        SendMessage(id, PacketID.SC_LogoutSuc, Logout_Suc);
                                        // 로그아웃 완료되었다는 메시지를 클라이언트로 보냄
                                    }
                                    else // 로그인 리스트에 해당 아이디가 없으면 로그인 성공
                                    {
                                        Console.WriteLine("로그인 성공");
                                        u = same_id[0].User_Id!;
                                        // 사용자(u)는 현재 로그인 한 사람
                                        _onlineuser.Add(same_id[0].User_Id!);
                                        // 로그인 리스트에 해당 아이디를 더함
                                        List<string> Login_Suc = new List<string>()
                                        {
                                            "로그인 성공",
                                        };
                                        LoginUser(u, id);
                                        SendMessage(id, PacketID.SC_LoginSuc, Login_Suc);
                                        // 로그인이 성공했다고 메시지를 클라이언트에게 보냄

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
                                        // User_Name을 utf8로 인코딩

                                        List<string> user_info_list = new List<string>()
                                        {
                                            encodingstring,         // User의 이름
                                            same_id[0].User_Id!,    // User의 아이디
                                            same_id[0].User_Birth!  // User의 생년월일을 보냄
                                        };
                                        SendMessage(id, PacketID.SC_LoginResult, user_info_list);
                                        // 로그인 성공 시 로그인 결과를 클라이언트에게 보냄
                                    }
                                }
                                else
                                // 가져온 list의 첫번째의 User_Pw가 (message[2] == 로그인 시 Pw)와 다르면 로그인 실패
                                {
                                    Console.WriteLine("패스워드 다름");

                                    List<string> password_incorect = new List<string>()
                                    {
                                       "패스워드 다름"
                                    };
                                    SendMessage(id, PacketID.SC_PwInCor, password_incorect);
                                    // 패킷 ID(패스워드가 다르다고)를 클라이언트로 보냄
                                }
                            }
                        }
                        break;
                    #endregion

                    case PacketID.CS_Logined:
                        // MainUser에서 요청
                        {
                            var login = uid.Where(x => _onlineuser.Count(s => x.Contains(s)) != 0).ToList();
                            // user_id가 전체 들어있는 리스트와 로그인된 사용자만 있는 리스트를 비교해서
                            // 같은 값이면 리스트 형태로 출력

                            string login_user = "";

                            foreach (var l in login)
                            {
                                login_user = login_user + l + " ";
                                // string login_user에 로그인된 사용자를 담아놨던 login을 하나씩 더함
                                Console.WriteLine(l);
                            }
                            List<string> logined_user = new List<string>()
                            {
                                login_user
                            };
                            SendMessage(id, PacketID.SC_LoginedUser, logined_user);
                            // 로그인 되어있는 사용자들을 리스트 형태로 클라이언트로 보냄
                        }
                        break;

                    case PacketID.CS_AllUserList:
                        // MainUser에서 요청
                        {
                            string idlist = "";
                            // 사용자 아이디 전체를 idlist로 담아서 클라이언트로 전달
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
                            // uid를 초기화 시켜줌
                        }
                        break;

                    case PacketID.CS_User:
                        // 이 경우 message[0] : cs_user.Tostring(), message[1] : _user(사용자 => MainPage.xaml에 있음)
                    {
                        List<string> user = new List<string>()
                        {
                            message[1]
                        };
                        SendMessage(id, PacketID.SC_User, user);
                            // MainUser로 현재 사용자가 누구인지 보냄
                    }
                    break;

                    case PacketID.CS_HomeBtn:
                        // 홈버튼 클릭 이벤트
                        {
                            using var db = new EFContext();
                            {
                                var q = db.User_TBLs!.ToList();
                                // User_TBLs를 리스트 형태로 가져옴

                                List<string> alluser = new List<string>();
                                alluser.Add(u);

                                foreach(var a in q)
                                {
                                    alluser.Add(a.User_Id!);
                                }
                                // alluser에 모든 회원가입된 id를 더함

                                alluser.Add(message[1]);

                                SendMessage(id, PacketID.SC_HomeBtnAlluser, alluser);
                                SendMessage(id, PacketID.SC_HomeBtnLogined, _onlineuser);
                                // 모든 회원가입된 유저가있는 리스트와
                                // 현재 로그인 되어있는 유저가 있는 리스트를 클라이언트로 보내줌
                            }
                        }
                        break;

                    case PacketID.CS_ChangePassword:
                        {
                            List<string> change_password_list = new List<string>();
                            string user = message[1];
                            // message[1]은 현재 로그인 중인 사용자임

                            change_password_list.Add(user);

                            SendMessage(id, PacketID.SC_ChangePassword, change_password_list);
                            // 현재 누가 로그인 중이라고 ChangePW로 보내줌
                        }
                        break;

                    case PacketID.CS_ChangePasswordInfo:
                        {
                            using var db = new EFContext();
                            {
                                var q = db.User_TBLs!.Where(d => d.User_Id == message[1]).ToList();
                                // message[1]은 현재 로그인 중인 유저의 아이디임
                                // User 테이블에서 이 아이디와 같은 열을 리스트로 가져옴

                                SHA256Managed sha256Managed = new SHA256Managed();

                                byte[] encryptBytes1 = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(message[2]));
                                string cur_pass = Convert.ToBase64String(encryptBytes1);

                                byte[] encryptBytes2 = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(message[3]));
                                string change_pass = Convert.ToBase64String(encryptBytes2);

                                // 현재 비밀번호와 바뀔 비밀번호 암호화 과정

                                if(cur_pass != q[0].User_Pw)
                                    // 메시지 보낸 현재 패스워드를 암호화한 상태에서 DB의 암호화 비밀번호와 비교
                                {
                                    List<string> incorrect_pass = new List<string>()
                                    {
                                        "Password Incorrect"
                                    };
                                    SendMessage(id, PacketID.SC_ChangePasswordFail, incorrect_pass);
                                    // 현재 비밀번호가 DB에 있는 비밀번호와 다른 경우 Password Incoreect를 보내줌
                                }
                                else if(cur_pass == q[0].User_Pw)
                                    // 현재 비밀번호가  DB와 동일한 경우
                                {
                                    q[0].User_Pw = change_pass;
                                    // 바뀐 비밀번호를 암호화 해서 DB에 다시 저장
                                    db.SaveChanges();

                                    List<string> change_password_suc = new List<string>()
                                    {
                                        "Change_Success"
                                    };
                                    SendMessage(id, PacketID.SC_ChangePasswordSuccess, change_password_suc);
                                    // 비밀번호 변경이 잘 되었다고 메시지를 보냄
                                }
                            }
                        }
                        break;

                    case PacketID.CS_ChatroomCreate:
                        {
                            // MainUser에서 요청
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.OnCreateChatRoom(message);
                            // 이 경우 message => Packet ID, 사용자, 채팅창 멤버
                        }
                        break;

                    case PacketID.CS_ChatroomList:
                        {
                            // MainPage에서 요청 => 하단 중앙 채팅 모양 버튼
                            ChatRoom chatroom = new ChatRoom(this);
                            chatroom.ChatRoomList(id, message);
                        }
                        break;

                    case PacketID.CS_ChatRoom:
                        {
                            // ChatListButton에서 요청
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
                            // 로그인 되어있는 리스트에서 message[1](누가 로그아웃을 요청했는지)를 지움
                            // => MainPage.xaml의 log_out_click 이벤트에서 보냄 
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

                            // 로그아웃 처리
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