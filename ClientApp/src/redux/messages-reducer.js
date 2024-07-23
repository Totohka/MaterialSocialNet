const SEND_MESSAGE = 'SEND-MESSAGE';
const CHANGE_MESSAGE_IN_CHAT = 'CHANGE-MESSAGE-IN-CHAT';

const initialState = {
    msgsData: [
        {id:1, message: 'Привет!', adress: 'incoming'},
        {id:2, message: 'Привет!', adress: 'outgoing'},
        {id:3, message: 'Как ты?!', adress: 'incoming'},
        {id:4, message: 'Нормально', adress: 'outgoing'}
        ],
    dialogsData: [
        {name : 'Анастасия Патюкова', id:1},
        {name : 'Алексей Волков', id: 2},
        {name :'Степан Кондрашов', id: 3},
        {name :'Мария Милешко', id: 4},
        {name : 'Эдуард Новиков', id: 5}
        ],
    newMessageText: ''
}

const messagesReducer = (state = initialState, action) =>{
    let stateCopy;
    switch(action.type){
        case SEND_MESSAGE:{
            let newMsg = {
                id:5, 
                message: state.newMessageText, 
                adress: 'outgoing'
            };
            stateCopy = {
                ...state,
                msgsData: [...state.msgsData, newMsg]    
            };
            return stateCopy;
        }
        case CHANGE_MESSAGE_IN_CHAT:{
            stateCopy = {
                ...state,
                newMessageText: action.newText
            };
            return stateCopy;
        }     
        default:
            return state;
    }
} 
export const changeMessageInChatActionCreator = (text) =>{
    return{
        type: CHANGE_MESSAGE_IN_CHAT,
        newText: text
    }
}
export const sendMsgActionCreator = () =>{
    return {
        type: SEND_MESSAGE
    }
}

export default messagesReducer;