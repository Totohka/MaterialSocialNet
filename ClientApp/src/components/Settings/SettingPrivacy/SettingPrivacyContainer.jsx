import React from 'react';
import { connect } from 'react-redux';
import SettingPrivacy from './SettingPrivacy';
import { changeInvateInChatActionCreator, changeShowMyDataBirthdayActionCreator, changeShowMyPostActionCreator } from '../../../redux/settings-privacy-reducer';

let mapStateToProps = (state) => {
    return{
        invateInChat: state.settingPrivacy.invateInChat,
        showMyPost: state.settingPrivacy.showMyPost,
        showMyDataBirthday: state.settingPrivacy.showMyDataBirthday,
    }
}

let mapDispatchToProps = (dispatch) => {
    return{
        updateInvateInChat: (invateInChat) => {
            dispatch(changeInvateInChatActionCreator(invateInChat));
        },
        updateShowMyPost: (showMyPost) => {
            dispatch(changeShowMyPostActionCreator(showMyPost));
        },
        updateShowMyDataBirthday: (showMyDataBirthday) => {
            dispatch(changeShowMyDataBirthdayActionCreator(showMyDataBirthday));
        }
    }
}

const SettingPrivacyContainer = connect(mapStateToProps, mapDispatchToProps)(SettingPrivacy);

export default SettingPrivacyContainer;