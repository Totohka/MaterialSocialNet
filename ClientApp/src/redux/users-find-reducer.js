const CHANGE_TEXT_IN_FIND_USERS = 'CHANGE-TEXT-IN-FIND-USERS';
const SET_USERS = 'SET-USERS';
const FOLLOW = 'FOLLOW';
const UNFOLLOW = 'UNFOLLOW';


const initialState = {
    usersData: [
        
    ],
    newSearchText: ''
}

const usersFindReducer = (state = initialState, action) =>{
    switch(action.type){
        case CHANGE_TEXT_IN_FIND_USERS:{
            let stateCopy = {...state};
            stateCopy.newSearchText = action.newText;
            return stateCopy;
        } 
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
        case UNFOLLOW: {
            return{
                ...state,
                usersData: state.usersData.map(u => {
                    if (u.id === action.userId){
                        return {...u, followed: false};
                    }
                    return u;
                })
            }
        }
        case SET_USERS:{
            let stateCopy = {...state};
            stateCopy.usersData = [...action.users];
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