import React from "react";
import { connect } from 'react-redux';
import { setAccessTokenActionCreator, setAccountSettingsActionCreator} from "../../redux/settings-account-reducer";
import Registration from "./Registration";

let mapStateToProps = (state) => {
  return{

  }
}

let mapDispatchToProps = (dispatch) => {
  return{
    setAccessTokenUser: (token) => {
      dispatch(setAccessTokenActionCreator(token));
    },
    setAccoutSetting: (user) => {
      dispatch(setAccountSettingsActionCreator(user));
    }
  }
}

const RegistrationContainer = connect(mapStateToProps, mapDispatchToProps)(Registration);
export default RegistrationContainer;
