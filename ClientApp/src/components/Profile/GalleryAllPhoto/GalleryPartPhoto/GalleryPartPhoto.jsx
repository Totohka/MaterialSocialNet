import './GalleryPartPhoto.css';
import PhotoInPart from './PhotoInPart/PhotoInPart';


const GalleryPartPhoto = (props) => {
    let fourPhoto = Object.entries(props);
    let arrayFoutPhoto = fourPhoto.map(el => <PhotoInPart photo={el[1]}/>);
    return (
        <div class="containerForGroupPhoto">
            {arrayFoutPhoto}
        </div>
    );
}

export default GalleryPartPhoto;