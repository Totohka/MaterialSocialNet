import {React, useEffect, useState} from 'react';
 import './GalleryAllPhoto.css';
import GalleryPartPhoto from './GalleryPartPhoto/GalleryPartPhoto';
import plus from './../../../images/plus.svg';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

const GalleryAllPhoto = ({id}) => {
    const [photos, setPhotos] = useState(props.galleryPhoto);
    let token = localStorage.getItem("token");
    let us = jwtDecode(token.slice(7,));
    const [content, setContent] = useState();
    const navigate = useNavigate();
    const GetAllPhotos = async() =>{
        try{
            const response = await axios.get("http://25.32.11.98:8086/api/Gallery/All",
            {
                params:{
                    userId:id
                },
                headers:{
                    'Authorization':token
                }
            })
            setPhotos(response.data);
            //console.log(response.data);
            
            props.getPhotos(response.data);
             console.log(photos);
            // console.log(props.photos);
        } catch (error){
            if (error.response.status==401){
                navigate('/login');
            }
        }
    }
    
   
    let allPhoto = [];
    useEffect(()=>{
        GetAllPhotos(); 
        
        let size = photos.length
    let count = Math.ceil(size / 3);
    let mod = size % 3;
    console.log(size, mod, count)
    let partPhoto;
        for(let i = 0; i < count; i++){
        if( i === count - 1){
            switch(mod){
                case 0:
                    partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} 
                                        photo2={"http://25.32.11.98:8086/Galleries/"+photos[1 + 3*i]}
                                        photo3={"http://25.32.11.98:8086/Galleries/"+photos[2 + 3*i]}/>;
                    break;
                case 1:
                    partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} />;
                    break;
                case 2:
                    partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} 
                                        photo2={"http://25.32.11.98:8086/Galleries/"+photos[1 + 3*i]}/>;
                    break;
            }
            allPhoto.push(partPhoto);
        }
        else{
            partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} 
                                        photo2={"http://25.32.11.98:8086/Galleries/"+photos[1 + 3*i]}
                                        photo3={"http://25.32.11.98:8086/Galleries/"+photos[2 + 3*i]}/>;
            allPhoto.push(partPhoto);
        }
    }
    setContent(allPhoto);
    // console.log(allPhoto)

    },[])
    

    return (
        <div className='allGallery'>
            <div className="allPhoto">
                {content}
            </div>
            
        </div>
    );
}

export default GalleryAllPhoto;
