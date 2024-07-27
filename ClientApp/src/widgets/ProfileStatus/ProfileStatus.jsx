import { useEffect, useState } from "react";
import { useEditStatusMutation } from "../../features/api/apiSlice";
import { useDispatch, useSelector } from "react-redux";
import { setUser } from "../../features/auth/userSlice";
import { useNavigate } from "react-router-dom";
import edit from "./../../images/edit.svg";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

const tryEditStatus = () => {
    let textar = document.getElementById('textarea_status');
    let button = document.getElementById('save_status');
    if (button.style.display != "block"){
      button.style.display = "block";
      textar.removeAttribute("disabled");
      textar.style.border = "1px solid white";
      textar.style.borderRadius = "5px";
    }else{
      button.style.display = "none";
      textar.disabled=true;
      textar.style.border = "none";
    }
  }
function ProfileStatus () {
    
    const user = useSelector(state=>state.user.userInfo);
    let token = localStorage.getItem("token");  
    const [id, setId] = useState(user.id);
    const navigate = useNavigate();
    const [status, setStatus] = useState(user.status);
    const [editStatus, {data, error}] = useEditStatusMutation();
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
    const saveStatus = async () =>{
        tryEditStatus();
        await editStatus({id, status, token});
    }
    useEffect(()=>{
        if (data){
          if(data!=401){
            token = data;
            localStorage.setItem("token",`Bearer ${token}`);
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
        <div className="status">
            <div style={{display:'flex'}}>
            <p>Статус: </p>
            <img src={edit} onClick={tryEditStatus}></img>
            </div>
            <textarea id="textarea_status" type="textarea" onChange={(e)=>{setStatus(e.target.value)}} value={status} placeholder="Введите ваш статус..." disabled>{status}</textarea>
            <button id="save_status" onClick={saveStatus} type="submit">Сохранить</button>
        </div>
    )
}
export default ProfileStatus;