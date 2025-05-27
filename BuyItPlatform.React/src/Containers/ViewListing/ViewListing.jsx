import './ViewListing.css'
import { useParams } from 'react-router-dom';

function ViewListing() {
    const {listingId } = useParams();

    return (
        <div className="holder">
        </div>
  );
}

export default ViewListing;