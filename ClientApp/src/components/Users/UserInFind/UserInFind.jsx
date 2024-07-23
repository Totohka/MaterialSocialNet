import React, { useEffect, useState } from "react";
import './UserInFind.css';
import photoLogo from './../../../images/standartLogo.jpg';
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { NavLink, useNavigate } from "react-router-dom";

const UsersInFind = ({user}) => {
    const[subscribe, setSubscribe] = useState(user.isSubscriber);
    const [aClass, setAClass] = useState('none');
    const [bClass, setBClass] = useState('block');
    let token=localStorage.getItem("token");
    console.log(token);
    let us = jwtDecode(token.slice(7,));
    //console.log(us);
    
    const navigate = useNavigate();
    function ToSubscribe (id) {
        setSubscribe(true);
        try{axios.post("http://25.32.11.98:8086/api/Subscribe",{
                userId: us.id,
                userFriendId: user.id},
                {
                    headers:{
                    'Authorization':token
                }
            }
        )} catch(error){
            console.log(error);
            if (error.response.status==401){
                navigate("/login");
                alert('Время вашего сеанса истекло');
            }
        }
    }
    function ToUnsubscribe(id){
        setSubscribe(false);
        try{axios.delete("http://25.32.11.98:8086/api/Subscribe",{
                    headers:{
                    'Authorization':token
                },
                data:{
                    userId: us.id,
                    userFriendId: user.id
                }
            }
                
        )} catch(error){
            console.log(error);
            if (error.response.status==401){
                navigate("/login");
                alert('Время вашего сеанса истекло');
            }
        }
    }
    useEffect(()=>{
        if (subscribe){
            setAClass('none');
            setBClass('block');
        }
        else{
            setAClass('block');
            setBClass('none');
        }
    }, [subscribe])
    return (
        <div className="userContainer">
            <NavLink className="NavCont" to={"/user/"+user.id}>
                <div className="userImg">
                    <img src={"http://25.32.11.98:8086/Avatars/"+user.avatar}></img>
                </div>
                <div className="userName">{user.firstName+' '+user.lastName}</div>
            </NavLink>
            
            <div className="subscribeButton">
                <button id="toSub" style={{display: aClass}} onClick={()=>ToSubscribe(user.id)} className="unSubscrinbed">Подписаться</button>
                <button id="toUnSub" style={{display: bClass}} onClick={()=>ToUnsubscribe(user.id)} className="subscribed">Отписаться</button>
            </div>
        </div>
    )
}

export default UsersInFind;