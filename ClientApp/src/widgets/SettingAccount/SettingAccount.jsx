import React from "react";

const SettingAccount = (props) => {
    let changeFirstName = (e) => {
        props.updateNewFirstName(e.target.value);
    }
    let changeLastName = (e) => {
        props.updateNewLastName(e.target.value);
    }
    let changeCountry = (e) => {
        props.updateNewCountry(e.target.value);
    }
    let changeCity = (e) => {
        props.updateNewCity(e.target.value);
    }
    let changeDataBithday = (e) => {
        props.updateNewDataBithday(e.target.value);
    }
    let changeStatus = (e) => {
        props.updateNewStatus(e.target.value);
    }
    let changeCountScale = (e) => {
        props.updateNewCountScale(e.target.value);
    }



    return (
        <div>
            <div>Редактировать имя пользователя</div>
            <input type='text' onChange={changeFirstName} value={props.firstName}></input>
            <div>Редактировать фамилию пользователя</div>
            <input type='text' onChange={changeLastName} value={props.lastName}></input>
            <div>Редактировать страну</div>
            <input type='text' onChange={changeCountry} value={props.country}></input>
            <div>Редактировать город</div>
            <input type='text' onChange={changeCity} value={props.city}></input>
            <div>Редактировать дату рождения</div>
            <input type='date' onChange={changeDataBithday} value={props.dataBithday}></input>
            <div>Редактировать статус</div>
            <input type='text' onChange={changeStatus} value={props.status}></input>
            <div>Масштабирование интерфейса</div>
            <input type='range' min='20' max='200'step='10' onChange={changeCountScale} value={props.countScale}></input>
        </div>
    );
}

export default SettingAccount;