import {React, useEffect, useState} from 'react';
import './UserGalleryInProfile.css';
import { NavLink, useNavigate, useParams } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import Popup from 'reactjs-popup';

const UserGalleryInProfile = (props) => {
    const [photos, setPhotos] = useState([]);
    let token = localStorage.getItem("token");
    const param = useParams();
    const id = Number(param['*']); 
    const navigate = useNavigate();
    const [content, setContent] = useState();
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
           
            // console.log(response.data);
            setPhotos(response.data);
            props.getFirstPhotos(response.data);
            // console.log(photos);
            // console.log(props.photos);
        } catch (error){
            if (error.response){
             if (error.response.status==401){
                navigate('/login');
            }   
            }
            
        }
    }
    
    useEffect(()=>{
        
        GetGallery();
        console.log(photos);
        
    },[])
    
    return (
    <div className='containerForGallery'>
        <div className='titleForPhotosInGallery'><NavLink to={'/gallery/'+id} id={id}>Галерея</NavLink></div>
        <div className='containerForPhotosInGallery'>
            {photos.map((ph)=>{
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

export default UserGalleryInProfile;