import React, { useEffect, useState } from 'react';
import './UserPosts.css';
import PostInPart from "./../../Profile/MyPosts/ProfilePartPost/PostInPart/PostInPart";
import plus from "./../../../images/plus.svg"
import Popup from 'reactjs-popup';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const UserPosts = ({id}) => {
    const navigate = useNavigate();
    let token = localStorage.getItem("token");
    //let us = jwtDecode(token.slice(7,));
    const [loadP, setLoadP] = useState(true);
    const [posts, setPosts] = useState([]);

    const getUserPosts=async()=>{
      setLoadP(true);
      try{
          const response = await axios.get("http://25.32.11.98:8087/api/Post/All",{
              params:{
                  userId:id
              },
              headers:{
                  'Authorization':token
              }
          });
          console.log(response.data);
          setPosts(response.data.posts);
          //props.getAllPosts(response.data.posts);
  
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
          setLoadP(false);
      }
    }
    useEffect(()=>{
        getUserPosts();
    },[])
    return (
      <div className='myPostsInProfile'>
        <div className='titlePostsInProfile'>
          <p>Посты</p>            
        </div>  
        <div className="cardsWrapperInPostInProfile">
        {loadP?<div>Подождите, идёт загрузка...</div>:posts.map((post)=>{
                    //console.log(photo.slice(photo.indexOf('/')+1, photo.indexOf('.')));
            return(<PostInPart key={post.id} post={post}></PostInPart>)
        })}
        </div> 
      </div>
    );
}

export default UserPosts;