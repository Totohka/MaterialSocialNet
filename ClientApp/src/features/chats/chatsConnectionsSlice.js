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
  nonReaden:null,
  connectionNotification:null,
  // chats:
  // [
  //   {
  //     chatId:null,
  //     messagesInfo:[
  //       {
  //         numberPage:0,
  //         messages:[]
  //       }
  //     ]
  //   }    
  // ],
  numberPage:-1,
  messages:[],
  chatId:null,
}

const chatsSlice = createSlice({
  name: 'chats',
  initialState,
  reducers: {
    setNonReadedChats(state,action){
        state.loading=true;
        state.nonReaden=action.payload;
        state.loading=false;
    },
    setNotifications(state, action){
        state.loading=true;
        state.connectionNotification=action;
        state.loading=false;
    },
    setMessages(state, action){
      // let newchats = state.chats.map(chat=>{
      //   if (chat.chatId==action.payload.chatId){
      //     let chatNew = chat.map((messagesInfo)=>{
      //       if (messagesInfo.numberPage==action.payload.messageInfo.numberPage){
      //         return action.payload.messageInfo;
      //       } else 
      //       {return messagesInfo;}
      //     });
      //     if (chatNew!=chat){
      //       return chatNew;
      //     }else return [action.payload.messageInfo, ...chatNew.messagesInfo];
      //   } else return chat; 
      // });
      // if (newchats!=state.chats){
      //   state.chats=newchats;
      // } else state.chats=[action.payload, ...state.chats];
      // if(state.chats.find(x=>x.chatId==action.payload.chatId)!=undefined){
      //   if(state.chats.find(x=>x.chatId==action.payload.chatId).messagesInfo.find(x => x.numberPage == action.payload.messagesInfo.numberPage)!=undefined){
      //     let objId = state.chats.findIndex(x=>x.chatId==action.payload.chatId);
      //     let msgId = state.chats[objId].messagesInfo.findIndex(x => x.numberPage == action.payload.messagesInfo.numberPage);
      //     state.chats[objId].messagesInfo = [...state.chats[objId].messagesInfo.slice(msgId, 1, action.payload.messagesInfo)];
      //   }
      //   else{
      //     let objId = state.chats.findIndex(x=>x.chatId==action.payload.chatId);
      //     state.chats[objId].messagesInfo = [action.payload.messagesInfo, ...state.chats[objId].messagesInfo]
      //   }
      // }
      // else{
      //   state.chats = [action.payload, ...state.chats];
      // }
      
      // if(state.messages.find(x=>x.numberPage==action.payload.numberPage)){
      //   let objId = state.messages.findIndex(x=>x.numberPage==action.payload.numberPage);
      //   state.messages=[...state.messages.slice(objId, 1, action.payload)];
      // }
      // else{
      state.loading=true;
      state.success=false;
      
      // console.log("setMessages before!", ...state.messages,  state.loading, state.success);
      // console.log(action.payload);
      // console.log(action.payload.messages);
      
      // if (state.numberPage!=null){
      //   console.log(state.numberPage, "no null");
        if(action.payload.numberPage>state.numberPage)
        { 
          // console.log(state.numberPage, "no null and <");
          state.messages=[...action.payload.messages, ...state.messages];
          state.numberPage=action.payload.numberPage;
        // }
      }else if(state.numberPage==-1){
        // console.log(state.numberPage);
        state.numberPage=action.payload.numberPage;
        state.messages=[...action.payload.messages];
      }
      // console.log("setMessages after!", state.messages, state.loading, state.success);
      state.success=true;
      state.loading=false;  
      
      
      // }
      console.log("setMessages!", state.messages, action.payload, state.loading, state.success);
    },
    setNewMessage(state, action){
      // if(state.chats.find(x=>x.chatId==action.payload.chatId)){
      //   let newObj = state.chats.find(x=>x.chatId==action.payload.chatId).msgs[0].messages;
      //   newObj.unshift(action.payload.message);
      //   state.chats.find(x=>x.chatId==action.payload.chatId).msgs.messages=[...state.chats.find(x=>x.chatId==action.payload.chatId).msgs.slice(-1, 1, {numberPage:0, newObj})];
      // }
      if(action.payload.chatId==state.chatId){
        state.messages=[...state.messages, action.payload.message];
        console.log('New Message!');
      }
      
    },
    clearMessages(state, action){
      // let objId = state.chats.findIndex(x => x.chatId === action.payload);
      // state.chats = [...state.chats.slice(objId, 1)];
      // console.log("cleared!", state.chats.find(x => x.chatId==action.payload));
      state.loading=true;
      state.success=false;
      state.numberPage=-1;
      state.messages=[];
      state.success=true;
      state.loading=false;
      console.log("cleared!", state.messages);
    },
    editMessage(state, action){
      // let objId = state.chats.find(x => x.chatId === action.payload).msgs.messages.findIndex(x=>x.id==action.payload.message.messageId);
      // state.chats.find(x => x.chatId === action.payload).msgs.messages = [...state.chats.find(x => x.chatId === action.payload).msgs.messages.slice(objId, 1, action.payload.message)];
      if(state.chatId==action.payload.chatRoomId){
        state.loading=true;
        state.success=false;  
        let messages = state.messages;
        let objId = messages.findIndex(x=>x.id==action.payload.id);
        messages[objId].msg=action.payload.msg;
        state.messages=[...messages]
        state.success=true;
        state.loading=false;
      }
    },
    deleteMessage(state, action){
      if(state.chatId==action.payload.chatRoomId){
        console.log('delete');
        let messages = state.messages;
        console.log(messages.length);
        let objId = messages.findIndex(x=>x.id==action.payload.id);
        console.log(objId);
        messages.splice(objId,1);
        console.log(messages.length);
        state.messages=[...messages];
      }
    },
    setChatId(state, action){
      if (state.chatId!=action.payload){
        state.loading=true;
        state.success=false;
        state.chatId=action.payload;
        state.numberPage=-1;
        state.messages=[];
        console.log("cleared!", state.messages, state.numberPage);
        state.loading=false;
      }
      
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
    setReaction(state,action){
      state.loading=true;
      state.success=false;

      let messages = state.messages;
      let objId = messages.findIndex(x=>x.id==action.payload.id);
      messages[objId].typeReacton = action.payload.typeReacton;
      let oldIndex = messages[objId].typeReactions.indexOf(action.payload.oldReaction);
      console.log(oldIndex);
      console.log(action.payload.typeReaction!='');
      if (oldIndex==-1){
        console.log("added reaction!");
        messages[objId].typeReactions=[...messages[objId].typeReactions, action.payload.typeReaction];
      }else if(action.payload.typeReaction!=''){
        console.log("type reaction !' ' ")
        messages[objId].typeReactions.splice(oldIndex, 1, action.payload.typeReaction);
        // messages[objId].typeReactions[objId]=action.payload.typeReaction;
        console.log(messages[objId]);
      }else {
        messages[objId].typeReactions.splice(oldIndex, 1);
      }
      state.messages=[...messages];
      console.log('redacted');

      state.success=true;
      state.loading=false;
    }
  },
  extraReducers(builder){}
})
export const {setNonReadedChats, setNotifications, setMessages, setNewMessage, clearMessages, editMessage, setChatId, setError, setLoading, deleteMessage, setReaction} = chatsSlice.actions;
export default chatsSlice.reducer;
