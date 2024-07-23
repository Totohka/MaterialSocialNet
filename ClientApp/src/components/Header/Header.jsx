import React, {useEffect, useState} from 'react';
import logo from './../../images/logo.png';
import profile from './../../images/profile.svg';
import mail from './../../images/mail.svg';
import ring from './../../images/ring.svg';
import logout from './../../images/logout.svg';
import './Header.css';
import Userfront, { LogoutButton } from "@userfront/toolkit/react";
import { NavLink, useLocation } from 'react-router-dom';
import Popup from 'reactjs-popup';

const Header = () => {
    // console.log(Userfront.user);
    const [head, setHead] = useState(<header></header>);
    const location = useLocation();
    useEffect(()=>{
        if ( window.location.pathname=='/login' || window.location.pathname=='/' || window.location.pathname=='/registration'){
            setHead(<header></header>);
        }
        else setHead(
                <header className='header'>
                    <p>Мы здесь для Вас. SocialNet</p>
                    <img id="logo" src={logo} />
                    <div id="icons">
                        <NavLink to='/profile'><img src={profile} /></NavLink>
                        <NavLink to='/dialogs'><img src={mail} /></NavLink>
                        <Popup trigger={<img src={ring} />}>
                            <div className='popup_notification'>
                                <p>Уведомления</p>
                                <div>Новых уведомлений нет</div>
                            </div>
                        </Popup>
                        <NavLink to='/'><img src={logout} /></NavLink>
                    </div>
                </header>
            );
    },[location.pathname])
    return (
        <>{head}</>
      );
    
}

export default Header;