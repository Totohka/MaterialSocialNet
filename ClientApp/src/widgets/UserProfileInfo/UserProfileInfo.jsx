import React from "react";
import "./UserProfileInfo.css";
import {useEffect, useState} from 'react';
import { useNavigate, useParams } from "react-router-dom";
import Info from "./Info";
import { useGetUserQuery } from "../../features/api/apiSlice";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";
import { useSelector } from "react-redux";

const UserProfileInfo = () => {

  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [it, setIt] = useState([]);
  const param = useParams();
  const id = Number(param['*']); 
  const { data: info,
    isLoading,
    isSuccess,
    isError,
    error } = useGetUserQuery(id);

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
    
    {isLoading?<div>Контент загружается...</div>:isSuccess?<Info it={info}></Info>:<></>}
    </>
  );
};

export default UserProfileInfo;
