import './Profile.css'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useParams } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import { useRef } from 'react';
import Api from '../../Api/Api';
import { useNavigate } from "react-router-dom";
import { Loading } from '../../Components'

function Profile() {
    const { userId } = useParams();
    const [authState, dispatch] = useContext(AuthContext);
    const [loading, setLoading] = useState(true);
    const [userProfile, setUser] = useState(null);
    const isFirstRender = useRef(true); // because useEffect runs twitce due to StrictMode component
    const navigate = useNavigate();

    useEffect(() => {

        const initAuth = async () => {
            try {
                const response = await Api.get(`authApi/user/GetUserProfile/${userId}`);

                if (!response.data.success) {
                    toast.error(response.data.message, {
                        autoClose: 2000 + response.data.message.length * 50,
                    });
                    console.log(response);
                    if (response.data.message === 'Refresh token is missing or expired') {
                        window.localStorage.setItem('user', null);
                        dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                        dispatch({ type: "SET_USER", payload: { user: null } });
                        navigate('/Login/');
                    }
                    return;
                }

                setUser(response.data.result);
            }
            catch (error) {
                toast.error(error.message, {
                    autoClose: 2000 + error.message.length * 50,
                });
                console.log(error.message);
            }
            finally {
                setLoading(false);
            }
        };

        if (isFirstRender.current) {
            isFirstRender.current = false; 
            initAuth();
        }

    }, []);

    return (
        <main>
            {loading ? <Loading /> :
                (
                    userProfile === null ? (<label>User Not Found</label>) : 
                        (
                            <div>
                                <label>{userProfile.id} | </label>
                                <label>{userProfile.userName} | </label>
                                <label>{userProfile.description} | </label>
                                <label>{userProfile.rating} | </label>
                            </div>
                        )
                )
            }
      </main>
  );
}

export default Profile;