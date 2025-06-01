import './CommentsDisplay.css'
import Api from '../../Api/Api';
import { useState, useEffect } from 'react';
function CommentsDisplay({ listingId }) {
    const [loading, setIsLoading] = useState(false);

    useEffect(() => {
        const loadComments = async () => {
            const userFromStorage = JSON.parse(localStorage.getItem('user'));

            dispatch({ type: "SET_USER", payload: { user: userFromStorage } });
            dispatch({ type: "SET_AUTH", payload: { isAuthenticated: userFromStorage !== null } });

            try {
                const response = await axios.get('https://localhost:7000/gateway/ping');
                console.log(response);
            }
            catch (ex) {

                console.error(ex);
            }
            setIsLoading(false);
        }
        //loadComments();

    }, []); 

    return (
        <></>
    );
}

export default CommentsDisplay;