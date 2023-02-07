using CommonLib.Network;
using PacketLib;
using System.Runtime.CompilerServices;
using System.Text;
using static NetServerDemo.Model;


namespace NetServerDemo.ChatLogic
{
    internal class ChatRoom
    {
        private int _roomId = 0;
        static List<ChatUser> _users = new List<ChatUser>();
        ServerRuntime? _runtime = null;

        public ChatRoom(ServerRuntime rt)
        {
            if (rt == null)
            {
                throw new ArgumentNullException(nameof(rt));
            }
            _runtime = rt;
        }
        
        public bool OnCreateChatRoom(List<string> message)
        {
            using var db = new EFContext();                    
            {
                ChatRoom_TBL ct = new ChatRoom_TBL();

                message.RemoveAll(s => s == "");
                // 리스트 message의 모든 공란 삭제

                ct.ChatRoom_Id = message[1] + "-ChatRoom";  // 채팅방 이름은 '만든 사람-ChatRoom'
                ct.ChatRoom_User = message[1];              // User에는 방장이 들어가니깐 초기값을 방장으로 지정
                for (int i = 2; 
                    i < message.Count; i++)
                {
                    ct.ChatRoom_User = ct.ChatRoom_User + " " + message[i];
                }                                           // 유저는 2번째부터 끝까지
                ct.ChatRoom_Maker = message[1];             // 채팅방 방장은 만든 사람
                ct.ChatRoom_Birth = DateTime.Now;           // 만들어진 시간은 DateTime을 이용한 현재

                db.Add(ct);
                db.SaveChanges();

                /*var cr = db.ChatRoom_TBLs!.LastOrDefault();
                // cr은 ChatRoom_TBLs의 마지막

                _roomId = cr!.ChatRoom_Num;*/
                // roomId는 마지막으로 만들어진 친구의 ChatRoom_num

                Console.WriteLine(message[1] + "의 채팅방 생성 완료");

                return true;
            }
        }

        public bool ChatRoomList(ConnectionID id, List<string> message)
        {
            // MainPage에서 채팅 버튼 클릭시 발생하는 이벤트

            using var db = new EFContext();
            {
                var p = db.ChatRoom_TBLs!.Where(d => d.ChatRoom_User!.Contains(message[1].ToString())).ToList();
                // ChatRoom_TBLs에서 message[1](User)이 들어간 채팅방을 모두 list로 가져옴
                if (p.Count == 0)
                    // 채팅방이 없으면
                {
                    Console.WriteLine("DB-Isnull");

                    List<string> dbnull = new List<string>()
                    {
                        "DB_Null",
                        message[1]
                        // message[1]은 현재 유저(로그인한 사람)
                    };

                    _runtime!.SendMessage(id, PacketID.SC_DbNull, dbnull);
                    //DB_Null과 User를 클라이언트(ChatRoomList)로 보냄
                }
                else
                {
                    // 채팅방이 있는 경우

                    List<string> chatroomListInfolist = new List<string>();
                    string content = "";
                    // content의 초기값은 ""
                    for (int i = 0; i < p.Count; i++)
                        // User 아이디에 맞는 채팅창 수 만큼
                    {
                        var q = db.Chat_TBLs!.Where(d => d.ChatRoom_Num.ToString() == p[i].ChatRoom_Num.ToString()).ToList();
                        // Chat_TBLs(채팅 전체)에서 Num이 p의 num과 같은 채팅방을 리스트 형태로 가져옴
                        if(q.Count == 0)
                        {
                            content = "null";
                        }
                        // 채팅이 없는경우 content는 null

                        else if (q.Count != 0)
                            // 채팅이 있는 경우
                        {
                            content = q[q.Count - 1].Chat_Content!;
                            // content는 그 채팅방에 마지막 채팅
                        }

                        byte[] byteEncode = Encoding.UTF8.GetBytes(content);
                        string encodingstring = Convert.ToBase64String(byteEncode);

                        chatroomListInfolist.Add(message[1].ToString());                // 현재 로그인한 사람
                        chatroomListInfolist.Add(p[i].ChatRoom_Num.ToString());         // 가져온 채팅방의 Num
                        chatroomListInfolist.Add(p[i].ChatRoom_Id!.ToString());         // 가져온 채팅방의 이름
                        chatroomListInfolist.Add(p[i].ChatRoom_User!.ToString());       // 가져온 채팅방의 구성원
                        chatroomListInfolist.Add(p[i].ChatRoom_Maker!);                 // 가져온 채팅방의 방장
                        chatroomListInfolist.Add(encodingstring);                              // 가져온 채팅방의 마지막 채팅을 리스트에 더해서
                        _runtime!.SendMessage(id, PacketID.SC_ChatInfo, chatroomListInfolist);
                        // 클라이언트로 보내고 리스트를 초기화 해줌
                        chatroomListInfolist.Clear();
                    }
                   
                }
                return true;
            }
        }

        public bool ChatRoomInfo(ConnectionID id, List<string> message)
        {
            string m = "";
            for (int j = 3; j < message.Count - 2; j++)
            {
                m = m + message[j] + " ";
            }

            List<string> chatroominfo = new List<string>()
            {
                message[1],
                message[2],
                m,
                message[message.Count - 2]
            };

            _runtime!.SendMessage(id, PacketID.SC_ChatRoomInfo, chatroominfo);

            m = "";
            return true;
        }
        
        public bool ChatRoomClick(ConnectionID id, List<string> message)
        {
            message.RemoveAll(s => s == "");
            _runtime!.SendMessage(id, PacketID.SC_ChatRoomClick, message);
            //message : 0.PacketID.SC_ChatRoomClick 1.user 2.room_num 3.room_name 4.room_member 5.room_maker

            ChatUser cu = new ChatUser(message[1], id);

            if(_users.Exists(x => x.ConnId == cu.ConnId || x.UserId == cu.UserId))
            {
                return false;
            }
            else
            {
                _users.Add(cu);
                return true;
            }
        }

        public bool Who(ConnectionID id, List<string> message)
        {
            List<string> who_list = new List<string>
            {
                "Who are you"
            };

            _runtime!.SendMessage(id, PacketID.SC_Who_are_you, who_list);

            return true;
        }

        public bool Record(ConnectionID id, List<string> message)
        {
            using var db = new EFContext();
            {
                var a = db.Chat_TBLs!.Where(x => x.ChatRoom_Num.ToString() == message[1].ToString()).ToList();
                foreach (var c in a)
                {
                    byte[] byteEncode = Encoding.UTF8.GetBytes(c.Chat_Content!);
                    string encodingstring = Convert.ToBase64String(byteEncode);

                    List<string> previous_chat = new List<string>()
                    {
                        c.User_Id!,
                        encodingstring,
                        c.Chat_Birth.ToString()!
                    };
                    _runtime!.SendMessage(id, PacketID.SC_PreChat, previous_chat);
                }
            }
            return true;
        }

        public bool OutRoom(ConnectionID id, List<string> message)
            // 채팅방 나가기 기능
        {
            using var db = new EFContext();
            {
                var w = db.ChatRoom_TBLs!.Where(d => d.ChatRoom_Num.ToString() == message[2]).ToList();

                w[0].ChatRoom_User = w[0].ChatRoom_User!.Replace(message[1], "");

                List<string> member = w[0].ChatRoom_User!.Split(' ').ToList();
                member.RemoveAll(s => s == "");

                if (message[1].ToString() == w[0].ChatRoom_Maker)
                {
                    if(member.Count > 0)
                    {
                        Random random = new Random();
                        int index = random.Next(member.Count);
                        w[0].ChatRoom_Maker = member[index];
                        // 방장이 나가면 그 후 방장은 랜덤으로 선택
                    }

                    else if (member.Count == 0)
                    {
                        db.ChatRoom_TBLs!.Remove(w.FirstOrDefault()!);
                        // 마지막 사람이 나가는 경우 그 방은 DB에서 삭제
                    }

                }
                db.SaveChanges();
                Console.WriteLine(message[2] + "번 방에서 " + message[1] + "아이디 사람 나감");

                List<string> maker_member = new List<string>()
                {
                    w[0].ChatRoom_Num.ToString(),
                    w[0].ChatRoom_Maker!,
                    w[0].ChatRoom_User!
                };

                foreach(var conn in _users)
                {
                    _runtime!.SendMessage(conn.ConnId, PacketID.SC_MakerAndUser, maker_member);
                }
            }

            

            List<string> outok_list = new List<string>()
            {
                "out ok"
            };
            _runtime!.SendMessage(id, PacketID.SC_OutOk, outok_list);

            return true;
        }

        public bool CureentMem(ConnectionID id, List<string> message)
        {
            using var db = new EFContext();
            {
                string room_num = message[1];
                string user = message[2];
                var mem = db.ChatRoom_TBLs!.Where(d => d.ChatRoom_Num.ToString() == message[1]).ToList();
                List<string> cur_mem = mem[0].ChatRoom_User!.Split(' ').ToList();
                cur_mem.Insert(0, room_num);
                cur_mem.Insert(1, user);
                _runtime!.SendMessage(id, PacketID.SC_CurrentMember, cur_mem);
            }
            return true;
        }

        public bool AllUser(ConnectionID id, List<string> message)
        {
            using var db = new EFContext();
            {
                var all = db.User_TBLs!.ToList();
                List<string> alllist = new List<string>();
                 
                foreach(var item in all)
                {
                    alllist.Add(item.User_Id!);
                }
                _runtime!.SendMessage(id, PacketID.SC_AllUserList, alllist);
            }
            return true;
        }

        public bool JoinUser(ConnectionID id, List<string> message)
        {
            using var db = new EFContext();
            {
                var room = db.ChatRoom_TBLs!.Where(d => d.ChatRoom_Num.ToString() == message[1]).ToList();

                int k = 0;
                
                for(int i = 2; i < message.Count - 1; i++)
                {
                    if (room[0].ChatRoom_User!.Contains(message[i]))
                    {
                        continue;
                    }
                    else
                    {
                        k++;
                    }
                }

                if(k == message.Count - 3)
                {
                    List<string> invitesuc = new List<string>()
                    {
                        "Invite-Success"
                    };
                    Console.WriteLine("초대 완료");

                    for(int j = 2; j < message.Count - 1; j++)
                    {
                        room[0].ChatRoom_User = room[0].ChatRoom_User + " " + message[j];
                    }

                    db.SaveChanges();

                    List<string> invitemember = new List<string>()
                    {
                        room[0].ChatRoom_Num.ToString(),
                        room[0].ChatRoom_User!,
                    };

                    foreach(var conn in _users)
                    {
                        _runtime!.SendMessage(conn.ConnId, PacketID.SC_InviteMember, invitemember);
                    }

                    _runtime!.SendMessage(id, PacketID.SC_InviteSuc, invitesuc);

                }
                else
                {
                    List<string> invitefail = new List<string>()
                    {
                        "Invite-Fail"
                    };
                    Console.WriteLine("초대 불가");
                    _runtime!.SendMessage(id, PacketID.SC_InviteFail, invitefail);
                }
                k = 0;
            }
            return true;
        }

        public bool RemoveUser(ConnectionID id, List<string> message)
        {
            string num = message[1];

            using var db = new EFContext();
            {
                var mem = db.ChatRoom_TBLs!.Where(d => d.ChatRoom_Num.ToString() == num).ToList();

                string member = mem[0].ChatRoom_User!;

                List<string> mem_list = member.Split(' ').ToList();
                mem_list.Insert(0, num);
                mem_list.Add(message[message.Count - 2]);

                _runtime!.SendMessage(id, PacketID.SC_RemoveMember, mem_list);
                // 멤버들을 ' '단위로 나누고 맨 마지막에는 지금 사용자를 보냄
            }
            return true;
        }

        public bool ExpulsionUser(ConnectionID id, List<string> message)
        {
            string room_num = message[1];

            using var db = new EFContext();
            {
                var expulsion = db.ChatRoom_TBLs!.Where(d => d.ChatRoom_Num!.ToString() == room_num).ToList();

                for(int i = 2; i < message.Count - 2; i++)
                {
                    if (expulsion[0].ChatRoom_User!.Contains(message[i]))
                    {
                        expulsion[0].ChatRoom_User = expulsion[0].ChatRoom_User!.Replace(message[i], "");
                    }
                }
                db.SaveChanges();

                List<string> expuision_user = new List<string>()
                {
                    expulsion[0].ChatRoom_Num.ToString(),
                    expulsion[0].ChatRoom_User!
                };

                foreach(var conn in _users)
                {
                    _runtime!.SendMessage(conn.ConnId, PacketID.SC_ExpulsionMember, expuision_user);
                }

                List<string> expusion_suc = new List<string>()
                    {
                        "ExpulsionResult_Success"
                    };

                _runtime!.SendMessage(id, PacketID.SC_ExpusionResult, expusion_suc);

            }
            return true;
        }

        public bool BackChat(ConnectionID id, List<string> message)
        {
            using var db = new EFContext();
            {
                var chat = db.ChatRoom_TBLs!.Where(d => d.ChatRoom_Num.ToString() == message[1]).ToList();
                string who = message[2];
                string member = chat[0].ChatRoom_User!;

                List<string> chat_info = new List<string>()
                {
                    who,
                    member
                };
                _runtime!.SendMessage(id, PacketID.SC_BackChat, chat_info);
            }
            return true;
        }

        public bool BroadcastAll(List<string> message)
        {
            using var db = new EFContext();
            {
                Chat_TBL ctb = new Chat_TBL();

                ctb.User_Id = message[1];
                ctb.Room_Id = message[2];
                ctb.Member_Id = message[3];
                ctb.Chat_Birth = DateTime.Now;
                ctb.Chat_Content = message[4];
                ctb.ChatRoom_Num = int.Parse(message[5]);

                db.Add(ctb);
                db.SaveChanges();

                Console.WriteLine(ctb.User_Id + "의 채팅 전송 완료");

                byte[] byteEncode = Encoding.UTF8.GetBytes(message[4]);
                string encodingstring = Convert.ToBase64String(byteEncode);

                List<string> chat_content = new List<string>()
                {
                    message[1],
                    message[5],
                    ctb.Chat_Birth.ToString()!,
                    encodingstring
                };

               foreach(var conn in _users)
                {
                    _runtime!.SendMessage(conn.ConnId, PacketID.SC_SendChat, chat_content);
                }
            }
            return true;
        }
    }
}