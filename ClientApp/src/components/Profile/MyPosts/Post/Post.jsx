import React, { useEffect, useState } from 'react';
import './Post.css';
import { useParams } from 'react-router-dom';
import standartImg from './../../../../images/standartLogo.jpg';
import edit from './../../../../images/edit.svg';
import bin from './../../../../images/bin.svg';
import dislike from './../../../../images/dislike.svg';
import like from './../../../../images/like.svg';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import {Popup} from 'reactjs-popup';
import { useNavigate } from 'react-router-dom';

const Post = (props) => {
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
  const [style, setStyle] = useState('none');
  const [loadP, setLoadP] = useState(true);
  const [post, setPost] = useState([]);
  //#region EditPost  
  let onPostImgChange = (e) => {
    if (!(e.target.files instanceof Blob)) {
        const file = e.target.value;
        setFile(file);
        setImage(e.target.files[0])
        //props.updateNewPostImgInProfile(e.target.files[0]);
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

  let onAddTag = () =>{
    setTags(newtag);
    setTag();
  } 
  const sendPost = async() => {
    setLoadP(true);
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
    try{
        await axios.put("http://25.32.11.98:8087/api/Post",
                formData,
                {
                  headers:{
                    'Authorization':token,
                    'content-type': 'multipart/form-data'
                  }
                })
        getPost();
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
  let onAddPost = () =>{
    if (newtext==''||newtitle==''){
      alert('Проверьте заполненность названия поста и текста!');
  }else{sendPost();}
  } 
  //#endregion
    const getPost = async() =>{
        setLoadP(true);
    try{
        const response = await axios.get("http://25.32.11.98:8087/api/Post",{
            params:{
                id:id
            },
            headers:{
                'Authorization':token
            }
        });
        console.log(response.data);
        setPost(response.data);
        setTitle(response.data.title);
        setFile(response.data.image);
        setImage();
        if(response.data.typeReaction!='') document.getElementById(response.data.typeReaction.toLowerCase()).classList.add("disabled_one");
        setText(response.data.text);
        setTags(response.data.tags);
        setCount(response.data.rating)

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
  function PlusCount(e){
    if (document.getElementById('like').classList.contains("disabled_one")){
      const data = {
        entity_id: id, 
        type_reaction_id: 1, //likeId = 1
        user_id: us.id
      };
      const headers = { 
        'Authorization': token
      };
      axios.delete('http://25.32.11.98:8088/ReactionPost',{data: data, headers: headers}).then(
        () => {
          setCount(count-1);
          document.getElementById('like').classList.remove("disabled_one");
        }
      ).catch(error => {
        console.error('There was an error!', error);
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
      const headers = { 
        'Authorization': token
      };
      axios.put('http://25.32.11.98:8088/ReactionPost', data, {headers:headers}).then(
        () => {
          setCount(count+2);
          document.getElementById('like').classList.add("disabled_one");
          document.getElementById('dislike').classList.remove("disabled_one");
        }
      ).catch(error => {
        console.error('There was an error!', error);
      })
    }
    else{
      const data = {
        entity_id: id, 
        type_reaction_id: 1, //likeId = 1
        user_id: us.id
      };
      const headers = { 
        'Authorization': token
      };
      axios.post('http://25.32.11.98:8088/ReactionPost', data, {headers:headers}).then(
        () => {
          setCount(count+1);
          document.getElementById('like').classList.add("disabled_one");
        }
      ).catch(error => {
        console.error('There was an error!', error);
      })
    }
  }
  function MinusCount(e){
    if (document.getElementById('dislike').classList.contains("disabled_one")){
      const data = {
        entity_id: id, 
        type_reaction_id: 2, //likeId = 1
        user_id: us.id
      };
      const header = { 
        'Authorization': token,
      };
      axios.delete('http://25.32.11.98:8088/ReactionPost',{data: data, headers: header}).then(
        () => {
          setCount(count+1);
          document.getElementById('dislike').classList.remove("disabled_one");
        }
      ).catch(error => {
        console.error('There was an error!', error);
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
      const headers = { 
        'Authorization': token,
      };
      axios.put('http://25.32.11.98:8088/ReactionPost', data, {headers:headers}).then(
        () => {
          setCount(count-2);
          document.getElementById('dislike').classList.add("disabled_one");
          document.getElementById('like').classList.remove("disabled_one");
        }
      ).catch(error => {
        console.error('There was an error!', error);
      })
    }
    else{
      const data = {
        entity_id: id, 
        type_reaction_id: 2, //likeId = 1
        user_id: us.id
      };
      const headers = { 
        'Authorization': token,
      };
      axios.post('http://25.32.11.98:8088/ReactionPost', data, {headers:headers}).then(
        () => {
          setCount(count-1);
          document.getElementById('dislike').classList.add("disabled_one");
        }
      ).catch(error => {
        console.error('There was an error!', error);
      })
    }
  }
  const deletePost = async() =>{
    setLoadP(true);
    try{
        await axios.delete("http://25.32.11.98:8087/api/Post", 
        {
            params:{
                userId:us.id,
                postId:id
            },
            headers:{
                'Authorization':token
            }
        }).then(()=>navigate('/news'))
    }catch(error){
      alert('Что-то пошло не так при удалении!');
        if (error.response) { // get response with a status code not in range 2xx
          if(error.response.status==401){
            navigate('/login');
          }
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
  const tryDeletePost = () =>{
      deletePost();
  }
  useEffect(()=>{
    getPost();
  },[])
  
  //const post = props.postsData.find((element) => element.id === id);
  let postImg = post.image === '' ? standartImg : "http://25.32.11.98:8087/PostsData/"+post.image;
  const tags = <div>{post.tags}</div>
  return (
    
    <div className="postPage">
      {loadP?<div>Подождите, пост загружается...</div>:
      <>
      <div className='postInAll'>
          <div className='postDescription'>
            <div className='fullNameInPostInAll'>{post.firstNameUser+' '+post.lastNameUser}</div>
            <div className='titleInPostInAll'>{post.title}</div>
            <div className='dateInPostInAll'>{post.dateCreate.slice(0,10).split("-").reverse().join(".")}</div>
          </div>
          <div className='imgInPostInAll' style={{backgroundImage: `url(${postImg})`}}>
          </div>
          <div className='messageInPostInAll'>
            {post.text}
          </div>
          <div className='tagsInPostInAll'>
            {tags}
          </div>
      </div>
      <div className='actionsWithPost'>
        {us.id==post.userId?<div className='postActions'>
          
          <Popup trigger={<div className='actionPost'>
            <img src={edit}></img>
            <p>Редактировать</p>
          </div>}>
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
                {newtags}
            </p>
            <div className='spaceForCreateNewPostButtonInProfile'>
                <input type='button' value='Сохранить изменения' className='createNewPostButtonInProfile' onClick={onAddPost}/>
            </div>
          </div>
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
        <div className='comments'>
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
        </div>
        <div className='newComment'>
          <textarea placeholder='Введите текст'></textarea>
          <input type='button' value='Отправить'></input>
        </div>
      </div>
      </>
      }
      
    </div>
    
  );
}

export default Post;