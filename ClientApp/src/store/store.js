import { configureStore } from "@reduxjs/toolkit";
import { apiSlice } from "../features/api/apiSlice";
import userReducer from "../features/auth/userSlice";
import userPhotosReducer from "../features/user/userPhotoSlice";
import userPostsReducer from "../features/user/userPostsSlice";
import chatsReducer from "../features/chats/chatsConnectionsSlice";
import { persistStore, persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { combineReducers } from "redux";
import { signal } from "../app/helpers/withSignalR";
import sessionStorage from "redux-persist/es/storage/session";
import notificatiosReducer from "../features/notifications/notificatiosSlice";
import { signalNotif } from "../app/helpers/signalRNotifications";



const rootReducer = combineReducers({
    user: userReducer,
    userPhotos: userPhotosReducer,
    userPosts: userPostsReducer,
    chats: chatsReducer,
    notifications: notificatiosReducer,
    [apiSlice.reducerPath]: apiSlice.reducer,
    // [signal.reducerPath]: signal.reducer,
    // profilePage: profileReducer,
    // messagesPage: messagesReducer,
    // sidebar: sidebarReducer,
    // usersFindPage: usersFindReducer,
    // settingAccount: settingsAccountReducer,
    // settingPrivacy: settingsPrivacyReducer,
    // settingNotification: settingNotificationReducer,
    // newsPage: newsReducer,
    // dashboardPage: dashboardReducer,
    // gallery: galleryReducer 
});
const persistConfig = {
    key: 'root',
    storage,
    whiteList:['user']
};
const persistedReducer = persistReducer(persistConfig, rootReducer);
const store = configureStore({
    reducer: persistedReducer,
    middleware: getDefaultMiddleware =>
        getDefaultMiddleware().concat([apiSlice.middleware, signal, signalNotif])
})
export const persistor = persistStore(store);
export default store;