import React, { useEffect, useState } from 'react';
import './NavBar.css';
import { NavLink, useLocation } from 'react-router-dom';
import news from './../../images/news.svg';
import mail from './../../images/mail.svg';
import users from './../../images/users.svg';
import settings from './../../images/settings.svg';
import dashboard from './../../images/dashboard.svg';
import profile from './../../images/profile.svg';

const NavBar = () => {
  const [nav, setNav] = useState(<nav></nav>);
  const location = useLocation();
  useEffect(() => {
    if ( location.pathname=='/login' || location.pathname=='/' || location.pathname=='/registration'){
      setNav(<nav></nav>)
    }
    else{
     setNav(<nav className="nav">
            <NavLink className='navButton' to='/profile'>
              <div className='navPosition'>
                <img src={profile}></img> 
                <p>Профиль</p>
              </div>
            </NavLink>
            <NavLink className='navButton' to='/news'>
              <div className='navPosition'>
                <img src={news}></img> 
                <p>Посты</p>
              </div>
            </NavLink>
            <NavLink className='navButton' to='/chats'>
              <div className='navPosition'>
                <img src={mail}></img>
                <p>Чаты</p>
              </div>
            </NavLink>
            <NavLink className='navButton' to='/users'>
              <div className='navPosition'>
                <img src={users}></img>
                <p>Пользователи</p>
              </div>
            </NavLink>
            <NavLink className='navButton' to='/settings'>
              <div className='navPosition'>
                <img src={settings}></img>
                <p>Настройки</p>
              </div>
            </NavLink>
            <NavLink className='navButton'to='/dashboard'>
              <div className='navPosition'> 
                <img src={dashboard}></img>
                <p>Статистика</p>
              </div>
            </NavLink>
          </nav>)
    }
  },[location.pathname])

 return (
  <>{nav}</>
);
    
}

export default NavBar;