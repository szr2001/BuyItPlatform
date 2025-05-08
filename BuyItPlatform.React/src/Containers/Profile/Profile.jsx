import './Profile.css'
import { useParams } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import Api from '../../Api/Api';
import { Loading } from '../../Components'
import { useNavigate } from "react-router-dom";
function Profile() {
    const { userId } = useParams();
    const [authState, dispatch] = useContext(AuthContext);
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        //useEffect can't be async, declare an async function and call it
        const initAuth = async () => {
            try {

            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };

        initAuth();
    }, []);

    return (
        <main>
            {loading ? <Loading /> :
                (
                    <label>{userId}</label>
                )
            }
      </main>
  );
}

export default Profile;