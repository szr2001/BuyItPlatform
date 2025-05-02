import './Login.css'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import axios from '../../Api/axios';
import { decodeToken, isExpired } from "react-jwt";
import { Loading } from '../../Components'
function Login() {

    const [loading, setIsLoading] = useState(false);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");


    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");
        const decoded = decodeToken(token);

        if (!isExpired(token)) {
            navigate("/");
        }
        return () => {
        };
    }, []);

    const handleLogin = async () =>
    {
        setIsLoading(true);
        try {
            const response = await axios.post('/login', {Email: email, Password: password});

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.log(response.data);
                return;
            }

            console.log(response);
            localStorage.setItem("token", response.data.result.token);
            navigate("/");
        }
        catch (error) {
            toast.error(error.message, {
                autoClose: 2000 + error.message.length * 50,
            });
            console.log(error.message);
        }
        finally {
            setIsLoading(false);
        }
    }

  return (
      <main>
          {
              loading ?
                  (
                    <div className="holder">
                        <Loading displayText="Verifying the crown m'lord..." />
                    </div>
                  ):(
                    <div className="holder">
                      <label className="welcome-text fade-in">Welcome home, M'lord!</label>
                      <div className="login-element fade-in">
                          <svg className="register-img" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M4 7.00005L10.2 11.65C11.2667 12.45 12.7333 12.45 13.8 11.65L20 7" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> <rect x="3" y="5" width="18" height="14" rx="2" stroke="currentColor" stroke-width="2" stroke-linecap="round"></rect> </g></svg>
                              <input onChange={(e) => { setEmail(e.target.value); } } className="login-text" autoComplete="off" type="email" placeholder="Email" />
                      </div>
                      <div className="login-element fade-in">
                          <svg className="register-img" viewBox="0 0 48 48" xmlns="http://www.w3.org/2000/svg" fill="currentColor"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g id="Layer_2" data-name="Layer 2"> <g id="invisible_box" data-name="invisible box"> <rect width="48" height="48" fill="none"></rect> </g> <g id="Layer_7" data-name="Layer 7"> <g> <path d="M39,18H35V13A11,11,0,0,0,24,2H22A11,11,0,0,0,11,13v5H7a2,2,0,0,0-2,2V44a2,2,0,0,0,2,2H39a2,2,0,0,0,2-2V20A2,2,0,0,0,39,18ZM15,13a7,7,0,0,1,7-7h2a7,7,0,0,1,7,7v5H15ZM37,42H9V22H37Z"></path> <circle cx="15" cy="32" r="3"></circle> <circle cx="23" cy="32" r="3"></circle> <circle cx="31" cy="32" r="3"></circle> </g> </g> </g> </g></svg>
                          <input onChange={(e) => { setPassword(e.target.value); }} className="login-text" autoComplete="off" type="password" placeholder="Password" />
                      </div>
                      <button className="login-button fade-in" onClick={handleLogin} >
                          <label className="login-text" >Thank you kindly!</label>
                      </button>
                    </div>
                  )
          }
          <ToastContainer
              toastClassName="custom-toast"
              bodyClassName="custom-toast-body"
              progressClassName="custom-progress"
          />
      </main>
  );
}

export default Login;