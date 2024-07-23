import React from 'react';
import './PostInNews.css';

const PostInNews = (props) => {
    return (
      <div className='postInNews' style={{backgroundImage: "url('https://w.forfun.com/fetch/03/03f8cd3f6796daaacc1fe43ffb7704b7.jpeg')"}}>
            <div>
              {props.name}
            </div>
            <div>
              Сообщение: {props.message}
            </div>
      </div>
    );
}

export default PostInNews;