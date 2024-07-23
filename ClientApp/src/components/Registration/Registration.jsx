import {React, useState} from 'react';
import './Registration.css';
import logo from "./../../images/logo.png"
import { NavLink, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

const Registration = (props) => {

    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [pass, setPass] = useState("");
    const [dateB, setDateB] = useState(Date);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
      e.preventDefault();
      debugger;
      await axios.post("http://25.32.11.98:8085/Auth",{
        id: 0,
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: pass,
        dateBirthday: dateB
      }, {
        headers: {
          'Content-Type': 'application/json'
        }
      }).then( res => {
        const token = res.data
        props.setAccessTokenUser("Bearer " + token);  
        const user = jwtDecode(token);
        props.setAccoutSetting(user);
      } );
      navigate("/profile");
    }

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