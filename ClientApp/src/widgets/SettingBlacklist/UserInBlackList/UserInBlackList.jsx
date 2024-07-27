import React from "react";
//import './UserInFind.css';

const UsersInBlackList = (props) => {
    return (
        <div>
            <div className="ContainerForUserInFind">
                <div className="ImgForUserInFind">
                    <img className="imgInUserForFind" src='https://fanibani.ru/images/wp-content/uploads/2021/06/na-avy-parni-35.jpg'></img>
                </div>
                <div className="UsernameForUserInFind">
                    {props.username}
                </div>
                <div>
                    <div className="BirthdayForUserInFind">
                        Год рождения: {props.birthday}
                    </div>
                    <div>
                        <div className="CountryForUserInFind">
                            Страна: {props.country}
                        </div>
                        <div className="CityForUserInFind">
                            Город: {props.city}
                        </div>
                    </div>
                </div>     
                <div className="DescriptionForUserInFind">
                    {props.description}
                </div>
                <div>
                    { props.followed ? 
                        <button onClick={() => {props.unfollow(props.id)}} className="buttonFollowForFindUsers">Отписаться</button> : 
                        <button onClick={() => {props.follow(props.id)}} className="buttonFollowForFindUsers">Подписаться</button> }
                </div>  
            </div>
        </div>
    )
}

export default UsersInFind;