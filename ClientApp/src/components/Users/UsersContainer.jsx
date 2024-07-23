import React from "react";
import { changeTextInFindUsersActionCreator, followActionCreator, setUsersActionCreator, unfollowActionCreator } from '../../redux/users-find-reducer';
import { connect } from 'react-redux';
import Users from "./Users";

let mapStateToProps = (state) => {
  return{
    usersData: state.usersFindPage.usersData,
    newSearchText: state.usersFindPage.newSearchText
  }
}

let mapDispatchToProps = (dispatch) => {
  return{
      updateNewTextInFindUsers: (text) => {
          dispatch(changeTextInFindUsersActionCreator(text));
      },
      follow: (userId) => {
          dispatch(followActionCreator(userId));
      },
      unfollow: (userId) => {
          dispatch(unfollowActionCreator(userId));
      },
      setStateUsers: (users) => {
          dispatch(setUsersActionCreator(users));
      }
  }
}

const UsersConteiner = connect(mapStateToProps, mapDispatchToProps)(Users);
export default UsersConteiner;
