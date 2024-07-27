import {React, useCallback, useEffect, useState} from 'react';
import './Registration.css';
import logo from "./../../images/logo.png"
import { NavLink, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import { useTrySignUpMutation } from '../../features/api/apiSlice';
import { useDispatch } from 'react-redux';
import { setUser } from '../../features/auth/userSlice';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { connectionNotif } from '../../app/helpers/signalRNotifications';

const Registration = () => {

    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [pass, setPass] = useState("");
    const [dateB, setDateB] = useState(Date);
    const [signUp, {data, error}] = useTrySignUpMutation();
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleSubmit = async (e) => {
      e.preventDefault();
      await signUp({fName:firstName, lName:lastName, email, pass, dateB});
    }
    const startConnect = useCallback((user)=>{
      if (connection.state !== HubConnectionState.Connected) {
          connection
            .start()
            .then(() => console.log("Connection started"))
            .catch((err) => console.error(err.toString()));
        }
      if (connectionNotif.state !== HubConnectionState.Connected) {
        connectionNotif
          .start()
          .then(() => {console.log("Connection started");
            connectionNotif.invoke('OnConnectedSendNotifications', String(user.id)).then(()=>console.log('Notififcations get'))
            .catch(error=>console.log(error));
          })
          .catch((err) => console.error(err.toString()));
      }
    },[])
    useEffect(()=>{
      if (data){
        if(data!="Email занят!"){
          if(data!=401){
            const token = data;
            connection.baseUrl=`http://25.32.11.98:8089/chat?t=${token}`;
            connectionNotif.baseUrl=`http://25.32.11.98:8090/notifications?t=${token}`
            const user = jwtDecode(token);
            localStorage.setItem("token",`Bearer ${token}`);

            dispatch(setUser(token));
            startConnect(user);
            navigate("/profile");
            
          }else{
            alert('Неправильное имя пользователя или пароль!');
          }
        }else{
          alert(data);
        }
      }
      if(error){
        alert(error);
      }
    },[data, error]);
    
    const handleChangeFirstName = (e) => {
      setFirstName(e.target.value);
    }
    const handleChangeLastName = (e) => {
      setLastName(e.target.value);
    }
    const handleChangeEmail = (e) => {
      setEmail(e.target.value);
    }
    const handleChangePass = (e) => {
      setPass(e.target.value);
    }
    const handleChangeDateB = (e) => {
      setDateB(e.target.value);
    }

    return (
      <div className="registration">
        <img src={logo}></img>
        <form className="form_registration" onSubmit={handleSubmit}>
        <div className="title_registration">Добро пожаловать в SocialNet!</div>
        <div className="input-container ic1">
          <input
            id="firstname"
            className="input"
            name="firstName"
            type="text"
            value={firstName}
            onChange={handleChangeFirstName}
            placeholder="Введите имя..."
          />
        </div>
        <div className="input-container ic2">
          <input
            id="lastname"
            className="input"
            name="lastName"
            type="text"
            value={lastName}
            onChange={handleChangeLastName}
            placeholder="Введите фамилию..."
          />
        </div>
        <div className="input-container ic2">
          <input
            id="email"
            className="input"
            name="email"
            type="email"
            value={email}
            onChange={handleChangeEmail}
            placeholder="Введите email..."
          />
        </div>
        <div className="input-container ic2">
          <input
            id="password"
            className="input"
            name="password"
            type="password"
            value={pass}
            onChange={handleChangePass}
            placeholder="Введите пароль..."
          />
        </div>
        <div className="input-container ic2">
          <input
            id="birthday"
            className="input"
            name="birthday"
            type="date"
            value={dateB}
            onChange={handleChangeDateB}
            placeholder="Введите дату рождения..."
          />
        </div>
        <button type="submit" className="submit_registration">
          Зарегистрироваться
        </button>
        <NavLink to='/login'>У вас уже есть аккаунт?</NavLink>
      </form>
      </div>
    );
}

export default Registration;