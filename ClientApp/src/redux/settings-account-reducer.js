const CHANGE_FIRSTNAME = 'CHANGE-FIRSTNAME';
const CHANGE_LASTNAME = 'CHANGE-LASTNAME';
const CHANGE_CITY = 'CHANGE-CITY';
const CHANGE_AVATAR = 'CHANGE-AVATAR';
const CHANGE_BACKGROUND = 'CHANGE-BACKGROUND';
const CHANGE_COUNTRY = 'CHANGE-COUNTRY';
const CHANGE_DATA_BIRTHDAY = 'CHANGE-DATA-BIRTHDAY';
const CHANGE_COUNT_SCALE = 'CHANGE-COUNT-SCALE';
const CHANGE_TEXT_STATUS_IN_SETTING = 'CHANGE-TEXT-STATUS-IN-SETTING';
const SET_ACCOUNT_SETTINGS = 'SET-ACCOUNT-SETTINGS';
const SET_ACCESS_TOKEN = 'SET-ACCESS-TOKEN';

const initialState = {
    id: 0,
    firstName: '',
    lastName: '',
    city: '',
    country: '',
    dataBithday: '2003-06-19',
    countScale: 100,
    status: '',
    avatar:'',
    background:'',
    accessToken: ''
}

const settingsAccountReducer = (state = initialState, action) =>{
    switch(action.type){
        case SET_ACCESS_TOKEN:{
            let stateCopy = {...state}
            stateCopy.accessToken = action.token;
            localStorage.setItem("token", stateCopy.accessToken);
            return stateCopy;
        }
        case CHANGE_FIRSTNAME:{
            let stateCopy = {...state};
            stateCopy.firstName = action.firstName;
            return stateCopy;
        } 
        case CHANGE_LASTNAME:{
            let stateCopy = {...state};
            stateCopy.lastName = action.lastName;
            return stateCopy;
        } 
        case CHANGE_CITY:{
            let stateCopy = {...state};
            stateCopy.city = action.city;
            return stateCopy;
        } 
        case CHANGE_COUNTRY:{
            let stateCopy = {...state};
            stateCopy.country = action.country;
            return stateCopy;
        } 
        case CHANGE_AVATAR:{
            let stateCopy = {...state};
            stateCopy.avatar = action.avatar;
            return stateCopy;
        } 
        case CHANGE_BACKGROUND:{
            let stateCopy = {...state};
            stateCopy.background = action.background;
            return stateCopy;
        } 
        case CHANGE_DATA_BIRTHDAY:{
            let stateCopy = {...state};
            stateCopy.dataBithday = action.dataBithday;
            return stateCopy;
        } 
        case CHANGE_COUNT_SCALE:{
            let stateCopy = {...state};
            stateCopy.countScale = action.countScale;
            return stateCopy;
        } 
        case CHANGE_TEXT_STATUS_IN_SETTING:{
            let stateCopy = {...state};
            stateCopy.status = action.status;
            return stateCopy;
        } 
        case SET_ACCOUNT_SETTINGS: {
            let stateCopy = {...state};
            stateCopy.id = action.user.id;
            stateCopy.firstName = action.user.firstName;
            stateCopy.lastName = action.user.lastName;
            stateCopy.city = action.user.city;
            stateCopy.avatar = action.user.avatar;
            stateCopy.background = action.user.background;
            stateCopy.country = action.user.country;
            stateCopy.dataBithday = action.user.dateOfBirth.substr(0, 10);
            stateCopy.status = action.user.status;
            console.log(stateCopy);
            return stateCopy
        }
        default:
            return state;
    } 
} 

export const changeBackgroundActionCreator = (background) => ({type: CHANGE_BACKGROUND, background});
export const changeAvatarActionCreator = (avatar) => ({type: CHANGE_AVATAR, avatar});
export const changeFirstNameActionCreator = (firstName) => ({type: CHANGE_FIRSTNAME, firstName});
export const changeLastNameActionCreator = (lastName) => ({type: CHANGE_LASTNAME, lastName})
export const changeCityActionCreator = (city) => ({type: CHANGE_CITY, city})
export const changeCountryActionCreator = (country) => ({type: CHANGE_COUNTRY, country})
export const changeDataBithdayActionCreator = (dataBithday) => ({type: CHANGE_DATA_BIRTHDAY, dataBithday})
export const changeScaleActionCreator = (countScale) => ({type: CHANGE_COUNT_SCALE, countScale})
export const changeStatusActionCreator = (status) => ({type: CHANGE_TEXT_STATUS_IN_SETTING, status})
export const setAccountSettingsActionCreator = (user) => ({type: SET_ACCOUNT_SETTINGS, user})
export const setAccessTokenActionCreator = (token) => ({type: SET_ACCESS_TOKEN, token})


export default settingsAccountReducer;