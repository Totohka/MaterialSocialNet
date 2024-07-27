import React, { useEffect } from "react";
import UsersInFind from "../../widgets/UserInFind/UserInFind";
import './Users.css';
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useLazyListUsersQuery, useListUsersQuery } from "../../features/api/apiSlice";
import { useSelector } from "react-redux";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

function Users(){
    // debugger;
    const [page, setPage] = useState(0);
    const [selection, setSelection] = useState(0);
    const [search, setSearch] = useState('');
    const [data, setData] = useState({page:page, selection:selection, search:search});
    const [uName, setUName] = useState('');
    const navigate = useNavigate();
    const us = useSelector(state=>state.user.userInfo);
    const { data: usersInfo,
        isLoading,
        isSuccess,
        isError,
        error } = useListUsersQuery(data);
    
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
                    if(usersInfo.status){
                        if(usersInfo.status==401){
                            logOut();
                        }
                    }
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
    const setSelected = (e) =>{
        setData({page:page, selection:e.target.value, search:search});
        setSelection(e.target.value);
        document.getElementById("select").classList.toggle('bordered')

    }
    

    const findUsers=(e)=>{
        setData({page:page, selection:selection, search:uName});
        setSearch(uName);
    }

    const downPage=()=>{
        setData({page:page-1, selection:selection, search:search});
        console.log(data);
        setPage(page-1);
    }
    const upPage=()=>{
        setData({page:page+1, selection:selection, search:search});
        console.log(data);
        setPage(page+1);
    }

    return (
        <div className="allUsers">
            <div className="usersSelect">
                <select id="select" onChange={setSelected}>
                    <option value="0">Все пользователи</option>
                    <option value="1">Ваши подписчики</option>
                    <option value="2">Друзья</option>
                </select>
            </div>
            <div className="findUser">
                <input className="findUser_input" onChange={(e)=>setUName(e.target.value)} value={uName} placeholder='Найти человека' type='text'/>
                <button className="findUser_button" onClick={findUsers}>Найти</button>
            </div>
            {isLoading?<></>:isSuccess?<div className="users_pages">
                {page+1>1?<button className="users_button" onClick={downPage}>&lt;</button>:<></>}
                {usersInfo.pageCount!=0?<div className="users_div">{page+1} из {usersInfo.pageCount}</div>:<></>}
                {page+1<usersInfo.pageCount?<button className="users_button" onClick={upPage}>&gt;</button>:<></>}
            </div>:<></>}
            
        {isLoading?<>Загрузка...</>:isSuccess?usersInfo.users==[]?<></>:
            usersInfo.users.map((el)=>{
                if (el===true) return <div>Подождите, пользователи загружаются...</div>
                if(el.id!=us.id){                   
                            return <UsersInFind key={el.id} user={el} />
                }
                else return <></>          
                        
        }):<></>}
            

            
        </div>
    )
}


export default Users;