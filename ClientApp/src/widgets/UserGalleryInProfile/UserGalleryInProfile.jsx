import {React, useEffect, useState} from 'react';
import './UserGalleryInProfile.css';
import { NavLink, useNavigate, useParams } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import Popup from 'reactjs-popup';
import { useGetFirstPhotosQuery, useGetUserFirstPhotosQuery } from '../../features/api/apiSlice';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';

const UserGalleryInProfile = () => {
    // const [photos, setPhotos] = useState([]);
    let token = localStorage.getItem("token");
    const param = useParams();
    const id = Number(param['*']); 
    const navigate = useNavigate();
    const [content, setContent] = useState();
    const { data: photos,
        isLoading,
        isSuccess,
        isError,
        error } = useGetUserFirstPhotosQuery(id);

    const logOut = () =>{
        localStorage.removeItem('token');
        if (connection.state !== HubConnectionState.Disconnected) {
            connection.stop()
                .then(() => console.log("Connection stoped"))
                .catch((err) => console.error(err.toString()));
            }
        navigate('/');
    }
    useEffect(()=>{

        if(!isLoading){
            if(isError){
                console.log(error);
                if (error.status==401){
                    logOut();
                }
                else{
                    alert(error.message);
                }
            }
        }
    },[isLoading, isSuccess, isError]);

    const GetGallery = async() =>{
        try{
            const response = await axios.get("http://25.32.11.98:8086/api/Gallery/user",
            {
                params:{
                    userId:id
                },
                headers:{
                    'Authorization':token
                }
            })
        } catch (error){
            if (error.response){
             if (error.response.status==401){
                navigate('/login');
            }   
            }
            
        }
    }
    
    
    return (
    <div className='containerForGallery'>
        <div className='titleForPhotosInGallery'><NavLink to={'/gallery/'+id} id={id}>Галерея</NavLink></div>
        <div className='containerForPhotosInGallery'>
            {isLoading?<>Загрузка...</>:isSuccess?photos==[]?<></>:photos.map((ph)=>{
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
            }):<></>}
        </div>
    </div>
    );
}

export default UserGalleryInProfile;