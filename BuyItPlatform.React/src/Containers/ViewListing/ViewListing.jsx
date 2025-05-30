import './ViewListing.css'
import { useParams } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { ImagesViewer } from '../../Components';

function ViewListing() {
    const {userId, listingId } = useParams();
    const location = useLocation();
    const listing = location.state?.listing;

    return (
        <main>
            <div className="holder">
                <div className="viewlisting">
                    <div className="viewlisting-left">
                        <ImagesViewer imagePaths={listing.imagePaths} />
                        <label>{listing.name}</label>
                        <label>{listing.description}</label>

                    </div>
                    <div className="viewlisting-right">
                        <label>{listing.price}</label>
                        <label>{listing.currency}</label>
                        <label>{listing.category}</label>
                    </div>
                </div>
            </div>
        </main>
  );
}

export default ViewListing;