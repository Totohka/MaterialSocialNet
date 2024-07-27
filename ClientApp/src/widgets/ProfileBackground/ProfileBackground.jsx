import { useDispatch, useSelector } from "react-redux";
import { useEditBackgroundMutation } from "../../features/api/apiSlice";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import plus from "./../../images/plus.svg";
import { setUser } from "../../features/auth/userSlice";
import Popup from "reactjs-popup";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

function ProfileBackground(){
    const user = useSelector(state=>state.user.userInfo);
    let token = localStorage.getItem("token");  
    const [id, setId] = useState(user.id);
    const navigate = useNavigate();
    const [uploadImage, setImage] = useState();
    const [load, setLoad] = useState(false);
    const [background, setBackground] = useState(user.background);
    const [fileName, setFile] = useState();
    const [style, setStyle] = useState('none');
    const [editBackground, {data, error}] = useEditBackgroundMutation();
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
    const onFoneAdd = async() => {
        setLoad(true);
        const formData = new FormData();
        formData.append("photo", uploadImage);
        formData.append("user_id", id);
        await editBackground({formData, token});
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
        setLoad(false);
      },[data, error]);

    return(
        <div className="background">
          {load?<img src=""/>:<img src={"http://25.32.11.98:8086/Backgrounds/"+user.background+`?time=${Date.now()}`}/>}
          <div className="changeFone">
            <Popup trigger={<button>Изменить фоновое фото</button> }>
              <div className="changeAvatar">
                <div className="file-input">
                  <input type="file" id='inputForDownloadAvatar' onChange={onFoneChange} accept=".jpg"/> 
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
    )
}
export default ProfileBackground;
