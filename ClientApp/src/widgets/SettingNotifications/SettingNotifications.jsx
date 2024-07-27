import React, { useEffect, useState } from "react";
import './SettingNotification.css';
import { useDispatch, useSelector } from "react-redux";
import { useEditNotificationMutation } from "../../features/api/apiSlice";
import { useNavigate } from "react-router-dom";
import { setNotification } from "../../features/auth/userSlice";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

const SettingNotifications = () => {
    const notification = useSelector(state=>state.user.userInfo.settingNotificationId);
    console.log(notification);
    const [changeSettings, {error:ChangeSettingsError}] = useEditNotificationMutation();
    const [idNotification, setIdNotification] = useState(notification);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const Privacy = {
        Yes: 1,
        No: 0
    }
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
        if(ChangeSettingsError){
            console.log(ChangeSettingsError);
                if (ChangeSettingsError.response.status==401){
                    logOut();
                }
                else{
                    alert(ChangeSettingsError.message);
                }
        }else{
            dispatch(setNotification(idNotification));
        }
    }, [ChangeSettingsError])
    // let changeCheckBoxFriendNotification = () =>{
    //     props.updateFriendNotification();
    // }
    let changeCheckBoxMessageNotification = (arg) =>{
        dispatch(setNotification(arg));
        setIdNotification(arg);
    }
    // let changeCheckBoxPostNotification = () =>{
    //     props.updatePostNotification();
    // }
    return (
        <div className="containerSettingNotification">
            <p>Настройки уведомлений</p>
            <div>
                <div className="question">Уведомлять о новых сообщениях?</div>
                <div className="answers">
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInNewMessage'
                            checked={notification==Privacy.Yes ? true : false} 
                            onClick={() => {changeCheckBoxMessageNotification(Privacy.Yes)}}/>
                        <div>Да</div>
                    </div>
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInNewMessage'
                            checked={notification==Privacy.No ? true : false} 
                            onClick={() => {changeCheckBoxMessageNotification(Privacy.No)}}/>
                        <div>Нет</div>
                    </div>
                </div>
            </div>
            {/* <div>
                <div className="question">Уведомлять о новых запросах в друзья</div>
                <div className="answers">
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInFriend'
                            checked={props.friendNotif ? true : false} 
                            onClick={() => {changeCheckBoxFriendNotification()}}/>
                        <div>Да</div>
                    </div>
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInFriend'
                            checked={!props.friendNotif ? true : false} 
                            onClick={() => {changeCheckBoxFriendNotification()}}/>
                        <div>Нет</div>
                    </div>
                </div>
            </div>
            <div>
                <div className="question">Уведомлять о новых постах в группах</div>
                <div className="answers">
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInPost'
                            checked={props.postNotif ? true : false} 
                            onClick={() => {changeCheckBoxPostNotification()}}/>
                        <div>Да</div>
                    </div>
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInPost'
                            checked={!props.postNotif ? true : false} 
                            onClick={() => {changeCheckBoxPostNotification()}}/>
                        <div>Нет</div>
                    </div>
                </div>
            </div>   */}
        </div>
    );
}

export default SettingNotifications;