import './ColorDisplay.css'

function ColorDisplay({ color }) {
    return (
        color ?
            <div className="fade-in">
                <div className="categorycolor-holder">
                    <div className={color}/>
                    <label className="categorydisplay-text">{color}</label>
                </div>
            </div>
            : null
    );
}

export default ColorDisplay;