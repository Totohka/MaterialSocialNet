import React from 'react';
import { connect } from 'react-redux';
import SettingNotifications from './SettingNotifications';
import { changeFriendNotificatioActionCreator, changeMessageNotificationActionCreator, changePostNotificatioActionCreator } from '../../../redux/settings-notification-reducer';
// import { changeInvateInChatActionCreator, changeShowMyDataBirthdayActionCreator, changeShowMyPostActionCreator } from '../../../redux/settings-privacy-reducer';

let mapStateToProps = (state) => {
    return{
        msgNotif: state.settingNotification.MessageNotif,
        friendNotif: state.settingNotification.FriendNotif,
        postNotif: state.settingNotification.PostNotif
    }
}

let mapDispatchToProps = (dispatch) => {
    return{
        updateMessageNotification: () => {
            dispatch(changeMessageNotificationActionCreator());
        },
        updateFriendNotification: () => {
            dispatch(changeFriendNotificatioActionCreator());
        },
        updatePostNotification: () => {
            dispatch(changePostNotificatioActionCreator());
        }
    }
}

const SettingNotificationsContainer = connect(mapStateToProps, mapDispatchToProps)(SettingNotifications);

export default SettingNotificationsContainer;