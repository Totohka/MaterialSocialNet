import { NavLink, useNavigate } from "react-router-dom";
import plus from "./../../../images/plus.svg"
import './SmallDialogItem.css';
import { useDispatch, useSelector } from "react-redux";
import { useGetChatsQuery } from "../../../features/api/apiSlice";
import { connectionNotif } from "../../../app/helpers/signalRNotifications";
import { connection } from "../../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { useEffect, useState } from "react";
import logo from './../../../images/logo.jpg'
import Popup from "reactjs-popup";
import UserList from "../../../widgets/UserList/UserList";
const SmallDialogItem = (props) => {
    // let path = '/dialogs/' + props.id;
    const dispatch = useDispatch();
    const [page, setPage] = useState(0);
    const [data, setData] = useState({page:page, search:''});
    const navigate = useNavigate();
    const { data: chatsInfo,
        isLoading,
        isSuccess,
        isError,
        error, refetch } = useGetChatsQuery(data);


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
        if(!isLoading){
            if(isSuccess){
            console.log(chatsInfo);
            }
            if(isError){
                console.log(error);
                if (error.status==401){
                    logOut();
                }
                else{
                    alert(error.message);
                }
            } 
            }   
        
        
    },[isLoading, isSuccess, isError]);
    return (
        <>
        <div className='dialogs_item'>
                <div className="small-chats-header">
                    <p>Чаты</p>
                    <Popup trigger={<img src={plus} ></img>}>
                        {close => (
                            <UserList close={close} bigPage={0} bigSearch={''}/>
                        )}
                    </Popup>
                </div>
                <div id='dialogs-scroll'>
                    {
                        isLoading?<>Загрузка...</>:isSuccess?chatsInfo.chatRooms.map((el)=>{
                            return(
                                <div className='small-item-chat'>
                                    <NavLink to={'/dialogs/' + el.id}>
                                            <div className='small-item-chat-img' style={{backgroundImage: `url(${logo})`}}></div>
                                            <p>{el.name}</p> 
                                    </NavLink>
                                </div> 
                            );
                        }):<></>
                    }
                    
                </div>
            </div>
        
        </>
    );
}

export default SmallDialogItem;