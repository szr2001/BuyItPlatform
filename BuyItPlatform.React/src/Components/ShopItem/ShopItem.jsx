import './ShopItem.css'
import { useNavigate} from "react-router-dom";
import { useEffect, useState, useRef } from "react";

function ShopItem({ overrideClass, listings, user, slotIndex, editable }) {
    const navigate = useNavigate();
    const [listing, setListing] = useState(null);
    const isFirstRender = useRef(true); // because useEffect runs twitce due to StrictMode component

    useEffect(() => {

        const initListing = () => {
            const foundListing = listings.find(item => item.slotId === slotIndex);
            if (foundListing) {
                setListing(foundListing);
            }
        };

        if (isFirstRender.current) {
            isFirstRender.current = false;
            initListing();
        }

    }, []);

    const viewItem = () =>
    {
        if (listing) {
            navigate(`/ViewListing/${listing.userId}/${listing.id}`, { state: { listing, user } });
        }
        else if (editable) {
            navigate(`/AddListing/${slotIndex}`);
        }

    }

    return (
        
        <div className={`${overrideClass} shop-item`} onClick={viewItem}>
            {
                listing ?
                    <>
                        <label className="shop-item-name">{listing.name}</label>
                        <label className={`shop-item-type-${listing.listingType}`}>
                            {`${listing.listingType}ing`}
                        </label>
                        <label className="shop-item-price">{`${listing.price} ${listing.currency}`}</label>
                    </>
                    :
                    null
            }
            {
                listing ?
                    (
                        listing.imagePaths[0] == null ? 
                        <svg fill="currentColor" className="shop-item-icon-empty" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 256 241" enable-background="new 0 0 256 241" xml:space="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M166.872,161.729c20.669,33.071-2.183,75.755-40.534,77.52l-0.067,0.063h-2.398h-70.56h-0.004 c-27.801,0-48.018-21.349-50.518-45.727c-1.081-10.546,1.149-21.658,7.522-31.856l57.148-91.437L25.208,2.688h126.768 l-40.541,64.865c10.727-2.809,27.699-4.946,36.494-3.674l-3.21,7.52c-7.873-0.538-22.594,1.439-31.862,3.908L166.872,161.729z M254.53,133.319l-64.01,39.29l-52.79-85.72l7.13-15.45c1.16,0.07,2.34,0.16,3.53,0.28c7.42,0.78,13.72,2.57,18.22,5.09 c-1.1,3.32-0.79,7.08,1.19,10.3c3.54,5.77,11.1,7.58,16.88,4.03c5.77-3.54,7.58-11.1,4.04-16.87c-3.55-5.78-11.11-7.59-16.88-4.04 c-0.13,0.08-0.26,0.16-0.39,0.24c-6.37-3.85-14.88-6-23.38-6.76l8.43-18.28l45.08,1.47L254.53,133.319z M220.8,143.359l-3.6-6.13 c6.34-5.1,7.44-11.96,4.28-17.34c-3.16-5.38-7.94-7-16.61-5.37c-6.15,1.24-8.95,1.14-10.15-0.9c-0.92-1.56-0.68-4.08,3.14-6.33 c4.35-2.56,7.91-2.92,9.91-3.09l-2.15-7.7c-2.63,0.18-5.66,0.94-9.9,3.08l-3.16-5.38l-5.87,3.45l3.3,5.6 c-5.64,5.05-7.05,11.39-3.9,16.75c3.48,5.92,9.63,6.42,17.24,4.71c5.26-1.01,8.06-0.93,9.2,1.51c1.49,2.54-0.07,5.18-3.4,7.14 c-3.86,2.27-8.17,3.08-11.5,3.31l2.26,7.93c3.03-0.05,7.49-1.29,11.75-3.43l3.31,5.63L220.8,143.359z"></path> </g></svg>
                        :
                        <img className="shop-item-icon" src={listing.imagePaths[0]}></img>
                    )
                    :
                    editable ?
                        <svg className="shop-item-add-icon" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path opacity="0.5" d="M2 12C2 7.28595 2 4.92893 3.46447 3.46447C4.92893 2 7.28595 2 12 2C16.714 2 19.0711 2 20.5355 3.46447C22 4.92893 22 7.28595 22 12C22 16.714 22 19.0711 20.5355 20.5355C19.0711 22 16.714 22 12 22C7.28595 22 4.92893 22 3.46447 20.5355C2 19.0711 2 16.714 2 12Z" stroke="currentColor" stroke-width="1.5"></path> <path d="M15 12L12 12M12 12L9 12M12 12L12 9M12 12L12 15" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path> </g></svg>
                        :
                        null
            }
        </div>
    );
}

export default ShopItem;