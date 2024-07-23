import UsersInFind from "../UserInFind/UserInFind";
import React, { useEffect } from "react";
import axios from "axios";
import { useState } from "react";

const SelectedUsers = ({select}) =>{
    const [uName, setUName] = useState('');
    
    let token=localStorage.getItem("token");
    const [usersElements, setUsersElements] = useState([]);
    const GetUsers = async() =>{
        console.log(token)
        const result = await axios.get("http://25.32.11.98:8086/api/User/All",{
            params:{
                who:select
            },
            headers:{
                'Authorization':token
            }
        });
        console.log(result);
        console.log(result.data.users);
        setUsersElements(result.data.users);
    }
    const FindUsers=(e)=>{
        setUName(e.target.value);
        GetUsers();
    }
    useEffect(()=>{
        GetUsers();

    },[])
    return(
        <div>
            <div>
                <input onChange={FindUsers} value={uName} placeholder='Найти человека' type='text'/>
            </div>
        {
            usersElements.map((el)=>{
            if(uName.length!==0){
                if (el.name.toLowerCase().includes(uName.toLowerCase()))
                
                    return <UsersInFind key={el.id} user={el}
                            />
                else return null;
            }
            else return <UsersInFind key={el.id} user={el}
                        />
        })}
        </div>
        
    )
}
export default SelectedUsers;