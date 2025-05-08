import { createContext, useReducer, useState, useEffect, useRef } from 'react';
import axios from 'axios'
import { Loading } from '../../Components'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const AuthContext = createContext();

const initialAuthState = {
    isAuthenticated: false,
    user: { username: "", email: "", id: "" },
    gatewayActive: false,
};

function Auth({ children }) {
    const isFirstRender = useRef(true); // because useEffect runs twitce due to StrictMode component
    const [isLoading, setIsLoading] = useState(true);

    const reducer = (state, action) => {
        switch (action.type) {
            case "SET_AUTH":
                return { ...state, isAuthenticated: action.payload.isAuthenticated };
            case "SET_USER":
                return { ...state, user: action.payload.user };
            case "SET_GATEWAYACTIVE":
                return { ...state, gatewayActive: action.payload.gatewayActive };
            default:
                return state;
        }
    };
    const [state, dispatch] = useReducer(reducer, initialAuthState);

    useEffect(() => {
        const initializeAuth = async () =>
        {
            const userFromStorage = JSON.parse(localStorage.getItem('user'));

            dispatch({ type: "SET_USER", payload: { user: userFromStorage } });
            dispatch({ type: "SET_AUTH", payload: { isAuthenticated: userFromStorage !== null } });

            try {
                const response = await axios.get('https://localhost:7054/gateway/ping');
                console.log(response);
            }
            catch (ex) {

                console.error(ex);
            }
            setIsLoading(false);
        }

        if (isFirstRender.current) {
            isFirstRender.current = false;
            initializeAuth();
        }
    }, []); 

    return (
        //useContext tutorial
        //https://www.youtube.com/watch?v=ttopq_3Cqns
        <AuthContext.Provider value={[state, dispatch] }>
            {isLoading ? (<Loading />) : children}
            <ToastContainer
                toastClassName="custom-toast"
                bodyClassName="custom-toast-body"
                progressClassName="custom-progress"
            />
        </AuthContext.Provider>
    )
}

export default Auth;
export { AuthContext };