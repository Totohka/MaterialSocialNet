import profileReducer from "./profile-reducer";
import messagesReducer from "./messages-reducer";
import sidebarReducer from "./sidebar-reducer";

let store = {
    _msg: 'Очень длинное сообщение. Очень длинное сообщение. Очень длинное сообщение. Очень длинное сообщение. Очень длинное сообщение. Очень длинное сообщение. Очень длинное сообщение. Очень длинное сообщение.Очень длинное сообщение. Очень длинное сообщение. ',
    _state: {
        profilePage: {
            postsData: [
                {id:1, message: 'Привет!', name: 'Патюкова Анастасия'},
                {id:2, message: 'Неа)', name: 'Патюков Дмитрий'}
                ],
            newPostText: ''
        },
        messagesPage: {
            msgsData: [
                {id:1, message: 'Привет!', adress: 'incoming'},
                {id:2, message: 'Привет!', adress: 'outgoing'},
                {id:3, message: 'Как ты?!', adress: 'incoming'},
                {id:4, message: 'Нормально', adress: 'outgoing'}
                ],
            dialogsData: [
                {name : 'Анастасия Патюкова', id:1},
                {name : 'Алексей Волков', id: 2},
                {name :'Степан Кондрашов', id: 3},
                {name :'Мария Милешко', id: 4},
                {name : 'Эдуард Новиков', id: 5},
                {name : 'Отдел по работе с кадрами', id: 6},
                {name : 'Очень яркая беседа с длинным названием', id: 7}
                ],
            newMessageText: ''
        },
        sidebar: {}
    },
    getState(){
        return this._state;
    },
    _callSubscriber(){
        console.log('change');
    },

    subscribe(observer){
        this._callSubscriber = observer;
    },
    
    changeMessageInChat(message){
        this._state.messagesPage.newMessageText = message;
        this._callSubscriber(this._state);
    },

    changePostInArea(message){
        this._state.profilePage.newPostText = message;
        this._callSubscriber(this._state);
    },

    addPost(){
        let myName = 'Патюков Дмитрий';
        let newPost = {id: 5, message: this._state.profilePage.newPostText, name: myName};
        this._state.profilePage.postsData.push(newPost);
        this._callSubscriber(this._state);
    },

    sendMessage(){
        let newMsg = {id:5, message: this._state.messagesPage.newMessageText, adress: 'outgoing'};
        this._state.messagesPage.msgsData.push(newMsg);
        this._callSubscriber(this._state);
    },

    dispatch(action){
        this._state.profilePage = profileReducer(this._state.profilePage, action);
        this._state.messagesPage = messagesReducer(this._state.messagesPage, action);
        this._state.sidebar = sidebarReducer(this._state.sidebar, action);
        //this._callSubscriber(this._state);
    }
}

export default store;

window.store = store;
