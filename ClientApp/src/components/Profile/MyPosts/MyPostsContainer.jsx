import React from 'react';
import {changePostInAreaImgInNewsActionCreator, addPostInNewsActionCreator, addTagsInPostInNewsActionCreator, changePostInAreaTitleInNewsActionCreator, changePostInAreaMessageInNewsActionCreator, changePostInAreaTagInNewsActionCreator } from '../../../redux/news-reducer';
import { addPostInProfileActionCreator, addTagsInPostInProfileActionCreator, changePostInAreaImgInProfileActionCreator, changePostInAreaTitleInProfileActionCreator, changePostInAreaMessageInProfileActionCreator, changePostInAreaTagInProfileActionCreator, getAllPostsActionCreator} from '../../../redux/profile-reducer';
import MyPosts from './MyPosts';
import { connect } from 'react-redux';

let mapStateToProps = (state) => {
  return{
      fullName: state.settingAccount.lastName + ' ' + state.settingAccount.firstName,
      postsData: state.profilePage.postsData,
      newPostTitleText: state.profilePage.newPostTitleText,
      newPostMessageText: state.profilePage.newPostMessageText,
      newPostTagText: state.profilePage.newPostTagText,
      newTagsInPost: state.profilePage.newTagsInPost
  }
}

let mapDispatchToProps = (dispatch) => {
  return{
    getAllPosts: (posts) => {
      dispatch(getAllPostsActionCreator(posts));
    },
    updateNewPostTitleTextInProfile: (text) => {
      dispatch(changePostInAreaTitleInProfileActionCreator(text));
      dispatch(changePostInAreaTitleInNewsActionCreator(text));
    },
    updateNewPostMessageTextInProfile: (text) => {
      dispatch(changePostInAreaMessageInProfileActionCreator(text));
      dispatch(changePostInAreaMessageInNewsActionCreator(text));
    },
    updateNewPostTagTextInProfile: (text) => {
      dispatch(changePostInAreaTagInProfileActionCreator(text));
      dispatch(changePostInAreaTagInNewsActionCreator(text))
    },
    updateNewPostImgInProfile: (img) => {
      dispatch(changePostInAreaImgInProfileActionCreator(img));
      dispatch(changePostInAreaImgInNewsActionCreator(img));
    },
    addTagsInPostInProfile: () => {
      dispatch(addTagsInPostInProfileActionCreator());
      dispatch(addTagsInPostInNewsActionCreator());
    },
    addPostInProfile: (fullName) => {
      dispatch(addPostInProfileActionCreator(fullName));
      dispatch(addPostInNewsActionCreator(fullName));
    }
  }
}

const MyPostsConteiner = connect(mapStateToProps, mapDispatchToProps)(MyPosts);
export default MyPostsConteiner;