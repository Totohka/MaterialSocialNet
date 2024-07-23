import React from 'react';
// import {changePostInAreaImgInNewsActionCreator, addPostInNewsActionCreator, addTagsInPostInNewsActionCreator, changePostInAreaTitleInNewsActionCreator, changePostInAreaMessageInNewsActionCreator, changePostInAreaTagInNewsActionCreator } from '../../../redux/news-reducer';
// import { addPostInProfileActionCreator, addTagsInPostInProfileActionCreator, changePostInAreaImgInProfileActionCreator, changePostInAreaTitleInProfileActionCreator, changePostInAreaMessageInProfileActionCreator, changePostInAreaTagInProfileActionCreator} from '../../../redux/profile-reducer';
import Post from './Post';
import { connect } from 'react-redux';

let mapStateToProps = (state) => {
  return{
      postsData: state.newsPage.postsData,
  }
}

let mapDispatchToProps = (dispatch) => {
  return{
    
  }
}

const PostContainer = connect(mapStateToProps, mapDispatchToProps)(Post);
export default PostContainer;