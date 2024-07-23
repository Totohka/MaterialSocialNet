import React from 'react';
import  GalleryPhotosInProfile from './GalleryPhotosInProfile';
import { connect } from 'react-redux';
import { getFirstPhotosInGalleryActionCreator, getPhotosInGalleryActionCreator } from '../../../redux/gallery-reducer';

let mapStateToProps = (state) => {
  return{
      // fullName: state.settingAccount.lastName + ' ' + state.settingAccount.firstName,
      photos: state.gallery.firstPhotos
  }
}

let mapDispatchToProps = (dispatch) => {
  return{
    getFirstPhotos: (photos) => {
      dispatch(getFirstPhotosInGalleryActionCreator(photos))
  },
  }
}

const GalleryPhotosInProfileContainer = connect(mapStateToProps, mapDispatchToProps)(GalleryPhotosInProfile);
export default GalleryPhotosInProfileContainer;