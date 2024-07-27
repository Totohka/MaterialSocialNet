import { useEffect } from "react";
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import { connectionNotif } from "../../app/helpers/signalRNotifications";
import { useGetChatQuery } from "../../features/api/apiSlice";
import { useNavigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import defImg from './../../images/logo.jpg';


const ChatInfo = () =>{
    const user = useSelector(state=>state.user.userInfo);
    let param = useParams();
    let id = Number(param['*']);
    const navigate = useNavigate();
    const {data: chatInfo, isLoading, isSuccess, isError, error} = useGetChatQuery(id);

    useEffect(()=>{
        if(!isLoading){
            if(isError){
                console.log(error);
                if (error.status==401){
                    logOut();
                }
                else{
                    alert(error.message);
                }
            } 
        }
    },[isLoading, isSuccess, isError]);

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
    // const getChatInfo = async() =>{
    //     try{
    //         const result = await axios.get("http://25.32.11.98:8089/api/Chat",{
    //             params:{
    //                 chatId:id
    //             },
    //             headers:{
    //                 'Authorization':token
    //             }
    //         });
    //         setChatInfo(result.data);
    //     } catch(error){
    //         console.log(error);
    //         if(error.response){
    //             if (error.response.status==401){
    //                 navigateU("/login");
    //                 alert('Время вашего сеанса истекло');
    //             }
    //         }
            
            
    //     }
    // }
    // useEffect(()=>{
    //     getChatInfo();
    // })
    return (
        <div className='chat_header'>
            {
                isLoading?<>Загрузка...</>:isSuccess?
                <>
                    <div className="chat_img" style={{backgroundImage: `url(${defImg})`}}></div>
                    {/* <div className='online'>
                        <div></div>
                        <p>online</p>
                    </div> */}
                    <p className='interlocutor'>{chatInfo?chatInfo.name:''}</p>
                </>:<></>
            }
            
        </div>
    );
}
export default ChatInfo;