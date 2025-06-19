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
                if (!loading) {
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
                {listingsChunk.length === 0
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