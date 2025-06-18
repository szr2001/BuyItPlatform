import './ListingType.css'

function ListingType({onTypeChangedCallback}) {

    return (

        <select className="listingType-input listingType-text" onChange={(e) => { onTypeChangedCallback?.(e.target.value); } }>
            <option className="listingType-option listingType-text" value="Sell">Sell</option>
            <option className="listingType-option listingType-text" value="Buy">Buy</option>
        </select>
    );
}

export default ListingType;