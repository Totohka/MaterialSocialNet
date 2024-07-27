import React, { useEffect } from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import bin from './../../images/bin.svg';
import "./DialogsItem.css";
import { useDeleteChatMutation } from '../../features/api/apiSlice';
import Popup from 'reactjs-popup';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { connectionNotif } from '../../app/helpers/signalRNotifications';
import { useSelector } from 'react-redux';
import logo from './../../images/logo.jpg';

const DialogItem = ({chat, args}) => {
    const [deleteChat, {error:DeleteChatError}] = useDeleteChatMutation();
    
    const navigate = useNavigate();
    const user = useSelector(state=>state.user.userInfo);
    const logOut = () =>{
        localStorage.clear();
        if (connection.state !== HubConnectionState.Disconnected) {
            connection.stop()
              .then(() => console.log("Connection stoped"))
              .catch((err) => console.error(err.toString()));
          }
          
          if (connectionNotif.state !== HubConnectionState.Disconnected) {
            connectionNotif.invoke("OnDisconnectedSendNotifications", String(user.id)).then(()=>{console.log('Disconnected');
                connectionNotif.stop()
              .then(() => console.log("Connection stoped"))
              .catch((err) => console.error(err.toString()));
            }).catch(error=>console.log(error));
            
          }
        navigate('/');
    }
    useEffect(()=>{
        if(DeleteChatError)
        {
            console.log(DeleteChatError);
            if (DeleteChatError.status){
                if(DeleteChatError.status==401){
                    logOut();
                }
            }else if (DeleteChatError.response.status==401){
                navigate('/login');
            }
            else{
                alert(DeleteChatError.message);
            }
        } else {

        }
                
    },[DeleteChatError]);
    // let path = '/dialogs/' + chat.id;
    return (
        <>
        <div className='line'></div>
        <div className='flex-chatItem'>
            <NavLink to={'/dialogs/' + chat.id}>
                <div className='item_chat'>
                    <div className='item_chat_img' style={{backgroundImage: `url(${logo})`}}></div>
                    <div className='item_chat_textBlock'>
                        <p className='item_chat_name'>{chat.name}</p> 
                        <div className='item_chat_message'>{chat.lastMessage}</div> 
                    </div>
                    
                </div>    
            </NavLink>
            <Popup trigger={<img src={bin}></img>} modal>          
                {close => (<div className="tryDeletePost">
                        <p>Вы уверены, что хотите удалить данный чат?</p>
                        <div>
                            <button onClick={()=>{deleteChat({chatId:chat.id, ...args}); close();}}>Да</button>
                            <button onClick={() => {
                                console.log('modal closed ');
                                close();
                                }}>Нет</button>
                        </div>
                </div>)}
            </Popup>
            
        </div>
        
        </>
    );
}

export default DialogItem;