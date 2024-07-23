const ADD_POST_IN_NEWS = 'ADD-POST-IN-NEWS';
const CHANGE_POST_IN_AREA_MESSAGE_IN_NEWS = 'CHANGE-POST-IN-AREA-MESSAGE-IN-NEWS';
const CHANGE_POST_IN_AREA_TITLE_IN_NEWS = 'CHANGE-POST-IN-AREA-TITLE-IN-NEWS';
const CHANGE_POST_IN_AREA_TAG_IN_NEWS = 'CHANGE-POST-IN-AREA-TAG-IN-NEWS';
const CHANGE_POST_IN_AREA_IMG_IN_NEWS = 'CHANGE-POST-IN-AREA-IMG-IN-NEWS';
const ADD_TAGS_IN_POST_IN_NEWS = 'ADD-TAGS-IN-POST-IN-NEWS';
const GET_POSTS = 'GET-POSTS';

const initialState = { 
  postsData: [
    
  ],
  newPostTitleText: '',
  newPostMessageText: '',
  newPostSmallMessageText: '',
  newPostTagText: '',
  newPostImg: '',
  newTagsInPost: []
}

const newsReducer = (state = initialState, action) =>{
    switch(action.type){
        case GET_POSTS:{
          let stateCopy = {...state};
          stateCopy.postsData.unshift(...action.posts);
          return stateCopy;
        }
        case ADD_POST_IN_NEWS:{
          //debugger;
          let myName = action.myName;
          let today = new Date();
          var options = { weekday: 'long', year: 'numeric', month: 'numeric', day: 'numeric' };
          let newPost = {
            id: 1000, 
            img: state.newPostImg,
            date: today.toLocaleDateString("ru-RU", options),
            title: state.newPostTitleText,
            smallMessage: state.newPostSmallMessageText,
            message: state.newPostMessageText, 
            name: myName,
            tegs: state.newTagsInPost
          };
          state.newTagsInPost = [];
          let stateCopy = {...state};
          stateCopy.postsData = [...state.postsData];
          stateCopy.postsData.unshift(newPost);
          return stateCopy;
        }  
        case ADD_TAGS_IN_POST_IN_NEWS:{
          let stateCopy = {...state};
          stateCopy.newTagsInPost = state.newPostTagText;
          return stateCopy;
        }  
        case CHANGE_POST_IN_AREA_MESSAGE_IN_NEWS:{
          let stateCopy = {...state};
          stateCopy.newPostMessageText = action.newText;
          if (stateCopy.newPostMessageText.length > 20){
            stateCopy.newPostSmallMessageText = stateCopy.newPostMessageText.slice(0, 20) + '...';
            //debugger;
          }
          else{
            stateCopy.newPostSmallMessageText = stateCopy.newPostMessageText;
          }
          return stateCopy;
        } 
        case CHANGE_POST_IN_AREA_TITLE_IN_NEWS:{
          let stateCopy = {...state};
          stateCopy.newPostTitleText = action.newText;
          return stateCopy;
        } 
        case CHANGE_POST_IN_AREA_IMG_IN_NEWS:{
          let stateCopy = {...state};
          stateCopy.newPostImg = action.newImg;
          return stateCopy;
        } 
        case CHANGE_POST_IN_AREA_TAG_IN_NEWS:{
          let stateCopy = {...state};
          stateCopy.newPostTagText = action.newText;
          return stateCopy;
        } 
        default:
            return state;
    }
    
} 
export const getAllPostsInNewsActionCreator = (posts) => {
  return{
    type: GET_POSTS,
    posts: [posts]
  }
}
export const addPostInNewsActionCreator = (fullName) =>{
    return{
      type: ADD_POST_IN_NEWS,
      myName: fullName
    }
  }

export const addTagsInPostInNewsActionCreator = (nameTag) =>{
    return{
      type: ADD_TAGS_IN_POST_IN_NEWS,
    }
  }

export const changePostInAreaImgInNewsActionCreator = (img) =>{
    return{
      type: CHANGE_POST_IN_AREA_IMG_IN_NEWS,
      newImg: img
    }
}

export const changePostInAreaMessageInNewsActionCreator = (text) =>{
    return{
      type: CHANGE_POST_IN_AREA_MESSAGE_IN_NEWS,
      newText: text
    }
}

export const changePostInAreaTitleInNewsActionCreator = (text) =>{
  return{
    type: CHANGE_POST_IN_AREA_TITLE_IN_NEWS,
    newText: text
  }
}

export const changePostInAreaTagInNewsActionCreator = (text) =>{
  return{
    type: CHANGE_POST_IN_AREA_TAG_IN_NEWS,
    newText: text
  }
}

export default newsReducer;