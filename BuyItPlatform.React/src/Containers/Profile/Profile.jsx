import './Profile.css'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useParams } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import { useRef } from 'react';
import Api from '../../Api/Api';
import { useNavigate } from "react-router-dom";
import { UserDesc, UserName, UserPic, UserPhone, UserRating, Loading } from '../../Components'

function Profile() {
    const {userId } = useParams();
    const [authState, dispatch] = useContext(AuthContext);
    const [userProfile, setUser] = useState(null);
    const [isOwnProfile, setIsOwnProfile] = useState(false);
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
                    console.error(response);
                }

                setUser(response.data.result);
            }
            catch (error) {
                toast.error(error.response.data.message, {
                    autoClose: 2000 + error.response.data.message.length * 50,
                });
                if (error.status === 401) {
                    window.localStorage.setItem('user', null);
                    dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                    dispatch({ type: "SET_USER", payload: { user: null } });
                    navigate('/Login/');
                }
                console.log(error);
            }
            finally {
                setIsOwnProfile(userId === authState.user.id);
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
                {
                    userProfile ?
                        <div className="profile">
                            <div className="profile-left">
                                <UserPic editable={isOwnProfile} picLink={userProfile.profileImgLink}/>
                                <UserName editable={isOwnProfile} name={userProfile.userName} />
                                <UserRating ratable={!isOwnProfile} rating={10} />
                                <UserPhone editable={isOwnProfile} phone={userProfile.phoneNumber} />
                                {
                                    isOwnProfile === true ? (<label>Rawr</label>) : null
                                }
                            </div>
                            <div className="profile-right">
                                <UserDesc editable={isOwnProfile} desc={userProfile.description}/>
                            </div>
                        </div>
                        :
                        <div className="profile">
                            <Loading/>
                        </div>
                }
            </div>
        </main>
  );
}

export default Profile;