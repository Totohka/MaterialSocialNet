import {React, useEffect, useState} from 'react';
 import './GalleryAllPhoto.css';
import plus from './../../images/plus.svg';
import { useNavigate, useParams } from 'react-router-dom';
import PhotoInPart from './GalleryPartPhoto/PhotoInPart/PhotoInPart';
import { useDispatch, useSelector } from 'react-redux';
import { useAddPhotoMutation, useDeletePhotoMutation, useGetPhotosQuery, useLazyGetPhotosQuery } from '../../features/api/apiSlice';
import { setPhotos } from '../../features/user/userPhotoSlice';
import { HubConnectionState } from 'redux-signalr';
import { connectionNotif } from '../../app/helpers/signalRNotifications';
import { connection } from '../../app/helpers/withSignalR';

const GalleryAllPhoto = (props) => {
    const user = useSelector(state=>state.user.userInfo);
    const [classH, setClassH] = useState(true);
    const [uploadImage, setImage] = useState();
    const [fileName, setFile] = useState();
    const [style, setStyle] = useState('none');
    let token = localStorage.getItem("token");
    const param = useParams();
    const id = Number(param['*']); 
    const navigate = useNavigate();
    const { data: photos,
        isLoading,
        isSuccess,
        isError,
        error } = useGetPhotosQuery({id});
    const [addPhoto, {error:AddPhotoError}] = useAddPhotoMutation();
    const [deletePhoto, {error:DeletePhotoError}] = useDeletePhotoMutation();
    const [getPhotos] = useLazyGetPhotosQuery();
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
        if(!isLoading){
            if(isSuccess){
            dispatch(setPhotos(photos));
            console.log(photos);
            console.log(isLoading);
            }
            if(isError){
                console.log(error);
                if (error.response.status==401){
                    logOut();
                }
                else{
                    alert(error.message);
                }
            }else if (AddPhotoError){
                console.log(AddPhotoError);
                if (AddPhotoError.response.status==401){
                    logOut();
                }
                else{
                    alert(AddPhotoError.message);
                }
            }else if(DeletePhotoError){
                console.log(DeletePhotoError);
                if (DeletePhotoError.response.status==401){
                    logOut();
                }
                else{
                    alert(DeletePhotoError.message);
                }
            }
        }
        
        
    },[isLoading, isSuccess, isError, AddPhotoError, DeletePhotoError]);

    const showDelete = () =>{
        if (classH){
            setClassH(false);
        }else {setClassH(true); }  
        
    }
    const tryDelete = async(e) =>{
        let val = Number(e.target.value);
        console.log(val);
        console.log('try delete');
        console.log({id, val, token});
        await deletePhoto({ id, val, token }).then(()=>{
            getPhotos({id})
        }).catch((error)=>{
            alert(error.message);
          if (error.response.status==401){
            localStorage.removeItem('token');
            navigate('/login');
          }
        });
    }
    //#region AddPhoto
    const onPhotoAdd = async() => {
        const formData = new FormData();
        formData.append("photo", uploadImage);
        formData.append("user_id", user.id);
        await addPhoto({formData, token, id}).then(()=>{
            getPhotos({id})
        }).catch((error)=>{
            alert(error.message);
          if (error.response.status==401){
            navigate('/login');
          }
        });
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


    return (
        <div className='allGallery'>
            <div className="allPhoto">
                {isLoading?<>Загрузка...</>:isSuccess?photos==null?<></>:photos.map((photo)=>{
            return(<div class="boxForOnePhoto">
            <button hidden={classH} onClick={tryDelete} className="deleteButton" value={photo.slice(photo.indexOf('/')+1,photo.indexOf('.'))}>Удалить</button>
            <PhotoInPart key={photo.slice(photo.indexOf('/')+1,photo.indexOf('.'))} photo={"http://25.32.11.98:8086/Galleries/"+photo} />
            </div>)
        }):<></>}
            </div>
            <div className='help-buttons'>
                <div class="file-input">
                    <input type="file" id='inputForDownloadPhotoInGallery' onChange={onPhotoChange} accept='.jpg'/> 
                </div>
                {user.id==id?<><div className='flexForDownloadPhotoInGallery'>
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
