//#region import
import React, { useState } from 'react';
import './App.css';
import Header from './components/Header/Header';
import Profile from './components/Profile/Profile';
import Auth from './components/Auth/Auth';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Registration from './components/Registration/Registration';
import ChatsConteiner from './components/Dialogs/ChatsContainer';
import UsersContainer from './components/Users/UsersContainer';
import Settings from './components/Settings/Setting';
import SettingSecurity from './components/Settings/SettingSecurity/SettingSecurity';
// import SettingNotifications from './components/Settings/SettingNotifications/SettingNotifications';
import SettingBlacklist from './components/Settings/SettingBlacklist/SettingBlacklist';
import AuthOrRegistration from './components/AuthOrRegistration/AuthOrRegistration';
import SettingAccountContainer from './components/Settings/SettingAccount/SettingAccountContainer';
import SettingPrivacyContainer from './components/Settings/SettingPrivacy/SettingPrivacyContainer';
import GalleryAllPhotoContainer from './components/Profile/GalleryAllPhoto/GalleryAllPhotoContainer';
import NewsContainer from './components/News/NewsContainer';
import PostContainer from './components/Profile/MyPosts/Post/PostContainer';
import RequireAuth from './components/RequireAuth/RequireAuth';
import ProfileContainer from './components/Profile/ProfileContainer';
import {
  useQuery,
  useMutation,
  useQueryClient,
  QueryClient,
  QueryClientProvider,
} from 'react-query';
import DashboardContainer from './components/Dashboard/DashboardContainer';
import SettingNotificationsContainer from './components/Settings/SettingNotifications/SettingNotificationContainer';
import NavBar from './components/NavBar/NavBar';
import AuthContainer from './components/Auth/AuthContainer';
import RegistrationContainer from './components/Registration/RegistrationContainer';
import ChatContainer from './components/Dialogs/Chat/ChatContainer';
import UserProfile from './components/UserProfile/UserProfile';
import Chat from './components/Dialogs/Chat/Chat';

//#endregion

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
    <BrowserRouter>
      <div className='app-wrapper'>
        <Header /> 
        <div className='app-wrapper-content'>
        <NavBar/>
          <Routes>
            <Route path='/post/*' element={<RequireAuth><PostContainer/></RequireAuth>}/>
            <Route path='/chats' element={<ChatsConteiner/>}></Route>
            <Route path='/dialogs/*' element={<Chat/>}/>
            <Route path='/profile' element={<RequireAuth><ProfileContainer/></RequireAuth>}/>  
            <Route path='/users' element={<UsersContainer/>}/>  
            <Route path='/user/*' element={<UserProfile/>}></Route>
            <Route path='/' element={<AuthContainer/>}/>  
            <Route path='/settings' element={<RequireAuth><Settings/></RequireAuth>}/>  
            {/* <Route path='/settingAccount' element={<RequireAuth><SettingAccountContainer /></RequireAuth>}/> 
            <Route path='/settingSecurity' element={<RequireAuth><SettingSecurity /></RequireAuth>}/> 
            <Route path='/settingPrivacy' element={<RequireAuth><SettingPrivacyContainer/></RequireAuth>}/> 
            <Route path='/settingNotification' element={<RequireAuth><SettingNotificationsContainer/></RequireAuth>}/> 
            <Route path='/settingBlacklist' element={<RequireAuth><SettingBlacklist/></RequireAuth>}/>  */}
            <Route path='/login' element={<AuthContainer/>}/>
            <Route path='/registration' element={<RegistrationContainer/>}/>
            <Route path='/gallery/*' element={<RequireAuth><GalleryAllPhotoContainer/></RequireAuth>}/>
            <Route path='/news' element={<RequireAuth><NewsContainer /></RequireAuth>}/>
            <Route path='/dashboard' element={<RequireAuth><DashboardContainer /></RequireAuth>}/>
          </Routes>
        </div>       
      </div>  
    </BrowserRouter>
    </ QueryClientProvider>
  );
}

export default App;