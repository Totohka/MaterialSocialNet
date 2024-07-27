//#region import
import React, { useCallback, useEffect, useState } from 'react';
import './App.css';
import Header from './widgets/Header/Header';
import Profile from './pages/Profile/Profile';
import Auth from './pages/Auth/Auth';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Registration from './pages/Registration/Registration';
import {
  QueryClient,
  QueryClientProvider,
} from 'react-query';
// import DashboardContainer from './components/Dashboard/DashboardContainer';
import NavBar from './widgets/NavBar/NavBar';
import GalleryAllPhoto from './widgets/GalleryAllPhoto/GalleryAllPhoto';
import Post from './pages/Post/Post';
import News from './pages/News/News';
import Users from './pages/Users/Users';
import UserProfile from './pages/UserProfile/UserProfile';
import Settings from './pages/Settings/Setting';
import Chats from './pages/Chats/Chats';
import Chat from './pages/Chat/Chat';
import RequireAuth from './pages/RequireAuth/RequireAuth';
import CreateChat from './pages/CreateChat/CreateChat';
import { connection } from './app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import axios from 'axios';
import { connectionNotif } from './app/helpers/signalRNotifications';
import { useSelector } from 'react-redux';

//#endregion

const queryClient = new QueryClient();

function App() {

  const [page, setPage] = useState(0);
  const [pageCount, setPageCount] = useState(0);
  const user = useSelector(state=>state.user.userInfo);
  const startConnect = useCallback(()=>{
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
      while (p<pageC){
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
          for (var chat in chatsInfo.chatRooms.value){
            console.log(chat);
            chatId = String(chat.id);
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
    }
  }
  useEffect(()=>{
    window.onload = function(){
      if (window.location.pathname!='/login' && window.location.pathname!='/' && window.location.pathname!='/registration'){
        console.log("Connecting...")
         connection.baseUrl=`http://25.32.11.98:8089/chat?t=${localStorage.getItem('token')}`;
        connectionNotif.baseUrl=`http://25.32.11.98:8090/notifications?t=${localStorage.getItem('token')}`;
        startConnect();
        startChatsConnection();
      }
    }
  },[])
  return (
    <QueryClientProvider client={queryClient}>
    <BrowserRouter>
      <div className='app-wrapper'>
        <Header/> 
        <div className='app-wrapper-content'>
        <NavBar/>
          <Routes>
            <Route path='/' element={<Auth/>}/>  
            <Route path='/login' element={<Auth/>}/>            
            <Route path='/registration' element={<Registration/>}/>            
            <Route path='/post/*' element={<RequireAuth><Post/></RequireAuth>}/>
            <Route path='/chats' element={<RequireAuth><Chats/></RequireAuth>}></Route>
            <Route path='/dialogs/*' element={<RequireAuth><Chat/></RequireAuth>}/>
            <Route path='/profile' element={<RequireAuth><Profile/></RequireAuth>}/>  
            <Route path='/users' element={<RequireAuth><Users/></RequireAuth>}/>  
            <Route path='/user/*' element={<RequireAuth><UserProfile/></RequireAuth>}></Route>
            <Route path='/createchat' element={<RequireAuth><CreateChat/></RequireAuth>}/>
            <Route path='/settings' element={<RequireAuth><Settings/></RequireAuth>}/> 


            <Route path='/gallery/*' element={<RequireAuth><GalleryAllPhoto/></RequireAuth>}/>
            <Route path='/news' element={<RequireAuth><News/></RequireAuth>}/>
            {/* <Route path='/dashboard' element={<RequireAuth>< /></RequireAuth>}/> */}
          </Routes>
        </div>       
      </div>  
    </BrowserRouter>
    </ QueryClientProvider>
  );
}


export default App;