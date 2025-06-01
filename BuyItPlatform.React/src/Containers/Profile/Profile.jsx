import './Profile.css'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useParams } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import { useRef } from 'react';
import Api from '../../Api/Api';
import { useNavigate } from "react-router-dom";
import { UserDesc, UserName, UserPic, UserPhone, UserRating, Loading, ShopItem } from '../../Components'

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

                console.log(response);
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

    const logOut = async () =>
    {
        try {
            const response = await Api.get(`authApi/auth/logout`);

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.error(response);
            }
        }
        catch (error) {
            toast.error(error.response.data.message, {
                autoClose: 2000 + error.response.data.message.length * 50,
            });
            console.log(error);
        }
        finally {
            window.localStorage.setItem('user', null);
            dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
            dispatch({ type: "SET_USER", payload: { user: null } });
            navigate('/Login/');
        }
    }

    return (
        <main>
            <div className="holder">
                {
                    userProfile ?
                        <div className="profile">
                            <div className="profile-left">
                                <UserPic editable={isOwnProfile} picLink={userProfile.profileImgLink}/>
                                <UserName editable={isOwnProfile} name={userProfile.userName} />
                                <UserRating ratable={!isOwnProfile} targetUserId={userId} totalRating={userProfile.numberOfRatings} rating={userProfile.averageRating} />
                                <UserPhone editable={isOwnProfile} phone={userProfile.phoneNumber} />
                                {
                                    isOwnProfile === true ?
                                    (
                                            <button onClick={logOut} className="profile-logout">
                                                <svg className="profile-icon" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M15 12L2 12M2 12L5.5 9M2 12L5.5 15" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M9.00195 7C9.01406 4.82497 9.11051 3.64706 9.87889 2.87868C10.7576 2 12.1718 2 15.0002 2L16.0002 2C18.8286 2 20.2429 2 21.1215 2.87868C22.0002 3.75736 22.0002 5.17157 22.0002 8L22.0002 16C22.0002 18.8284 22.0002 20.2426 21.1215 21.1213C20.3531 21.8897 19.1752 21.9862 17 21.9983M9.00195 17C9.01406 19.175 9.11051 20.3529 9.87889 21.1213C10.5202 21.7626 11.4467 21.9359 13 21.9827" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path> </g></svg>
                                                Log Out
                                            </button>
                                    ) : null
                                }
                            </div>
                            <div className="profile-right">
                                <UserDesc editable={isOwnProfile} desc={userProfile.description} />
                                <div className="shop-table" >
                                    <img className="shop-table-background" src="/userShop.png"/>
                                    <ShopItem editable={isOwnProfile} user={userProfile} slotIndex={0} listings={userProfile.listings} overrideClass={"shop-item-1"}/>
                                    <ShopItem editable={isOwnProfile} user={userProfile} slotIndex={1} listings={userProfile.listings} overrideClass={"shop-item-2"}/>
                                    <ShopItem editable={isOwnProfile} user={userProfile} slotIndex={2} listings={userProfile.listings} overrideClass={"shop-item-3"}/>
                                    <ShopItem editable={isOwnProfile} user={userProfile} slotIndex={3} listings={userProfile.listings} overrideClass={"shop-item-4"}/>
                                    <ShopItem editable={isOwnProfile} user={userProfile} slotIndex={4} listings={userProfile.listings} overrideClass={"shop-item-5"}/>
                                </div>
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