import { createSlice } from '@reduxjs/toolkit'
//import { userLogin } from './authAction'
import { jwtDecode } from 'jwt-decode'
import { useSelector } from 'react-redux'

const token = localStorage.getItem('token')
  ? localStorage.getItem('token')
  : null

const initialState = {
  loading: false,
  userInfo:null,
  token: null, // for storing the JWT
  error: null,
  success: false, // for monitoring the registration process.
}

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser(state,action){
        state.token=action.payload;
        state.userInfo=jwtDecode(action.payload);
    },
    setPrivacy(state, action){
      state.userInfo.settingPrivacyId=action.payload;
    },
    setNotification(state, action){
      state.userInfo.settingNotificationId=action.payload;
    }
  },
  extraReducers(builder){}
})
export const {setUser, setPrivacy, setNotification} = userSlice.actions;
export default userSlice.reducer;
