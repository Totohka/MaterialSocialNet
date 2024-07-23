import {React, useEffect, useState} from 'react';
 import './GalleryAllPhoto.css';
import GalleryPartPhoto from './GalleryPartPhoto/GalleryPartPhoto';
import plus from './../../../images/plus.svg';
import bin from './../../../images/bin.svg';
import axios from 'axios';
import Popup from 'reactjs-popup';
import { useNavigate, useParams } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import PhotoInPart from './GalleryPartPhoto/PhotoInPart/PhotoInPart';

const GalleryAllPhoto = (props) => {
    let mod;
    let size;
    let count;
    const [classH, setClassH] = useState(true);
    const [uploadImage, setImage] = useState();
    const [load, setLoad] = useState(true);
    const [fileName, setFile] = useState();
    const [style, setStyle] = useState('none');
    const [photos, setPhotos] = useState(props.galleryPhoto);
    let token = localStorage.getItem("token");
    let us = jwtDecode(token.slice(7,));
    const param = useParams();
    const id = Number(param['*']); 
    const [content, setContent] = useState();
    const navigate = useNavigate();
    const showDelete = () =>{
        //document.getElementById('dM').classList.toggle('bright')
        if (classH){
            setClassH(false);
        }else {setClassH(true); }  
        
    }
    const tryDelete = (e) =>{
        let val = e.target.value;
        console.log(val);
        console.log('try delete');
        deletePhoto(val);
        // GetAllPhotos();
    }
    const deletePhoto = async(photo) => {
        setLoad(true);
        try{
            let us = jwtDecode(token.slice(7,));
            await axios.delete("http://25.32.11.98:8086/api/Gallery", 
            {
                params:{
                    userId:us.id,
                    imageId:photo
                },
                headers:{
                    'Authorization':token
                }
            });
            GetAllPhotos();
        }catch(error){
            if (error.response) { // get response with a status code not in range 2xx
                console.log(error.response.data);
                console.log(error.response.status);
                console.log(error.response.headers);
              } else if (error.request) { // no response
                console.log(error.request);
                // instance of XMLHttpRequest in the browser
                // instance ofhttp.ClientRequest in node.js
              } else { // Something wrong in setting up the request
                console.log('Error', error.message);
              }
            console.log(error.config);
        }finally{
            setLoad(false);
        }
    }
    const GetAllPhotos = async() =>{
        setLoad(true);
        //setContent(<div>Подождите, идёт загрузка...</div>);
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
            // size = photos.length
            // count = Math.ceil(size / 3);
            // mod = size % 3;
            // console.log(props.photos);
        } catch (error){
            if (error.response.status==401){
                navigate('/login');
            }
        }finally{
            
            setLoad(false);
            
        }
    }
    //#region AddPhoto
    const SendPhoto = async() =>{
        setLoad(true);
        
        console.log("send photo")
        const formData = new FormData();
        formData.append("photo", uploadImage);
        formData.append("user_id", us.id);
        try{
          await axios.post("http://25.32.11.98:8086/api/Gallery",
                    formData,
                    {
                      headers:{
                        'Authorization':token,
                        'content-type': 'multipart/form-data'
                      }
                    })
          
        }catch (error) {
          alert(error.message);
          if (error.response.status==401){
            navigate('/login');
          }
        }finally{
          
          setLoad(false);
          GetAllPhotos(); 
        }
    
      }
    const onPhotoAdd = () => {
        props.addNewPhotoInGallery();
        props.updateNewPhotoInGallery('');
        SendPhoto();
        
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
    //#endregion

    let allPhoto = [];
    useEffect(()=>{
        GetAllPhotos(); 
        // setContent()
        // if (load){
        //     setContent(<div>Подождите, идёт загрузка...</div>);
        // }
        // else{
        //     
        //     console.log(size, mod, count)
        //     let partPhoto;
        //         for(let i = 0; i < count; i++){
        //         if( i === count - 1){
        //             switch(mod){
        //                 case 0:
        //                     partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} 
        //                                         photo2={"http://25.32.11.98:8086/Galleries/"+photos[1 + 3*i]}
        //                                         photo3={"http://25.32.11.98:8086/Galleries/"+photos[2 + 3*i]}/>;
        //                     break;
        //                 case 1:
        //                     partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} />;
        //                     break;
        //                 case 2:
        //                     partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} 
        //                                         photo2={"http://25.32.11.98:8086/Galleries/"+photos[1 + 3*i]}/>;
        //                     break;
        //             }
        //             allPhoto.push(partPhoto);
        //         }
        //         else{
        //             partPhoto = <GalleryPartPhoto photo1={"http://25.32.11.98:8086/Galleries/"+photos[0 + 3*i]} 
        //                                         photo2={"http://25.32.11.98:8086/Galleries/"+photos[1 + 3*i]}
        //                                         photo3={"http://25.32.11.98:8086/Galleries/"+photos[2 + 3*i]}/>;
        //             allPhoto.push(partPhoto);
        //         }
        //     }
        //     setContent(allPhoto);
        // }
        
    // console.log(allPhoto)

    },[])
    

    return (
        <div className='allGallery'>
            <div className="allPhoto">
                {load?<div>Подождите, идёт загрузка...</div>:photos.map((photo)=>{
                    //console.log(photo.slice(photo.indexOf('/')+1, photo.indexOf('.')));
            return(<div class="boxForOnePhoto">
            <button hidden={classH} onClick={tryDelete} className="deleteButton" value={photo.slice(photo.indexOf('/')+1,photo.indexOf('.'))}>Удалить</button>
            <PhotoInPart key={photo.slice(photo.indexOf('/')+1,photo.indexOf('.'))} photo={"http://25.32.11.98:8086/Galleries/"+photo} />
            
            </div>)
        })}
            </div>
            <div className='help-buttons'>
                <div class="file-input">
                    <input type="file" id='inputForDownloadPhotoInGallery' onChange={onPhotoChange} accept='.jpg, .jpeg'/> 
                </div>
                {us.id==id?<><div className='flexForDownloadPhotoInGallery'>
                    <div className='downloadPhotoInGallery'>
                        <img src={plus}></img>
                        <label for="inputForDownloadPhotoInGallery" >Добавить фотографию</label>
                    </div>
                    <span style={{display: style}}>{fileName}</span>
                    <input style={{display: style}} type='button' onClick={onPhotoAdd} value='Загрузить'/>
                </div>
                <div onClick={showDelete} className='deletePhoto'>
                        <img src={plus}></img>
                        <label>Удалить фотографию</label>
                </div>
                </>:<></>}
                
            </div>
            
            
        </div>
    );
}

export default GalleryAllPhoto;
