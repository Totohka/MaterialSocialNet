import React from 'react';
import { sendMsgActionCreator, changeMessageInChatActionCreator } from '../../redux/messages-reducer';
import { connect } from 'react-redux';
import Chats from './Chats';

let mapStateToProps = (state) => {
    return{
        dialogsData: state.messagesPage.dialogsData,
        msgsData: state.messagesPage.msgsData,
        newMessageText: state.messagesPage.newMessageText
    }
}

let mapDispatchToProps = (dispatch) => {
    return{
        updateNewMessageText: (text) => {
            dispatch(changeMessageInChatActionCreator(text));
        },
        sendMsg: () => {
            dispatch(sendMsgActionCreator());
            dispatch(changeMessageInChatActionCreator(''));
        }
    }
}

const ChatsContainer = connect(mapStateToProps, mapDispatchToProps)(Chats);
export default ChatsContainer;