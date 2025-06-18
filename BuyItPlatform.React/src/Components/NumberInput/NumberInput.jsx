import './NumberInput.css'
import { useState } from 'react';

function NumberInput({onNumberChangedCallback, maxChar}) {

    const [price, setPrice] = useState(25);

    return (
        
        <div>
            <input className="numberInput" autoComplete="off"
                maxLength={maxChar} type="tel" value={price}
                onChange={(e) => {
                    const numericValue = e.target.value.replace(/\D/g, '');
                    setPrice(numericValue);
                    onNumberChangedCallback?.(numericValue);
                }}
                placeholder="phone..."></input>
        </div>
    );
}

export default NumberInput;