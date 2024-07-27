import React, { useEffect, useState } from 'react';
import './Post.css';
import { useParams } from 'react-router-dom';
import standartImg from './../../images/standartLogo.jpg';
import edit from './../../images/edit.svg';
import bin from './../../images/bin.svg';
import dislike from './../../images/dislike.svg';
import like from './../../images/like.svg';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import {Popup} from 'reactjs-popup';
import { useNavigate } from 'react-router-dom';
import { useAddReactionMutation, useDeletePostMutation, useDeleteReactionMutation, useEditPostMutation, useEditReactionMutation, useGetPostQuery, useLazyGetPostQuery, useLazyGetPostsQuery } from './../../features/api/apiSlice';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import Comments from '../../widgets/Comments/Comments';

const Post = () => {
  const navigate = useNavigate();
  const param = useParams();
  const id = Number(param['*']); 
  const [count, setCount] = useState(0);
  let token = localStorage.getItem("token");
  let us = jwtDecode(token.slice(7,));

  const [newfileName, setFile] = useState();
  const [newtitle, setTitle] = useState();
  const [newtext, setText] = useState();
  const [newtags, setTags] = useState();
  const [newtag, setTag] = useState();
  const [uploadImage, setImage] = useState();

  const [editPost, {error:EditPostError}] = useEditPostMutation();
  const [deletePost, {error:DeletePostError}] = useDeletePostMutation();
  const [deleteReaction, {error:DeleteReactionError}] = useDeleteReactionMutation();
  const [addReaction, {error:AddReactionError}] = useAddReactionMutation();
  const [editReaction, {error:EditReactionError}] = useEditReactionMutation();

  const { data: post,
    isLoading,
    isSuccess,
    isError,
    error } = useGetPostQuery({entity_id:id});

  const [getPost] = useLazyGetPostQuery();

  const logOut = () =>{
    localStorage.removeItem('token');
    if (connection.state !== HubConnectionState.Disconnected) {
        connection.stop()
          .then(() => console.log("Connection stoped"))
          .catch((err) => console.error(err.toString()));
      }
    navigate('/');
}
  useEffect(()=>{
    if(!isLoading){
        if(isSuccess){
        console.log(post);
        console.log(isLoading);
        setTags(post.tags);
        setText(post.text);
        setTitle(post.title);
        setCount(post.rating);
        if(post.typeReaction!='') document.getElementById(post.typeReaction.toLowerCase()).classList.add("disabled_one");
        }
        if(isError){
            console.log(error);
            if (error.status==401){
                logOut();
            }
            else{
                alert(error.message);
            }
        }else if (EditPostError){
          console.log(EditPostError);
          if (EditPostError.response.status==401){
              logOut();
          }
          else{
              alert(EditPostError.message);
          }
        }
        else if (DeletePostError){
          console.log(DeletePostError);
          if (DeletePostError.response.status==401){
              logOut();
          }
          else{
              alert(DeletePostError.message);
          }
        }
        else if (AddReactionError){
          console.log(AddReactionError);
          if (AddReactionError.response.status==401){
              logOut();
          }
          else{
              alert(AddReactionError.message);
          }
        }
        else if (DeleteReactionError){
          console.log(DeleteReactionError);
          if (DeleteReactionError.response.status==401){
              logOut();
          }
          else{
              alert(DeleteReactionError.message);
          }
        }
        else if (EditReactionError){
          console.log(EditReactionError);
          if (EditReactionError.response.status==401){
              logOut();
          }
          else{
              alert(EditReactionError.message);
          }
        }
    }
},[isLoading, isSuccess, isError, EditPostError, DeletePostError, 
  EditReactionError, DeleteReactionError, AddReactionError]);
  //#region EditPost  
  let onPostImgChange = (e) => {
    if (!(e.target.files instanceof Blob)) {
        const file = e.target.value;
        setFile(file);
        setImage(e.target.files[0]);
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

  let onAddTag = () =>{
    setTags([...newtags, newtag]);
    setTag();
  } 
  const sendPost = async() => {
    console.log("send post")
    const formData = new FormData();
    formData.append("id", post.id);
    if (uploadImage!=undefined){
      formData.append("image", uploadImage);
    }
    formData.append("user_id", us.id);
    formData.append("title", newtitle);
    formData.append("text", newtext);
    formData.append("tags", newtags);
    await editPost({formData:formData, entity_id:id}).then(()=>{
      // getPost()
    }).catch((error)=>{
        alert(error.message);
      if (error.response.status==401){
        navigate('/login');
      }
    })
  }
  let onAddPost = () =>{
    if (newtext==''||newtitle==''){
      alert('Проверьте заполненность названия поста и текста!');
  }else{sendPost();}
  } 
  //#endregion

  //#region Reactions
  async function PlusCount(e){
    if (document.getElementById('like').classList.contains("disabled_one")){
      const data = {
        entity_id: id, 
        type_reaction_id: 1, //likeId = 1
        user_id: us.id
      };
      await deleteReaction(data).then(()=>{
        setCount(count-1);
        document.getElementById('like').classList.remove("disabled_one");
        // getPost({id, token})
    }).catch((error)=>{
        alert(error.message);
      if (error.response.status==401){
        navigate('/login');
      }
    })
    }
    else if (document.getElementById('dislike').classList.contains("disabled_one"))
    {
      const data = {
        entity_id: id, 
        type_reaction_id: 1, //likeId = 1
        type_reaction_old_id: 2, //dislikeId = 2
        user_id: us.id
      };
      await editReaction(data).then(()=>{
        setCount(count+2);
        document.getElementById('like').classList.add("disabled_one");
        document.getElementById('dislike').classList.remove("disabled_one");
        // getPost({id, token})
    }).catch((error)=>{
        alert(error.message);
      if (error.response.status==401){
        navigate('/login');
      }
    })
    }
    else{
      const data = {
        entity_id: id, 
        type_reaction_id: 1, //likeId = 1
        user_id: us.id
      };
      await addReaction(data).then(async()=>{
          setCount(count+1);
          // await getPost({id, token});
          document.getElementById('like').classList.add("disabled_one");
      }).catch((error)=>{
          alert(error.message);
        if (error.response.status==401){
          navigate('/login');
        }
      })
    }
  }

  async function MinusCount(e){
    if (document.getElementById('dislike').classList.contains("disabled_one")){
      const data = {
        entity_id: id, 
        type_reaction_id: 2, //likeId = 1
        user_id: us.id
      };
      await deleteReaction(data).then(()=>{
        setCount(count+1);
        document.getElementById('dislike').classList.remove("disabled_one");
        // getPost({id, token})
    }).catch((error)=>{
        alert(error.message);
      if (error.response.status==401){
        navigate('/login');
      }
    })
    }
    else if (document.getElementById('like').classList.contains("disabled_one"))
    {
      const data = {
        entity_id: id, 
        type_reaction_id: 2, //likeId = 1
        type_reaction_old_id: 1, //dislikeId = 2
        user_id: us.id
      };
      await editReaction(data).then(()=>{
        setCount(count-2);
        document.getElementById('dislike').classList.add("disabled_one");
        document.getElementById('like').classList.remove("disabled_one");
        // getPost({id, token})
    }).catch((error)=>{
        alert(error.message);
      if (error.response.status==401){
        navigate('/login');
      }
    })
    }
    else{
      const data = {
        entity_id: id, 
        type_reaction_id: 2, //likeId = 1
        user_id: us.id
      };
      await addReaction(data).then(async()=>{
          setCount(count-1);
          document.getElementById('dislike').classList.add("disabled_one");
          // await getPost({id, token});
      }).catch((error)=>{
          alert(error.message);
        if (error.response.status==401){
          navigate('/login');
        }
      })
    }
  }
  //#endregion
  
  const tryDeletePost = () =>{
      deletePost({id: us.id, entity_id:id}).finally(()=>{
        if(!DeletePostError){
          navigate('/profile');
        }
      })
  }
  

  return (
    
    <div className="postPage">
      {isLoading?<>Загрузка...</>:isSuccess?post==null?<></>:
      <>
      <div className='postInAll'>
          <div className='postDescription'>
            <div className='fullNameInPostInAll'>{post.firstNameUser+' '+post.lastNameUser}</div>
            <div className='titleInPostInAll'>{post.title}</div>
            <div className='dateInPostInAll'>{post.dateCreate.slice(0,10).split("-").reverse().join(".")}</div>
          </div>
          <div className='imgInPostInAll' style={{backgroundImage: post.image === '' ? `url(${standartImg })`: `url(http://25.32.11.98:8087/PostsData/${post.image})`}}>
          </div>
          <div className='messageInPostInAll'>
            {post.text}
          </div>
          <div className='tagsInPostInAll'>
            {post.tags.split(',').map((e)=><div>{e}</div>)}
          </div>
      </div>
      <div className='actionsWithPost'>
        {us.id==post.userId?<div className='postActions'>
          
          <Popup trigger={<div className='actionPost'>
            <img src={edit}></img>
            <p>Редактировать</p>
          </div>}>
            {close => (
              <div className='formForCreateNewPostInProfile'>
            <div class="file-input">
                <input type="file" id='inputForEditPost' onChange={onPostImgChange} required/> 
            </div>
            <div className='flexForDownloadPost'>
                <div className='downloadPosts'>
                    <label for="inputForEditPost">Изменить фотографию к посту</label>
                </div>
                <span >{newfileName}</span>
            </div>
            <div className='spaceForTitleInCreateNewPost'>
                <input type='text' placeholder='Название поста' value={newtitle} onChange={onPostTitleChange}  required/>
            </div>
            <div className='spaceForMessageInCreateNewPost'>
                <textarea type='text' placeholder='Новый пост' value={newtext} onChange={onPostMessageChange} required/>
            </div>
            <div className='spaceForCreateNewTagsInCreateNewPost'>
                <input id="input_post_tag" type='text' placeholder='Новый тег' value={newtag} onChange={onPostTagsChange} /> 
                <input id="add_post_tag" type='button' value='Создать тег' onClick={onAddTag}/>
            </div>
            <p className='spaceForTagsInCreateNewPost'>
                {Array(newtags).join('')}
            </p>
            <div className='spaceForCreateNewPostButtonInProfile'>
                <input type='button' value='Сохранить изменения' className='createNewPostButtonInProfile' onClick={()=>{ 
                  onAddPost();
                  close();                  
                  }}/>
            </div>
          </div>
            )}
          
        </Popup>

          <Popup trigger={<div className='actionPost'>
                            <img src={bin}></img>
                            <p>Удалить</p>
                          </div>}
          modal>          
          {close => (<div className="tryDeletePost">
                <p>Вы уверены, что хотите удалить данный пост?</p>
                <div>
                  <button onClick={tryDeletePost}>Да</button>
                  <button onClick={() => {
              console.log('modal closed ');
              close();
            }}>Нет</button>
                </div>
          </div>)}
        </Popup>
        </div>:<></>}
        <div className='rating'>
            Рейтинг: {count}
          </div>
        <div className='score'>
          <div className="actionPost" onClick={PlusCount} id='like'>
            <img src={like}></img>
            <p>Нравится</p>
          </div>
          <div className="actionPost" onClick={MinusCount} id='dislike'>
            <img src={dislike}></img>
            <p>Не нравится</p>
          </div>
        </div>
        <Comments/>
        {/* <div className='comments'>
          <p>Комментарии</p>
          <div className='comment'>
            <div style={{backgroundImage: `url(${standartImg})`}}></div>
            <p>Очень интересная работа! Читал и наслаждался. Кто-нибудь знает, у этого автора есть другие соц.сети?</p>
          </div>
          <div className='comment'>
            <div style={{backgroundImage: `url(${standartImg})`}}></div>
            <p>Нет, он пользуется только SocialNet</p>
          </div>
          <div className='comment'>
            <div style={{backgroundImage: `url(${standartImg})`}}></div>
            <p>Придётся остаться...</p>
          </div>
          {/* {comments} */}
        {/* </div> */} 
        {/* <div className='newComment'>
          <textarea placeholder='Введите текст'></textarea>
          <input type='button' value='Отправить'></input>
        </div> */}
      </div>
      </>
      :<></>}
      
    </div>
    
  );
}

export default Post;