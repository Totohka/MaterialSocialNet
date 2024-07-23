const CHANGE_MESSAGE_NOTIF = 'CHANGE-MESSAGE-NOTIF';
const CHANGE_FRIEND_NOTIF = 'CHANGE-FRIEND-NOTIF';
const CHANGE_POST_NOTIF = 'CHANGE-POST-NOTIF';

const CHANGE_INVATE_IN_CHAT = 'CHANGE-INVATE-IN-CHAT';
const CHANGE_SHOW_MY_POST = 'CHANGE-SHOW-MY-POST';
const CHANGE_SHOW_MY_DATA_BIRTHDAY = 'CHANGE-SHOW-MY-DATA-BIRTHDAY';

const CHANGE_USERNAME = 'CHANGE_USERNAME';
const CHANGE_CITY = 'CHANGE_CITY';
const CHANGE_COUNTRY = 'CHANGE_COUNTRY';
const CHANGE_DATA_BIRTHDAY = 'CHANGE_DATA_BIRTHDAY';
const CHANGE_COUNT_SCALE = 'CHANGE_COUNT_SCALE';

const SET_USERS = 'SET-USERS';
const FOLLOW = 'FOLLOW';
const UNFOLLOW = 'UNFOLLOW';

const Privacy = {
    All: 3,
    Friends: 2,
    Nobody: 1
}

const initialState = {
    SettingAccount: {
        userName: '',
        city: '',
        country: '',
        dataBithday: '',
        countScale: 100
    },

    SettingNotification: {
        MessageNotif: true,
        FriendNotif: true,
        PostNotif: true
    },

    SettingPrivacy: {
        invateInChat: Privacy.Friends,
        showMyPost: Privacy.Friends,
        showMyDataBirthday: Privacy.Friends
    },

    SettingSecurity: {
        pass: '' //?
    }

}

const settingsReducer = (state = initialState, action) =>{
    switch(action.type){
        case CHANGE_MESSAGE_NOTIF:{
            let stateCopy = {...state};
            
            return stateCopy;
        }
        // case CHANGE_TEXT_IN_FIND_USERS:{
        //     let stateCopy = {...state};
        //     stateCopy.newSearchText = action.newText;
        //     return stateCopy;
        // } 
        case FOLLOW: {
            let stateCopy = {...state};
            stateCopy.usersData = state.usersData.map(u => {
                if (u.id === action.userId){
                    return {...u, followed: true};
                }
                return u;
            });
            
            return stateCopy;
        }
        case SET_USERS:{
            let stateCopy = {...state};
            stateCopy.usersData = [...action.users];
            console.log(stateCopy);
            return stateCopy;
        }
        default:
            return state;
    } 
} 

export const followActionCreator = (userId) => ({type: FOLLOW, userId})
export const unfollowActionCreator = (userId) => ({type: UNFOLLOW, userId})
export const setUsersActionCreator = (users) => ({ type: SET_USERS, users }) 
export const changeTextInFindUsersActionCreator = (text) =>{
    return{
        type: CHANGE_TEXT_IN_FIND_USERS,
        newText: text
    }
}

export default usersFindReducer;