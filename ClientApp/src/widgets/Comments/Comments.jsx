import { useNavigate, useParams } from "react-router-dom";
import { connection } from "../../app/helpers/withSignalR";
import { connectionNotif } from "../../app/helpers/signalRNotifications";
import { HubConnectionState } from "redux-signalr";
import { useAddCommentMutation, useDeleteCommentMutation, useEditCommentMutation, useGetCommentsQuery } from "../../features/api/apiSlice";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import edit from './../../images/edit.svg';
import bin from './../../images/bin.svg';
import standartImg from './../../images/standartLogo.jpg';
import "./Comments.css";

const Comments = () => {
    const navigate = useNavigate();
    const [newComment, setNewComment] = useState('');
    const [showId, setShowId] = useState(0);
    const [commentId, setCommentId] = useState(0);
    const param = useParams();    
    const id = Number(param['*']); 
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
    const tryAddComment = () =>{
        if (newComment==''){
            alert('Нельзя отправить пустой комментарий!');
        }else{
            addComment({post_id:id, user_id:user.id, text:newComment});
            setNewComment('');
        }
    }
    const tryEditComment = () =>{
        if (newComment==''){
            alert('Нельзя отправить пустой комментарий!');
        }else{
            showHide(showId);
            editComment({post_id:id, id:commentId.id, text:newComment});
            
        }
    }
    const tryToUpdate = (e) =>{
        let el = JSON.parse(e.target.value);
        // console.log(el);
        setNewComment(el.text);
        setCommentId(el);
        document.getElementById("update_comment_button").style.display="block";
        document.getElementById("add_comment_button").style.display="none";
    }
    const tryToDelete = (e) =>{
         showHide(showId);
        let el = JSON.parse(e.target.value);
        
        deleteComment({id:el.id, post_id:id});
        
    }
    const showHide = (num) =>{
        if(document.getElementById("commentNav_"+num).style.display=="block"){
            document.getElementById("commentNav_"+num).style.display="none";
            document.getElementById("update_comment_button").style.display="none";
            document.getElementById("add_comment_button").style.display="block";
            setNewComment('');
            setShowId(0);
        }else{
            if(showId!=0){
                document.getElementById("commentNav_"+showId).style.display="none";
            }
            setShowId(num);
            document.getElementById("commentNav_"+num).style.display="block";
            document.getElementById("update_comment_button").style.display="none";
            document.getElementById("add_comment_button").style.display="block";
        }
    }
    const showNav = (e) =>{
        showHide(e.target.value);

    }
    const {data: comments, isLoading, isSuccess, isError, error} = useGetCommentsQuery({post_id:id});
    const [addComment, {data: AddComment, error: AddCommentError}] = useAddCommentMutation();
    const [deleteComment, {data: DeleteComment, error: DeleteCommentError}] = useDeleteCommentMutation();
    const [editComment, {data: EditComment, error: EditCommentError}] = useEditCommentMutation();
    useEffect(()=>{
        if(!isLoading){
            if(isSuccess){
            //dispatch(setPhotos(photos));
            console.log(comments);
            // console.log(comments.length==0)
            // console.log(post);
            // console.log(isLoading);
            // setTags(post.tags);
            // setText(post.text);
            // setTitle(post.title);
            }
            if(isError){
                console.log(error);
                if (error.status==401){
                    logOut();
                }
                else{
                    alert(error.message);
                }
            }
            if(AddCommentError){
                console.log(AddCommentError);
                if (AddCommentError.status==401){
                    logOut();
                }
                else{
                    alert(AddCommentError.message);
                }
            }
            if(DeleteCommentError){
                console.log(DeleteCommentError);
                if (DeleteCommentError.status==401){
                    logOut();
                }
                else{
                    alert(DeleteCommentError.message);
                }
            }
            if(EditCommentError){
                console.log(EditCommentError);
                if (EditCommentError.status==401){
                    logOut();
                }
                else{
                    alert(EditCommentError.message);
                }
            }
        }
    },[isLoading, isSuccess, isError, AddCommentError, DeleteCommentError, EditCommentError])
    return (
        <>
        <div className='comments'>
          <p>Комментарии</p>
          <div id="scrolled">
            {isLoading?<>Загрузка...</>:isSuccess?comments.length==0?<div className='noComment'>Оставьте первый комментарий!</div>:comments.map((el)=>{
                console.log(el.id);
            return(
                <div className='comment' key={el.id} >
                    <div className="commentPicture" title={el.firstName+' '+el.lastName} onClick={()=>navigate(user.id==el.userId?'/profile':`/user/${el.userId}`)}>
                        <img src={"http://25.32.11.98:8086/Avatars/"+el.userId+".jpg"} />
                    </div>
                    {/* <div style={{backgroundImage: `url(http://25.32.11.98:8086/Avatars/${el.userId})`}}></div> */}
                    <div className="commentInfo">
                        <div className="commentInfo_userName">{el.firstName+' '+el.lastName}</div>
                        <div className="comment_text">{el.text}</div>
                    </div>
                    {el.userId==user.id?<div className="navComment">
                        <button onClick={showNav} className='commentnavShow' value={el.id}></button>
                        <div className={`commentNav`} id={"commentNav_"+el.id}>
                                    <button onClick={(e)=>{tryToUpdate(e)}} value={JSON.stringify(el)} className='commentNav__button'><img  src={edit}/></button>
                                    <div className='commentNav__line'></div>
                                    <button onClick={(e)=>{tryToDelete(e)}} value={JSON.stringify(el)} className='commentNav__button'><img  src={bin}/></button>
                        </div>
                    </div>:<></>}
                </div>
            )}
            ):<></>}
            </div>
          
          {/* <div className='comment'>
            <div style={{backgroundImage: `url(${standartImg})`}}></div>
            <p>Нет, он пользуется только SocialNet</p>
          </div>
          <div className='comment'>
            <div style={{backgroundImage: `url(${standartImg})`}}></div>
            <p>Придётся остаться...</p>
          </div> */}
          {/* {comments} */}
        </div>
        <div className='newComment'>
          <textarea placeholder='Введите текст' value={newComment} onChange={(e)=>setNewComment(e.target.value)}></textarea>
          <input type='button' value='Отправить' onClick={tryAddComment} id='add_comment_button'></input>
          <input type='button' value='Сохранить' style={{display:'none'}}onClick={tryEditComment} id='update_comment_button'></input>
        </div>
        </>
    );
}
export default Comments;