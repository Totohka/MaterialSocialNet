import { createSlice } from '@reduxjs/toolkit'
//import { userLogin } from './authAction'
import { jwtDecode } from 'jwt-decode'
import { useSelector } from 'react-redux'



const initialState = {
  loading: true,
  photos:[],
  firstPhotos:[],
  error: null,
  success: false, // for monitoring the registration process.
}

const userPhotos = createSlice({
  name: 'userPhotos',
  initialState,
  reducers: {
    setPhotos(state,action){
        state.loading=true;
        state.photos=action.payload;
        state.loading=false;
    },
    setFirstPhotos(state, action){
        state.firstPhotos=action.payload;
    }
  },
  extraReducers(builder){}
})
export const {setPhotos, setFirstPhotos} = userPhotos.actions;
export default userPhotos.reducer;
