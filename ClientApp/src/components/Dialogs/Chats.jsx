import { useEffect, useState } from "react";
import DialogItem from "./DialogItem/DialogsItem";
import plus from "./../../images/plus.svg"
import "./Chats.css"
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import Popup from "reactjs-popup";

function Chats(props){
    const [cName, setCName]=useState("");
    const [fName, setFName]=useState("");
    const [chatName, setChatName]=useState("");
    const [desc, setDesc]=useState("");
    const [chatUs, setChatUs]=useState([]);

    const [usersElements, setUsersElements] = useState([true]);
    const getFriends = async() =>{
        console.log(token)
        //console.log(selection);
        try{
            const result = await axios.get("http://25.32.11.98:8086/api/User/All",{
                params:{
                    who:2
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
    let dialogsElements = props.dialogsData;
    // debugger;
    const navigateU = useNavigate();
    let token=localStorage.getItem("token");
    let us = jwtDecode(token);
    const [chatsElements, setChatsElements] = useState([true]);
    const getChats = async() =>{
        console.log(token);
        try{
            const result = await axios.get("http://25.32.11.98:8089/api/Chat/All",{
                headers:{
                    'Authorization':token
                }
            });
            console.log(result);
            console.log(result.data.chatRooms);
            setChatsElements(result.data.chatRooms);
        } catch(error){
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    navigateU("/login");
                    alert('Время вашего сеанса истекло');
                }
                console.log(error.response);
            }
            
            
        }
    }
    const FindChat=(e)=>{
        setCName(e.target.value);
    }
    const changeChatName=(e)=>{
        setChatName(e.target.value);
    }
    const changeDescription=(e)=>{
        setDesc(e.target.value);
    }
    const changeFriendName=(e)=>{
        setFName(e.target.value);
    }
    const resetNew=()=>{
        setDesc("");
        setChatName("");
        setChatUs([]);
        setFName("");
    }
    const tryAddToChat=(e)=>{
        if (e.target.classList.contains("added")) {
            let newChatUs = chatUs.filter((el)=>el!=e.target.value);
            setChatUs(newChatUs);
        } else{
            let newChatUs = chatUs;
            newChatUs.push(e.target.value);
            setChatUs(newChatUs);
        }
        console.log(chatUs);
        e.target.classList.toggle("added");
    }
    const createChat = async() =>{
        console.log(token);
        try{
            const result = await axios.post("http://25.32.11.98:8089/api/Chat",{
                Name:chatName,
                Description:desc,
                LastMessage:""
            },{
                params:{
                    userId: us.id
                },
                headers:{
                    'Authorization':token
                }
            });
            console.log(result);
        } catch(error){
            console.log(error);
            if(error.response){
                if (error.response.status==401){
                    navigateU("/login");
                    alert('Время вашего сеанса истекло');
                }
                console.log(error.response);
            }
        }
    }
    const tryCreateChat = () =>{
        createChat();
    }
    useEffect(()=>{
        getChats();
        getFriends();
    },[])
    
    return(
        <div className="chats">
            <div className="chats-header">
                <p>Чаты</p>
                <Popup trigger={<img src={plus} ></img>} onClose={resetNew}>
                    <div className="newchat__form">
                        <input type="text" onChange={changeChatName} className="newchat__form_input" placeholder="Введите название для чата..." value={chatName}></input>
                        <input type="text" onChange={changeDescription} className="newchat__form_input" placeholder="Введите описание чата..." value={desc}></input>
                        <input type="text" onChange={changeFriendName} className="newchat__form_input" placeholder="Введите имя собеседника..." value={fName}></input>
                        <div id="newchat__form_friends" className="newchat__form_friends">
                            {usersElements.map((el)=>{
                                if(el===true) return <div>Подождите, данные загружаются...</div>
                                else{
                                    if(fName.length!==0){
                                        if (el.firstName.toLowerCase().includes(fName.toLowerCase())||el.lastName.toLowerCase().includes(fName.toLowerCase()))
                                            return <button onClick={tryAddToChat} className="newchat__form_friend" key={el.id} value={el.id}>
                                                <div className="newchat__form_friend_img">
                                                    <img src={"http://25.32.11.98:8086/Avatars/"+el.avatar}></img>
                                                </div>
                                                <div className="newchat__form_friend_name">{el.firstName+' '+el.lastName}</div>
                                            </button>
                                        else return <></>;
                                    }
                                    else return <button onClick={tryAddToChat} className="newchat__form_friend" key={el.id} value={el.id}>
                                                    <div className="newchat__form_friend_img">
                                                        <img src={"http://25.32.11.98:8086/Avatars/"+el.avatar}></img>
                                                    </div>
                                                    <div className="newchat__form_friend_name">{el.firstName+' '+el.lastName}</div>
                                                </button>
                                }
                            })}
                        </div>
                        <button className="newchat__form_button" onClick={tryCreateChat}>Создать чат</button>
                    </div>
                </Popup>
            </div>
            <div className="find-chat">
                <input placeholder="Введите название чата..." value={cName} onChange={FindChat}></input>
            </div>
            {/* <div className="double-line"></div> */}
            <div id="diItems" className="diItems">
                {chatsElements.map((el)=>{
                if (el===true) return <div>Подождите, данные загружаются...</div>
                else{
                    if(cName.length!==0){
                        if (el.name.toLowerCase().includes(cName.toLowerCase()))
                            return <DialogItem key={el.id} name={el.name} id={el.id}/>
                        else return null;
                    }
                    else return <DialogItem key={el.id} name={el.name} id={el.id}/>
                } })}
            </div>
        </div>
    )
}
export default Chats;