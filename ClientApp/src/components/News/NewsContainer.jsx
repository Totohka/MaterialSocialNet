import React from 'react';
import  News from './News';
import { connect } from 'react-redux';
import {changePostInAreaImgInNewsActionCreator, addPostInNewsActionCreator, addTagsInPostInNewsActionCreator, changePostInAreaTitleInNewsActionCreator, changePostInAreaMessageInNewsActionCreator, changePostInAreaTagInNewsActionCreator, getAllPostsInNewsActionCreator } from '../../redux/news-reducer';
import { addPostInProfileActionCreator, addTagsInPostInProfileActionCreator, changePostInAreaImgInProfileActionCreator, changePostInAreaTitleInProfileActionCreator, changePostInAreaMessageInProfileActionCreator, changePostInAreaTagInProfileActionCreator, getAllPostsActionCreator} from '../../redux/profile-reducer';

let mapStateToProps = (state) => {
  return{
    fullName: state.settingAccount.lastName + ' ' + state.settingAccount.firstName,
    postsData: state.newsPage.postsData,
    newPostTitleText: state.newsPage.newPostTitleText,
    newPostMessageText: state.newsPage.newPostMessageText,
    newPostTagText: state.newsPage.newPostTagText,
    newTagsInPost: state.newsPage.newTagsInPost
  }
}

let mapDispatchToProps = (dispatch) => {
  return{
    getAllPosts:(posts) =>{
      dispatch(getAllPostsActionCreator(posts));
      dispatch(getAllPostsInNewsActionCreator(posts));
    },
    updateNewPostTitleTextInNews: (text) => {
      dispatch(changePostInAreaTitleInNewsActionCreator(text));
      dispatch(changePostInAreaTitleInProfileActionCreator(text));
    },
    updateNewPostMessageTextInNews: (text) => {
      dispatch(changePostInAreaMessageInNewsActionCreator(text));
      dispatch(changePostInAreaMessageInProfileActionCreator(text));
    },
    updateNewPostTagTextInNews: (text) => {
      dispatch(changePostInAreaTagInNewsActionCreator(text));
      dispatch(changePostInAreaTagInProfileActionCreator(text));
    },
    updateNewPostImgInNews: (img) => {
      dispatch(changePostInAreaImgInNewsActionCreator(img));
      dispatch(changePostInAreaImgInProfileActionCreator(img));
    },
    addTagsInPostInNews: () => {
      dispatch(addTagsInPostInNewsActionCreator());
      dispatch(addTagsInPostInProfileActionCreator());
    },
    addPostInNews: (fullName) => {
      dispatch(addPostInNewsActionCreator(fullName));
      dispatch(addPostInProfileActionCreator(fullName));
    }
  }
}

const NewsContainer = connect(mapStateToProps, mapDispatchToProps)(News);
export default NewsContainer;