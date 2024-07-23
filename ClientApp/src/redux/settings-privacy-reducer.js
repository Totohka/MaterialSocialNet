const CHANGE_INVATE_IN_CHAT = 'CHANGE-INVATE-IN-CHAT';
const CHANGE_SHOW_MY_POST = 'CHANGE-SHOW-MY-POST';
const CHANGE_SHOW_MY_DATA_BIRTHDAY = 'CHANGE-SHOW-MY-DATA-BIRTHDAY';
const SET_PRIVACY_SETTING = 'SET-PRIVACY-SETTING';

const Privacy = {
    All: 2,
    Friends: 1,
    Nobody: 0
}

const initialState = {
    invateInChat: Privacy.Friends,
    showMyPost: Privacy.Friends,
    showMyDataBirthday: Privacy.Friends
}

const settingsPrivacyReducer = (state = initialState, action) =>{
    switch(action.type){
        case CHANGE_INVATE_IN_CHAT:{
            let stateCopy = {...state};   
            stateCopy.invateInChat = action.invateInChat;
            return stateCopy;
        }
        case CHANGE_SHOW_MY_POST: {
            let stateCopy = {...state};
            stateCopy.showMyPost = action.showMyPost;
            return stateCopy;
        }
        case CHANGE_SHOW_MY_DATA_BIRTHDAY:{
            let stateCopy = {...state};
            stateCopy.showMyDataBirthday = action.showMyDataBirthday;
            return stateCopy;
        }
        case SET_PRIVACY_SETTING:{
            let stateCopy = {...state};
            stateCopy.showMyDataBirthday = action.setting.showDateBirthday;
            stateCopy.showMyPost = action.setting.showPost;
            stateCopy.invateInChat = action.setting.invateChats;
            return stateCopy;
        }
        default:
            return state;
    } 
} 

export const changeInvateInChatActionCreator = (invateInChat) => ({type: CHANGE_INVATE_IN_CHAT, invateInChat})
export const changeShowMyPostActionCreator = (showMyPost) => ({type: CHANGE_SHOW_MY_POST, showMyPost})
export const changeShowMyDataBirthdayActionCreator = (showMyDataBirthday) => ({ type: CHANGE_SHOW_MY_DATA_BIRTHDAY, showMyDataBirthday }) 
export const setPrivacySettingActionCreator = (setting) => ({type: SET_PRIVACY_SETTING, setting})

export default settingsPrivacyReducer;