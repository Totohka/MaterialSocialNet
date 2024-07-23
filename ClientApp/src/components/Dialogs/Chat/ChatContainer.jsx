import React from 'react';
import { sendMsgActionCreator, changeMessageInChatActionCreator } from '../../../redux/messages-reducer';
import { connect } from 'react-redux';
import Dialog from './Chat';
import Chat from './Chat';

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

const ChatContainer = connect(mapStateToProps, mapDispatchToProps)(Chat);
export default ChatContainer;