import './ShopItem.css'
import { useNavigate } from "react-router-dom";

function ShopItem({ overrideClass, listing, editable }) {
    const navigate = useNavigate();

    const viewItem = () =>
    {
        if (listing) {
            navigate(`/ViewListing/${listing.userId}/${listing.id}`, { state: { listing } });
        }
        else if (editable) {
            navigate(`/AddListing/`);
        }

    }

    return (
        <div className={`${overrideClass} shop-item`} onClick={viewItem}>
            {
                listing ?
                    <img className="shop-item-icon" src={listing.imagePaths[0]}></img>
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