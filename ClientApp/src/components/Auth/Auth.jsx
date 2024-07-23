import { React, useState } from 'react';
import './Auth.css';
import logo from "./../../images/logo.png";
import { NavLink, redirect, useNavigate } from "react-router-dom";
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

const Auth = (props) => {

    const [email, setEmail] = useState("");
    const [pass, setPass] = useState("");
    const navigate = useNavigate();

    const changeEmail = (e) => {
      setEmail(e.target.value);
    }

    const changePass = (e) => {
      setPass(e.target.value);
    }

    const handleSubmit = async (e) => {
      e.preventDefault();
      await axios.get('http://25.32.11.98:8085/Auth', { params: { email: email, password: pass}}).then(
        res => {
          if (res.data!=401){
            const token = res.data
          props.setAccessTokenUser("Bearer " + token);  
          const user = jwtDecode(token);
          props.setAccoutSetting(user);
          navigate("/profile");
          }else
          alert('Неправильное имя пользователя или пароль!');
        }
      )
      
    }

    return (
      <div className="auth">
        <img src={logo}></img>
        <div className="form_auth">
        <form onSubmit={handleSubmit}>
        <div className="title_auth">С возвращением в SocialNet!</div>
          <div className="input-container ic1">
            <input id="email" className="input" 
                  name="email"
                  type="text"
                  value={email}
                  onChange={changeEmail} 
            placeholder="Введите email..." />
          </div>
          <div className="input-container ic2">
            <input id="password" className="input" 
                  name="password"
                  type="password"
                  value={pass}
                  onChange={changePass} 
            placeholder="Введите пароль..." />
          </div>
          <button className="submit" type="submit">Войти</button>
          <NavLink to='/registration'>Вы здесь впервые?</NavLink>
          <NavLink to='/'>Забыли пароль?</NavLink>
        </form>
      </div>
      </div>
    );
}

export default Auth;