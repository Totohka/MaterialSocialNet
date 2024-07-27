import React, { useEffect, useState } from 'react';
import './MyPosts.css';
import plus from "./../../images/plus.svg"
import Popup from 'reactjs-popup';
import { jwtDecode } from 'jwt-decode';
import { useNavigate } from 'react-router-dom';
import PostInPart from "../PostInPart/PostInPart";
import { useAddPostMutation, useGetPostsQuery, useLazyGetPostsQuery } from '../../features/api/apiSlice';
import { setPosts } from '../../features/user/userPostsSlice';
import { useDispatch, useSelector } from 'react-redux';
import { connection } from '../../app/helpers/withSignalR';
import { HubConnectionState } from 'redux-signalr';
import { connectionNotif } from '../../app/helpers/signalRNotifications';

const MyPosts = () => {
    const user = useSelector(state=>state.user.userInfo);
    const navigate = useNavigate();
    let token = localStorage.getItem("token");
    const id = user.id;
    // let us = jwtDecode(token.slice(7,));
    const [fileName, setFile] = useState();
    const [uploadImage, setImage] = useState();
    const [style, setStyle] = useState('none');
    const [title, setTitle] = useState();
    const [text, setText] = useState();
    const [tags, setTags] = useState([]);
    const [tag, setTag] = useState([]);
    const [page, setPage] = useState(0);
    const [data, setData] = useState({id:id, page:page});
    // const [getPosts] = useLazyGetPostsQuery();

    const { data: postsInfo,
        isLoading,
        isSuccess,
        isError,
        error } = useGetPostsQuery(data);
    const [addPost, {error:AddPostError}] = useAddPostMutation();
    const dispatch = useDispatch();
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
            dispatch(setPosts(postsInfo.posts));
            console.log(postsInfo.posts);
            console.log(isLoading);
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
        formData.append("user_id", id);
        formData.append("title", title);
        formData.append("text", text);
        formData.append("tags", tags);
        formData.append("date_create", new Date().toJSON());
        addPost({formData, token}).then(async()=>{
            //await getPosts({id, token});
        }).catch((error)=>{
            alert(error.message);
          if (error.response.status==401){
            logOut();
          }
        });
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
    } 

    let onAddPost = () =>{
        if (text==''||title==''||uploadImage==undefined){
            alert('Проверьте заполненность названия поста, текста и фото!');
        }else{
            createPost();
        }
        
    } 
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

    return (
      <div className='myPostsInProfile'>
        <div className='titlePostsInProfile'>
          <p>Мои посты</p>            
        <Popup trigger={<img src={plus}></img>}>
        {close=>(
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
                <input type='button' value='Опубликовать пост' className='createNewPostButtonInProfile' onClick={()=>{onAddPost(); close();}}/>
            </div>
          </div>
        )}
          
        </Popup>

        </div>  
        {isLoading?<></>:isSuccess?<div className="users_pages">
                {page+1>1?<button className="users_button" onClick={downPage}>&lt;</button>:<></>}
                {postsInfo.pageCount!=0?<div className="users_div">{page+1} из {postsInfo.pageCount}</div>:<></>}
                {page+1<postsInfo.pageCount?<button className="users_button" onClick={upPage}>&gt;</button>:<></>}
            </div>:<></>}
        <div className="cardsWrapperInPostInProfile">
        {isLoading?<>Загрузка...</>:isSuccess?postsInfo.posts==[]?<></>:postsInfo.posts.map((post)=>{
            return(<PostInPart key={post.id} post={post}></PostInPart>)
        }):<></>}
        </div> 
      </div>
    );
}

export default MyPosts;