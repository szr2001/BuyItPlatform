import './CurrencyType.css'

function CurrencyType({ onCurrencyChangedCallback }) {

    return (

        <select className="currencyType-input listingType-text" onChange={(e) => { onCurrencyChangedCallback(e.target.value); }}>
            <option className="currencyType-option listingType-text" value="Eur">Eur</option>
            <option className="currencyType-option listingType-text" value="Ron">Ron</option>
        </select>
    );
}

export default CurrencyType;