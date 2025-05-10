import './Profile.css'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useParams } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import { useRef } from 'react';
import Api from '../../Api/Api';
import { useNavigate } from "react-router-dom";
import { UserDesc, UserName, UserPic, UserPhone, UserRating } from '../../Components'

function Profile() {
    const {userId } = useParams();
    const [authState, dispatch] = useContext(AuthContext);
    const [userProfile, setUser] = useState(null);
    const isFirstRender = useRef(true); // because useEffect runs twitce due to StrictMode component
    const navigate = useNavigate();

    useEffect(() => {

        const initAuth = async () => {
            try {
                const response = await Api.get(`authApi/user/getUserProfile/${userId}`);

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
            }
        };

        if (isFirstRender.current) {
            isFirstRender.current = false; 
            initAuth();
        }

    }, []);

    return (
        <main>
            <div className="holder">
                <div className="profile">
                    <div className="profile-left">
                        <UserPic editable={true} picLink={"https://i.imgur.com/sqNAHAw.png"}/>
                        <UserName editable={true} name={ "CocaineMaster"} />
                        <UserRating editable={true} rating={9} />
                        <UserPhone editable={true} phone={"07719827345"} />
                    </div>
                    <div className="profile-right">
                        <UserDesc editable={true} desc={"I am here to sell cocaine, please, $40/g, I will not negotiate, this cocaine stuff is very expensive and please"}/>
                    </div>
                </div>
            </div>
        </main>
  );
}

export default Profile;