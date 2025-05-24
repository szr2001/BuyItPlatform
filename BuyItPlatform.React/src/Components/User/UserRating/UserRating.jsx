import './UserRating.css'
import { Loading } from '../../../Components';
import { useState } from 'react';
import Api from '../../../Api/Api';
import { useContext, useEffect } from 'react';
import { AuthContext } from '../../Auth/Auth'
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";

function UserRating({ ratable, targetUserId, rating, totalRaters }) {
    const [authState, dispatch] = useContext(AuthContext);
    const [rateState, setRate] = useState(rating);
    const [newRate, setNewRate] = useState(5);
    const [editing, setEditing] = useState(false);
    const [leftRating, setLeftRating] = useState(false);
    const navigate = useNavigate();

    const rateUser = async () => {

        try {
            if (!targetUserId) {
                toast.error("couldn't find target user", {
                    autoClose: 2000,
                });
                return;
            }
            const response = await Api.post(`userRatingApi/rateUser`, { TargetUserId: targetUserId, Rating: newRate});

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.error(response.data);
                return;
            }

            setEditing(false);
            setLeftRating(true);
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

    function RenderStars({stars}){
        let fullStars = stars / 2;
        let halfStars = stars % 2;
        return (
            <>
                {
                    Array.from({ length: fullStars }).map((_, i) => (
                        <svg className={editing ? "userrate-icon-editing fade-in" : "userrate-icon fade-in"} viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M9.00001 0H7.00001L5.51292 4.57681L0.700554 4.57682L0.0825195 6.47893L3.97581 9.30756L2.48873 13.8843L4.10677 15.0599L8.00002 12.2313L11.8933 15.0599L13.5113 13.8843L12.0242 9.30754L15.9175 6.47892L15.2994 4.57681L10.4871 4.57681L9.00001 0Z" fill="currentColor"></path> </g></svg>
                    ))
                }
                {
                    Array.from({ length: halfStars }).map((_, x) => (
                        <svg className={editing ? "userrate-icon-editing fade-in" : "userrate-icon fade-in" } viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M0.700554 4.57682L0.0825195 6.47893L3.97581 9.30756L2.48873 13.8843L4.10677 15.0599L8.00002 12.2313L8.00001 0H7.00001L5.51292 4.57681L0.700554 4.57682Z" fill="currentColor"></path> </g></svg>))
                }
            </>
        );
    };


    return rateState && rateState != -1 ?
        (
            !leftRating && ratable ?
                editing ?
                    <div>
                        <div className="userrate-holder userrated-stars-input"
                            onMouseMove={(e) => {
                                const rect = e.currentTarget.getBoundingClientRect();
                                const relativeX = e.clientX - rect.left;
                                const percent = relativeX / rect.width;
                                const hoveredValue = Math.min(10, Math.max(0, Math.round(percent * 10)));
                                setNewRate(hoveredValue);
                            }}>
                            <RenderStars stars={newRate}/>
                        </div>
                        <svg className="userrate-icon-options userrate-icon-hover" onClick={rateUser} viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path fill-rule="evenodd" clip-rule="evenodd" d="M12 22C7.28595 22 4.92893 22 3.46447 20.5355C2 19.0711 2 16.714 2 12C2 7.28595 2 4.92893 3.46447 3.46447C4.92893 2 7.28595 2 12 2C16.714 2 19.0711 2 20.5355 3.46447C22 4.92893 22 7.28595 22 12C22 16.714 22 19.0711 20.5355 20.5355C19.0711 22 16.714 22 12 22ZM16.0303 8.96967C16.3232 9.26256 16.3232 9.73744 16.0303 10.0303L11.0303 15.0303C10.7374 15.3232 10.2626 15.3232 9.96967 15.0303L7.96967 13.0303C7.67678 12.7374 7.67678 12.2626 7.96967 11.9697C8.26256 11.6768 8.73744 11.6768 9.03033 11.9697L10.5 13.4393L14.9697 8.96967C15.2626 8.67678 15.7374 8.67678 16.0303 8.96967Z" fill="currentColor"></path> </g></svg>
                        <svg className="userrate-icon-options userrate-icon-hover" onClick={() => { setEditing(false); }} fill="currentColor" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <title>cancel</title> <path d="M16 29c-7.18 0-13-5.82-13-13s5.82-13 13-13 13 5.82 13 13-5.82 13-13 13zM21.961 12.209c0.244-0.244 0.244-0.641 0-0.885l-1.328-1.327c-0.244-0.244-0.641-0.244-0.885 0l-3.761 3.761-3.761-3.761c-0.244-0.244-0.641-0.244-0.885 0l-1.328 1.327c-0.244 0.244-0.244 0.641 0 0.885l3.762 3.762-3.762 3.76c-0.244 0.244-0.244 0.641 0 0.885l1.328 1.328c0.244 0.244 0.641 0.244 0.885 0l3.761-3.762 3.761 3.762c0.244 0.244 0.641 0.244 0.885 0l1.328-1.328c0.244-0.244 0.244-0.641 0-0.885l-3.762-3.76 3.762-3.762z"></path> </g></svg>
                    </div>
                    :
                    <div className="userrate-holder">
                        <RenderStars stars={rateState} />
                        <svg className="userrate-icon-backgroound userrate-edit-icon userrate-icon-hover" onClick={() => { setEditing(true); }} viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21.2799 6.40005L11.7399 15.94C10.7899 16.89 7.96987 17.33 7.33987 16.7C6.70987 16.07 7.13987 13.25 8.08987 12.3L17.6399 2.75002C17.8754 2.49308 18.1605 2.28654 18.4781 2.14284C18.7956 1.99914 19.139 1.92124 19.4875 1.9139C19.8359 1.90657 20.1823 1.96991 20.5056 2.10012C20.8289 2.23033 21.1225 2.42473 21.3686 2.67153C21.6147 2.91833 21.8083 3.21243 21.9376 3.53609C22.0669 3.85976 22.1294 4.20626 22.1211 4.55471C22.1128 4.90316 22.0339 5.24635 21.8894 5.5635C21.7448 5.88065 21.5375 6.16524 21.2799 6.40005V6.40005Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M11 4H6C4.93913 4 3.92178 4.42142 3.17163 5.17157C2.42149 5.92172 2 6.93913 2 8V18C2 19.0609 2.42149 20.0783 3.17163 20.8284C3.92178 21.5786 4.93913 22 6 22H17C19.21 22 20 20.2 20 18V13" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                    </div>
                :
                <div className="userrate-holder">
                    <RenderStars stars={rateState} />
                </div>
        )
        :
        (
            <div className="userphone-holder">
                <Loading />
            </div>
        );
}

export default UserRating;