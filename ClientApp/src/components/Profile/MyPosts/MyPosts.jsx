import React, { useEffect, useState } from 'react';
import './MyPosts.css';
import ProfilePartPost from './ProfilePartPost/ProfilePartPost';
import plus from "./../../../images/plus.svg"
import Popup from 'reactjs-popup';
import  axios  from 'axios';
import { jwtDecode } from 'jwt-decode';
import { useNavigate } from 'react-router-dom';
import PostInPart from "./ProfilePartPost/PostInPart/PostInPart";

const MyPosts = (props) => {

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
            params:{
                userId:us.id
            },
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
            props.updateNewPostImgInProfile(e.target.files[0]);
            setStyle('block');
        }    
    }
    
    let onPostMessageChange = (e) =>{
        props.updateNewPostMessageTextInProfile(e.target.value);
    } 

    let onPostTitleChange = (e) =>{
        props.updateNewPostTitleTextInProfile(e.target.value);
    } 

    let onPostTagsChange = (e) =>{
        props.updateNewPostTagTextInProfile(e.target.value);
    } 
//#endregion
    let onAddTag = () =>{
        props.addTagsInPostInProfile();
        //props.updateNewPostTagTextInProfile('');
    } 

    let onAddPost = () =>{
        if (props.newPostMessageText==''||props.newPostTitleText==''||uploadImage==undefined){
            alert('Проверьте заполненность названия поста, текста и фото!');
        }else{
            createPost();
            //props.addPostInProfile(props.fullName);
            props.updateNewPostTagTextInProfile('');
            props.updateNewPostTitleTextInProfile('');
            props.updateNewPostMessageTextInProfile('');
        }
        
    } 
    
    
    useEffect(()=>{
        getAllPosts();
    },[])
        let allPosts = [];

    return (
      <div className='myPostsInProfile'>
        <div className='titlePostsInProfile'>
          <p>Мои посты</p>            
        <Popup trigger={<img src={plus}></img>}>
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
        <div className="cardsWrapperInPostInProfile">
        {loadP?<div>Подождите, идёт загрузка...</div>:posts.map((post)=>{
                    //console.log(photo.slice(photo.indexOf('/')+1, photo.indexOf('.')));
            return(<PostInPart key={post.id} post={post}></PostInPart>)
        })}
        </div> 
      </div>
    );
}

export default MyPosts;