import background from "./../../../images/background.jpg";
import logo from "./../../../images/logo.png";
import "./UserProfileInfo.css";
import location from "./../../../images/location.svg";
const Info = ({it}) =>{
    console.log(it);
    return(
    <div>
        <div className="profile_info">
            <div className="background">
            <img src={background} />
            </div>
            <div className="profilePicture">
            <img src={"http://25.32.11.98:8086/Avatars/"+it.avatar} />
            </div>
            <div className="description">
            <div className="name">
                <div id="default_name">
                <div>
                {it.firstName+" "+it.lastName}
                </div>
                </div>
            </div>
            <div className="country">
                <img src={location}></img>
                Страна: <div id="default_country">{it.country}</div>
            </div>
            <div className="city">
                <img src={location}></img>
                Город: <div id="default_city">{it.city}</div>
                </div>
            <div id="divB" className="birth">
                Дата рождения: <div id="default_birth">{it.dateBirthday.slice(0,10).split("-").reverse().join(".")}</div>
                </div>
            </div>
        </div>
        <div className="status">
            <div>
            <p>Статус: </p>
            <div class="user_status">{it.status}</div>
            </div>
        </div>
    </div>
    )
}
export default Info;