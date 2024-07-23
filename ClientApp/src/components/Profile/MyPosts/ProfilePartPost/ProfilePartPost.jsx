import React from 'react';
import './ProfilePartPost.css';
import PostInPart from './PostInPart/PostInPart';

const ProfilePartPost = (props) => {
    let twoPosts = Object.entries(props);
    let arraytwoPosts //= twoPosts.map(el => <PostInPart key={el[1].id} post={el[1]}/>);
    return (
            <section className="cardsWrapperInPostInProfile">
                {arraytwoPosts}
            </section>
            
    );
}

export default ProfilePartPost;