import React from "react";
import { connect } from 'react-redux';
import { setAccessTokenActionCreator, setAccountSettingsActionCreator} from "../../redux/settings-account-reducer";
import Auth from "./Auth";

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

const AuthContainer = connect(mapStateToProps, mapDispatchToProps)(Auth);
export default AuthContainer;
