import {React, useEffect, useState} from 'react';
import './GalleryPhotosInProfile.css';
import { NavLink, useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import Popup from 'reactjs-popup';

const GalleryPhotosInProfile = (props) => {
    const [photos, setPhotos] = useState(props.photos);
    let token = localStorage.getItem("token");
    let us = jwtDecode(token.slice(7,));
    const navigate = useNavigate();
    const [content, setContent] = useState();
    const GetGallery = async() =>{
        try{
            const response = await axios.get("http://25.32.11.98:8086/api/Gallery/user",
            {
                params:{
                    userId:us.id
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
            if (error.response.status==401){
                navigate('/login');
            }
        }
    }
    
    useEffect(()=>{
        
        GetGallery();
        console.log(photos);
        
    },[])
    
    return (
    <div className='containerForGallery'>
        <div className='titleForPhotosInGallery'><NavLink to={'/gallery/'+us.id}>Галерея</NavLink></div>
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

export default GalleryPhotosInProfile;