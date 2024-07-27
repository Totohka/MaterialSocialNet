import './PhotoInPart.css';
import Popup from 'reactjs-popup';
import bin from './../../../../images/bin.svg';
import { axios } from 'axios';
import { jwtDecode } from 'jwt-decode';
const PhotoInPart = (props) => {
    return (
        <>
        
        <Popup trigger={
                <img src={props.photo}/>}>
                <div id='fP' className='fullPhoto'>
                    <img src={props.photo}></img>
                </div>
                </Popup>
            
        
        </>
    );
}

export default PhotoInPart;