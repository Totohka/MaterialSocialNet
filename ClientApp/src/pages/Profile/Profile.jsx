import {React, useCallback, useEffect} from 'react';
import './Profile.css';
import { jwtDecode } from 'jwt-decode';
import { useSelector } from 'react-redux';
import ProfileInfo from '../../widgets/ProfileInfo/ProfileInfo';
import { setUser } from '../../features/auth/userSlice';
import GalleryPhotosInProfile from '../../widgets/GalleryPhotosInProfile/GalleryPhotosInProfile';
import MyPosts from '../../widgets/MyPosts/MyPosts';
import useSignalR from '../../app/useSignalR';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';

const Profile = () => {
// const selectUserInfo = useSelector(state=>state.user.userInfo);
//   useEffect(() => {
//     const token = localStorage.getItem("token").slice(7,);
//     console.log(selectUserInfo);
//     if(token&&selectUserInfo==null){
//       console.log("set!")
//       setUser(token);
//     }
//     const user = jwtDecode(token);
    
//   }, []);
// const {connection} = useSignalR();

// const handleClick = useCallback(() => {
//   console.log("helo!");
//   if (connection.state !== HubConnectionState.Connected) {
//     connection
//       .start()
//       .then(() => console.log("Connection started"))
//       .catch((err) => console.error(err.toString()));
//   }
// }, []);
// const stopClick = useCallback(()=>{
//   console.log("stop!");
//   if (connection.state !== HubConnectionState.Disconnected) {
//     connection.stop()
//       .then(() => console.log("Connection stoped"))
//       .catch((err) => console.error(err.toString()));
//   }
// })
  return (
    <>
      <div className='profile'>
        {/* <button onClick={handleClick}>Connect</button>
        <button onClick={stopClick}>Disconnect</button> */}

       <ProfileInfo />  
      <GalleryPhotosInProfile />
      <MyPosts />    
      </div>
      {/* <div className='friendsInfo'>
        <SubscribeCount/>
        <FriendsInfo/>
      </div> */}
    </>
    
    );
}

export default Profile;