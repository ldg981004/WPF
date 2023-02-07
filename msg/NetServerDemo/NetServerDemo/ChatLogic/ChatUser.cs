using CommonLib.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServerDemo.ChatLogic
{
    internal class ChatUser
    {
        public ChatUser(string userId, ConnectionID connId)
        {
            UserId = userId;
            ConnId = connId;
            // ChatUser라는 클래스를 만들어서 string UserId(로그인 시 아이디),
            // ConnectionID connId(서버 접속 시 고유 아이디)를 가지고 있음
        }

        public string UserId { get; private set; }
        public ConnectionID ConnId { get; private set; }

        // string 형태의 UserID와 ConnectionID 형태의 ConnId 생성


    }
}
