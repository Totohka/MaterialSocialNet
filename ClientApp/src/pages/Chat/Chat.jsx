import React, { useEffect, useState } from 'react';
import './Chat.css';
import MessageItem from '../../widgets/MessegeItem/MessageItem';
import SmallDialogItem from './SmallDialogItem/SmallDialogItem';
import plus from "./../../images/plus.svg"
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import reaction_button from './../../images/reaction_button.svg';
import Popup from 'reactjs-popup';
import useSignalR from '../../app/useSignalR';
import { useGetMessagesQuery, useLazyGetMessagesQuery } from '../../features/api/apiSlice';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { useDispatch, useSelector } from 'react-redux';
import { clearMessages, setChatId, setError, setLoading, setMessages, setNewMessage, setNonReadedChats } from '../../features/chats/chatsConnectionsSlice';
import getMessages from '../../app/helpers/axiosMessages';
import { HttpTransportType, HubConnectionBuilder } from '@microsoft/signalr';
import Reaction from '../../widgets/Reaction/Reaction';
import ChatInfo from '../ChatInfo/ChatInfo';

const Chat = ({chat}) => {
    // const connect = new HubConnectionBuilder()
    // .withUrl(
    //     `http://25.32.11.98:8089/chat?t=${
    //       localStorage.getItem("token")
    //         ? localStorage.getItem("token").slice(7)
    //         : localStorage.getItem("token")
    //     }`,
    //     {
    //       skipNegotiation: true,
    //       transport: HttpTransportType.WebSockets,
    //     }
    //   )
    //   .build();
    let param = useParams();
    let id = Number(param['*']);
    const [mes, setMes] = useState();
    const token = localStorage.getItem('token');
    // const us = jwtDecode(token.slice(7,));
    const us = useSelector(state=>state.user.userInfo);
    //const [messages, setMessages] = useState([true]);
    const [pageCount, setPageCount] = useState(0);
    const numberPage = useSelector(state=>state.chats.numberPage);
    const navigateU = useNavigate();
    const [text, setText] = useState();
    const [showId, setShowId] = useState(0);
    const [showReactId, setShowReactId] = useState(0);

    const [reactId, setReactId] = useState(0);
    const [page, setPage] = useState(0);
    const [chatInfo, setChatInfo] = useState(null);
    const navigate = useNavigate();
    const [reacts, setReacts] = useState([false]);

    const [data, setData] = useState({chatId:id, page:page})
    const dispatch = useDispatch();
    // const chats = useSelector(state=>state.chats.chats);
    const messages = useSelector(state=>state.chats.messages);
    const chatId = useSelector(state=>state.chats.chatId);
    const isLoading = useSelector(state=>state.chats.loading);
    const isSuccess = useSelector(state=>state.chats.success);
    
    // const [getMessages, { data: messagesInfo,
    //     isLoading,
    //     isSuccess,
    //     isError,
    //     error }] = useLazyGetMessagesQuery(data);

    const logOut = () =>{
        localStorage.removeItem('token');
        if (connection.state !== HubConnectionState.Disconnected) {
            connection.stop()
                .then(() => console.log("Connection stoped"))
                .catch((err) => console.error(err.toString()));
            }
        navigate('/');
    }
    const getMes = async(arg) =>{
        // const dispatch = useDispatch();
        const token = localStorage.getItem('token');
        //dispatch(setLoading());
        try{
            const result = await axios.get("http://25.32.11.98:8089/api/Message/All",{
                params:arg,
                headers:{
                    'Authorization':token
                }
            });
            setPageCount(result.data.pageCount);
            dispatch(setMessages(result.data));
            if(result.data.numberPage==0){
                let div = document.getElementById('messages_in_chat');
                div.scrollTop = div.scrollHeight - div.clientHeight;
            }
            else{
                let div = document.getElementById('messages_in_chat');
                div.scrollTop = div.clientHeight;
            }
            console.log(messages);
            setPage(numberPage);
            console.log(pageCount);
            // return {data: result.data, error: false};
    
        } catch(error){
            // dispatch(setError(error));
            // return {data:null, error:error};
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    logOut();
                    
                }
            }   
        }
        
    }
    // const connected =async()=>{
    //     try{
    //         let result = await connect.send("OnConnectedChatAsync", (chatId));
    //         console.log("chat Connection started", data);
    //         connect.on("Receive", (chat_room_id,
    //             message,
    //             first_name,
    //             last_name,
    //             user_id,
    //             date_send,
    //             idMessage)=>{
    //             console.log("Received!", message);
    //         })
    //     }catch(error){
    //         console.error(error.toString())
    //     }
            
    // }
    useEffect( () => {
        if (page==0){
            dispatch(setChatId(Number(id)));
            
            // if (connect.state !== HubConnectionState.Connected) {
            //     connect
            //       .start()
            //       .then(() => console.log("Connection started"))
            //       .catch((err) => console.error(err.toString()));
            //   }
            // connected();
        }
        // dispatch(clearMessages());    
        // dispatch(setLoading());
        getMes({chatId:Number(id), number:page});
        // if (connection.state !== HubConnectionState.Connected) {
        //     connection
        //       .start()
        //       .then(() => console.log("Connection started"))
        //       .catch((err) => console.error(err.toString()));
        //   }
        // connection.on('Receive', (chat_room_id, 
        //     message, 
        //     first_name, 
        //     last_name, 
        //     user_id, 
        //     date_send, 
        //     idMessage) => {
        //       console.log("Receive!");
        //       let element = {
        //         chatId:Number(chat_room_id),
        //         message:{
        //           id:idMessage,
        //           msg:message,
        //           userDTO:{
        //             id:Number(user_id),
        //             firstName:first_name,
        //             lastName:last_name,
        //             avatar:`${user_id}.jpg`
        //           },
        //           dateSend:date_send,
        //           typeReactions:[],
        //           typeReaction:""
        //         }
        //       }
        //       dispatch(setNonReadedChats(chat_room_id));
        //       dispatch(setNewMessage(element));
        //   });
        //   console.log("receive on?");

    },[])
    // useEffect(()=>{
        
    //     if(isLoading && page==0){
    //         // dispatch(setChatId(id));
    //         dispatch(clearMessages());
    //     }
    //     if(!isLoading){
    //         if(isSuccess){
    //         dispatch(setMessages(messagesInfo));
    //         // console.log(postsInfo);
    //         // console.log(isLoading);
    //         setPageCount(messagesInfo.pageCount);
    //         console.log(messages);
    //         // console.log(postsInfo.posts);
    //         // console.log(posts);
    //         }
    //         if(isError){
    //             console.log(error);
    //             if (error.status==401){
    //                 logOut();
    //             }
    //             else{
    //                 alert(error.message);
    //             }
    //         }
    //         else{
    //             if (chatId!=id){
    //                 dispatch(setChatId(id));
    //             }
    //         }
    //     }
    // },[isLoading, isSuccess, isError, data]);
    // useState(()=>{
    //     if(chatId!=id){
    //         let result = getMessages(data);
    //         se
    //     }
    // },[])

    const getChatInfo = async() =>{
        try{
            const result = await axios.get("http://25.32.11.98:8089/api/Chat",{
                params:{
                    chatId:id
                },
                headers:{
                    'Authorization':token
                }
            });
            setChatInfo(result.data);
        } catch(error){
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    navigateU("/login");
                    alert('Время вашего сеанса истекло');
                }
            }
            
            
        }
    }
    const getAllReactions = async() =>{
        try{
            const result = await axios.get("http://25.32.11.98:8088/TypeReaction/All",{
                headers:{
                    "Authorization":token
                }
            })
            setReacts(result.data);
            console.log(result);
        }catch(error){
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    navigate("/login");
                    alert('Время вашего сеанса истекло');
                }
            }   
        }
    }
    // const getMessages = async(x=-1) =>{
    //     if(x==-1){
    //         x=numberPage;
    //     }
    //     console.log(numberPage);
    //     try{
    //         const result = await axios.get("http://25.32.11.98:8089/api/Message/All",{
    //             params:{
    //                 chatId: id,
    //                 number: x
    //             },
    //             headers:{
    //                 'Authorization':token
    //             }
    //         });
    //         console.log(result.data);
    //         setPageCount(result.data.pageCount);
    //         if(x!=0){
    //             setMessages(messages=>[...result.data.messages,...messages]);
    //         } else {setMessages(result.data.messages);
    //         let div = document.getElementById('messages_in_chat');
    //         div.scrollTop = div.scrollHeight - div.clientHeight;}
    //     } catch(error){
    //         console.log(error);
    //         if(error.response){
    //             if (error.response.status==401){
    //                 navigateU("/login");
    //                 alert('Время вашего сеанса истекло');
    //             }
    //         }   
    //     }
    // }
    const sendMessage = async() =>{
        console.log(new Date().toJSON());
        try{
            const result = await axios.post("http://25.32.11.98:8089/api/Message",{
                message: text,
                date_send: new Date().toJSON(),
                chat_room_id: Number(id),
                user_id:Number(us.id),
                last_name:us.lastName,
                first_name:us.firstName
            },{
                
                headers:{
                    'Authorization':token
                }
            });
            
            setText('');
            //getMessages();
            let div = document.getElementById('messages_in_chat');
            div.scrollTop = div.scrollHeight - div.clientHeight;
        } catch(error){
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    logOut();
                }
            }   
        }
    }
    const updateMessage = async() =>{
        try{
            showHide(mes.id);
            const result = await axios.put("http://25.32.11.98:8089/api/Message",{
                id:mes.id,
                msg: text,
                dateSend: mes.dateSend,
                chatRoomId: id,
                userId:us.id
            },{
                headers:{
                    'Authorization':token
                }
            });
            
            setText('');
            //getMessages();
            //let div = document.getElementById('messages_in_chat');
            //div.scrollTop = div.scrollHeight - div.clientHeight;
        } catch(error){
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    logOut();
                }
            }   
        }
    }
    const deleteMessage = async(el) =>{
        try{
            showHide(el);
            const result = await axios.delete("http://25.32.11.98:8089/api/Message",{
                params:{
                    messageId:el,
                    chatId:id
                },
                headers:{
                    'Authorization':token
                }
            });
            //getMessages();
            
            //let div = document.getElementById('messages_in_chat');
            //div.scrollTop = div.scrollHeight - div.clientHeight;
        } catch(error){
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    navigateU("/login");
                    alert('Время вашего сеанса истекло');
                }
            }   
        }
    }
    // useEffect(()=>{
    //     getChatInfo();
    //     getMessages();
    //     getAllReactions();
    // },[])
    const tryToUpdate = (e) =>{
        let el = JSON.parse(e.target.value);
        setText(el.msg);
        setMes(el);
        document.getElementById("update_message_button").style.display="block";
        document.getElementById("send_message_button").style.display="none";
    }
    const tryToDelete = (e) =>{
        deleteMessage(e.target.value);
    }
    const showHide = (num) =>{
        if(document.getElementById("messageNav_"+num).style.display=="block"){
            document.getElementById("messageNav_"+num).style.display="none";
            document.getElementById("update_message_button").style.display="none";
            document.getElementById("send_message_button").style.display="block";
            setText('');
            setShowId(0);
        }else{
            if(showId!=0){
                document.getElementById("messageNav_"+showId).style.display="none";
            }
            setShowId(num);
            document.getElementById("messageNav_"+num).style.display="block";
            document.getElementById("update_message_button").style.display="none";
            document.getElementById("send_message_button").style.display="block";
        }
    }
    const showNav = (e) =>{
        showHide(e.target.value);

    }
    const showReacts = (e) =>{
        //console.log(e.target.value);
        if(document.getElementById("messageNav__allreactions"+e.target.value).style.display=="flex"){
            document.getElementById("messageNav__allreactions"+e.target.value).style.display="none";
            setShowReactId(0);
        }else{
            let myReact = messages.filter(el => el.id==e.target.value)[0].typeReaction;
            // console.log(myReact);
            let typeReacts = new Set(reacts);
            // console.log(typeReacts);
            // console.log(Array.from(typeReacts).filter(el=>el.name==myReact)[0])
            if(myReact!="") setReactId(Array.from(typeReacts).filter(el=>el.name==myReact)[0].id);
            else setReactId(0);
            if(showReactId!=0){
                document.getElementById("messageNav__allreactions"+showReactId).style.display="none";
            }
            setShowReactId(e.target.value);
            document.getElementById("messageNav__allreactions"+e.target.value).style.display="flex";
        }
        

    }
    const showPrev = () =>{
        getMes({number:1+page, chatId:Number(id)});
        setPage(page+1);
        console.log("want prev!");
        // setNumberPage(numberPage+1);
        // getMessages(numberPage+1);
    }
    const onUpdateMsg = () =>{
        updateMessage();
        document.getElementById("send_message_button").style.display="block";
        document.getElementById("update_message_button").style.display="none";
    }
    let onSendMsg = () => {
        sendMessage();
    }
    const trySendReaction = (e) =>{
        if(document.getElementById("messageNav__allreactions_button"+showReactId+e.target.value).classList.contains("reacted")){
            const data = {
                entity_id: showReactId, 
                type_reaction_id: e.target.value, //likeId = 1
                user_id: us.id
              };
              const headers = { 
                'Authorization': token
              };
              axios.delete('http://25.32.11.98:8088/ReactionMessage',{data: data, headers: headers}).then(
                () => {
                    setReactId(0);
                    e.target.classList.toggle("reacted");
                }
              ).catch(error => {
                console.error('There was an error!', error);
              })
        }else{
            if(reactId!=0){
                const data = {
                    entity_id: showReactId, 
                    type_reaction_id: e.target.value, //likeId = 1
                    type_reaction_old_id: reactId, //dislikeId = 2
                    user_id: us.id
                  };
                  const headers = { 
                    'Authorization': token
                  };
                  axios.put('http://25.32.11.98:8088/ReactionMessage', data, {headers:headers}).then(
                    () => {
                      document.getElementById("messageNav__allreactions_button"+showReactId+reactId).classList.toggle("reacted");
                      setReactId(e.target.value);
                      e.target.classList.toggle("reacted");
                    }
                  ).catch(error => {
                    console.error('There was an error!', error);
                  })
            }else{
                const data = {
                  entity_id: showReactId, 
                  type_reaction_id: e.target.value, //likeId = 1
                  user_id: us.id
                };
                const headers = { 
                  'Authorization': token
                };
                axios.post('http://25.32.11.98:8088/ReactionMessage', data, {headers:headers}).then(
                  () => {
                    setReactId(e.target.value);
                      e.target.classList.toggle("reacted");
                  }
                ).catch(error => {
                  console.error('There was an error!', error);
                })
            }
        }
    }
    let onMessageChange = (e) => {
        let txt = e.target.value;
        setText(txt);
    }
    // let dialogsElements = props.dialogsData.map( (el) => <SmallDialogItem key={el.id} name={el.name} id={el.id}/> );;
    return (
        <div className='dialogs'> 
            <div className='chat_item'>
                <ChatInfo/>
                {/* <div className='chat_header'>
                    <div className="chat_img" style={{backgroundImage: `url(${defImg})`}}></div>
                    <div className='online'>
                        <div></div>
                        <p>online</p>
                    </div>
                    <p className='interlocutor'>{chatInfo?chatInfo.name:''}</p>
                </div> */}
                
                <div id="messages_in_chat" className='messages_in_chat'>
                    {numberPage<pageCount-1?<div className='showPrev'>
                                                        <button className="showPrev__button" onClick={showPrev}>Показать предыдущие сообщения</button>
                                                    </div>:<></>}
                    {isLoading?<>Загрузка...</>:isSuccess?
                    messages.length==0?<div>Пока нет сообщений</div>:
                    // <>messages{console.log(messages)}</>
                    messages.map((el)=>{
                        // console.log(messages);
                        // console.log(el);
                        // return elem.messages.map((el)=>{
                        if (el===null) return <div>Пока нет сообщений</div>
                        else {
                            let typeReacts = new Set(el.typeReactions);
                            if (el.userDTO.id==us.id) {
                            
                            return <>
                                    
                                    <div style={{display:'flex', marginBottom: '5px'}}>
                                        <div style={{width:'100%'}}>
                                            
                                            <MessageItem  key={el.id} dateSend={el.dateSend} message={el.msg} adress={el.userDTO} mesId={el.id}/>
                                            
                                            <Reaction el={el} typeReacts={typeReacts} inout="out"/>
                                            {/* <div className='messageNav__allreactions out' id={"messageNav__allreactions"+el.id}>
                                                {reacts.map((reaction)=>{
                                                    if (reaction==false) return <div>Загрузка...</div>
                                                    else {
                                                        if (reaction.name==el.typeReaction){
                                                            
                                                            return <button className='message__allreactions_button reacted' 
                                                                                                            onClick={trySendReaction} value={reaction.id} 
                                                                                                                                id={"messageNav__allreactions_button"+el.id+reaction.id}> 
                                                                                                            <img src={process.env.PUBLIC_URL+`/images/${reaction.name.toLowerCase()}.svg`}></img></button>
                                                        } 
                                                        else return <button className='message__allreactions_button' onClick={trySendReaction} value={reaction.id} 
                                                                                                                                id={"messageNav__allreactions_button"+el.id+reaction.id}>
                                                                                                            <img src={process.env.PUBLIC_URL+`/images/${reaction.name.toLowerCase()}.svg`}></img></button>
                                                    } 
                                                })}
                                            </div> */}
                                        </div>
                                        
                                        <button onClick={showNav} className='navShow' value={el.id}></button>
                                    </div>
                                    <div className={`messageNav`} id={"messageNav_"+el.id}>
                                        <button onClick={tryToUpdate} value={JSON.stringify(el)} className='messageNav__button'>Редактировать</button>
                                        <div className='messageNav__line'></div>
                                        <button onClick={tryToDelete} value={el.id} className='messageNav__button'>Удалить</button>
                                    </div>
                                </>
                        }
                        else {
                            return <div style={{display:'flex', alignItems:'center', marginBottom: '5px'}}>
                            <div style={{width:'100%'}}>
                                <MessageItem  key={el.id} dateSend={el.dateSend} message={el.msg} adress={el.userDTO} mesId={el.id}/>
                                <Reaction el={el} typeReacts={typeReacts} inout="inc"/>
                                {/* <div className='message__reactions inc'>
                                    <button className="message__reactions_button"  value={el.id} onClick={showReacts}><img className='message__reactions_button' src={reaction_button}/></button>
                                    {Array.from(typeReacts).map((elem)=><div className='message__reactions_reaction'> 
                                                        <img src={process.env.PUBLIC_URL+`/images/${elem.toLowerCase()}.svg`}></img>
                                                        {el.typeReactions.filter(val => val==elem).length}
                                                    </div>)}
                                </div>
                                <div className='messageNav__allreactions inc'  id={"messageNav__allreactions"+el.id}>
                                    {reacts.map((reaction)=>{
                                        if (reaction==false) return <div>Загрузка...</div>
                                        else {
                                            if (reaction.name==el.typeReaction) return <button className='message__allreactions_button reacted' 
                                                                                                            onClick={trySendReaction} value={reaction.id} 
                                                                                                                        id={"messageNav__allreactions_button"+el.id+reaction.id}> 
                                                                                                            <img src={process.env.PUBLIC_URL+`/images/${reaction.name.toLowerCase()}.svg`}></img></button>
                                                        else return <button className='message__allreactions_button' onClick={trySendReaction} value={reaction.id} 
                                                                                                                        id={"messageNav__allreactions_button"+el.id+reaction.id}>
                                                                                                            <img src={process.env.PUBLIC_URL+`/images/${reaction.name.toLowerCase()}.svg`}></img></button>
                                        } 
                                    })}
                                </div> */}
                            </div>
                            
                        </div>
                        }} 
                    })
                    
                    :<>Не удалось загрузить!</>}
                </div>
                <form className='send_button_in_chat'>
                    <textarea onChange={onMessageChange} className='textbox_for_msg' value={text} placeholder='Напишите сообщение' required/>
                    <input id="send_message_button" onClick={onSendMsg} className='button_for_send_msg' type='button' value='Отправить'/>
                    <input id="update_message_button"  onClick={onUpdateMsg} className='button_for_send_msg' style={{display: "none"}} type='button' value='Изменить'/>
                </form>
            </div>
            <SmallDialogItem/>
            {/* <div className='dialogs_item'>
                <div className="small-chats-header">
                    <p>Чаты</p>
                    <img src={plus}></img>
                </div>
                <div id='dialogs-scroll'>
                    {/* {dialogsElements} 
                </div>
            </div> */}
        </div>
    );
}

export default Chat;