import { useEffect, useState } from "react";
import { useGetTypeReactionsQuery } from "../../features/api/apiSlice"
import { connection } from "../../app/helpers/withSignalR";
import { HubConnectionState } from "redux-signalr";
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";
import reaction_button from './../../images/reaction_button.svg';
import { useNavigate } from "react-router-dom";
import "./Reaction.css";
import { editMessage, setReaction } from "../../features/chats/chatsConnectionsSlice";
import { connectionNotif } from "../../app/helpers/signalRNotifications";

const Reaction = ({el, typeReacts, inout}) =>{
    const isLoadingReact = useSelector(state=>state.chats.loading);
    const isSuccessReact = useSelector(state=>state.chats.success);
    const [reactId, setReactId] = useState(0);
    const [showReactId, setShowReactId] = useState(0);
    const [hidden, setHidden] = useState('hidden');
    const us = useSelector(state=>state.user.userInfo);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const {data: reacts, isLoading, isSuccess, isError, error} = useGetTypeReactionsQuery();
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
    const showReacts = (e) =>{
        //console.log(e.target.value);
        if(hidden=='showed'){
            setHidden('hidden');
            // setShowReactId(0);
        }else{
            setHidden('showed');
            // let myReact = messages.filter(el => el.id==e.target.value)[0].typeReaction;
            // // console.log(myReact);
            // let typeReacts = new Set(reacts);
            // // console.log(typeReacts);
            // // console.log(Array.from(typeReacts).filter(el=>el.name==myReact)[0])
            // console.log(el.typeReaction);
            // console.log(reacts);
            // console.log(reacts.find(elem=>elem.name==el.typeReaction).id);
            
            // if(showReactId!=0){
            //     document.getElementById("messageNav__allreactions"+showReactId).style.display="none";
            // }
            // setShowReactId(e.target.value);
            // document.getElementById("messageNav__allreactions"+e.target.value).style.display="flex";
        }
        

    }
    // const getMessage = async() =>{
    //     try{
    //         let response = await axios.get("http://25.32.11.98:8089/api/Message", {
    //             params:{
    //                 messageId:el.id
    //             },
    //             headers:{
    //                 'Authorization':localStorage.getItem('token')
    //             }
    //         });
    //         dispatch()
    //     }
    // }
    const trySendReaction = (e) =>{
        if(document.getElementById("messageNav__allreactions_button"+el.id+e.target.value).classList.contains("reacted")){
            const data = {
                entity_id: el.id, 
                type_reaction_id: e.target.value, //likeId = 1
                user_id: us.id
              };
              const headers = { 
                'Authorization': localStorage.getItem('token')
              };
              axios.delete('http://25.32.11.98:8088/ReactionMessage',{data: data, headers: headers}).then(
                () => {
                    
                    // getMessage();
                    console.log('Delete!', {id:el.id, typeReaction:'', oldReaction:reacts.find(elem=>elem.id==reactId).name});
                    dispatch(setReaction({id:el.id, typeReaction:'', oldReaction:reacts.find(elem=>elem.id==reactId).name}))
                    e.target.classList.toggle("reacted");
                    setHidden('hidden');
                    setReactId(0);
                }
              ).catch(error => {
                console.error('There was an error!', error);
              })
        }else{
            if(reactId!=0){
                const data = {
                    entity_id: el.id, 
                    type_reaction_id: e.target.value, //likeId = 1
                    type_reaction_old_id: reactId, //dislikeId = 2
                    user_id: us.id
                  };
                  const headers = { 
                    'Authorization': localStorage.getItem('token')
                  };
                  axios.put('http://25.32.11.98:8088/ReactionMessage', data, {headers:headers}).then(
                    () => {
                        console.log({id:el.id, typeReaction:reacts.find(elem=>elem.id==e.target.value).name, oldReaction:reacts.find(elem=>elem.id==reactId).name})
                      document.getElementById("messageNav__allreactions_button"+el.id+reactId).classList.toggle("reacted");
                      dispatch(setReaction({id:el.id, typeReaction:reacts.find(elem=>elem.id==e.target.value).name, oldReaction:reacts.find(elem=>elem.id==reactId).name}))
                      setReactId(e.target.value);
                      setHidden('hidden');
                      e.target.classList.toggle("reacted");
                    }
                  ).catch(error => {
                    console.error('There was an error!', error);
                  })
            }else{
                const data = {
                  entity_id: el.id, 
                  type_reaction_id: e.target.value, //likeId = 1
                  user_id: us.id
                };
                const headers = { 
                  'Authorization': localStorage.getItem('token')
                };
                axios.post('http://25.32.11.98:8088/ReactionMessage', data, {headers:headers}).then(
                  () => {
                    console.log({id:el.id, typeReaction:reacts.find(elem=>elem.id==e.target.value).name, oldReaction:''});
                    dispatch(setReaction({id:el.id, typeReaction:reacts.find(elem=>elem.id==e.target.value).name, oldReaction:''}))
                    setReactId(e.target.value);
                    setHidden('hidden');
                    e.target.classList.toggle("reacted");
                  }
                ).catch(error => {
                  console.error('There was an error!', error);
                })
            }
        }
    }
    useEffect(()=>{
        if(!isLoading){
            if(isSuccess){
                if(el.typeReaction!="") setReactId(reacts.find(elem=>elem.name==el.typeReaction).id);
                else setReactId(0);
            }
            if(isError){
                if(error.status==401){
                    logOut();
                }
            }
        }
    },[isLoading, isSuccess, isError])
    return(
        <>
        {
            isLoadingReact?<></>:!isSuccess?<></>:
            <>
            <div className={`message__reactions ${inout}`}>
                                                    <button className="message__reactions_button" value={el.id} onClick={showReacts}><img className='message__reactions_button' src={reaction_button}/></button>
                                                    {Array.from(new Set(el.typeReactions)).map((elem)=>
                                                    elem!=null?
                                                    <div className='message__reactions_reaction'> 
                                                        <img src={process.env.PUBLIC_URL+`/images/${String(elem).toLowerCase()}.svg`}></img>
                                                        {/* {console.log(elem)}
                                                        {console.log(Array.from(new Set(el.typeReactions)))} */}
                                                        {el.typeReactions.filter(val => val==elem).length}
                                                    </div>:<></>)}
                                                </div>
            <div className={`messageNav__allreactions ${inout} ${hidden}`}  id={"messageNav__allreactions"+el.id}>
                {isLoading?<>Загрузка...</>:isSuccess?reacts.map((reaction)=>{
                    if (reaction==false) return <div>Загрузка...</div>
                    else {
                        // console.log(reaction.name, "el.TypeReaction", el.typeReaction, el.id)
                        if (reaction.name==el.typeReaction) return <button className='message__allreactions_button reacted' 
                                                                                        onClick={trySendReaction} value={reaction.id} 
                                                                                                    id={"messageNav__allreactions_button"+el.id+reaction.id}> 
                                                                                        <img src={process.env.PUBLIC_URL+`/images/${reaction.name.toLowerCase()}.svg`}></img></button>
                                    else return <button className='message__allreactions_button' onClick={trySendReaction} value={reaction.id} 
                                                                                                    id={"messageNav__allreactions_button"+el.id+reaction.id}>
                                                                                        <img src={process.env.PUBLIC_URL+`/images/${reaction.name.toLowerCase()}.svg`}></img></button>
                    } 
                }):<></>}
            </div>
            </>
        }
        </>
    )
}
export default Reaction;