import './Colors.css'
import { useState, useRef } from 'react';

function Colors({ onColorChanged }) {
    const [selectedColor, setColor] = useState(null);

    const colors = [
        "Red",
        "Blue",
        "Green",
        "Yellow",
        "Black",
        "White",
        "Gray",
        "Orange",
        "Purple",
        "Brown",
        "Pink",
        "Gold",
        "Silver"
    ];


    const handleColorClick = (color) => {

        if (selectedColor === color) {
            setColor(null);
            onColorChanged?.(null);
            console.log(null);
        } else {
            setColor(color);
            onColorChanged?.(color);
            console.log(color);
        }
    };

    return (
        <div className="fade-in colors-holder">
            {
                colors.map((color, i) => (
                    <div key={`color-${i}`} onClick={() => { handleColorClick(color)} }
                        className={`${selectedColor === null ? "fade-back" : selectedColor !== color ? 'almost-fade' : 'selected-color'}`}>
                        <div className="color-holder">
                            <div className={color} />
                            <label className="color-text">{color}</label>
                        </div>
                    </div>
                ))
            }
        </div>
    );
}

export default Colors;