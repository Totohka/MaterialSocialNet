import React from 'react';
import './Settings.css';
import { NavLink } from 'react-router-dom';
import SettingBlackList from './SettingBlacklist';


let mapStateToProps = (state) => {
  return{

  }
}

let mapDispatchToProps = (dispatch) => {
  return{

  }
}

const SettingBlackListContainer = connect(mapStateToProps, mapDispatchToProps)(SettingBlackList);
export default SettingBlackListContainer;