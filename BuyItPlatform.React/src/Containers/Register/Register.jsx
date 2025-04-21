import './Register.css'
import { useState } from 'react';

function Register() {

    const handleRegister = async () =>
    {

    }

    return (
        <main>
            <div className="holder">
                <label className="welcome-text">Wait right there, are you from around these lands?</label>
                <div className="register-element">
                    <svg className="register-img" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21 12.4984L14 12.4984M3 12.4984L10 12.4984M7 4.5V19.4984M17 4.5V19.4984M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V10.9C21 8.65979 21 7.53968 20.564 6.68404C20.1805 5.93139 19.5686 5.31947 18.816 4.93597C17.9603 4.5 16.8402 4.5 14.6 4.5H9.4C7.15979 4.5 6.03968 4.5 5.18404 4.93597C4.43139 5.31947 3.81947 5.93139 3.43597 6.68404C3 7.53968 3 8.65979 3 10.9V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5ZM10 10.4984H14V14.4984H10V10.4984Z" stroke="currentColor" stroke-width="2" stroke-linejoin="round"></path> </g></svg>
                    <input className="register-text" autoComplete="off" type="text" id="searchItem" placeholder="Name" />
                </div>
                <div className="register-element">
                    <svg className="register-img" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21 12.4984L14 12.4984M3 12.4984L10 12.4984M7 4.5V19.4984M17 4.5V19.4984M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V10.9C21 8.65979 21 7.53968 20.564 6.68404C20.1805 5.93139 19.5686 5.31947 18.816 4.93597C17.9603 4.5 16.8402 4.5 14.6 4.5H9.4C7.15979 4.5 6.03968 4.5 5.18404 4.93597C4.43139 5.31947 3.81947 5.93139 3.43597 6.68404C3 7.53968 3 8.65979 3 10.9V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5ZM10 10.4984H14V14.4984H10V10.4984Z" stroke="currentColor" stroke-width="2" stroke-linejoin="round"></path> </g></svg>
                    <input className="register-text" autoComplete="off" type="text" id="searchItem" placeholder="Email" />
                </div>
                <div className="register-element">
                    <svg className="register-img" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21 12.4984L14 12.4984M3 12.4984L10 12.4984M7 4.5V19.4984M17 4.5V19.4984M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V10.9C21 8.65979 21 7.53968 20.564 6.68404C20.1805 5.93139 19.5686 5.31947 18.816 4.93597C17.9603 4.5 16.8402 4.5 14.6 4.5H9.4C7.15979 4.5 6.03968 4.5 5.18404 4.93597C4.43139 5.31947 3.81947 5.93139 3.43597 6.68404C3 7.53968 3 8.65979 3 10.9V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5ZM10 10.4984H14V14.4984H10V10.4984Z" stroke="currentColor" stroke-width="2" stroke-linejoin="round"></path> </g></svg>
                    <input className="register-text" autoComplete="off" type="text" id="searchItem" placeholder="Password" />
                </div>
                <div className="register-element">
                    <svg className="register-img" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21 12.4984L14 12.4984M3 12.4984L10 12.4984M7 4.5V19.4984M17 4.5V19.4984M6.2 19.5H17.8C18.9201 19.5 19.4802 19.5 19.908 19.282C20.2843 19.0903 20.5903 18.7843 20.782 18.408C21 17.9802 21 17.4201 21 16.3V10.9C21 8.65979 21 7.53968 20.564 6.68404C20.1805 5.93139 19.5686 5.31947 18.816 4.93597C17.9603 4.5 16.8402 4.5 14.6 4.5H9.4C7.15979 4.5 6.03968 4.5 5.18404 4.93597C4.43139 5.31947 3.81947 5.93139 3.43597 6.68404C3 7.53968 3 8.65979 3 10.9V16.3C3 17.4201 3 17.9802 3.21799 18.408C3.40973 18.7843 3.71569 19.0903 4.09202 19.282C4.51984 19.5 5.07989 19.5 6.2 19.5ZM10 10.4984H14V14.4984H10V10.4984Z" stroke="currentColor" stroke-width="2" stroke-linejoin="round"></path> </g></svg>
                    <input className="register-text" autoComplete="off" type="text" id="searchItem" placeholder="Repeat Password" />
                </div>
                <button className="register-button" onClick={handleRegister} >
                    <label className="register-text" >Maybe...</label>
                </button>
            </div>
      </main>
  );
}

export default Register;