import './RatingStars.css'

function RatingStars({stars, highlight}) {
    let fullStars = stars / 2;
    let halfStars = stars % 2;
    return (
        <>
            { //always add a key on map elements so react can update them correctly
                Array.from({ length: fullStars }).map((_, i) => (
                    <svg key={`full-star-${i}`} className={highlight ? "ratingstars-icon-highlight fade-in" : "ratingstars-icon fade-in"} viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M9.00001 0H7.00001L5.51292 4.57681L0.700554 4.57682L0.0825195 6.47893L3.97581 9.30756L2.48873 13.8843L4.10677 15.0599L8.00002 12.2313L11.8933 15.0599L13.5113 13.8843L12.0242 9.30754L15.9175 6.47892L15.2994 4.57681L10.4871 4.57681L9.00001 0Z" fill="currentColor"></path> </g></svg>
                ))
            }
            {
                Array.from({ length: halfStars }).map((_, x) => (
                    <svg key={`half-star-${x}`} className={highlight ? "ratingstars-icon-highlight fade-in" : "ratingstars-icon fade-in"} viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M0.700554 4.57682L0.0825195 6.47893L3.97581 9.30756L2.48873 13.8843L4.10677 15.0599L8.00002 12.2313L8.00001 0H7.00001L5.51292 4.57681L0.700554 4.57682Z" fill="currentColor"></path> </g></svg>))
            }
        </>
    );
}

export default RatingStars;