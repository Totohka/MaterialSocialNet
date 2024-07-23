import { NavLink } from "react-router-dom";
import './SmallDialogItem.css';
const SmallDialogItem = (props) => {
    let path = '/dialogs/' + props.id;
    return (
        <><div className='small-item-chat'>
            <NavLink to={path}>
                    <div className='small-item-chat-img'></div>
                    <p>{props.name}</p> 
            </NavLink>
            </div> 
        </>
    );
}

export default SmallDialogItem;