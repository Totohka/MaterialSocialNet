import React from 'react';
import { NavLink } from 'react-router-dom';
import bin from './../../../images/bin.svg';
import "./DialogsItem.css";

const DialogItem = (props) => {
    let path = '/dialogs/' + props.id;
    return (
        <>
        <div className='line'></div>
        <div className='flex-chatItem'>
            <NavLink to={path}>
                <div className='item_chat'>
                    <div className='item_chat_img'></div>
                    <p>{props.name}</p> 
                </div>    
            </NavLink>
            <img src={bin}></img>
        </div>
        
        </>
    );
}

export default DialogItem;