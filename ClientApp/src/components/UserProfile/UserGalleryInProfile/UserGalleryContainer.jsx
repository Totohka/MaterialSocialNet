import React from 'react';
import { connect } from 'react-redux';
import { getFirstPhotosInGalleryActionCreator, getPhotosInGalleryActionCreator } from '../../../redux/gallery-reducer';
import UserGalleryInProfile from './UserGalleryInProfile';

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

const GalleryPhotosUserContainer = connect(mapStateToProps, mapDispatchToProps)(UserGalleryInProfile);
export default GalleryPhotosUserContainer;