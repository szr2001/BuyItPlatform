import './NumberInput.css'
import { useState } from 'react';

function NumberInput({onNumberChangedCallback}) {

    const [price, setPrice] = useState(25);

    return (
        
        <div>
            <input className="numberInput" autoComplete="off"
                maxLength={15} type="tel" value={price}
                onChange={(e) => {
                    const numericValue = e.target.value.replace(/\D/g, '');
                    setPrice(numericValue);
                    onNumberChangedCallback(numericValue);
                }}
                placeholder="phone..."></input>
        </div>
    );
}

export default NumberInput;