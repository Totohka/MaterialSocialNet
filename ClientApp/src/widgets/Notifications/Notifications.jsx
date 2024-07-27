import { useDispatch, useSelector } from "react-redux";
import { deleteNotification } from "../../features/notifications/notificatiosSlice";
import { useNavigate } from "react-router-dom";
import './Notifications.css';

const Notifications = ({close}) =>{
    const isLoading = useSelector(state=>state.notifications.loading);
    const isSuccess = useSelector(state=>state.notifications.success);
    const notifications = useSelector(state=>state.notifications.notifications);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const goToChat = ({id}) =>{
        dispatch(deleteNotification({PostId:-1, ChatRoomId:id}));
        // close();
        navigate(`/dialogs/${id}`);
    }
    const goToPost = ({id}) =>{
        dispatch(deleteNotification({PostId:id, ChatRoomId:-1}));
        // close();
        console.log(id);
        navigate(`/post/${id}`);
    }
    return(
        <div className='popup_notification'>
            <p>Уведомления</p>
            {isLoading?<>Загрузка</>:!isSuccess?<></>:
            notifications!=[]?
                notifications.map((el)=>{
                    console.log(el);
                    if(el.hasOwnProperty('ChatRoomId')){
                        return(<div onClick={()=>{goToChat({id:el.ChatRoomId}); close();}}>У вас новое сообщение!<br/>Нажмите, чтобы перейти к чату</div>);
                    }else if(el.hasOwnProperty('PostId')){
                        return(<div onClick={()=>{goToPost({id:el.PostId}); close(); }}>Новый пост!<br/>Нажмите, чтобы перейти к нему</div>);
                    }
                    // el==null?<div>Новых уведомлений нет</div>
                    // :<div>{el}</div>
                }):<div>Новых уведомлений нет</div>
            }
            
        </div>
    );
}
export default Notifications;