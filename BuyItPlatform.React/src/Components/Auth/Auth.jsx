import { createContext, useReducer, useEffect } from 'react';

const AuthContext = createContext();
const initialAuthState = {
    isAuthenticated: false,
    user: { username: "", email: "", id: "" },
};

function Auth({ children }) {

    const reducer = (state, action) => {
        switch (action.type) {
            case "SET_AUTH":
                return { ...state, isAuthenticated: action.payload.isAuthenticated };
            case "SET_USER":
                return { ...state, user: action.payload.user };
            default:
                return state;
        }
    };
    const [state, dispatch] = useReducer(reducer, initialAuthState);

    useEffect(() => {
        const userFromStorage = JSON.parse(localStorage.getItem('user'));

        dispatch({ type: "SET_USER", payload: { user: userFromStorage } });
        dispatch({ type: "SET_AUTH", payload: { isAuthenticated: userFromStorage !== null } });

    }, []); 

    return (
        //useContext tutorial
        //https://www.youtube.com/watch?v=ttopq_3Cqns
        <AuthContext.Provider value={[state, dispatch] }>
            { children }
        </AuthContext.Provider>
    )
}

export default Auth;
export { AuthContext };