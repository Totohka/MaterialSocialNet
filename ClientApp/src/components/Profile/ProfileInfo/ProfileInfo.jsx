import React, { useState } from "react";
import background from "./../../../images/background.jpg";
import logo from "./../../../images/logo.jpg";
import "./ProfileInfo.css";
import location from "./../../../images/location.svg";
import edit from "./../../../images/edit.svg";
import axios from "axios";
import plus from "./../../../images/plus.svg";
import Popup from "reactjs-popup";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";

const tryEditStatus = () => {
  let textar = document.getElementById('textarea_status');
  let button = document.getElementById('save_status');
  button.style.display = "block";
  textar.removeAttribute("disabled");
  textar.style.border = "1px solid white";
  textar.style.borderRadius = "5px";
}
const tryEditDescription = () =>{
  let firstname_acc = document.getElementById('firstname_acc');
  let def_name=document.getElementById('default_name');
  let lastname_acc = document.getElementById('lastname_acc');
  let country_acc = document.getElementById('country_acc');
  let city_acc = document.getElementById('city_acc');
  let birthday_acc = document.getElementById('birthday_acc');
  let button = document.getElementById('save_description');
  
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


const ProfileInfo = (props) => {
  const navigate = useNavigate();
  const [uploadImage, setImage] = useState();
  const [load, setLoad] = useState(false);
  
  const [fileName, setFile] = useState();
  const [style, setStyle] = useState('none');
  let token = localStorage.getItem("token");
  console.log(token);
  let x ="";
  if (props.firstName=="" && props.lastName=="")
    x="Фамилия Имя";
  //#region Setters
  const SetStatus = (e) => {
    //http request
    props.changeStatusUser(e.target.value);
  }
  const SetDateBirthDay = (e) => {
    props.changeDateBirthday(e.target.value)
  }
  const SetFirstName = (e) => {
    props.changeFirstName(e.target.value)
  }
  const SetLastName = (e) => {
    props.changeLastName(e.target.value)
  }
  const SetCountry = (e) => {
    props.changeCountry(e.target.value)
  }
  const SetCity = (e) => {
    props.changeCity(e.target.value)
  }
  //#endregion
  const EditUser = async() => {
    try{
      const response=await axios.put("http://25.32.11.98:8086/api/User",{
          "id":props.id,
          "first_name":props.firstName,
          "last_name":props.lastName,
          "city":props.city,
          "country":props.country,
          "date_birthday":props.dataBithday,
        },
      {
        headers: {
          'Authorization': token
        }
      }
      );
      console.log(response.data);
      token = response.data;
      props.setToken('Bearer '+token);
      let newus = jwtDecode(token);
      props.setUser(newus);
      console.log(props);
    } catch (error){
      alert(error.message);
      if(error.response.status==401){
        navigate('/login');
      }
    }
  }
  const EditStatus = async() => {
    try{
      const response=await axios.put("http://25.32.11.98:8086/api/User",{
          "id":props.id,
          "status":props.description
        },
      {
        headers: {
          'Authorization': token
        }
      }
      );
      console.log(response.data);
      token = response.data;
      props.setToken('Bearer '+token);
      let newus = jwtDecode(token);
      props.setUser(newus);
      console.log(props);
    } catch (error){
      alert(error.message);
      if(error.response.status==401){
        navigate('/login');
      }
    }
  }
  const editStatus = () =>{
    let textar = document.getElementById('textarea_status');
    let button = document.getElementById('save_status');
    button.style.display = "none";
    textar.disabled=true;
    textar.style.border = "none";
    // SetStatus(textar.value);
    EditStatus();
    
  }
  const editDescription = () =>{
    let firstname_acc = document.getElementById('firstname_acc');
    let def_name=document.getElementById('default_name');
    let lastname_acc = document.getElementById('lastname_acc');
    let country_acc = document.getElementById('country_acc');
    let city_acc = document.getElementById('city_acc');
    let birthday_acc = document.getElementById('birthday_acc');
    let button = document.getElementById('save_description');

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
    EditUser();
  }
  const SendAvatar = async() =>{
    setLoad(true);
    console.log("send avatar")
    const formData = new FormData();
    formData.append("photo", uploadImage);
    formData.append("user_id", props.id);
    try{
      const response=await axios.put("http://25.32.11.98:8086/api/Avatar",
                formData,
                {
                  headers:{
                    'Authorization':token,
                    'content-type': 'multipart/form-data'
                  }
                })
      
      console.log(response.data);
      token = response.data;
      props.setToken('Bearer '+token);
      let newus = jwtDecode(token);
      props.setUser(newus);
      console.log(props);
      
    }catch (error) {
      alert(error.message);
      if (error.response.status==401){
        navigate('/login');
      }
    }finally{
      setLoad(false);
    }

  }
  const onPhotoAdd = () => {
    
    SendAvatar();
    setFile('');
    setStyle('none');
    
}

const onPhotoChange = (e) => {
    if (!(e.target.files instanceof Blob)) {
    const file = e.target.value;
    setFile(file);
    setImage(e.target.files[0]);  
    setStyle('block');
  }
};
const SendBackground = async() =>{
  setLoad(true);
  console.log("send background")
  const formData = new FormData();
  formData.append("photo", uploadImage);
  formData.append("user_id", props.id);
  try{
    const response=await axios.put("http://25.32.11.98:8086/api/Background",
              formData,
              {
                headers:{
                  'Authorization':token,
                  'content-type': 'multipart/form-data'
                }
              })
    
    console.log(response.data);
    token = response.data;
    props.setToken('Bearer '+token);
    let newus = jwtDecode(token);
    props.setUser(newus);
    console.log(props);
    
  }catch (error) {
    alert(error.message);
    if (error.response.status==401){
      navigate('/login');
    }
  }finally{
    setLoad(false);
  }

}
const onFoneAdd = () => {
    
  SendBackground();
  setFile('');
  setStyle('none');
  
}

const onFoneChange = (e) => {
  if (!(e.target.files instanceof Blob)) {
  const file = e.target.value;
  setFile(file);
  setImage(e.target.files[0]);  
  setStyle('block');
}
};
  return (
    <div>
      <div className="profile_info">
        <div className="background">
          {load?<img src={background}/>:<img src={"http://25.32.11.98:8086/Backgrounds/"+props.background+`?time=${Date.now()}`}/>}
          <div className="changeFone">
            <Popup trigger={<button>Изменить фоновое фото</button> }>
              <div className="changeAvatar">
                <div className="file-input">
                  <input type="file" id='inputForDownloadAvatar' onChange={onFoneChange} accept=".jpg, .jpeg"/> 
                </div>
                <div className='downloadAvatar'>
                    
                    <label for="inputForDownloadAvatar"><img src={plus}></img> Выбрать фотографию</label>
                </div>
                <span style={{display: style}}>{fileName}</span>
                <input style={{display: style}} type='button' onClick={onFoneAdd} value='Загрузить'/>
              </div>
          </Popup>
          </div>
        </div>
        <div className="profilePicture">
        {load?<p>Загрузка...</p>:<img className="avatar" src={"http://25.32.11.98:8086/Avatars/"+props.avatar+`?time=${Date.now()}`} />}
          <Popup trigger={load?<></>:<div className="editAvatar">Изменить</div> }>
              <div className="changeAvatar">
                <div className="file-input">
                  <input type="file" id='inputForDownloadAvatar' onChange={onPhotoChange} accept=".jpg, .jpeg"/> 
                </div>
                <div className='downloadAvatar'>
                    
                    <label for="inputForDownloadAvatar"><img src={plus}></img> Выбрать фотографию</label>
                </div>
                <span style={{display: style}}>{fileName}</span>
                <input style={{display: style}} type='button' onClick={onPhotoAdd} value='Загрузить'/>
              </div>
          </Popup>
          {/* <img id="plusPhoto" src={plus}></img> */}
        </div>
        <div className="description">
          <div className="name">
            <input id="firstname_acc" onChange={SetFirstName} value={props.firstName} placeholder="Имя"></input>
            <input id="lastname_acc" onChange={SetLastName} value={props.lastName} placeholder="Фамилия"></input>
            <div id="default_name">
              <div>
              {x+props.firstName+" "+props.lastName}
              </div>
            </div>
            <img id="desc_img" src={edit} onClick={tryEditDescription}></img>
          </div>
          <div className="country">
            <img src={location}></img>
            Страна: <div id="default_country">{props.country}</div>
            <input id="country_acc" type="text" onChange={SetCountry} value={props.country}></input>
          </div>
          <div className="city">
            <img src={location}></img>
            Город: <div id="default_city">{props.city}</div>
            <input id="city_acc" type="text" onChange={SetCity} value={props.city}></input>
          </div>
          <div id="divB" className="birth">
            Дата рождения: <div id="default_birth">{props.dataBithday.split("-").reverse().join(".")}</div>
            <input id="birthday_acc" type="date" onChange={SetDateBirthDay} value={props.dataBithday}></input>
          </div>
          <button id="save_description" onClick={editDescription} type="submit">Сохранить</button>
        </div>
      </div>
      <div className="status">
        <div>
          <p>Статус: </p>
          <img src={edit} onClick={tryEditStatus}></img>
        </div>
        <textarea id="textarea_status" type="textarea" onChange={SetStatus} value={props.status} placeholder="Введите ваш статус..." disabled>{props.status}</textarea>
        <button id="save_status" onClick={editStatus} type="submit">Сохранить</button>
      </div>
    </div>
  );
};

export default ProfileInfo;
