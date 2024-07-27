import { createSlice } from '@reduxjs/toolkit'
//import { userLogin } from './authAction'
import { jwtDecode } from 'jwt-decode'
import { useSelector } from 'react-redux'



const initialState = {
  loading: true,
  posts:[],
  error: null,
  success: false, // for monitoring the registration process.
}

const userPosts = createSlice({
  name: 'userPosts',
  initialState,
  reducers: {
    setPosts(state,action){
        state.loading=true;
        state.posts=action.payload;
        state.loading=false;
    },
  },
  extraReducers(builder){}
})
export const {setPosts} = userPosts.actions;
export default userPosts.reducer;
