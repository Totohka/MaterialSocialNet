import React, {useEffect, useState} from 'react';
import logo from './../../images/logo.png';
import profile from './../../images/profile.svg';
import mail from './../../images/mail.svg';
import ring from './../../images/ring.svg';
import logout from './../../images/logout.svg';
import ringAlarm from './../../images/ringAlarm.svg'
import './Header.css';
import Userfront, { LogoutButton } from "@userfront/toolkit/react";
import { NavLink, useLocation, useNavigate } from 'react-router-dom';
import Popup from 'reactjs-popup';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { useDispatch, useSelector } from 'react-redux';
import { connectionNotif } from '../../app/helpers/signalRNotifications';
import { deleteNotification } from '../../features/notifications/notificatiosSlice';
import Notifications from '../Notifications/Notifications';

const Header = () => {
    // console.log(Userfront.user);
    const [head, setHead] = useState(<header></header>);
    const user = useSelector(state=>state.user.userInfo);
    const notifications = useSelector(state=>state.notifications.notifications);
    const location = useLocation();
    const navigate = useNavigate();
    const dispatch = useDispatch();
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
        if ( window.location.pathname=='/login' || window.location.pathname=='/' || window.location.pathname=='/registration'){
            setHead(<header></header>);
        }
        else setHead(
                <header className='header'>
                    <p>Мы здесь для Вас. SocialNet</p>
                    <img id="logo" src={logo} />
                    <div id="icons">
                        <NavLink to='/profile'><img src={profile} /></NavLink>
                        <NavLink to='/chats'><img src={mail} /></NavLink>
                        <Popup trigger={notifications.length==0?<img src={ring} />:<img src={ringAlarm}/>}>
                            {close=>(<Notifications close={close}/>)}
                        </Popup>
                        <img onClick={logOut} src={logout} />
                    </div>
                </header>
            );
    },[location.pathname])
    return (
        <>{head}</>
      );
    
}

export default Header;