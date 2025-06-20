import './ListingsDisplay.css'
import { ListingOverview, Loading} from '../../Components';
import { useEffect, useState } from "react";

function ListingsDisplay({ listingsChunk, onScrolledToBottomCallback }) {
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const handleScroll = () => {
            const scrollTop = window.scrollY;
            const windowHeight = window.innerHeight;
            const fullHeight = document.documentElement.scrollHeight;

            if (scrollTop + windowHeight >= fullHeight - 50) {
                if (!loading && listingsChunk) {
                    setLoading(true);
                    onScrolledToBottomCallback?.(listingsChunk.length).finally(() => {
                        setLoading(false);
                    });
                    console.log("END");
                }
            }
        };

        window.addEventListener("scroll", handleScroll);
        return () => window.removeEventListener("scroll", handleScroll);
    }, [loading, onScrolledToBottomCallback]);

    return (
        <div className="listing-holder">
            <div className="listingDisplay">
                {
                    listingsChunk === null ?
                        <div className="listing-notfound-holder fade-in">
                            <svg className="listing-notfound-icon" version="1.0" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" viewBox="0 0 64 64" enable-background="new 0 0 64 64" xml: space="preserve" fill="currentColor"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <path fill="currentColor" d="M62.242,53.757L51.578,43.093C54.373,38.736,56,33.56,56,28C56,12.536,43.464,0,28,0S0,12.536,0,28 s12.536,28,28,28c5.56,0,10.736-1.627,15.093-4.422l10.664,10.664c2.344,2.344,6.142,2.344,8.485,0S64.586,56.101,62.242,53.757z M28,54C13.641,54,2,42.359,2,28S13.641,2,28,2s26,11.641,26,26S42.359,54,28,54z"></path> <path fill="currentColor" d="M28,4C14.745,4,4,14.745,4,28s10.745,24,24,24s24-10.745,24-24S41.255,4,28,4z M44,29c-0.553,0-1-0.447-1-1 c0-8.284-6.716-15-15-15c-0.553,0-1-0.447-1-1s0.447-1,1-1c9.389,0,17,7.611,17,17C45,28.553,44.553,29,44,29z"></path> </g> </g></svg>
                            <label className="listing-notfound-text">Nothing Found!</label>
                        </div>
                        :
                        listingsChunk.length === 0
                        ? <Loading />
                        : listingsChunk.map((listing, i) => (
                            <ListingOverview listing={listing} key={`listing-${i}`} />
                        ))
                }
            </div>
            <div className="listing-loading">
                {loading && <Loading />}
            </div>
        </div>
    );
}

export default ListingsDisplay;