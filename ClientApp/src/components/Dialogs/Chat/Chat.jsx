import React, { useEffect, useState } from 'react';
import './Chat.css';
import MessageItem from './MessegeItem/MessageItem';
import defImg from './../../../images/logo.jpg';
import SmallDialogItem from './SmallDialogItem/SmallDialogItem';
import plus from "./../../../images/plus.svg"
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import reaction_button from './../../../images/reaction_button.svg';
import Popup from 'reactjs-popup';

const Chat = ({chat}) => {
    let param = useParams();
    let id = Number(param['*']);
    const [mes, setMes] = useState();
    const token = localStorage.getItem('token');
    const us = jwtDecode(token.slice(7,));
    const [messages, setMessages] = useState([true]);
    const [pageCount, setPageCount] = useState(0);
    const [numberPage, setNumberPage] = useState(0);
    const navigateU = useNavigate();
    const [text, setText] = useState();
    const [showId, setShowId] = useState(0);
    const [showReactId, setShowReactId] = useState(0);

    const [reactId, setReactId] = useState(0);

    const [chatInfo, setChatInfo] = useState(null);
    const navigate = useNavigate();
    const [reacts, setReacts] = useState([false]);
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
    const getMessages = async(x=-1) =>{
        if(x==-1){
            x=numberPage;
        }
        console.log(numberPage);
        try{
            const result = await axios.get("http://25.32.11.98:8089/api/Message/All",{
                params:{
                    chatId: id,
                    number: x
                },
                headers:{
                    'Authorization':token
                }
            });
            console.log(result.data);
            setPageCount(result.data.pageCount);
            if(x!=0){
                setMessages(messages=>[...result.data.messages,...messages]);
            } else {setMessages(result.data.messages);
            let div = document.getElementById('messages_in_chat');
            div.scrollTop = div.scrollHeight - div.clientHeight;}
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
    const sendMessage = async() =>{
        try{
            const result = await axios.post("http://25.32.11.98:8089/api/Message",{
                msg: text,
                dateSend: new Date().toJSON(),
                chatRoomId: id,
                userId:us.id
            },{
                
                headers:{
                    'Authorization':token
                }
            });
            
            setText('');
            getMessages();
            let div = document.getElementById('messages_in_chat');
            div.scrollTop = div.scrollHeight - div.clientHeight;
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
    const updateMessage = async() =>{
        try{
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
            getMessages();
            let div = document.getElementById('messages_in_chat');
            div.scrollTop = div.scrollHeight - div.clientHeight;
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
    const deleteMessage = async(el) =>{
        try{
            const result = await axios.delete("http://25.32.11.98:8089/api/Message",{
                params:{
                    messageId:el,
                },
                headers:{
                    'Authorization':token
                }
            });
            getMessages();
            setText('');
            let div = document.getElementById('messages_in_chat');
            div.scrollTop = div.scrollHeight - div.clientHeight;
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
    useEffect(()=>{
        getChatInfo();
        getMessages();
        getAllReactions();
    },[])
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
    const showNav = (e) =>{
        if(document.getElementById("messageNav_"+e.target.value).style.display=="block"){
            document.getElementById("messageNav_"+e.target.value).style.display="none";
            document.getElementById("update_message_button").style.display="none";
            document.getElementById("send_message_button").style.display="block";
            setText('');
            setShowId(0);
        }else{
            if(showId!=0){
                document.getElementById("messageNav_"+showId).style.display="none";
            }
            setShowId(e.target.value);
            document.getElementById("messageNav_"+e.target.value).style.display="block";
            document.getElementById("update_message_button").style.display="none";
            document.getElementById("send_message_button").style.display="block";
        }
        

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
        setNumberPage(numberPage+1);
        getMessages(numberPage+1);
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
                <div className='chat_header'>
                    <div className="chat_img" style={{backgroundImage: `url(${defImg})`}}></div>
                    <div className='online'>
                        <div></div>
                        <p>online</p>
                    </div>
                    <p className='interlocutor'>{chatInfo?chatInfo.name:''}</p>
                </div>
                
                <div id="messages_in_chat" className='messages_in_chat'>
                    {numberPage==pageCount-1?<></>:<div className='showPrev'>
                                                        <button className="showPrev__button" onClick={showPrev}>Показать предыдущие сообщения</button>
                                                    </div>}
                    {messages==null?<div>Пока нет сообщений</div>:messages.map((el)=>{
                        if (el===true) return <div>Пока нет сообщений</div>
                        else {
                            let typeReacts = new Set(el.typeReactions);
                            if (el.userDTO.id==us.id) {
                            
                            return <>
                                    
                                    <div style={{display:'flex'}}>
                                        <div style={{width:'100%'}}>
                                            
                                            <MessageItem  key={el.id} dateSend={el.dateSend} message={el.msg} adress={el.userDTO} mesId={el.id}/>
                                            <div className='message__reactions out'>
                                                <button className="message__reactions_button" value={el.id} onClick={showReacts}><img className='message__reactions_button' src={reaction_button}/></button>
                                                {Array.from(typeReacts).map((elem)=><div className='message__reactions_reaction'> 
                                                    <img src={process.env.PUBLIC_URL+`/images/${elem.toLowerCase()}.svg`}></img>
                                                    {el.typeReactions.filter(val => val==elem).length}
                                                 </div>)}
                                            </div>
                                            <div className='messageNav__allreactions out' id={"messageNav__allreactions"+el.id}>
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
                                            </div>
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
                            return <div style={{display:'flex', alignItems:'center'}}>
                            <div style={{width:'100%'}}>
                                <MessageItem  key={el.id} dateSend={el.dateSend} message={el.msg} adress={el.userDTO} mesId={el.id}/>
                                <div className='message__reactions inc'>
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
                                </div>
                            </div>
                            
                        </div>
                        }} 
                    })}
                </div>
                <form className='send_button_in_chat'>
                    <textarea onChange={onMessageChange} className='textbox_for_msg' value={text} placeholder='Напишите сообщение' required/>
                    <input id="send_message_button" onClick={onSendMsg} className='button_for_send_msg' type='button' value='Отправить'/>
                    <input id="update_message_button"  onClick={onUpdateMsg} className='button_for_send_msg' style={{display: "none"}} type='button' value='Изменить'/>
                </form>
            </div>
            <div className='dialogs_item'>
                <div className="small-chats-header">
                    <p>Чаты</p>
                    <img src={plus}></img>
                </div>
                <div id='dialogs-scroll'>
                    {/* {dialogsElements} */}
                </div>
            </div>
        </div>
    );
}

export default Chat;