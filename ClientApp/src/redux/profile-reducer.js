// const ADD_POST = 'ADD-POST';
// const CHANGE_POST_IN_AREA = 'CHANGE-POST-IN-AREA';
const ADD_POST_IN_PROFILE = 'ADD-POST-IN-PROFILE';
const CHANGE_POST_IN_AREA_MESSAGE_IN_PROFILE = 'CHANGE-POST-IN-AREA-MESSAGE-IN-PROFILE';
const CHANGE_POST_IN_AREA_TITLE_IN_PROFILE = 'CHANGE-POST-IN-AREA-TITLE-IN-PROFILE';
const CHANGE_POST_IN_AREA_TAG_IN_PROFILE = 'CHANGE-POST-IN-AREA-TAG-IN-PROFILE';
const CHANGE_POST_IN_AREA_IMG_IN_PROFILE = 'CHANGE-POST-IN-AREA-IMG-IN-PROFILE';
const ADD_TAGS_IN_POST_IN_PROFILE = 'ADD-TAGS-IN-POST-IN-PROFILE';
const GET_POSTS = 'GET-POSTS';

const initialState = { 
  userData: {
    id: 1,
    firstName: '',
    lastName: '',
    city: '',
    country: '',
    dataBithday: '',
    description: '',
    avatar:'',
  },
  postsData: [

  ],
  newPostText: '',
  newPostTitleText: '',
  newPostMessageText: '',
  newPostSmallMessageText: '',
  newPostTagText: '',
  newPostImg: '',
  newTagsInPost: []
}

const profileReducer = (state = initialState, action) =>{

  switch(action.type){
    case GET_POSTS:{
      let stateCopy = {...state};
      stateCopy.postsData.unshift(...action.posts);
      return stateCopy;
    }
    case ADD_POST_IN_PROFILE:{
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
    case ADD_TAGS_IN_POST_IN_PROFILE:{
      let stateCopy = {...state};
      // stateCopy.newTagsInPost = [...state.newTagsInPost];
      stateCopy.newTagsInPost=state.newPostTagText;
      return stateCopy;
    }  
    case CHANGE_POST_IN_AREA_MESSAGE_IN_PROFILE:{
      let stateCopy = {...state};
      stateCopy.newPostMessageText = action.newText;
      if (stateCopy.newPostMessageText.length > 20){
        stateCopy.newPostSmallMessageText = stateCopy.newPostMessageText.slice(0, 20) + '...';
      }
      else{
        stateCopy.newPostSmallMessageText = stateCopy.newPostMessageText;
      }
      return stateCopy;
    } 
    case CHANGE_POST_IN_AREA_TITLE_IN_PROFILE:{
      let stateCopy = {...state};
      stateCopy.newPostTitleText = action.newText;
      return stateCopy;
    } 
    case CHANGE_POST_IN_AREA_IMG_IN_PROFILE:{
      let stateCopy = {...state};
      stateCopy.newPostImg = action.newImg;
      return stateCopy;
    } 
    case CHANGE_POST_IN_AREA_TAG_IN_PROFILE:{
      let stateCopy = {...state};
      stateCopy.newPostTagText = action.newText;
      return stateCopy;
    } 
    default:
        return state;
}
    
} 

export const getAllPostsActionCreator = (posts) => {
  return{
    type: GET_POSTS,
    posts: [posts]
  }
}
export const addPostInProfileActionCreator = (fullName) =>{
  return{
    type: ADD_POST_IN_PROFILE,
    myName: fullName
  }
}

export const addTagsInPostInProfileActionCreator = () =>{
  return{
    type: ADD_TAGS_IN_POST_IN_PROFILE,
  }
}

export const changePostInAreaImgInProfileActionCreator = (img) =>{
  return{
    type: CHANGE_POST_IN_AREA_IMG_IN_PROFILE,
    newImg: img
  }
}

export const changePostInAreaMessageInProfileActionCreator = (text) =>{
  return{
    type: CHANGE_POST_IN_AREA_MESSAGE_IN_PROFILE,
    newText: text
  }
}

export const changePostInAreaTitleInProfileActionCreator = (text) =>{
return{
  type: CHANGE_POST_IN_AREA_TITLE_IN_PROFILE,
  newText: text
}
}

export const changePostInAreaTagInProfileActionCreator = (text) =>{
return{
  type: CHANGE_POST_IN_AREA_TAG_IN_PROFILE,
  newText: text
}
}

export default profileReducer;