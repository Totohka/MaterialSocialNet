// const backendURL = 'http://127.0.0.1:5000'
// export const userLogin = createAsyncThunk(
//   'auth/login',
//   async ({ email, pass }, { rejectWithValue }) => {
//         await axios.get('http://25.32.11.98:8085/Auth', { params: { email: email, password: pass}}).then(
//           res => {
//             if (res.data!=401){
//             const token = res.data
//             localStorage.setItem("token",`Bearer ${token}`);
//             //props.setAccessTokenUser("Bearer " + token);  
//             //const user = jwtDecode(token);
//             //props.setAccoutSetting(user);
//             //navigate("/profile");
//             return token;
//             }else
//             alert('Неправильное имя пользователя или пароль!');
//           }
//         ).catch (error=>{
//             // return custom error message from API if any
//             if (error.response && error.response.data.message) {
//               return rejectWithValue(error.response.data.message)
//             } else {
//               return rejectWithValue(error.message)
//             }
//         })
//     // try {
//     //   // configure header's Content-Type as JSON
//     //   const config = {
//     //     headers: {
//     //       'Content-Type': 'application/json',
//     //     },
//     //   }
//     //   const { data } = axios.get('http://25.32.11.98:8085/Auth', { params: { email: email, password: pass}}
//     //   // store user's token in local storage
//     //   localStorage.setItem('token', data)
//     //   return data
//     // } catch (error) {
//     //   // return custom error message from API if any
//     //   if (error.response && error.response.data.message) {
//     //     return rejectWithValue(error.response.data.message)
//     //   } else {
//     //     return rejectWithValue(error.message)
//     //   }
//     // }
//   }
// )