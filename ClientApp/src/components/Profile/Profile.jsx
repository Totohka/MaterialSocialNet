import {React, useEffect} from 'react';
import './Profile.css';
import MyPostsConteiner from './MyPosts/MyPostsContainer';
import ProfileInfoContainer from './ProfileInfo/ProfileInfoContainer';
import GalleryPhotosInProfileContainer from './GalleryPhotosInProfile/GalleyPhotosInProfileContainer';
import { jwtDecode } from 'jwt-decode';

const Profile = (props) => {

  useEffect(() => {
    const token = localStorage.getItem("token").slice(7,);
    const user = jwtDecode(token);
    props.setStateUser(user);
  }, []);

  return (
    <>
      <div className='profile'>
      <ProfileInfoContainer />  
      <GalleryPhotosInProfileContainer />
      <MyPostsConteiner />    
      </div>
      {/* <div className='friendsInfo'>
        <SubscribeCount/>
        <FriendsInfo/>
      </div> */}
    </>
    
    );
}

export default Profile;