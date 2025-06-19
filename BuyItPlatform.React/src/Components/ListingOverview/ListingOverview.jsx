import './ListingOverview.css'
import { useNavigate } from "react-router-dom";

function ListingOverview({listing}) {
    
    const navigate = useNavigate();

    const viewListing = async () => {
        navigate(`/ViewListing/${listing.userId}/${listing.id}`, { state: { listing } });
    }

    return (
        <div className={`holder listingOverview-holder fade-in`} onClick={viewListing}>
            {
                listing.imagePaths.length === 0 || listing.imagePaths[0] === " " ?
                    <svg fill="currentColor" className="listingOverview-img" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 256 241" enable-background="new 0 0 256 241" xml:space="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M166.872,161.729c20.669,33.071-2.183,75.755-40.534,77.52l-0.067,0.063h-2.398h-70.56h-0.004 c-27.801,0-48.018-21.349-50.518-45.727c-1.081-10.546,1.149-21.658,7.522-31.856l57.148-91.437L25.208,2.688h126.768 l-40.541,64.865c10.727-2.809,27.699-4.946,36.494-3.674l-3.21,7.52c-7.873-0.538-22.594,1.439-31.862,3.908L166.872,161.729z M254.53,133.319l-64.01,39.29l-52.79-85.72l7.13-15.45c1.16,0.07,2.34,0.16,3.53,0.28c7.42,0.78,13.72,2.57,18.22,5.09 c-1.1,3.32-0.79,7.08,1.19,10.3c3.54,5.77,11.1,7.58,16.88,4.03c5.77-3.54,7.58-11.1,4.04-16.87c-3.55-5.78-11.11-7.59-16.88-4.04 c-0.13,0.08-0.26,0.16-0.39,0.24c-6.37-3.85-14.88-6-23.38-6.76l8.43-18.28l45.08,1.47L254.53,133.319z M220.8,143.359l-3.6-6.13 c6.34-5.1,7.44-11.96,4.28-17.34c-3.16-5.38-7.94-7-16.61-5.37c-6.15,1.24-8.95,1.14-10.15-0.9c-0.92-1.56-0.68-4.08,3.14-6.33 c4.35-2.56,7.91-2.92,9.91-3.09l-2.15-7.7c-2.63,0.18-5.66,0.94-9.9,3.08l-3.16-5.38l-5.87,3.45l3.3,5.6 c-5.64,5.05-7.05,11.39-3.9,16.75c3.48,5.92,9.63,6.42,17.24,4.71c5.26-1.01,8.06-0.93,9.2,1.51c1.49,2.54-0.07,5.18-3.4,7.14 c-3.86,2.27-8.17,3.08-11.5,3.31l2.26,7.93c3.03-0.05,7.49-1.29,11.75-3.43l3.31,5.63L220.8,143.359z"></path> </g></svg>
                    :
                    <img className="listingOverview-img" src={listing.imagePaths[0]} />
            }
            <div className="listingOverview-right">
                <label className="listingOverview-title">{listing.name}</label>
                <label className="listingOverview-desc">{listing.description}</label>
                <label className="listing-category listing-category-text">{listing.category}</label>
                <div className="listingOverview-holder-price">
                    {
                        listing.listingType === "Sell" ?
                            (
                                <>
                                    <svg className={`listingOverview-icon listingOverview-${listing.listingType}`} fill="currentColor" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" viewBox="0 0 372.372 372.372" xml: space="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <path d="M368.712,219.925c-5.042-8.951-14.563-14.511-24.848-14.511c-4.858,0-9.682,1.27-13.948,3.672l-83.024,46.756 c-1.084,0.61-1.866,1.642-2.163,2.85c-1.448,5.911-4.857,14.164-12.865,19.911c-8.864,6.361-20.855,7.686-35.466,3.939 c-0.088-0.022-0.175-0.047-0.252-0.071L148.252,267.6c-2.896-0.899-4.52-3.987-3.621-6.882c0.72-2.316,2.83-3.872,5.251-3.872 c0.55,0,1.101,0.084,1.634,0.249l47.645,14.794c0.076,0.023,0.154,0.045,0.232,0.065c11.236,2.836,20.011,2.047,26.056-2.288 c7.637-5.48,8.982-15.113,9.141-16.528c0.006-0.045,0.011-0.09,0.014-0.136c0.003-0.023,0.004-0.036,0.005-0.039 c0.001-0.015,0.002-0.03,0.003-0.044c0.001-0.01,0.001-0.019,0.002-0.029c0.909-11.878-6.756-22.846-18.24-26.089l-0.211-0.064 c-0.35-0.114-35.596-11.626-58.053-18.034c-2.495-0.711-9.37-2.366-19.313-2.366c-13.906,0-34.651,3.295-54.549,19.025 L1.67,292.159c-1.889,1.527-2.224,4.278-0.758,6.215l44.712,59.06c0.725,0.956,1.801,1.584,2.99,1.744 c0.199,0.027,0.398,0.04,0.598,0.04c0.987,0,1.954-0.325,2.745-0.935l57.592-44.345c1.294-0.995,3.029-1.37,4.619-0.995 l93.02,21.982c6.898,1.63,14.353,0.578,20.523-2.9l130.16-73.304C371.555,251.012,376.418,233.61,368.712,219.925z"></path> <path d="M316.981,13.155h-170c-5.522,0-10,4.477-10,10v45.504c0,5.523,4.478,10,10,10h3.735v96.623c0,5.523,4.477,10,10,10h142.526 c5.523,0,10-4.477,10-10V78.658h3.738c5.522,0,10-4.477,10-10V23.155C326.981,17.632,322.503,13.155,316.981,13.155z M253.016,102.417h-42.072c-4.411,0-8-3.589-8-8c0-4.411,3.589-8,8-8h42.072c4.411,0,8,3.589,8,8 C261.016,98.828,257.427,102.417,253.016,102.417z M306.981,58.658h-3.738H160.716h-3.735V33.155h150V58.658z"></path> </g> </g></svg>
                                </>
                            ) :
                            (
                                <>
                                    <svg className={`listingOverview-icon listingOverview-${listing.listingType}`} fill="currentColor" version="1.2" baseProfile="tiny" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" viewBox="-351 153 256 256" xml: space="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M-116.4,345.9l-15.2-125.8h-30.5v-15.2h40.5v-49.7h-97.1v49.7h41.5v15.2H-243v-38.8h-50.8v38.8h-20.6l-15.2,125.8h-15.2 v56.9h243.7v-56.9H-116.4z M-153.7,254c5.6,0,10.2,4.6,10.2,10.2c0,5.6-4.6,10.2-10.2,10.2c-5.6,0-10.2-4.6-10.2-10.2 C-163.9,258.6-159.3,254-153.7,254z M-143.5,305.6c0,5.6-4.6,10.2-10.2,10.2c-5.6,0-10.2-4.6-10.2-10.2s4.6-10.2,10.2-10.2 C-148.1,295.4-143.5,299.9-143.5,305.6z M-210.4,195v-29h80.4v29H-210.4z M-186.7,254c5.6,0,10.2,4.6,10.2,10.2 c0,5.6-4.6,10.2-10.2,10.2c-5.6,0-10.2-4.6-10.2-10.2C-196.9,258.6-192.3,254-186.7,254z M-186.7,295.4c5.6,0,10.2,4.6,10.2,10.2 c0,5.6-4.6,10.2-10.2,10.2c-5.6,0-10.2-4.6-10.2-10.2C-196.9,300-192.3,295.4-186.7,295.4z M-287.8,250.5v-30.4v-32.8h38.8v32.8 v30.5v16.6h-38.8V250.5z M-299.1,258.5h5.3v14.6h50.8v-14.6h4.8l3.9,19.5H-303L-299.1,258.5z M-116.4,390.4h-213.2v-20.2h213.2 L-116.4,390.4L-116.4,390.4z"></path> </g></svg>
                                </>
                            )
                    }
                    <label className={`listingOverview-price listingOverview-${listing.listingType}`} >{listing.price}</label>
                    <label className={`listingOverview-price listingOverview-${listing.listingType}`} >{listing.currency}</label>
                </div>
            </div>
        </div>
    );
}

export default ListingOverview;