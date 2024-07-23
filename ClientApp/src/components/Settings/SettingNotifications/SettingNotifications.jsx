import React from "react";
import './SettingNotification.css';

const SettingNotifications = (props) => {
    console.log(props);
    let changeCheckBoxFriendNotification = () =>{
        props.updateFriendNotification();
    }
    let changeCheckBoxMessageNotification = () =>{
        props.updateMessageNotification();
    }
    let changeCheckBoxPostNotification = () =>{
        props.updatePostNotification();
    }
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
                            checked={props.msgNotif ? true : false} 
                            onClick={() => {changeCheckBoxMessageNotification()}}/>
                        <div>Да</div>
                    </div>
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInNewMessage'
                            checked={!props.msgNotif ? true : false} 
                            onClick={() => {changeCheckBoxMessageNotification()}}/>
                        <div>Нет</div>
                    </div>
                </div>
            </div>
            <div>
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
            </div>  
        </div>
    );
}

export default SettingNotifications;