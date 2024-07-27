import React, { useEffect, useState } from "react";
import "./News.css";
import Popup from "reactjs-popup";
import plus from "./../../images/plus.svg";
import { jwtDecode } from 'jwt-decode';
import { useNavigate } from 'react-router-dom';
import ProfilePartPost from "../../widgets/PostInPart/PostInPart";
import { useAddPostMutation, useListNewsQuery } from "../../features/api/apiSlice";
import { useSelector } from "react-redux";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";
const News = () => {
  const navigate = useNavigate();
    let token = localStorage.getItem("token");
    const us = useSelector(state=>state.user.userInfo);
    const [fileName, setFile] = useState();
    const [uploadImage, setImage] = useState();
    const [style, setStyle] = useState('none');
    const [title, setTitle] = useState();
    const [text, setText] = useState();
    const [tags, setTags] = useState([]);
    const [tag, setTag] = useState([]);
    const [page, setPage] = useState(0);
    const [pageCount, setPageCount] = useState(1);
    const { data: postsInfo,
      isLoading,
      isSuccess,
      isError,
      error } = useListNewsQuery(page);
    const [addPost, {error:AddPostError}] = useAddPostMutation();

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
          // console.log(postsInfo);
          // console.log(isLoading);
          setPageCount(postsInfo.pageCount);
          // console.log(postsInfo.posts);
          // console.log(posts);
          }
          if(isError){
              console.log(error);
              if (error.status==401){
                  logOut();
              }
              else{
                  alert(error.message);
              }
          }else if (AddPostError){
              console.log(AddPostError);
              if (AddPostError.response.status==401){
                  logOut();
              }
              else{
                  alert(AddPostError.message);
              }
          }
      }
  },[isLoading, isSuccess, isError, AddPostError]);
const createPost = async()=>{

  console.log("create post")
  const formData = new FormData();
  formData.append("image", uploadImage);
  formData.append("user_id", us.id);
  formData.append("title", title);
  formData.append("text", text);
  formData.append("tags", tags);
  formData.append("date_create", new Date().toJSON());
  addPost({formData, token});
}
//#region Changes
  let onPostImgChange = (e) => {
    if (!(e.target.files instanceof Blob)) {
        const file = e.target.value;
        setFile(file);
        setImage(e.target.files[0]);
        setStyle('block');
    }
  }
  let onPostMessageChange = (e) =>{
      setText(e.target.value);
  } 

  let onPostTitleChange = (e) =>{
    setTitle(e.target.value);
  } 

  let onPostTagsChange = (e) =>{
    setTag(e.target.value);
    
  } 
//#endregion
let onAddTag = () =>{
  setTags([...tags, tag]);
  setTag('');
} 

let onAddPost = () =>{
    if (text=='' || title=='' || uploadImage==undefined){
        alert('Проверьте заполненность названия поста, текста и фото!');
    }else{
        createPost();
    }
}


  return (
    <div className="newsInSocialNetwork">
      <div className="postsInNews">
        <div className="pages">
          {page+1>1?<button className="news_pages_button" onClick={()=>setPage(page-1)}>&lt;</button>:<></>}
          <div className="news_pages_div">{page+1} из {pageCount}</div>
          {page+1<pageCount?<button className="news_pages_button" onClick={()=>setPage(page+1)}>&gt;</button>:<></>}
        </div>

        <div className="cardsWrapperInPostInNews">
        { isLoading?<>Загрузка...</>:isSuccess?postsInfo.posts==[]?<></>:postsInfo.posts.map((post)=>{
            return(<ProfilePartPost key={post.id} post={post}></ProfilePartPost>)
        }):<></>}
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
        {close => (
          <div className='formForCreateNewPostInProfile'>
            <p>Новый пост</p>
            <div class="file-input">
                <input type="file" id='inputForDownloadPost' onChange={onPostImgChange} accept=".jpg"/> 
            </div>
            <div className='flexForDownloadPost'>
                <div className='downloadPosts'>
                    <label for="inputForDownloadPost">Добавить фотографию к посту</label>
                </div>
                <span style={{display: style}}>{fileName}</span>
            </div>
            <div className='spaceForTitleInCreateNewPost'>
                <input type='text' placeholder='Название поста' value={title} onChange={onPostTitleChange} />
            </div>
            <div className='spaceForMessageInCreateNewPost'>
                <textarea type='text' placeholder='Содержание поста' value={text} onChange={onPostMessageChange}  />
            </div>
            <div className='spaceForCreateNewTagsInCreateNewPost'>
                <input id="input_post_tag" type='text' placeholder='Новый тег' value={tag} onChange={onPostTagsChange}/> 
                <input id="add_post_tag" type='button' value='Создать тег' onClick={onAddTag}/>
            </div>
            <p className='spaceForTagsInCreateNewPost'>
                {tags}
            </p>
            <div className='spaceForCreateNewPostButtonInProfile'>
                <input type='button' value='Опубликовать пост' className='createNewPostButtonInProfile' onClick={()=>{onAddPost(); close()}}/>
            </div>
          </div>
        )}
        
      </Popup>
    </div>
  );
};

export default News;
