import './Listing.css'
import Api from '../../Api/Api';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useContext } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import { useNavigate } from "react-router-dom";

function Listing() {
    const [authState, dispatch] = useContext(AuthContext);
    const navigate = useNavigate();

    const test = async () => {
        try {
            const response = await Api.get('listingsApi/GetListingWithId/2');

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.error(response.data);
                if (response.data.message === 'Refresh token is missing or expired') {
                    window.localStorage.setItem('user', null);
                    dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                    dispatch({ type: "SET_USER", payload: { user : null } });
                    navigate('/Login/');
                }
                return;
            }

            console.log(response);
        }
        catch (error) {
            toast.error(error.message, {
                autoClose: 2000 + error.message.length * 50,
            });
            console.error(error.message);
        }
        finally {
        }
    }

    return (
        <div className="holder">
            <button className="login-button" onClick={ test }> Get
            </button>
        </div>
  );
}

export default Listing;