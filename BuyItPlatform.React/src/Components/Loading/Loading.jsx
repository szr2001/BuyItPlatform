import './Loading.css'

function Loading({displayText, overrideClass}) {
    return (
        <div className={`holder loading-holder fade-in ${overrideClass}`}>
            <svg className="loading-icon" fill="currentColor" viewBox="-7 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <title>loading</title> <path d="M2.080 14.040l4-1.040c0.44-0.12 0.72-0.56 0.6-1.040-0.12-0.44-0.56-0.72-1.040-0.6l-2.080 0.56c0.68-0.88 1.56-1.6 2.64-2.080 1.64-0.72 3.44-0.76 5.12-0.12 1.64 0.64 2.96 1.92 3.68 3.52 0.2 0.44 0.68 0.6 1.12 0.44 0.44-0.2 0.6-0.68 0.44-1.12-0.88-2.040-2.52-3.6-4.6-4.44-2.080-0.8-4.36-0.76-6.4 0.12-1.36 0.6-2.48 1.52-3.36 2.68l-0.52-1.96c-0.12-0.44-0.56-0.72-1.040-0.6-0.44 0.12-0.72 0.56-0.6 1.040l1.040 4c0.12 0.56 0.4 0.8 1 0.64zM17.72 22.52l-1.040-3.96c0 0-0.16-0.8-0.96-0.6v0l-4 1.040c-0.44 0.12-0.72 0.56-0.6 1.040 0.12 0.44 0.56 0.72 1.040 0.6l2.080-0.56c-1.76 2.32-4.88 3.28-7.72 2.16-1.64-0.64-2.96-1.92-3.68-3.52-0.2-0.44-0.68-0.6-1.12-0.44-0.44 0.2-0.6 0.68-0.44 1.12 0.88 2.040 2.52 3.6 4.6 4.44 1 0.4 2 0.56 3.040 0.56 2.64 0 5.12-1.24 6.72-3.4l0.52 1.96c0.080 0.36 0.44 0.64 0.8 0.64 0.080 0 0.16 0 0.2-0.040 0.4-0.16 0.68-0.6 0.56-1.040z"></path> </g></svg>
            <label className="loading-text">{displayText}</label>
        </div>
  );
}

export default Loading;