import './Listing.css'
import axios from '../../Api/axios';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
function Listing() {

    const test = async () => {
        try {
            const response = await axios.get('listingsApi/GetListingWithId/2');

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.log(response.data);
                return;
            }

            console.log(response);
        }
        catch (error) {
            toast.error(error.message, {
                autoClose: 2000 + error.message.length * 50,
            });
            console.log(error.message);
        }
        finally {
        }
    }

    return (
        <div className="holder">
            <button className="login-button" onClick={ test }> Get
            </button>
            <ToastContainer
                toastClassName="custom-toast"
                bodyClassName="custom-toast-body"
                progressClassName="custom-progress"
            />
        </div>
  );
}

export default Listing;