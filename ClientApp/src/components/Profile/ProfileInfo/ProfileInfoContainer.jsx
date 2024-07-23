import React from 'react';
import ProfileInfo from './ProfileInfo';
import { connect } from 'react-redux';
import {changeAvatarActionCreator, changeBackgroundActionCreator, changeCityActionCreator, changeCountryActionCreator, changeDataBithdayActionCreator, changeStatusActionCreator, changeFirstNameActionCreator, changeLastNameActionCreator, setAccessTokenActionCreator, setAccountSettingsActionCreator} from "../../../redux/settings-account-reducer";

let mapStateToProps = (state) => {
    return{
        id: state.settingAccount.id,
        firstName: state.settingAccount.firstName,
        lastName: state.settingAccount.lastName,
        city: state.settingAccount.city,
        country: state.settingAccount.country,
        dataBithday: state.settingAccount.dataBithday,
        status: state.settingAccount.status,
        avatar:state.settingAccount.avatar,
        background:state.settingAccount.background
    }
}

let mapDispatchToProps = (dispatch) => {
    return{
        changeStatusUser: (text) => {
            dispatch(changeStatusActionCreator(text))
        },
        changeDateBirthday: (date) => {
            dispatch(changeDataBithdayActionCreator(date))
        },
        changeFirstName: (text) => {
            dispatch(changeFirstNameActionCreator(text))
        },
        changeLastName: (text) => {
            dispatch(changeLastNameActionCreator(text))
        },
        changeCountry: (text) => {
            dispatch(changeCountryActionCreator(text))
        },
        changeCity: (text) => {
            dispatch(changeCityActionCreator(text))
        },
        changeAvatar: (text) => {
            dispatch(changeAvatarActionCreator(text))
        },
        changeBackground: (text) => {
            dispatch(changeBackgroundActionCreator(text))
        },
        setUser: (user) => {
            dispatch(setAccountSettingsActionCreator(user))
        },
        setToken: (token) => {
            dispatch(setAccessTokenActionCreator(token))
        }
    }
}

const ProfileInfoContainer = connect(mapStateToProps, mapDispatchToProps)(ProfileInfo);

export default ProfileInfoContainer;