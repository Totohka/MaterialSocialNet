import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import Popup from "reactjs-popup";
import plus from "./../../images/plus.svg";
import { useEditAvatarMutation } from "../../features/api/apiSlice";
import { setUser } from "../../features/auth/userSlice";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { jwtDecode } from "jwt-decode";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

function ProfileAvatar(){
    const user = useSelector(state=>state.user.userInfo);
    let token = localStorage.getItem("token");  
    const [id, setId] = useState(user.id);
    const navigate = useNavigate();
    const [uploadImage, setImage] = useState();
    const [load, setLoad] = useState(false);
    const [avatar, setAvatar] = useState(user.avatar);  
    const [fileName, setFile] = useState();
    const [style, setStyle] = useState('none');
    const [editAvatar, {data, error}] = useEditAvatarMutation();
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
    const onPhotoAdd = async() => {
        setLoad(true);
        const formData = new FormData();
        formData.append("photo", uploadImage);
        formData.append("user_id", id);
        await editAvatar({formData, token});
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
    useEffect(()=>{
        
        if (data){
          if(data!=401){
            token = data;
            console.log(jwtDecode(token));
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
        setLoad(false);
    
      },[data, error]);
    return(
        <div className="profilePicture">
        {load?<p>Загрузка...</p>:<img className="avatar" src={"http://25.32.11.98:8086/Avatars/"+user.avatar+`?time=${Date.now()}`} />}
          <Popup trigger={load?<></>:<div className="editAvatar">Изменить</div> }>
              <div className="changeAvatar">
                <div className="file-input">
                  <input type="file" id='inputForDownloadAvatar' onChange={onPhotoChange} accept=".jpg"/> 
                </div>
                <div className='downloadAvatar'>
                    
                    <label for="inputForDownloadAvatar"><img src={plus}></img> Выбрать фотографию</label>
                </div>
                <span style={{display: style}}>{fileName}</span>
                <input style={{display: style}} type='button' onClick={onPhotoAdd} value='Загрузить'/>
              </div>
          </Popup>
        </div>
    )
}
export default ProfileAvatar;