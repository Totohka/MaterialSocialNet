import axios from "axios";
import { setError, setLoading, setMessages } from "../../features/chats/chatsConnectionsSlice";
import { useDispatch } from "react-redux";

const getMessages = async(arg) =>{
    // const dispatch = useDispatch();
    const token = localStorage.getItem('token');
    // dispatch(setLoading());
    try{
        const result = await axios.get("http://25.32.11.98:8089/api/Message/All",{
            params:arg,
            headers:{
                'Authorization':token
            }
        });
        // dispatch(setMessages(result.data));
        return {data: result.data, error: false};

    } catch(error){
        // dispatch(setError(error));
        return {data:null, error:error};
        // console.log(error);
        // if(error.response){
        //     if (error.response.status==401){
        //         navigateU("/login");
        //         alert('Время вашего сеанса истекло');
        //     }
        // }   
    }
}
export default getMessages;