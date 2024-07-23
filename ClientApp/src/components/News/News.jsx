import React, { useEffect, useState } from "react";
import "./News.css";
import NewsPartPost from "./../Profile/MyPosts/ProfilePartPost/ProfilePartPost";
import Popup from "reactjs-popup";
import plus from "./../../images/plus.svg";
import  axios  from 'axios';
import { jwtDecode } from 'jwt-decode';
import { useNavigate } from 'react-router-dom';
import PostInPart from './../Profile/MyPosts/ProfilePartPost/PostInPart/PostInPart';
const News = (props) => {
  const navigate = useNavigate();
    let token = localStorage.getItem("token");
    let us = jwtDecode(token.slice(7,));
    const [fileName, setFile] = useState();
    const [uploadImage, setImage] = useState();
    const [style, setStyle] = useState('none');
    const [loadP, setLoadP] = useState(true);
    const [posts, setPosts] = useState(props.postsData);

    const getAllPosts = async() =>{
      setLoadP(true);
      try{
          const response = await axios.get("http://25.32.11.98:8087/api/Post/All",{
              headers:{
                  'Authorization':token
              }
          });
          console.log(response.data);
          setPosts(response.data.posts);
          props.getAllPosts(response.data.posts);
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
    const createPost = async()=>{
      setLoadP(true);
      console.log("create post")
      const formData = new FormData();
      formData.append("image", uploadImage);
      formData.append("user_id", us.id);
      formData.append("title", props.newPostTitleText);
      formData.append("text", props.newPostMessageText);
      formData.append("tags", props.newPostTagText);
      formData.append("date_create", new Date().toJSON());
      try{
          await axios.post("http://25.32.11.98:8087/api/Post",
                  formData,
                  {
                    headers:{
                      'Authorization':token,
                      'content-type': 'multipart/form-data'
                    }
                  })
          getAllPosts();
          //props.addPostInProfile(props.fullName);
      }catch (error) {
        alert(error.message);
        if (error.response.status==401){
          navigate('/login');
        }
      }finally{
        //setPosts(props.postsData);
        setLoadP(false);
         
      }
  }
//#region Changes
  let onPostImgChange = (e) => {
      if (!(e.target.files instanceof Blob)) {
          const file = e.target.value;
          setFile(file);
          setImage(e.target.files[0])
          props.updateNewPostImgInNews(e.target.files[0]);
          setStyle('block');
      }}
  let onPostMessageChange = (e) =>{
      props.updateNewPostMessageTextInNews(e.target.value);
  } 

  let onPostTitleChange = (e) =>{
      props.updateNewPostTitleTextInNews(e.target.value);
  } 

  let onPostTagsChange = (e) =>{
      props.updateNewPostTagTextInNews(e.target.value);
  } 
//#endregion
let onAddTag = () =>{
    props.addTagsInPostInNews();
    //props.updateNewPostTagTextInProfile('');
} 

let onAddPost = () =>{
    if (props.newPostMessageText==''||props.newPostTitleText==''||uploadImage==undefined){
        alert('Проверьте заполненность названия поста, текста и фото!');
    }else{
        createPost();
        //props.addPostInProfile(props.fullName);
        props.updateNewPostTagTextInNews('');
        props.updateNewPostTitleTextInNews('');
        props.updateNewPostMessageTextInNews('');
    }
    
} 
useEffect(()=>{
  getAllPosts();
},[])

  // let allPosts = [];
  // let length = props.postsData.length;
  // let count = Math.ceil(length / 2);
  // let mod = length % 2;
  // let partPosts;

  // for (let i = 0; i < count; i++) {
  //   if (i === count - 1) {
  //     switch (mod) {
  //       case 0:
  //         partPosts = (
  //           <NewsPartPost
  //             post1={props.postsData[0 + 2 * i]}
  //             post2={props.postsData[1 + 2 * i]}
  //           />
  //         );
  //         break;
  //       case 1:
  //         partPosts = <NewsPartPost post1={props.postsData[0 + 2 * i]} />;
  //         break;
  //     }
  //     allPosts.push(partPosts);
  //   } else {
  //     partPosts = (
  //       <NewsPartPost
  //         post1={props.postsData[0 + 2 * i]}
  //         post2={props.postsData[1 + 2 * i]}
  //       />
  //     );
  //     allPosts.push(partPosts);
  //   }
  // }

  return (
    <div className="newsInSocialNetwork">
      <div className="postsInNews">
        <div className="cardsWrapperInPostInNews">
        {loadP?<div>Подождите, идёт загрузка...</div>:posts.map((post)=>{
                    //console.log(photo.slice(photo.indexOf('/')+1, photo.indexOf('.')));
            return(<PostInPart key={post.id} post={post}></PostInPart>)
        })}
      </div>
      </div>
      
      <Popup
        trigger={
          <div className="formForCreateNewPost">
            <img src={plus}></img>
            <p>Создать новый пост</p>
          </div>
        }
      >
        <div className='formForCreateNewPostInProfile'>
            <p>Новый пост</p>
            <div class="file-input">
                <input type="file" id='inputForDownloadPost' onChange={onPostImgChange} /> 
            </div>
            <div className='flexForDownloadPost'>
                <div className='downloadPosts'>
                    <label for="inputForDownloadPost">Добавить фотографию к посту</label>
                </div>
                <span style={{display: style}}>{fileName}</span>
            </div>
            <div className='spaceForTitleInCreateNewPost'>
                <input type='text' placeholder='Название поста' value={props.newPostTitleText} onChange={onPostTitleChange} />
            </div>
            <div className='spaceForMessageInCreateNewPost'>
                <textarea type='text' placeholder='Содержание поста' value={props.newPostMessageText} onChange={onPostMessageChange}  />
            </div>
            <div className='spaceForCreateNewTagsInCreateNewPost'>
                <input id="input_post_tag" type='text' placeholder='Новый тег' value={props.newPostTagText} onChange={onPostTagsChange}/> 
                <input id="add_post_tag" type='button' value='Создать тег' onClick={onAddTag}/>
            </div>
            <p className='spaceForTagsInCreateNewPost'>
                {props.newTagsInPost}
            </p>
            <div className='spaceForCreateNewPostButtonInProfile'>
                <input type='button' value='Опубликовать пост' className='createNewPostButtonInProfile' onClick={onAddPost}/>
            </div>
          </div>
      </Popup>
    </div>
  );
};

export default News;
