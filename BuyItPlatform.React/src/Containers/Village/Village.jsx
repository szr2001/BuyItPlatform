import './Village.css'
import { toast } from 'react-toastify';
import { useState, useEffect, useRef } from 'react';
import { useContext } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import { useNavigate } from "react-router-dom";
import Api from '../../Api/Api';
import { Loading, UserOverview } from '../../Components'

function Village() {

    const [authState, dispatch] = useContext(AuthContext);
    const isFirstRender = useRef(true); // because useEffect runs twitce due to StrictMode component
    const [scoreboardUsers, setScoreboardUsers] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const loadScoreboardUsers = async () => {
            try {
                const response = await Api.get('userRatingApi/getUsersScoreboard');

                if (!response.data.success) {
                    toast.error(response.data.message, {
                        autoClose: 2000 + response.data.message.length * 50,
                    });
                    console.error(response);
                    return;
                }
                setScoreboardUsers(response.data.result);
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
        }

        if (isFirstRender.current) {
            isFirstRender.current = false;
            loadScoreboardUsers();
        }
    }, []); 

    return (
        <main>
            <div className="holder">
                <label className="village-title">Most respected villagers</label>
                <div className="villagers">
                {
                    scoreboardUsers === null ?
                        (
                                <Loading displayText="Fetching the legends..." />
                        ) : (
                            scoreboardUsers.map((user, index) => (
                                <UserOverview key={user.id || index} user={user} />
                            ))
                        )
                }
                </div>
            </div>
        </main>
    );
}

export default Village;