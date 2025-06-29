﻿import './Register.css'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import { Loading } from '../../Components'
import Api from '../../Api/Api';

function Register() {

    const [registerData, setRegisterData] = useState({
        Name: '',
        Email: '',
        Password: '',
        RepeatPassword: '',
    });
    const [loading, setIsLoading] = useState(false);

    const navigate = useNavigate();

    //handle input using the input name and value to assign the value to the object parameter with the same name
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setRegisterData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleRegister = async () =>
    {
        setIsLoading(true);

        try {
            const response = await Api.post('authApi/auth/register', registerData);

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.error(response);
                return;
            }

            console.log(response.data);
            navigate("/Login");
        }
        catch (error) {
            const errorText = error?.response?.data?.message || error.message || "An unexpected error occurred";
            toast.error(errorText, {
                autoClose: 2000 + errorText.length * 50,
            });
            console.error(error);
        }
        finally {
            setIsLoading(false);
        }
    }

    return (
        <main>
            {
                loading ? (
                    <div className="holder">
                        <Loading displayText = "Registering peasent..." />
                    </div>
                ):(
                    <div className="holder">
                    <label className="welcome-text fade-in">Wait right there, are you from around these lands?</label>
                    <div className="register-element fade-in">
                        <svg className="register-img" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g id="User / User_Card_ID"> <path id="Vector" d="M6 18C6.06366 18 6.12926 18 6.19691 18H12M6 18C5.01173 17.9992 4.49334 17.9868 4.0918 17.7822C3.71547 17.5905 3.40973 17.2837 3.21799 16.9074C3 16.4796 3 15.9203 3 14.8002V9.2002C3 8.08009 3 7.51962 3.21799 7.0918C3.40973 6.71547 3.71547 6.40973 4.0918 6.21799C4.51962 6 5.08009 6 6.2002 6H17.8002C18.9203 6 19.4796 6 19.9074 6.21799C20.2837 6.40973 20.5905 6.71547 20.7822 7.0918C21 7.5192 21 8.07899 21 9.19691V14.8031C21 15.921 21 16.48 20.7822 16.9074C20.5905 17.2837 20.2837 17.5905 19.9074 17.7822C19.48 18 18.921 18 17.8031 18H12M6 18C6.00004 16.8954 7.34317 16 9 16C10.6569 16 12 16.8954 12 18M6 18C6 18 6 17.9999 6 18ZM18 14H14M18 11H15M9 13C7.89543 13 7 12.1046 7 11C7 9.89543 7.89543 9 9 9C10.1046 9 11 9.89543 11 11C11 12.1046 10.1046 13 9 13Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> </g> </g></svg>
                        <input className="register-text" autoComplete="off" type="text" placeholder="Name" onChange={handleInputChange} name="Name" />
                    </div>
                    <div className="register-element fade-in">
                        <svg className="register-img" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M4 7.00005L10.2 11.65C11.2667 12.45 12.7333 12.45 13.8 11.65L20 7" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> <rect x="3" y="5" width="18" height="14" rx="2" stroke="currentColor" stroke-width="2" stroke-linecap="round"></rect> </g></svg>
                        <input className="register-text" autoComplete="off" type="email" placeholder="Email" onChange={handleInputChange} name="Email" />
                    </div>
                    <div className="register-element fade-in">
                        <svg className="register-img" viewBox="0 0 48 48" xmlns="http://www.w3.org/2000/svg" fill="currentColor"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g id="Layer_2" data-name="Layer 2"> <g id="invisible_box" data-name="invisible box"> <rect width="48" height="48" fill="none"></rect> </g> <g id="Layer_7" data-name="Layer 7"> <g> <path d="M39,18H35V13A11,11,0,0,0,24,2H22A11,11,0,0,0,11,13v5H7a2,2,0,0,0-2,2V44a2,2,0,0,0,2,2H39a2,2,0,0,0,2-2V20A2,2,0,0,0,39,18ZM15,13a7,7,0,0,1,7-7h2a7,7,0,0,1,7,7v5H15ZM37,42H9V22H37Z"></path> <circle cx="15" cy="32" r="3"></circle> <circle cx="23" cy="32" r="3"></circle> <circle cx="31" cy="32" r="3"></circle> </g> </g> </g> </g></svg>
                        <input className="register-text" autoComplete="off" type="password" placeholder="Password" onChange={handleInputChange} name="Password" />
                    </div>
                    <div className="register-element fade-in">
                        <svg className="register-img" viewBox="0 0 48 48" xmlns="http://www.w3.org/2000/svg" fill="currentColor"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g id="Layer_2" data-name="Layer 2"> <g id="invisible_box" data-name="invisible box"> <rect width="48" height="48" fill="none"></rect> </g> <g id="Layer_7" data-name="Layer 7"> <g> <path d="M39,18H35V13A11,11,0,0,0,24,2H22A11,11,0,0,0,11,13v5H7a2,2,0,0,0-2,2V44a2,2,0,0,0,2,2H39a2,2,0,0,0,2-2V20A2,2,0,0,0,39,18ZM15,13a7,7,0,0,1,7-7h2a7,7,0,0,1,7,7v5H15ZM37,42H9V22H37Z"></path> <circle cx="15" cy="32" r="3"></circle> <circle cx="23" cy="32" r="3"></circle> <circle cx="31" cy="32" r="3"></circle> </g> </g> </g> </g></svg>
                        <input className="register-text" autoComplete="off" type="password" placeholder="Repeat Password" onChange={handleInputChange} name="RepeatPassword" />
                    </div>
                    <button className="register-button fade-in" onClick={handleRegister} >
                        <label className="register-text" >Maybe...</label>
                    </button>
                    </div>
                )
            }
      </main>
  );
}

export default Register;