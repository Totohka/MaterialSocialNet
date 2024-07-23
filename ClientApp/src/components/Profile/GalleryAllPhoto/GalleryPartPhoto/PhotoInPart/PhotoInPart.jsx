import './PhotoInPart.css';
import Popup from 'reactjs-popup';
import bin from './../../../../../images/bin.svg';
import { axios } from 'axios';
import { jwtDecode } from 'jwt-decode';
const PhotoInPart = (props) => {
    
    const showDelete = () =>{
        //document.getElementById('dM').classList.toggle('bright')
        document.getElementById('dP').classList.toggle('hidden');
    }
    const tryDelete = () =>{
        console.log('try delete');
        // deletePhoto();
        // GetAllPhotos();
    }
    return (
        <>
        
        <Popup trigger={
                <img src={props.photo}/>}>
                <div id='fP' className='fullPhoto'>
                    <img src={props.photo}></img>
                    {/* <div id="dM" className="deleteMenu" onClick={showDelete}>
                        <div className='dm-1'></div>
                        <div className='dm-1'></div>
                        <div className='dm-1'></div>
                        <div id="dP" onClick={tryDelete} className='deletePhoto hidden'>
                            <img src={bin}></img>
                            <div>Удалить</div>
                        </div>
                    </div> */}
                </div>
                </Popup>
            
        
        </>
    );
}

export default PhotoInPart;