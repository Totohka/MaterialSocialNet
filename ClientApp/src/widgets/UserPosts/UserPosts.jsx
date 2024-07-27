import React, { useEffect, useState } from 'react';
import './UserPosts.css';
// import PostInPart from "./../../Profile/MyPosts/ProfilePartPost/PostInPart/PostInPart";
import plus from "./../../images/plus.svg"
import Popup from 'reactjs-popup';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom';
import ProfilePartPost from '../PostInPart/PostInPart';
import { useGetPostsQuery, useLazyGetPostsQuery, useLazyGetUserPostsQuery, useListNewsQuery } from '../../features/api/apiSlice';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { connectionNotif } from '../../app/helpers/signalRNotifications';
import { useSelector } from 'react-redux';

const UserPosts = () => {
    const navigate = useNavigate();
    let token = localStorage.getItem("token");
    const param = useParams();
    const id = Number(param['*']); 
    const [page, setPage] = useState(0);
    const [data, setData] = useState({id:id, page:page});
    const { data: postsInfo,
      isLoading,
      isSuccess,
      isError,
      error } = useGetPostsQuery(data);

      const user = useSelector(state=>state.user.userInfo);
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
          console.log(data);
          console.log(postsInfo);
          console.log(page);
        }
        if(isError){
            console.log(error);
            if (error.response.status==401){
                logOut();
            }
            else{
                alert(error.message);
            }
        }
      }
        
    },[isLoading, isSuccess, isError]);
    // const getUserPosts=async()=>{
    //   setLoadP(true);
    //   try{
    //       const response = await axios.get("http://25.32.11.98:8087/api/Post/All",{
    //           params:{
    //               userId:id
    //           },
    //           headers:{
    //               'Authorization':token
    //           }
    //       });
    //       console.log(response.data);
    //       setPosts(response.data.posts);
    //       //props.getAllPosts(response.data.posts);
  
    //   }catch(error){
    //       if (error.response) { // get response with a status code not in range 2xx
    //           console.log(error.response.data);
    //           console.log(error.response.status);
    //           console.log(error.response.headers);
    //         } else if (error.request) { // no response
    //           console.log(error.request);
    //           // instance of XMLHttpRequest in the browser
    //           // instance ofhttp.ClientRequest in node.js
    //         } else { // Something wrong in setting up the request
    //           console.log('Error', error.message);
    //         }
    //         console.log(error.config);
    //   }finally{
    //       setLoadP(false);
    //   }
    // }
    const downPage=()=>{
      setData({page:page-1, id:id});
      console.log(data);
      setPage(page-1);
  }
  const upPage=()=>{
      setData({page:page+1, id:id});
      console.log(data);
      setPage(page+1);
  }
    // useEffect(()=>{
    //     getUserPosts();
    // },[])
    return (
      <div className='myPostsInProfile'>
        <div className='titlePostsInProfile'>
          <p>Посты</p>            
        </div>  
        {isLoading?<></>:isSuccess?<div className="users_pages">
                {page+1>1?<button className="users_button" onClick={downPage}>&lt;</button>:<></>}
                {postsInfo.pageCount!=0?<div className="users_div">{page+1} из {postsInfo.pageCount}</div>:<></>}
                {page+1<postsInfo.pageCount?<button className="users_button" onClick={upPage}>&gt;</button>:<></>}
            </div>:<></>}
        <div className="cardsWrapperInPostInProfile">
          { isLoading?<>Загрузка...</>:isSuccess?postsInfo.posts==[]?<></>:postsInfo.posts.map((post)=>{
              return(<ProfilePartPost key={post.id} post={post}></ProfilePartPost>)
          }):<></>}
        </div> 
        {isLoading?<></>:isSuccess?<div className="users_pages">
                {page+1>1?<button className="users_button" onClick={downPage}>&lt;</button>:<></>}
                {postsInfo.pageCount!=0?<div className="users_div">{page+1} из {postsInfo.pageCount}</div>:<></>}
                {page+1<postsInfo.pageCount?<button className="users_button" onClick={upPage}>&gt;</button>:<></>}
            </div>:<></>}
      </div>
    );
}

export default UserPosts;