import { useEffect, useState } from "react";
import DialogItem from "../../widgets/DialogItem/DialogsItem";
import plus from "./../../images/plus.svg"
import "./Chats.css"
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import Popup from "reactjs-popup";
import { useAddChatMutation, useGetChatsQuery, useInviteUserMutation, useListUsersQuery } from "../../features/api/apiSlice";
import { useDispatch, useSelector } from "react-redux";
import UserList from "../../widgets/UserList/UserList";
import { setNonReadedChats } from "../../features/chats/chatsConnectionsSlice";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

function Chats(){
    const [cName, setCName]=useState("");
    const [search, setSearch] = useState("");
    const [page, setPage] = useState(0);
    const [data, setData] = useState({page:page, search:search});

    const nonReaden = useSelector(state=>state.chats.nonReaden);
    const dispatch = useDispatch();

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
        if(nonReaden){
            dispatch(setNonReadedChats(false));
            refetch();
            // setData({page:page, search:search});
        }
        
        
    },[isLoading, isSuccess, isError, nonReaden
    ]);

    const findChats=()=>{
        setData({page:page, search:cName});
        setSearch(cName);
    }
   
    
    
    
    
    
    const downPage=()=>{
        setData({page:page-1, search:search});
        console.log(data);
        setPage(page-1);
    }
    const upPage=()=>{
        setData({page:page+1, search:search});
        console.log(data);
        setPage(page+1);
    }
    
    return(
        <div className="chats">
            <div className="chats-header">
                <p>Чаты</p>
                <Popup trigger={<img src={plus} ></img>}>
                    {close => (
                        <UserList close={close} bigPage={page} bigSearch={search}/>
                    )}
                    
                </Popup>
            </div>
            <div className="findChat">
                <input className="findChat_input" placeholder="Введите название чата..." value={cName} onChange={(e)=>setCName(e.target.value)}></input>
                <button className="findChat_button" onClick={findChats}>Найти</button>
            </div>
            {/* <div className="double-line"></div> */}
            {isLoading?<></>:isSuccess?<div className="chats_pages">
                {page+1>1?<button className="chats_pages_button" onClick={downPage}>&lt;</button>:<></>}
                {chatsInfo.pageCount!=0?<div className="chats_pages_div">{page+1} из {chatsInfo.pageCount}</div>:<></>}
                {page+1<chatsInfo.pageCount?<button className="chats_pages_button" onClick={upPage}>&gt;</button>:<></>}
            </div>:<></>}
            <div id="diItems" className="diItems">
                {isLoading?<div>Подождите, данные загружаются...</div>:isSuccess?chatsInfo.chatRooms==[]?<></>:chatsInfo.chatRooms.map((el)=>{
                    return <DialogItem key={el.id} chat={el} args={data}/>
                 }):<></>}
            </div>
        </div>
    )
}
export default Chats;