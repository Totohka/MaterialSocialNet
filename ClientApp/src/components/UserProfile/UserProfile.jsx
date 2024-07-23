import {React, useEffect, useState} from 'react';
import './UserProfile.css';
// import MyPostsConteiner from './MyPosts/MyPostsContainer';
// import ProfileInfoContainer from './ProfileInfo/ProfileInfoContainer';
// import GalleryPhotosInProfileContainer from './GalleryPhotosInProfile/GalleyPhotosInProfileContainer';
import { jwtDecode } from 'jwt-decode';
import UserProfileInfo from './UserProfileInfo/UserProfileInfo';
import UserGalleryInProfile from './UserGalleryInProfile/UserGalleryInProfile';
import UserPosts from './UserPosts/UserPosts';
import { useNavigate, useParams } from 'react-router-dom';
import  axios  from 'axios';

const UserProfile = () => {
  const navigateU = useNavigate();
  const param = useParams();
  const id = Number(param['*']); 
 
  return (
    <>
      <div className='profile'>
        <>
          <UserProfileInfo id={id}/>  
          <UserGalleryInProfile/>
          <UserPosts id={id}/>    
        </>
      </div>
      {/* <div className='friendsInfo'>
        <SubscribeCount/>
        <FriendsInfo/>
      </div> */}
    </>
    
    );
}

export default UserProfile;