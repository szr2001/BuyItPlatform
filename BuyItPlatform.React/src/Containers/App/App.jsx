import './App.css'
import { Home, Login, Register } from "../../Containers";
import { Routes, Route } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import { Navbar } from '../../Components';
function App() {
    return (
        <BrowserRouter>
            <Navbar/>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/Login" element={<Login />} />
                <Route path="/Register" element={<Register />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;