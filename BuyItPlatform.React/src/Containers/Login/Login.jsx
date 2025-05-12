import './Login.css'
import { toast } from 'react-toastify';
import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import Api from '../../Api/Api';
import { Loading } from '../../Components'
import { useContext } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'

function Login() {

    const [authState, dispatch] = useContext(AuthContext);
    const [loading, setIsLoading] = useState(false);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();
    const test = async () => {
        setIsLoading(true);
        try {
            const response = await Api.get('authApi/auth/refreshToken');

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.log(response.data);
                if (response.data.message === 'Refresh token is missing or expired') {
                    window.localStorage.setItem('user', null);
                    dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                    dispatch({ type: "SET_USER", payload: { user: null } });
                }
                return;
            }

            navigate('/');
            console.log(response);
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
    const handleLogin = async () =>
    {
        setIsLoading(true);
        try {
            const response = await Api.post('authApi/auth/login', {Email: email, Password: password});

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.log(response.data);
                return;
            }
            window.localStorage.setItem('user', JSON.stringify(response.data.result));

            //move the logic in the APP or in a AUTH component
            dispatch({ type: "SET_USER", payload: { user: response.data.result } });
            dispatch({ type: "SET_AUTH", payload: { isAuthenticated: true} });

            navigate('/');
            console.log(response);
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
                          <button className="login-button fade-in" onClick={test} >
                              <label className="login-text" >Test!</label>
                          </button>
                    </div>
                  )
          }
      </main>
  );
}

export default Login;