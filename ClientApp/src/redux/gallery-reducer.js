const ADD_PHOTO_IN_GALLERY = 'ADD-PHOTO-IN-GALLERY';
const CHANGE_PHOTO_IN_GALLERY = 'CHANGE-PHOTO-IN-GALLERY';
const GET_PHOTOS_IN_GALLERY = 'GET_PHOTOS_IN_GALLERY';
const GET_FIRSTPHOTOS_IN_GALLERY = 'GET_FIRSTPHOTOS_IN_GALLERY';

const initialState = { 
  galleryPhoto: [
    'https://images.unsplash.com/photo-1544568100-847a948585b9?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjE0NTg5fQ',
    'https://w.forfun.com/fetch/56/5656d35727009cabea6ce79973a9702c.jpeg',
    'https://w.forfun.com/fetch/03/03f8cd3f6796daaacc1fe43ffb7704b7.jpeg',
    'https://proprikol.ru/wp-content/uploads/2020/04/krasivye-kartinki-vysokogo-razresheniya-3.jpg',
    'https://w.forfun.com/fetch/25/2529ce3d3391789f369c4ec9a07a1b82.jpeg',
    'https://klike.net/uploads/posts/2023-02/1675839044_3-490.jpg',
    'https://gas-kvas.com/uploads/posts/2023-02/1675484953_gas-kvas-com-p-fonovie-risunki-priroda-na-ves-ekran-2.jpg',
    'https://fikiwiki.com/uploads/posts/2022-02/1644965580_6-fikiwiki-com-p-kartinki-priroda-na-zastavku-telefona-6.jpg',
    'https://gas-kvas.com/uploads/posts/2023-02/1675484251_gas-kvas-com-p-kartinki-dlya-fonovogo-risunka-raboch-stol-43.jpg',
    'https://images.unsplash.com/photo-1544568100-847a948585b9?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjE0NTg5fQ',
    'https://images.unsplash.com/photo-1517423440428-a5a00ad493e8?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjE0NTg5fQ',
    'https://images.unsplash.com/photo-1510771463146-e89e6e86560e?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=400&fit=max&ixid=eyJhcHBfaWQiOjE0NTg5fQ'
  ],
  firstPhotos:[],
  newPhotoInGallery: '',
}

const galleryReducer = (state = initialState, action) =>{
  switch(action.type){
    case ADD_PHOTO_IN_GALLERY:{
      let stateCopy = {...state};
      stateCopy.galleryPhoto.unshift(state.newPhotoInGallery);
      return stateCopy;
    }  
    case CHANGE_PHOTO_IN_GALLERY:{
      let stateCopy = {...state};
      stateCopy.newPhotoInGallery = action.newPhoto;
      // console.log(action.newPhoto);
      return stateCopy;
    } 
    case GET_PHOTOS_IN_GALLERY:{
      let stateCopy = {...state};
      stateCopy.galleryPhoto = action.photos;
      return stateCopy;
    }
    case GET_FIRSTPHOTOS_IN_GALLERY:{
      let stateCopy = {...state};
      stateCopy.firstPhotos = action.photos;
      return stateCopy;
    }
    default:
        return state;
  }   
} 

export const addPhotoInGalleryActionCreator = () =>{
  return{
    type: ADD_PHOTO_IN_GALLERY,
  }
}

export const changePhotoInGalleryActionCreator = (photo) =>{
  return{
    type: CHANGE_PHOTO_IN_GALLERY,
    newPhoto: photo
  }
}

export const getPhotosInGalleryActionCreator = (photos) =>{
  return{
    type: GET_PHOTOS_IN_GALLERY,
    photos: photos
  }
}
export const getFirstPhotosInGalleryActionCreator = (photos) =>{
  return{
    type: GET_FIRSTPHOTOS_IN_GALLERY,
    photos: photos
  }
}


export default galleryReducer;