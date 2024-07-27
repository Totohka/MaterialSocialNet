import { createSlice } from '@reduxjs/toolkit'
//import { userLogin } from './authAction'
import { jwtDecode } from 'jwt-decode'
import { useSelector } from 'react-redux'
import useSignalR from '../../app/useSignalR'
import { useEditMessageMutation } from '../api/apiSlice'



const initialState = {
  loading: false,
  success: false,
  isError: false,
  error:null,
  notifications:[],
}

const notificationSlice = createSlice({
  name: 'notifications',
  initialState,
  reducers: {
    setNonReadedChats(state,action){
        state.loading=true;
        state.nonReaden=action.payload;
        state.loading=false;
    },
    setNotifications(state, action){
      
      state.loading=true;
      state.success=false;
      
    //   console.log('here notif');
    //     if(action.payload.numberPage>state.numberPage)
    //     { 
    //       // console.log(state.numberPage, "no null and <");
          
    //     //   state.numberPage=action.payload.numberPage;
    //     // }
    //   }else if(state.numberPage==-1){
    //     // console.log(state.numberPage);
    //     state.numberPage=action.payload.numberPage;
    //     state.notifications=[...action.payload.notifications];
    //   }
      state.notifications=[...action.payload];
      // console.log("setMessages after!", state.notifications, state.loading, state.success);
      state.success=true;
      state.loading=false;  
      
      
      // }
      console.log("setNotifications!", state.notifications, action.payload, state.loading, state.success);
    },
    setNewNotification(state, action){
      // if(state.chats.find(x=>x.chatId==action.payload.chatId)){
      //   let newObj = state.chats.find(x=>x.chatId==action.payload.chatId).msgs[0].notifications;
      //   newObj.unshift(action.payload.message);
      //   state.chats.find(x=>x.chatId==action.payload.chatId).msgs.notifications=[...state.chats.find(x=>x.chatId==action.payload.chatId).msgs.slice(-1, 1, {numberPage:0, newObj})];
      // }
        state.notifications=[...state.notifications, action.payload];
        console.log('New Notification!');
    //   }
      
    },
    clearMessages(state, action){
      // let objId = state.chats.findIndex(x => x.chatId === action.payload);
      // state.chats = [...state.chats.slice(objId, 1)];
      // console.log("cleared!", state.chats.find(x => x.chatId==action.payload));
      state.loading=true;
      state.success=false;
      state.numberPage=-1;
      state.notifications=[];
      state.success=true;
      state.loading=false;
      console.log("cleared!", state.notifications);
    },
    deleteNotification(state, action){
        console.log('delete');
        let notifications = state.notifications;
        let clearedNotif;
        console.log(notifications.length);
        if(action.payload.ChatRoomId!=-1){
            clearedNotif = notifications.filter(x=>x.ChatRoomId!=action.payload.ChatRoomId);
        }else{
            clearedNotif = notifications.filter(x=>x.PostId!=action.payload.PostId);
        }
        console.log(clearedNotif);
        state.notifications=[...clearedNotif];
    },
    setLoading(state){
      state.loading=true;
      // state.success=false;
    },
    setError(state, action){
      state.error=action.payload;
      state.isError=true;
      state.loading=false;
      state.success=false;
    },
    
  },
  extraReducers(builder){}
})
export const {setNotifications, setNewNotification, setError, setLoading, clearNotifications, deleteNotification} = notificationSlice.actions;
export default notificationSlice.reducer;
