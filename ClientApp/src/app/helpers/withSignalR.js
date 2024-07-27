import { useDispatch, useSelector } from "react-redux";
import {
  HubConnectionBuilder,
  signalMiddleware,
  withCallbacks,
  HttpTransportType,
} from "redux-signalr";
import {
  deleteMessage,
  editMessage,
  setNewMessage,
  setNonReadedChats,
} from "../../features/chats/chatsConnectionsSlice";

export const connection = new HubConnectionBuilder()
  .withUrl(
    `http://25.32.11.98:8089/chat?t=`,
    {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets,
    }
  ).withAutomaticReconnect()
  .build();
  

const callbacks = withCallbacks()
  .add("Receive",
  (
    chat_room_id,
    message,
    first_name,
    last_name,
    user_id,
    date_send,
    idMessage
    ) =>
    (dispatch) => {
      console.log("Receive!");
      let element = {
        chatId: Number(chat_room_id),
        message: {
          id: idMessage,
          msg: message,
          userDTO: {
            id: Number(user_id),
            firstName: first_name,
            lastName: last_name,
            avatar: `${user_id}.jpg`,
          },
          dateSend: date_send,
          typeReactions: [],
          typeReaction: "",
        },
      };
      // console.log(element);
      dispatch(setNewMessage(element));
      dispatch(setNonReadedChats(chat_room_id));
    }
  )
  .add("UpdateMessage", (
    Id,
    Msg,
    ChatRoomId
  )=>(dispatch)=>{
    dispatch(editMessage({id:Id, msg:Msg, chatRoomId:ChatRoomId}));
  } )
  .add("DeleteMessage",(
    messageId,
    chatId
  )=>(dispatch)=>{
    dispatch(deleteMessage({id:messageId, chatRoomId:chatId}));
  })

export const signal = signalMiddleware({
  callbacks,
  connection,
  shouldConnectionStartImmediately: false,
});
