import React from "react";
import './SettingPrivacy.css';

const SettingPrivacy = (props) => {
    const Privacy = {
        All: 2,
        Friends: 1,
        Nobody: 0
    }
    let changeRadioMyBirthDay = (idRadio) =>{
        props.updateShowMyDataBirthday(idRadio);
    }
    let changeRadioInvateChat = (idRadio) =>{
        props.updateInvateInChat(idRadio);
    }
    let changeRadioShowPosts = (idRadio) =>{
        props.updateShowMyPost(idRadio);
    }
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
                            checked={props.invateInChat === Privacy.All ? true : false} 
                            onClick={() => {changeRadioInvateChat(Privacy.All)}}/>
                        <div>Все</div>
                    </div>
                    <div className="answer">
                        <input 
                            type='radio' 
                            name='radioInvateInChat' 
                            checked={props.invateInChat === Privacy.Friends ? true : false} 
                            onClick={() => {changeRadioInvateChat(Privacy.Friends)}}/>
                            <div>Друзья</div>
                    </div>
                    <div className="answer">
                        
                        <input 
                            type='radio' 
                            name='radioInvateInChat' 
                            checked={props.invateInChat === Privacy.Nobody ? true : false} 
                            onClick={() => {changeRadioInvateChat(Privacy.Nobody)}}/>
                            <div>Никто</div>
                    </div>
                </div>
            </div>
            <div>
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
            </div>            
        </div> 
    );
}

export default SettingPrivacy;