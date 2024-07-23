import React from 'react';
import  GalleryAllPhoto from './GalleryAllPhoto';
import { connect } from 'react-redux';
import { addPhotoInGalleryActionCreator, changePhotoInGalleryActionCreator, getPhotosInGalleryActionCreator } from '../../../redux/gallery-reducer';

let mapStateToProps = (state) => {
  return{
      galleryPhoto: state.gallery.galleryPhoto,
      newPhoto: state.gallery.newPhotoInGallery
  }
}

let mapDispatchToProps = (dispatch) => {
  return{
    updateNewPhotoInGallery: (text) => {
      dispatch(changePhotoInGalleryActionCreator(text));
    },
    addNewPhotoInGallery: () => {
      dispatch(addPhotoInGalleryActionCreator());
    },
    getPhotos: (photos) => {
      dispatch(getPhotosInGalleryActionCreator(photos));
    }
  }
}

const GalleryAllPhotoContainer = connect(mapStateToProps, mapDispatchToProps)(GalleryAllPhoto);
export default GalleryAllPhotoContainer;