import React from "react";
import background from "./../../../images/background.jpg";
import logo from "./../../../images/logo.png";
import "./UserProfileInfo.css";
import location from "./../../../images/location.svg";
import edit from "./../../../images/edit.svg";
import axios from "axios";
import {useEffect, useState} from 'react';
import { useNavigate } from "react-router-dom";
import Info from "./Info";




const UserProfileInfo = ({id}) => {
  // let x ="";
  // if (user.firstName=="" && user.lastName=="")
  //   x="Фамилия Имя";
  const token = localStorage.getItem("token");
  const navigateU = useNavigate();
  const [isLoaded, setIsLoaded] = useState(true);
  const [it, setIt] = useState([]);
  const getUser = async() =>{
    setIsLoaded(true);
    try{
      const result = await axios.get("http://25.32.11.98:8086/api/User",{
        params:{
          id: id,
        },
        headers:{
          "Authorization": token,
        }
      });
      setIt(result.data);
      console.log(result.data);
      console.log(it);
    }catch(error){
      console.log(error);
      if (error.response){
        if (error.response.status==401){
          navigateU("/login");
          alert('Время вашего сеанса истекло');
        }
        if (error.response.status == 500)
          alert("Что-то пошло не так, пожалуйста, подождите, мы уже исправляем!");
      }
    }finally{
      setIsLoaded(false);
      //console.log(it);
    }
  }
  useEffect(() => {
    getUser();
  }, []);
  
  return (
    <>
    
    {isLoaded?<div>Контент загружается...</div>:<Info it={it}></Info>}
    </>
  );
};

export default UserProfileInfo;
