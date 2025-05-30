import './ImagesViewer.css'
import { useState, useRef } from 'react';
function ImagesViewer({imagePaths}) {
    const imgRef = useRef(null);
    const [ imgIndex, setImgindex ] = useState(0);
    const [imgCount, setImgCount] = useState(imagePaths.length - 1);

    const nextImage = () => {

        if (imgIndex < imgCount) {
            setImgindex(imgIndex + 1);
        }
        else{
            setImgindex(0);
        }
        replayFadeAnim();
        console.log(imgIndex)
    }
    const prevImage = () => {

        if (imgIndex >= imgCount) {
            setImgindex(imgIndex - 1);
        }
        else {
            setImgindex(imgCount);
        }
        replayFadeAnim();
        console.log(imgIndex)
    }
    const replayFadeAnim = () => {
        if (imgRef.current) {
            const el = imgRef.current;
            el.classList.remove("fade-in");

            //force refresh
            void el.offsetWidth;

            el.classList.add("fade-in");
        }
    }
    return (
        <div className="imgview-holder">
            {
                imgCount > 0 ?
                    <>
                        <div className="imgview-background">
                            <img ref={imgRef} src={imagePaths[imgIndex]} className="imgview-img fade-in"/>
                            <svg onClick={nextImage} className="imgview-button imgview-button-right" viewBox="-0.5 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M13.4092 16.4199L10.3492 13.55C10.1935 13.4059 10.0692 13.2311 9.98425 13.0366C9.89929 12.8422 9.85547 12.6321 9.85547 12.4199C9.85547 12.2077 9.89929 11.9979 9.98425 11.8035C10.0692 11.609 10.1935 11.4342 10.3492 11.29L13.4092 8.41992" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M7 21.4202H17C19.2091 21.4202 21 19.6293 21 17.4202V7.42017C21 5.21103 19.2091 3.42017 17 3.42017H7C4.79086 3.42017 3 5.21103 3 7.42017V17.4202C3 19.6293 4.79086 21.4202 7 21.4202Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                            <svg onClick={prevImage} className="imgview-button imgview-button-left" viewBox="-0.5 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M13.4092 16.4199L10.3492 13.55C10.1935 13.4059 10.0692 13.2311 9.98425 13.0366C9.89929 12.8422 9.85547 12.6321 9.85547 12.4199C9.85547 12.2077 9.89929 11.9979 9.98425 11.8035C10.0692 11.609 10.1935 11.4342 10.3492 11.29L13.4092 8.41992" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M7 21.4202H17C19.2091 21.4202 21 19.6293 21 17.4202V7.42017C21 5.21103 19.2091 3.42017 17 3.42017H7C4.79086 3.42017 3 5.21103 3 7.42017V17.4202C3 19.6293 4.79086 21.4202 7 21.4202Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                        </div>
                    </>
                     :
                    <img src={imagePaths[imgIndex]} className="imgview-img"/>
            }
        </div>
    );
}

export default ImagesViewer;