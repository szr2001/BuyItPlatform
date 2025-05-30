import './ViewListing.css'
import { useParams } from 'react-router-dom';
import { useLocation } from 'react-router-dom';

function ViewListing() {
    const {userId, listingId } = useParams();
    const location = useLocation();
    const listing = location.state?.listing;

    return (
        <main>
            <div className="holder">
                <label>{listing.name}</label>
                <label>{listing.description}</label>
                <label>{listing.price}</label>
                <label>{listing.currency}</label>
                <label>{listing.category}</label>
            </div>
        </main>
  );
}

export default ViewListing;