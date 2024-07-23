import React from "react";
import { connect } from 'react-redux';
import { setAccountSettingsActionCreator } from "../../redux/settings-account-reducer";
import Profile from "./Profile";
import { setPrivacySettingActionCreator } from "../../redux/settings-privacy-reducer";

let mapStateToProps = (state) => {
  return{

  }
}

let mapDispatchToProps = (dispatch) => {
  return{
      setStateUser: (user) => {
        dispatch(setAccountSettingsActionCreator(user));
      },
      setStatePrivacySetting: (setting) =>{
        dispatch(setPrivacySettingActionCreator(setting));
      }
  }
}

const ProfileContainer = connect(mapStateToProps, mapDispatchToProps)(Profile);
export default ProfileContainer;
