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
import { setNewNotification, setNotifications } from "../../features/notifications/notificatiosSlice";

export const connectionNotif = new HubConnectionBuilder()
  .withUrl(
    `http://25.32.11.98:8090/notification?t=`,
    {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets,
    }
  ).withAutomaticReconnect()
  .build();
  

const callbacks = withCallbacks()
    .add('SendNotification', (json)=>(dispatch)=>{
        console.log('getNotif');
        console.log(json);
        console.log(JSON.parse(json));
        let obj = JSON.parse(json);
        // console.log(obj.NotificationPosts);
        // console.log(obj.NotificationChatRooms);
        let notifications = [...obj.NotificationPosts, ...obj.NotificationChatRooms];
        console.log(notifications);
        
        dispatch(setNotifications(notifications));
    })
    .add('NotificationPost', (
        FirstName,
        LastName,
        Id,
        PostId
    )=>(dispatch)=>{
        let obj = {FirstName:FirstName, LastName:LastName, UserId:Id, PostId:PostId};
        console.log("PostNotify!", obj);
        dispatch(setNewNotification(obj));
    })
    .add('NotificationChat', (
        Name,
        LastMessage,
        ChatRoomId
    )=>(dispatch)=>{
        let obj = {Name:Name, LastMessage:LastMessage, ChatRoomId:ChatRoomId};
        console.log("ChatNotify!", obj);
        dispatch(setNewNotification(obj));
    })

export const signalNotif = signalMiddleware({
  callbacks:callbacks,
  connection:connectionNotif,
  shouldConnectionStartImmediately: false,
});
