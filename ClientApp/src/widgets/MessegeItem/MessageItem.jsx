import React, { useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './MessageItem.css';

const MessageItem = ({adress, message, chat, dateSend, mesId, allReacts }) => {
    const token = localStorage.getItem("token");
    const us = jwtDecode(token.slice(7,));
    let date = new Date(dateSend+'Z').toLocaleTimeString();
    return (
        <div className={adress.id==us.id?"outgoing":"incoming"}>
            
            <div className='item_message'>
                {message}
                <div className='time_message'>{date.slice(0,-3)}</div>
            </div>
            <div className='ava_message'>
                <img src={'http://25.32.11.98:8086/Avatars/'+adress.avatar}></img>
            </div>
        </div>
    );
}

export default MessageItem;