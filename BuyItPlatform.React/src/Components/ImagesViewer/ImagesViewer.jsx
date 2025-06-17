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
                imgCount > -1 ?
                    <>
                        <div className="imgview-background">
                            <img ref={imgRef} src={imagePaths[imgIndex]} className="imgview-img fade-in" />
                            {
                                imgCount > 0 ?
                                <>
                                    <svg onClick={nextImage} className="imgview-button imgview-button-right" viewBox="-0.5 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M13.4092 16.4199L10.3492 13.55C10.1935 13.4059 10.0692 13.2311 9.98425 13.0366C9.89929 12.8422 9.85547 12.6321 9.85547 12.4199C9.85547 12.2077 9.89929 11.9979 9.98425 11.8035C10.0692 11.609 10.1935 11.4342 10.3492 11.29L13.4092 8.41992" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M7 21.4202H17C19.2091 21.4202 21 19.6293 21 17.4202V7.42017C21 5.21103 19.2091 3.42017 17 3.42017H7C4.79086 3.42017 3 5.21103 3 7.42017V17.4202C3 19.6293 4.79086 21.4202 7 21.4202Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                                    <svg onClick={prevImage} className="imgview-button imgview-button-left" viewBox="-0.5 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M13.4092 16.4199L10.3492 13.55C10.1935 13.4059 10.0692 13.2311 9.98425 13.0366C9.89929 12.8422 9.85547 12.6321 9.85547 12.4199C9.85547 12.2077 9.89929 11.9979 9.98425 11.8035C10.0692 11.609 10.1935 11.4342 10.3492 11.29L13.4092 8.41992" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M7 21.4202H17C19.2091 21.4202 21 19.6293 21 17.4202V7.42017C21 5.21103 19.2091 3.42017 17 3.42017H7C4.79086 3.42017 3 5.21103 3 7.42017V17.4202C3 19.6293 4.79086 21.4202 7 21.4202Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                                </>
                                :
                                null
                            }
                        </div>
                    </>
                     :
                    <div className="imgview-background">
                        <svg fill="currentColor" className="shop-item-icon-empty" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 256 241" enable-background="new 0 0 256 241" xml:space="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M166.872,161.729c20.669,33.071-2.183,75.755-40.534,77.52l-0.067,0.063h-2.398h-70.56h-0.004 c-27.801,0-48.018-21.349-50.518-45.727c-1.081-10.546,1.149-21.658,7.522-31.856l57.148-91.437L25.208,2.688h126.768 l-40.541,64.865c10.727-2.809,27.699-4.946,36.494-3.674l-3.21,7.52c-7.873-0.538-22.594,1.439-31.862,3.908L166.872,161.729z M254.53,133.319l-64.01,39.29l-52.79-85.72l7.13-15.45c1.16,0.07,2.34,0.16,3.53,0.28c7.42,0.78,13.72,2.57,18.22,5.09 c-1.1,3.32-0.79,7.08,1.19,10.3c3.54,5.77,11.1,7.58,16.88,4.03c5.77-3.54,7.58-11.1,4.04-16.87c-3.55-5.78-11.11-7.59-16.88-4.04 c-0.13,0.08-0.26,0.16-0.39,0.24c-6.37-3.85-14.88-6-23.38-6.76l8.43-18.28l45.08,1.47L254.53,133.319z M220.8,143.359l-3.6-6.13 c6.34-5.1,7.44-11.96,4.28-17.34c-3.16-5.38-7.94-7-16.61-5.37c-6.15,1.24-8.95,1.14-10.15-0.9c-0.92-1.56-0.68-4.08,3.14-6.33 c4.35-2.56,7.91-2.92,9.91-3.09l-2.15-7.7c-2.63,0.18-5.66,0.94-9.9,3.08l-3.16-5.38l-5.87,3.45l3.3,5.6 c-5.64,5.05-7.05,11.39-3.9,16.75c3.48,5.92,9.63,6.42,17.24,4.71c5.26-1.01,8.06-0.93,9.2,1.51c1.49,2.54-0.07,5.18-3.4,7.14 c-3.86,2.27-8.17,3.08-11.5,3.31l2.26,7.93c3.03-0.05,7.49-1.29,11.75-3.43l3.31,5.63L220.8,143.359z"></path> </g></svg>
                    </div>
            }
        </div>
    );
}

export default ImagesViewer;