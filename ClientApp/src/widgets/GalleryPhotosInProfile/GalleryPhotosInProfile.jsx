import {React, useEffect, useState} from 'react';
import './GalleryPhotosInProfile.css';
import { NavLink, useNavigate } from 'react-router-dom';
import Popup from 'reactjs-popup';
import { useGetFirstPhotosQuery } from '../../features/api/apiSlice';
import { useDispatch, useSelector } from 'react-redux';
import { setFirstPhotos } from '../../features/user/userPhotoSlice';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { connectionNotif } from '../../app/helpers/signalRNotifications';

const GalleryPhotosInProfile = () => {

    const user = useSelector(state=>state.user.userInfo);
    const userPhotos = useSelector(state=>state.userPhotos.firstPhotos);
    let token = localStorage.getItem("token");  
    const [id, setId] = useState(user.id);
    const [photos, setPhotos] = useState(userPhotos);    
    const { data, error, isLoading } = useGetFirstPhotosQuery({id, token});
    const navigate = useNavigate();
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
    
    useEffect(()=>{
        if(data){
           dispatch(setFirstPhotos(data));
        }
        if(error){
            if (error.status==401){
                logOut();
            }
            else{
                alert(error.message);
            }
        }
        console.log(photos);
    },[data, error])
    
    return (
    <div className='containerForGallery'>
        <div className='titleForPhotosInGallery'><NavLink to={'/gallery/'+id}>Галерея</NavLink></div>
        <div className='containerForPhotosInGallery'>
            {isLoading?<></>:userPhotos==null?<></>:userPhotos.map((ph)=>{
                return(<Popup trigger={
                    <div className='containerForOnePhotoInGallery'>
                <img src={"http://25.32.11.98:8086/Galleries/"+ph}></img>
            </div>
                }>
                    <div className='fullPhoto'>
                        <img src={"http://25.32.11.98:8086/Galleries/"+ph}></img>
                    </div>
                </Popup>
                )
            })}
        </div>
    </div>
    );
}

export default GalleryPhotosInProfile;