import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useEditUserMutation } from "../../features/api/apiSlice";
import { setUser } from "../../features/auth/userSlice";
import location from "./../../images/location.svg";
import edit from "./../../images/edit.svg";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

const tryEditDescription = () =>{
    let firstname_acc = document.getElementById('firstname_acc');
    let def_name=document.getElementById('default_name');
    let lastname_acc = document.getElementById('lastname_acc');
    let country_acc = document.getElementById('country_acc');
    let city_acc = document.getElementById('city_acc');
    let birthday_acc = document.getElementById('birthday_acc');
    let button = document.getElementById('save_description');
    
    if ( button.style.display != "block" ){
      lastname_acc.style.display = "block";
      firstname_acc.style.display = "block";
      button.style.display = "block";
      def_name.style.display = "none";
      document.getElementById('default_country').style.display = "none";
      document.getElementById('default_city').style.display = "none";
      document.getElementById('default_birth').style.display = "none";
  
      country_acc.removeAttribute("disabled");
      city_acc.removeAttribute("disabled");
      birthday_acc.removeAttribute("disabled");
  
      country_acc.style.display = "block";
      country_acc.style.border = "1px solid white";
      country_acc.style.borderRadius = "5px";
  
      birthday_acc.style.display = "block";
      birthday_acc.style.border = "1px solid white";
      birthday_acc.style.borderRadius = "5px";
  
      city_acc.style.display = "block";
      city_acc.style.border = "1px solid white";
      city_acc.style.borderRadius = "5px";
      
      document.getElementById("divB").style.marginTop = "10px";
    }
    else{
      lastname_acc.style.display = "none";
      firstname_acc.style.display = "none";
      button.style.display = "none";
      country_acc.style.display = "none";
      city_acc.style.display = "none";
      birthday_acc.style.display = "none";
      def_name.style.display = "flex";
      def_name.style.flexDirection = "row";
      document.getElementById('default_country').style.display = "block";
      document.getElementById('default_city').style.display = "block";
      document.getElementById('default_birth').style.display = "block";
  
      country_acc.disabled=true;
      city_acc.disabled=true;
      birthday_acc.disabled=true;
  
      country_acc.style.border = "none";
      city_acc.style.border = "none";
  
      birthday_acc.style.border = "none";
      
      document.getElementById("divB").style.marginTop = "0px";
    }
    
  }
function ProfileDescription () {
    const user = useSelector(state=>state.user.userInfo);
    let token = localStorage.getItem("token");  
    const [id, setId] = useState(user.id);
    const navigate = useNavigate();
    const [firstName, setFirstName] = useState(user.firstName);
    const [lastName, setLastName] = useState(user.lastName);
    const [city, setCity] = useState(user.city);
    const [country, setCountry] = useState(user.country);
    const [dateOfBirth, setDateOfBirth] = useState(user.dateOfBirth);
    const [editUser, {data, error}] = useEditUserMutation();
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
    const editDescription = async() =>{
        tryEditDescription()
        await editUser({id, firstName, lastName, city, country, dateOfBirth, token});
      }
    useEffect(()=>{
    if (data){
        if(data!=401){
        token = data;
        localStorage.setItem("token",`Bearer ${token}`);
        console.log('setUser!')
        dispatch(setUser(token));
        }else{
        alert('Время вашего сеанса истекло!');
        logOut();
        }
    }
    if(error){
        alert(error);
    }
    },[data, error]);
    return(
        <div className="description">
          <div className="name">
            <input id="firstname_acc" onChange={(e)=>{setFirstName(e.target.value)}} value={firstName} placeholder="Имя"></input>
            <input id="lastname_acc" onChange={(e)=>{setLastName(e.target.value)}} value={lastName} placeholder="Фамилия"></input>
            <div id="default_name">
              <div>
              {user.firstName+" "+user.lastName}
              </div>
            </div>
            <img id="desc_img" src={edit} onClick={tryEditDescription}></img>
          </div>
          <div className="country">
            <img src={location}></img>
            Страна: <div id="default_country">{user.country}</div>
            <input id="country_acc" type="text" onChange={(e)=>{setCountry(e.target.value)}} value={country}></input>
          </div>
          <div className="city">
            <img src={location}></img>
            Город: <div id="default_city">{user.city}</div>
            <input id="city_acc" type="text" onChange={(e)=>{setCity(e.target.value)}} value={city}></input>
          </div>
          <div id="divB" className="birth">
            Дата рождения: <div id="default_birth">{dateOfBirth.split("-").reverse().join(".").split(" ")[0]}</div>
            <input id="birthday_acc" type="date" onChange={(e)=>{setDateOfBirth(e.target.value)}} value={dateOfBirth}></input>
          </div>
          <button id="save_description" onClick={editDescription} type="submit">Сохранить</button>
        </div>
    )
}
export default ProfileDescription;