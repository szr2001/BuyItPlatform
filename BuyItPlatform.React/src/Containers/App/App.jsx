import './App.css'
import { Home, Login, Register, Profile } from "../../Containers";
import { Routes, Route } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import { Navbar, Auth } from '../../Components';

function App() {
    return (
        <BrowserRouter>
            <Auth>
                <Navbar/>
                <Routes>
                    <Route path="/" element={<Home/>} />
                    <Route path="/Login" element={<Login/>} />
                    <Route path="/Register" element={<Register/>} />
                    <Route path="/Profile/:userId" element={<Profile />} />
                </Routes>
            </Auth>
        </BrowserRouter>
    );
}

export default App;