using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketLib
{
    public enum PacketID    // 패킷아이디를 지정해 놓는 클래스
                            // Login을 1번으로 지정해서 그 다음부터는 1씩 증가
    {
        //client to server        
        CS_ReqSignUp = 1,

        CS_ReqLogin,

        CS_RecvMessage,

        SC_DupId,

        SC_SignUpResult,

        SC_NoId,

        SC_AlreadyLogin,

        SC_LogoutSuc,

        SC_LoginSuc,

        SC_PwInCor,

        SC_LoginResult,
        
        SC_LoginedUser,
        
        SC_AllUser,

        CS_LogOut,

        CS_ChatroomCreate,

        CS_ChatroomList,

        SC_ChatInfo,

        CS_ChatRoom,

        SC_ChatRoomInfo,

        CS_ChatRoomClick,

        SC_ChatRoomClick,

        CS_Previous,

        SC_Who_are_you,

        CS_I_am,

        SC_PreChat,

        CS_OutRoom,

        SC_OutOk,

        CS_ChatMember,

        SC_CurrentMember,

        CS_AllUser,

        SC_AllUserList,

        CS_ChatJoin,

        SC_InviteSuc,

        SC_InviteFail,

        CS_BackChat,

        SC_BackChat,

        CS_InviteBack,

        CS_SendChat,

        SC_SendChat,

        CS_User,

        SC_User,

        CS_Logined,

        CS_AllUserList,

        CS_Maker,

        CS_RemoveUser,

        SC_RemoveMember,

        CS_MemberExpulsion,

        SC_ExpusionResult,

        SC_DbNull,

        CS_HomeBtn,

        SC_HomeBtnAlluser,

        SC_HomeBtnLogined,

        CS_ExpulsionBack,

        CS_ChangePassword,

        SC_ChangePassword,

        CS_ChangePasswordInfo,

        SC_ChangePasswordFail,

        SC_ChangePasswordSuccess,

        SC_LogOutUser,

        SC_LogOutList,

        SC_LoginUserList,

        SC_MakerAndUser,

        SC_InviteMember,

        SC_ExpulsionMember,

        Max
    }

}
