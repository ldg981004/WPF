using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServerDemo
{
    internal class Model
    {
       

        public class User_TBL
        {
            [Key]
            public int User_Num { get; set; }
            public string? User_Name { get; set; }
            [Key]
            public string? User_Id { get; set; }
            public string? User_Pw { get; set; }
            public string? User_Birth { get; set; }
            public string? User_Phone { get; set; }

        }
        public class ChatRoom_TBL
        {
            [Key]
            public int ChatRoom_Num { get; set; }
            public string? ChatRoom_Id { get; set; }
            [Key]
            public string? ChatRoom_User { get; set; }
            public string? ChatRoom_Maker { get; set; }
            public DateTime? ChatRoom_Birth { get; set; }
        }

        public class Chat_TBL
        {
            public int Chat_Num { get; set; }
            public string? User_Id { get; set; }
            public int? ChatRoom_Num { get; set; }
            public string? Room_Id { get; set; }
            public string? Member_Id { get; set; }
            public DateTime? Chat_Birth { get; set; }
            public string? Chat_Content { get; set; }
            
        }

    }
}
