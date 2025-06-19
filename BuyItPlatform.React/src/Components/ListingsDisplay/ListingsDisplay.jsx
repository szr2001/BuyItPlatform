import './ListingsDisplay.css'
import { useState, useEffect } from 'react';
import { ListingOverview, Loading} from '../../Components';

function ListingsDisplay({ listingsChunk, listingsPerPageCount, totalListings, maxVisiblePages, onPageChangedCallback, onReachedTheEndCallback }) {
    const [activePage, setActivePage] = useState(1);
    const totalPage = Math.max(Math.ceil(totalListings / listingsPerPageCount), 1);
    const [activeListings, setActiveListings] = useState([]);

    const displayListings = (skip,take) => {
        if (listingsChunk && listingsChunk.length > 0) {
            console.log("Skip,Take", {skip,take});
            console.log(listingsChunk);
            setActiveListings(listingsChunk.slice(skip, take));
        }
    }

    useEffect(() => {
        console.log(listingsChunk);
        displayListings(0, listingsPerPageCount);
    }, [listingsChunk, listingsPerPageCount]);

    const openPage = (page) => {
        setActivePage(page);
        let skip = listingsPerPageCount * (page);
        let take = skip + listingsPerPageCount;
        displayListings(skip, take);
    }

    return (
        <div className = "listing-holder" >
            <div className="listingDisplay">
                {
                    activeListings.length === 0 ?
                    <Loading />
                    :
                    activeListings.map((listing, i) => (
                        <ListingOverview listing={listing} key={`listing-${i}`} />
                    ))
            }
            </div>
            <div className="listing-pages-holder">
                {
                    Array.from({ length: totalPage }).map((_, x) => (
                        <label className="listing-page-number" key={`listing-page-${x}`} onClick={() => { openPage(x); } }>{x + 1}</label>
                    ))
                }
            </div>
        </div >
  );
}

export default ListingsDisplay;