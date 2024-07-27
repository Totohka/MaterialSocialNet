import React, { useEffect, useState } from "react";
import './SettingPrivacy.css';
import { useDispatch, useSelector } from "react-redux";
import { useEditPrivacyMutation } from "../../features/api/apiSlice";
import { setPrivacy } from "../../features/auth/userSlice";
import { useNavigate } from "react-router-dom";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

const SettingPrivacy = (props) => {
    const privacy = useSelector(state=>state.user.userInfo.settingPrivacyId);
    const [changeSettings, {error:ChangeSettingsError}] = useEditPrivacyMutation();
    const [idPrivacy, setIdPrivacy] = useState(privacy);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const Privacy = {
        All: 2,
        Friends: 1,
        Nobody: 0
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
            dispatch(setPrivacy(idPrivacy));
        }
    }, [ChangeSettingsError])
    // const changeRadioMyBirthDay = (idRadio) =>{
    //     props.updateShowMyDataBirthday(idRadio);
    // }
    const changeRadioInvateChat = (idRadio) =>{
        setIdPrivacy(idRadio);
        //changeSettings(idRadio);
        dispatch(setPrivacy(idRadio));
    }
    // const changeRadioShowPosts = (idRadio) =>{
    //     props.updateShowMyPost(idRadio);
    // }
    return (
        <div className="containerSettingPrivacy">
            <p>Настройки приватности</p>
            <div>
                <div className="question">Кто может приглашать меня в чаты?</div>
                <div className="answers">
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInvateInChat' 
                            checked={privacy == Privacy.All ? true : false} 
                            onClick={() => {changeRadioInvateChat(Privacy.All)}}/>
                        <div>Все</div>
                    </div>
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInvateInChat' 
                            checked={privacy == Privacy.Friends ? true : false} 
                            onClick={() => {changeRadioInvateChat(Privacy.Friends)}}/>
                            <div>Друзья</div>
                    </div>
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInvateInChat' 
                            checked={privacy == Privacy.Nobody ? true : false} 
                            onClick={() => {changeRadioInvateChat(Privacy.Nobody)}}/>
                            <div>Никто</div>
                    </div>
                </div>
            </div>
            {/* <div>
                <div className="question">Кто может видеть мои посты?</div>
                <div className="answers">
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInShowMyPosts' 
                            checked={props.showMyPost === Privacy.All ? true : false} 
                            onClick={() => {changeRadioShowPosts(Privacy.All)}}/>
                        <div>Все</div>
                    </div>
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInShowMyPosts' 
                            checked={props.showMyPost === Privacy.Friends ? true : false} 
                            onClick={() => {changeRadioShowPosts(Privacy.Friends)}}/>
                        <div>Друзья</div>
                    </div>
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInShowMyPosts' 
                            checked={props.showMyPost === Privacy.Nobody ? true : false} 
                            onClick={() => {changeRadioShowPosts(Privacy.Nobody)}}/>
                        <div>Никто</div>
                    </div>
                    <div></div>
                </div>
            </div>
            <div>
                <div className="question">Кто может видеть дату моего рождения?</div>
                <div className="answers">
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInShowBirthday' 
                            checked={props.showMyDataBirthday === Privacy.All ? true : false} 
                            onClick={() => {changeRadioMyBirthDay(Privacy.All)}}/>
                        <div>Все</div>
                    </div>
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInShowBirthday' 
                            checked={props.showMyDataBirthday === Privacy.Friends ? true : false} 
                            onClick={() => {changeRadioMyBirthDay(Privacy.Friends)}}/>
                        <div>Друзья</div>
                    </div>
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInShowBirthday'
                            checked={props.showMyDataBirthday === Privacy.Nobody ? true : false} 
                            onClick={() => {changeRadioMyBirthDay(Privacy.Nobody)}}/>
                        <div>Никто</div>
                    </div>
                </div> 
            </div>   */}         
        </div> 
    );
}

export default SettingPrivacy;