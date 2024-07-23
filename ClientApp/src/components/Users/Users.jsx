import React, { useEffect } from "react";
import UsersInFind from "./UserInFind/UserInFind";
import './Users.css';
import axios from "axios";
import { useState } from "react";
import SelectedUsers from "./SelectedUsers/SelectedUsers";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

function Users(props){
    // debugger;
    const [selection, setSelection] = useState('0');
    const setSelected = (e) =>{
        setSelection(e.target.value);
        document.getElementById("select").classList.toggle('bordered')
        console.log(e.target.value);
        
        GetUsers(e.target.value);
    }
    const [uName, setUName] = useState('');
    const navigateU = useNavigate();
    let token=localStorage.getItem("token");
    let us = jwtDecode(token);
    const [usersElements, setUsersElements] = useState([true]);
    const GetUsers = async(selected=0) =>{
        console.log(token)
        console.log(selection);
        try{
            const result = await axios.get("http://25.32.11.98:8086/api/User/All",{
                params:{
                    who:selected
                },
                headers:{
                    'Authorization':token
                }
            });
            console.log(result);
            console.log(result.data.users);
            setUsersElements(result.data.users);
        } catch(error){
            console.log(error);
            if (error.response.status==401){
                navigateU("/login");
                alert('Время вашего сеанса истекло');
            }
            
        }
    }
    const FindUsers=(e)=>{
        setUName(e.target.value);
        GetUsers(selection);
    }
    useEffect(()=>{
        GetUsers();

    },[])
    return (
        <div className="allUsers">
            <div className="usersSelect">
                <select id="select" onChange={setSelected}>
                    <option value="0">Все пользователи</option>
                    <option value="1">Ваши подписчики</option>
                    <option value="2">Друзья</option>
                </select>
            </div>
            <div className="findUser">
                <input onChange={FindUsers} value={uName} placeholder='Найти человека' type='text'/>
            </div>
        {
            usersElements.map((el)=>{
                if (el===true) return <div>Подождите, пользователи загружаются...</div>
                if(el.id!=us.id){
                    if(uName.length!==0){
                        if (el.name.toLowerCase().includes(uName.toLowerCase()))
                        
                            return <UsersInFind key={el.id} user={el} 
                                    />
                        else return null;
                    }
                else return <UsersInFind key={el.id} user={el}/>
                } 
            
                        
        })}
            

            
        </div>
    )
}


export default Users;