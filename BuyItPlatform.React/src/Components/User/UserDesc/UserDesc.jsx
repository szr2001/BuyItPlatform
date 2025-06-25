import './UserDesc.css'
import { Loading } from '../../../Components';
import { useState } from 'react';
import Api from '../../../Api/Api';
import { useContext } from 'react';
import { AuthContext } from '../../Auth/Auth'
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
function UserDesc({ editable, desc }) {

    const [authState, dispatch] = useContext(AuthContext);
    const [descState, setDesc] = useState(desc);
    const [newDesc, setNewDesc] = useState(desc);
    const [editing, setEditing] = useState(false);
    const navigate = useNavigate();

    const updateDesc = async () => {

        try {
            if (newDesc === "") {
                toast.error("Description is empty", {
                    autoClose: 2000,
                });
                return;
            }

            setEditing(false);
            setDesc(null);
            const response = await Api.post(`authApi/user/updateUserDesc/${newDesc}`);

            if (!response.data.success) {
                const errorText = error?.response?.data?.message || error.message || "An unexpected error occurred";
                toast.error(errorText, {
                    autoClose: 2000 + errorText.length * 50,
                });
                console.log(response.data);
            }

            setDesc(newDesc);
        }
        catch (error) {
            if (error.status === 401) return;
            const errorText = error?.response?.data?.message || error.message || "An unexpected error occurred";
            toast.error(errorText, {
                autoClose: 2000 + errorText.length * 50,
            });
            console.log(error);
            if (error.status === 403) {
                window.localStorage.setItem('user', null);
                dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                dispatch({ type: "SET_USER", payload: { user: null } });
                navigate('/Login/');
            }
        }
        finally {
        }
    }

    return descState ?
        (
            editable ?
                editing ?
                    <div className="userdesc-holder fade-in">
                        <textarea className="userdesc-input userdesc-text" autoComplete="off"
                            maxLength={250} type="text" value={newDesc}
                            onChange={(e) => { setNewDesc(e.target.value); }}
                            placeholder="desc..."></textarea>
                        <svg className="userdesc-icon userdesc-icon-hover" onClick={updateDesc} viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path fill-rule="evenodd" clip-rule="evenodd" d="M12 22C7.28595 22 4.92893 22 3.46447 20.5355C2 19.0711 2 16.714 2 12C2 7.28595 2 4.92893 3.46447 3.46447C4.92893 2 7.28595 2 12 2C16.714 2 19.0711 2 20.5355 3.46447C22 4.92893 22 7.28595 22 12C22 16.714 22 19.0711 20.5355 20.5355C19.0711 22 16.714 22 12 22ZM16.0303 8.96967C16.3232 9.26256 16.3232 9.73744 16.0303 10.0303L11.0303 15.0303C10.7374 15.3232 10.2626 15.3232 9.96967 15.0303L7.96967 13.0303C7.67678 12.7374 7.67678 12.2626 7.96967 11.9697C8.26256 11.6768 8.73744 11.6768 9.03033 11.9697L10.5 13.4393L14.9697 8.96967C15.2626 8.67678 15.7374 8.67678 16.0303 8.96967Z" fill="currentColor"></path> </g></svg>
                        <svg className="userdesc-icon userdesc-icon-hover" onClick={() => { setEditing(false);}} fill="currentColor" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <title>cancel</title> <path d="M16 29c-7.18 0-13-5.82-13-13s5.82-13 13-13 13 5.82 13 13-5.82 13-13 13zM21.961 12.209c0.244-0.244 0.244-0.641 0-0.885l-1.328-1.327c-0.244-0.244-0.641-0.244-0.885 0l-3.761 3.761-3.761-3.761c-0.244-0.244-0.641-0.244-0.885 0l-1.328 1.327c-0.244 0.244-0.244 0.641 0 0.885l3.762 3.762-3.762 3.76c-0.244 0.244-0.244 0.641 0 0.885l1.328 1.328c0.244 0.244 0.641 0.244 0.885 0l3.761-3.762 3.761 3.762c0.244 0.244 0.641 0.244 0.885 0l1.328-1.328c0.244-0.244 0.244-0.641 0-0.885l-3.762-3.76 3.762-3.762z"></path> </g></svg>
                    </div>
                    :
                    <div className="userdesc-holder">
                        <div className="fade-in">
                            <label className="userdesc-text">{descState}</label>
                        </div>
                        <svg className="userdesc-edit-icon userdesc-icon-hover" onClick={() => { setEditing(true); } } viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21.2799 6.40005L11.7399 15.94C10.7899 16.89 7.96987 17.33 7.33987 16.7C6.70987 16.07 7.13987 13.25 8.08987 12.3L17.6399 2.75002C17.8754 2.49308 18.1605 2.28654 18.4781 2.14284C18.7956 1.99914 19.139 1.92124 19.4875 1.9139C19.8359 1.90657 20.1823 1.96991 20.5056 2.10012C20.8289 2.23033 21.1225 2.42473 21.3686 2.67153C21.6147 2.91833 21.8083 3.21243 21.9376 3.53609C22.0669 3.85976 22.1294 4.20626 22.1211 4.55471C22.1128 4.90316 22.0339 5.24635 21.8894 5.5635C21.7448 5.88065 21.5375 6.16524 21.2799 6.40005V6.40005Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M11 4H6C4.93913 4 3.92178 4.42142 3.17163 5.17157C2.42149 5.92172 2 6.93913 2 8V18C2 19.0609 2.42149 20.0783 3.17163 20.8284C3.92178 21.5786 4.93913 22 6 22H17C19.21 22 20 20.2 20 18V13" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                    </div>
                :
                <div className="userdesc-holder fade-in">
                    <label userName="userdesc-text">{descState}</label>
                </div>
        )
        :
        (
            <div className="userdesc-holder">
                <Loading />
            </div>
        );
}

export default UserDesc;