const CHANGE_MESSAGE_NOTIF = 'CHANGE-MESSAGE-NOTIF';
const CHANGE_FRIEND_NOTIF = 'CHANGE-FRIEND-NOTIF';
const CHANGE_POST_NOTIF = 'CHANGE-POST-NOTIF';

const initialState = {
    MessageNotif: true,
    FriendNotif: true,
    PostNotif: true
}

const settingNotificationReducer = (state = initialState, action) =>{
    switch(action.type){
        case CHANGE_MESSAGE_NOTIF:{
            let stateCopy = {...state};
            stateCopy.MessageNotif = !stateCopy.MessageNotif;
            return stateCopy;
        }
        case CHANGE_FRIEND_NOTIF: {
            let stateCopy = {...state};
            stateCopy.FriendNotif = !stateCopy.FriendNotif;
            return stateCopy;
        }
        case CHANGE_POST_NOTIF:{
            let stateCopy = {...state};
            stateCopy.PostNotif = !stateCopy.PostNotif;
            return stateCopy;
        }
        default:
            return state;
    } 
} 

export const changeMessageNotificationActionCreator = () => ({type: CHANGE_MESSAGE_NOTIF})
export const changeFriendNotificatioActionCreator = (friendNotif) => ({type: CHANGE_FRIEND_NOTIF, friendNotif})
export const changePostNotificatioActionCreator = (postNotif) => ({ type: CHANGE_POST_NOTIF, postNotif }) 

export default settingNotificationReducer;