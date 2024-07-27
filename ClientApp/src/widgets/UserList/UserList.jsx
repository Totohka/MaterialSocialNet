import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

import { useAddChatMutation, useGetChatsQuery, useInviteUserMutation, useLazyListQuery, useListUsersQuery } from "../../features/api/apiSlice";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

const UserList = ({close, bigPage, bigSearch}) =>{
    const [addChat, {data:ChatId, error:AddChatError}] = useAddChatMutation();
    const [cName, setCName]=useState("");
    const [search, setSearch] = useState("");
    const [fName, setFName]=useState("");
    const [chatName, setChatName]=useState("");
    const [desc, setDesc]=useState("");
    const [page, setPage] = useState(0);
    const [chatUs, setChatUs]=useState([]);
    const [data, setData] = useState({page:page, search:fName, selection:0});
    const navigate = useNavigate();


    const { data: usersInfo,
        isLoading,
        isSuccess,
        isError,
        error } = useListUsersQuery(data);

    const id = useSelector(state=>state.user.userInfo.id);

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
        console.log('Try');
        if(!isLoading){
            if(isSuccess){
                console.log(usersInfo);
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
        }else if(AddChatError)
            {
                console.log(AddChatError);
                if (AddChatError.status){
                    if(AddChatError.status==401){
                        logOut();
                    }
                }else if (AddChatError.response.status==401){
                    navigate('/login');
                }
                else{
                    alert(AddChatError.message);
                }
            } 
        if(ChatId){
            connection.invoke("OnConnectedChatAsync", (String(ChatId)))
            .then((data) => console.log("chat Connection started", data))
            .catch((err) => console.error(err.toString()));
            close();
        }
    },[isLoading, isSuccess, isError, AddChatError, ChatId])

    const downPage=()=>{
        setData({page:page-1, search:fName, selection:0});
        console.log(data);
        setPage(page-1);
    }
    const upPage=()=>{
        setData({page:page+1, search:fName, selection:0});
        console.log(data);
        setPage(page+1);
    }
    const resetNew=()=>{
        setDesc("");
        setChatName("");
        setChatUs([]);
        setFName("");
    }
    const tryAddToChat=(e)=>{
        if (e.target.classList.contains("added")) {
            console.log("filtred ", [...chatUs.filter((el)=>el!=e.target.value)]);

            setChatUs([...chatUs.filter((el)=>el!=e.target.value)]);
        } else{
            setChatUs([...chatUs, Number(e.target.value)]);
        }
        console.log(chatUs);
        e.target.classList.toggle("added");
    }
    const createChat = async() =>{
        let data = {
            name:chatName,
            description:desc,
            lastMessage:"",
            userIdsByChat:chatUs
        };
        addChat({data:data, id:id, page:bigPage, search:bigSearch});
    }
    const find = () =>{
        setData({page:0, search:fName, selection:0});
        setPage(0);
    }
    
    return(
        <div className="newchat__form">
                        <input type="text" onChange={(e)=>setChatName(e.target.value)} className="newchat__form_input" placeholder="Введите название для чата..." value={chatName}></input>
                        <input type="text" onChange={(e)=>setDesc(e.target.value)} className="newchat__form_input" placeholder="Введите описание чата..." value={desc}></input>
                        <div className="newchat__form_find">
                            <input type="text" onChange={(e)=>setFName(e.target.value)} className="newchat__form_find_input" placeholder="Введите имя собеседника..." value={fName}></input>
                            <button className="newchat__form_find_button" onClick={find}>Найти</button>
                        </div>
                        {isLoading?<></>:isSuccess?<div className="chats_pages">
                            {page+1>1?<button className="chats_pages_button" onClick={downPage}>&lt;</button>:<></>}
                            {usersInfo.pageCount!=0?<div className="chats_pages_div">{page+1} из {usersInfo.pageCount}</div>:<></>}
                            {page+1<usersInfo.pageCount?<button className="chats_pages_button" onClick={upPage}>&gt;</button>:<></>}
                        </div>:<></>}
                        <div id="newchat__form_friends" className="newchat__form_friends">
                            {isLoading?<div>Подождите, данные загружаются...</div>:isSuccess?usersInfo.users==[]?<></>:usersInfo.users.map((el)=>{
                                return <button onClick={tryAddToChat} className="newchat__form_friend" key={el.id} value={el.id}>
                                                    <div className="newchat__form_friend_img">
                                                        <img src={"http://25.32.11.98:8086/Avatars/"+el.avatar}></img>
                                                    </div>
                                                    <div className="newchat__form_friend_name">{el.firstName+' '+el.lastName}</div>
                                                </button>
                                
                            }):<></>}
                        </div>
                        <button className="newchat__form_button" onClick={() => {
              createChat();
            //   close();
              resetNew();
            }}>Создать чат</button>
            </div>
            
    );
}
export default UserList;