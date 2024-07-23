import React from 'react';
import standartImgPost from './../../../../../images/standartLogo.jpg'
import { useNavigate } from 'react-router-dom';
import edit from './../../../../../images/edit.svg';
import './PostInPart.css';

const ProfilePartPost = (props) => {
    let imgForBackgroundImage = props.post.image === '' ? standartImgPost : "http://25.32.11.98:8087/PostsData/"+props.post.image;
    let id = props.post.id;
    //let tagsStr = props.post.join();
    let tags = <div className='tag'>{props.post.tags}</div>
    const navigate = useNavigate();

    return (
        <div className="cardGridSpaceInProfile" onClick={() => navigate(`/post/${id}`)}>
            <div className="imageInNavInProfile">
                <img src={imgForBackgroundImage}/>
            </div>
            <div className='textInPostInProfile'>
                <div className='flexForDateAndAuthor'>
                    <div className="dateInPostInProfile"><p>{props.post.dateCreate.slice(0,10).split("-").reverse().join(".")}</p></div>
                    <div className='authorInPostInProfile'><p>{props.post.firstNameUser+' '+props.post.lastNameUser}</p></div>
                </div>
                <div className='titleInPostInProfile'>{props.post.title}</div>
                {/* <div className='smallMessageInPostInProfile'>{props.post.smallMessage}</div> */}
                <div className="tagsInPostInProfile">
                    {tags}
                </div>
            </div>
        </div>       
               
    );
}

export default ProfilePartPost;