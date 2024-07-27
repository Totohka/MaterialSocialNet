import { React, useCallback, useEffect, useState } from 'react';
import './Auth.css';
import logo from "./../../images/logo.png";
import { NavLink, redirect, useNavigate } from "react-router-dom";
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import { useGetChatsQuery, useLazyGetChatsQuery, useTryAuthMutation, useTryAuthQuery} from '../../features/api/apiSlice';
import { useDispatch } from 'react-redux';
import { setUser } from '../../features/auth/userSlice';
import useSignalR from '../../app/useSignalR';
import { setChats, setNewMessage } from '../../features/chats/chatsConnectionsSlice';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { connectionNotif } from '../../app/helpers/signalRNotifications';

const Auth = () => {
    const [email, setEmail] = useState("");
    const [pass, setPass] = useState("");
    const [loginUser, {data, error, refetch}] = useTryAuthMutation();
    // const [data, setData] = useState(true);
    // const [error, setError] = useState(false);
    const [page, setPage] = useState(0);
    const [pageCount, setPageCount] = useState(0);
    const [dataChat, setDataChat] = useState({page:page, search:''});
    const navigate = useNavigate();
    
    const dispatch = useDispatch();
    const changeEmail = (e) => {
      setEmail(e.target.value);
    }
    const [getChats] = useLazyGetChatsQuery();
    const startChatsConnection = async() => {
      let pageC=0;
      let chatId;
      try{
        let response = await axios.get('http://25.32.11.98:8089/api/Chat/All', {
          params:{number:page, search:''},
          headers:{
              'Authorization':localStorage.getItem('token')
          }
        });
        let chatsInfo=response.data;
        setPageCount(chatsInfo.pageCount);
        console.log(chatsInfo);
        pageC=chatsInfo.pageCount;
        console.log(chatsInfo.chatRooms);
        for (var chat in chatsInfo.chatRooms){
          console.log(chatsInfo.chatRooms[chat]);
          chatId = String(chatsInfo.chatRooms[chat].id);
          connection.invoke("OnConnectedChatAsync", (chatId))
          .then((data) => console.log("chat Connection started", data))
          .catch((err) => console.error(err.toString()));
        }
      }finally{
        let p = 0;
        p++;
        console.log(p);
        console.log(pageC);
        while (p<pageC){
          console.log('tried');
          try{
            let response = await axios.get('http://25.32.11.98:8089/api/Chat/All', {
              params:{number:p, search:''},
              headers:{
                  'Authorization':localStorage.getItem('token')
              }
            });
            let chatsInfo=response.data;
            p++;
            console.log(chatsInfo);
            for (var chat in chatsInfo.chatRooms){
              console.log(chat);
              chatId = String(chatsInfo.chatRooms[chat].id);
              connection.invoke("OnConnectedChatAsync", (chatId))
              .then((data) => console.log("chat Connection started", data))
              .catch((err) => console.error(err.toString()));
            }
          }catch(error){
            console.log(error);
          }
          // await getChats({page:p, search:''}).then(()=>{
          //   p++;
          //   console.log(chatsInfo);
          //   for (var chat in chatsInfo.chatRooms){
          //     chatId = String(chat.id);
          //     connection.invoke("OnConnectedChatAsync", (chatId))
          //     .then(() => console.log("chat Connection started"))
          //     .catch((err) => console.error(err.toString()));
          //   }
          // });
        }
        navigate("/profile");
      }
      // connection.on('Receive', (chat_room_id, 
      //   message, 
      //   first_name, 
      //   last_name, 
      //   user_id, 
      //   date_send, 
      //   idMessage) => {
      //     console.log("Receive!");
      //     let element = {
      //       chatId:Number(chat_room_id),
      //       message:{
      //         id:idMessage,
      //         msg:message,
      //         userDTO:{
      //           id:Number(user_id),
      //           firstName:first_name,
      //           lastName:last_name,
      //           avatar:`${user_id}.jpg`
      //         },
      //         dateSend:date_send,
      //         typeReactions:[],
      //         typeReaction:""
      //       }
      //     }
      //     dispatch(setNewMessage(element));
      // });
      // console.log(response);
      // .then((onfulfilled)=>{
      //   //setPageCount(chatsInfo.pageCount);
      //   console.log(chatsInfo);
      //   pageC=chatsInfo.pageCount;
        
      //   for (var chat in chatsInfo.chatRooms){
      //     chatId = String(chat.id);
      //     connection.invoke("OnConnectedChatAsync", (chatId))
      //     .then(() => console.log("chat Connection started"))
      //     .catch((err) => console.error(err.toString()));
      //   }
      // }).finally(async()=>{
      //   let p = page;
      //   p++;
      //   console.log(p);
      //   while (p<pageC){
      //     await getChats({page:p, search:''}).then(()=>{
      //       p++;
      //       console.log(chatsInfo);
      //       for (var chat in chatsInfo.chatRooms){
      //         chatId = String(chat.id);
      //         connection.invoke("OnConnectedChatAsync", (chatId))
      //         .then(() => console.log("chat Connection started"))
      //         .catch((err) => console.error(err.toString()));
      //       }
      //     });
      //   }
      // })
      
    }
    const startConnect = useCallback((user)=>{
      if (connection.state !== HubConnectionState.Connected) {
          connection
            .start()
            .then(() => console.log("Connection started"))
            .catch((err) => console.error(err.toString()));
        }
      if (connectionNotif.state !== HubConnectionState.Connected) {
        connectionNotif
          .start()
          .then(() => {console.log("Connection started");
            connectionNotif.invoke('OnConnectedSendNotifications', String(user.id)).then(()=>console.log('Notififcations get'))
            .catch(error=>console.log(error));
          })
          .catch((err) => console.error(err.toString()));
      }
    },[])
    const stopConnect = useCallback(()=>{
      if (connection.state !== HubConnectionState.Disconnected) {
        connection.stop()
          .then(() => console.log("Connection stoped"))
          .catch((err) => console.error(err.toString()));
      }
   },[])
   const startNotify = useCallback((user)=>{
    
   },[])
    const changePass = (e) => {
      setPass(e.target.value);
    }
    useEffect(()=>{
      if (data){
        if(data!=401){
          const token = data;
          const user = jwtDecode(token);
          //localStorage.setItem("token",`Bearer ${token}`);
          connection.baseUrl=`http://25.32.11.98:8089/chat?t=${token}`;
          connectionNotif.baseUrl=`http://25.32.11.98:8090/notifications?t=${token}`
          dispatch(setUser(token));
          
          // setPageCount(chatsInfo.pageCount);
          startConnect(user);
          startNotify(user);
          startChatsConnection();
          // console.log(connection);
          // dispatch(setChats(connection));
          
        }else{
          alert('Неправильное имя пользователя или пароль!');
        }
      }
      if(error){
        alert(error);
      }
    },[data, error]);
    // const loginIn = async() =>{
    //   try{
    //       const response = await axios.get('http://25.32.11.98:8085/Auth', { params: { email: email, password: pass}});
    //       //localStorage.setItem("token",`Bearer ${response.data}`);
    //       console.log(response.data);
    //       // dispatch(setUser(response.data));
    //       //setData(response.data);
    //   }catch(error){
    //       console.log(error.message);
    //       setError(error);
    //   }
    // }
    const handleSubmit = async (e) => {
      e.preventDefault();
      // loginIn();
      await loginUser({email, pass});
    }

    return (
      <div className="auth">
        <img src={logo}></img>
        <div className="form_auth">
        <form onSubmit={handleSubmit}>
        <div className="title_auth">С возвращением в SocialNet!</div>
          <div className="input-container ic1">
            <input id="email" className="input" 
                  name="email"
                  type="text"
                  value={email}
                  onChange={changeEmail} 
            placeholder="Введите email..." />
          </div>
          <div className="input-container ic2">
            <input id="password" className="input" 
                  name="password"
                  type="password"
                  value={pass}
                  onChange={changePass} 
            placeholder="Введите пароль..." />
          </div>
          <button className="submit" type="submit">Войти</button>
          <NavLink to='/registration'>Вы здесь впервые?</NavLink>
          <NavLink to='/'>Забыли пароль?</NavLink>
        </form>
      </div>
      </div>
    );
}

export default Auth;