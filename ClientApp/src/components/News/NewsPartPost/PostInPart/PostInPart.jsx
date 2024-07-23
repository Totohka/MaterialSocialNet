import React from 'react';
import standartImgPost from './../../../../images/standartLogo.jpg';
import { useNavigate } from 'react-router-dom';

const NewsPartPost = (props) => {
    const navigate = useNavigate();
    let id = props.post.id;
    let tags = props.post.tegs.map(el => <div className='tag'>{el}</div>);
    let imgForBackgroundImage = props.post.img === '' ? standartImgPost : props.post.img;
    return (
        <div className="cardGridSpaceInNews" onClick={() => navigate(`/post/${id}`)}>
            <div className="cardInNews">
                <div className="imageInNavInNews" style={{backgroundImage: `url(${imgForBackgroundImage})`}}/>
                <div className='textInPostInNews'>
                    <div className="dateInPostInNews">{props.post.date}</div>
                    <div className='authorInPostInNews'>{props.post.name}</div>
                    <h1 className='titleInPostInNews'>{props.post.title}</h1>
                    <div className='smallMessageInPostInNews'>{props.post.smallMessage}</div>
                    <div className="tagsInPostInNews">
                        {tags}
                    </div>
                </div>
            </div>
        </div>          
    );
}

export default NewsPartPost;