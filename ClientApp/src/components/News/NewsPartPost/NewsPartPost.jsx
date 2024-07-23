import React from 'react';
import PostInPart from './PostInPart/PostInPart';

const NewsPartPost = (props) => {
    let twoPosts = Object.entries(props);
    let arraytwoPosts = twoPosts.map(el => <PostInPart post={el[1]}/>);
    return (
            <section class="cardsWrapperInPostInNews">
                {arraytwoPosts}
            </section>
            
    );
}

export default NewsPartPost;