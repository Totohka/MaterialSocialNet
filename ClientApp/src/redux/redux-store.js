import { combineReducers, legacy_createStore } from "redux";
import profileReducer from "./profile-reducer";
import messagesReducer from "./messages-reducer";
import sidebarReducer from "./sidebar-reducer";
import usersFindReducer from "./users-find-reducer";
import settingsAccountReducer from "./settings-account-reducer";
import settingsPrivacyReducer from "./settings-privacy-reducer";
import newsReducer from "./news-reducer";
import galleryReducer from "./gallery-reducer";
import settingNotificationReducer from "./settings-notification-reducer";
import dashboardReducer from "./dashboard-reducer";

let reducers = combineReducers({
    profilePage: profileReducer,
    messagesPage: messagesReducer,
    sidebar: sidebarReducer,
    usersFindPage: usersFindReducer,
    settingAccount: settingsAccountReducer,
    settingPrivacy: settingsPrivacyReducer,
    settingNotification: settingNotificationReducer,
    newsPage: newsReducer,
    dashboardPage: dashboardReducer,
    gallery: galleryReducer 
});

let store = legacy_createStore(reducers);

export default store;

