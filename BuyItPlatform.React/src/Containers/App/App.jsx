import './App.css'
import { Home, Login, Register, Profile, Village, ViewListing, AddListing } from "../../Containers";
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
                    <Route path="/Village" element={<Village />} />
                    <Route path="/Profile/:userId" element={<Profile />} />
                    <Route path="/ViewListing/:userId/:listingId" element={<ViewListing />} />
                    <Route path="/AddListing/:slotIndex" element={<AddListing />} />
                </Routes>
            </Auth>
        </BrowserRouter>
    );
}

export default App;