import './App.css'
import { Home, Login, Register } from "../../Containers";
import { Routes, Route } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import { Navbar } from '../../Components';
import { createContext, useReducer, useEffect } from 'react';

const AuthContext = createContext();

const initialAuth = {
    isAuthenticated: false,
    user: {username: "", email: "", id:""},
};

function App() {

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
    const [state, dispatch] = useReducer(reducer, initialAuth);

    useEffect(() => {
        const userFromStorage = JSON.parse(localStorage.getItem('user'));
        console.log(userFromStorage);

        dispatch({ type: "SET_AUTH", payload: { isAuthenticated: userFromStorage !== null } });

        dispatch({ type: "SET_USER", payload: { user: userFromStorage } });

    }, []); 

    return (
        //useContext tutorial
        //https://www.youtube.com/watch?v=ttopq_3Cqns
        <BrowserRouter>
            <AuthContext.Provider value={[state, dispatch]}>
                <Navbar/>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/Login" element={<Login />} />
                    <Route path="/Register" element={<Register />} />
                </Routes>
            </AuthContext.Provider>
        </BrowserRouter>
    );
}

export default App;
export { AuthContext };