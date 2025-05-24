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
    
    return (
        <div className="holder">
        </div>
  );
}

export default Listing;