import './ListingsDisplay.css'
import { ListingOverview, Loading } from '../../Components';

function ListingsDisplay({ listings, listingsPerPageCount, onPageChangedCallback }) {
    return (
        <div className = "listing-holder" >
            <div className="listingDisplay">
                {
                    listings.length === 0 ?
                    <Loading />
                    :
                    listings.map((listing, i) => (
                        <ListingOverview listing={listing} key={`listing-${i}`} />
                    ))
            }
            </div>
        </div >
  );
}

export default ListingsDisplay;