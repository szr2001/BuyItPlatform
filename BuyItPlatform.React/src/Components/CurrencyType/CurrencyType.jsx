import './CurrencyType.css'

function CurrencyType({ onCurrencyChangedCallback }) {

    return (

        <select className="currencyType-input listingType-text" onChange={(e) => { onCurrencyChangedCallback?.(e.target.value); }}>
            <option className="currencyType-option listingType-text" value="Eur">Eur</option>
            <option className="currencyType-option listingType-text" value="Ron">Ron</option>
            <option className="currencyType-option listingType-text" value="Usd">Usd</option>
            <option className="currencyType-option listingType-text" value="Aud">Aud</option>
            <option className="currencyType-option listingType-text" value="Cad">Cad</option>
        </select>
    );
}

export default CurrencyType;