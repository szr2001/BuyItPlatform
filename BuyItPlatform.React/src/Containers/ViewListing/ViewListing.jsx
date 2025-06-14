import './ViewListing.css'
import { useParams } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { ImagesViewer, UserOverview, UserPhone, CategoryDisplay, ColorDisplay, TagsDisplay } from '../../Components';
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import Api from '../../Api/Api';
import { useContext } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
function ViewListing() {
    const {userId, listingId } = useParams();
    const location = useLocation();
    const listing = location.state?.listing;
    const user = location.state?.user;
    const navigate = useNavigate();
    const [authState, dispatch] = useContext(AuthContext);

    const deleteListing = async () =>
    {
        try {
            const response = await Api.get(`listingsApi/deleteListing/${listingId}`);

            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.error(response);
            }
            navigate(`/Profile/${userId}`);
        }
        catch (error) {
            toast.error(error.response.data.message, {
                autoClose: 2000 + error.response.data.message.length * 50,
            });
            if (error.status === 401) {
                window.localStorage.setItem('user', null);
                dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                dispatch({ type: "SET_USER", payload: { user: null } });
                navigate('/Login/');
            }
            console.log(error);
        }
    }

    return (
        <main>
            <div className="holder">
                <div className="viewlisting">
                    <div className="viewlisting-left">
                        <ImagesViewer imagePaths={listing.imagePaths} />
                        <label className="viewlisting-title" >{listing.name}</label>
                        <label className="viewlisting-desc" >{listing.description}</label>
                    </div>
                    <div className="viewlisting-right">
                        <UserOverview user={user} overrideClasas={"viewlisting-user"} />
                        <UserPhone editable={false} phone={user.phoneNumber} overrideClasas={"viewlisting-user"} />
                        <div>
                            {
                                listing.listingType === "Sell" ?
                                    (
                                        <>
                                            <svg className="viewlisting-icon" fill="currentColor" height="200px" width="200px" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" viewBox="0 0 372.372 372.372" xml: space="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <path d="M368.712,219.925c-5.042-8.951-14.563-14.511-24.848-14.511c-4.858,0-9.682,1.27-13.948,3.672l-83.024,46.756 c-1.084,0.61-1.866,1.642-2.163,2.85c-1.448,5.911-4.857,14.164-12.865,19.911c-8.864,6.361-20.855,7.686-35.466,3.939 c-0.088-0.022-0.175-0.047-0.252-0.071L148.252,267.6c-2.896-0.899-4.52-3.987-3.621-6.882c0.72-2.316,2.83-3.872,5.251-3.872 c0.55,0,1.101,0.084,1.634,0.249l47.645,14.794c0.076,0.023,0.154,0.045,0.232,0.065c11.236,2.836,20.011,2.047,26.056-2.288 c7.637-5.48,8.982-15.113,9.141-16.528c0.006-0.045,0.011-0.09,0.014-0.136c0.003-0.023,0.004-0.036,0.005-0.039 c0.001-0.015,0.002-0.03,0.003-0.044c0.001-0.01,0.001-0.019,0.002-0.029c0.909-11.878-6.756-22.846-18.24-26.089l-0.211-0.064 c-0.35-0.114-35.596-11.626-58.053-18.034c-2.495-0.711-9.37-2.366-19.313-2.366c-13.906,0-34.651,3.295-54.549,19.025 L1.67,292.159c-1.889,1.527-2.224,4.278-0.758,6.215l44.712,59.06c0.725,0.956,1.801,1.584,2.99,1.744 c0.199,0.027,0.398,0.04,0.598,0.04c0.987,0,1.954-0.325,2.745-0.935l57.592-44.345c1.294-0.995,3.029-1.37,4.619-0.995 l93.02,21.982c6.898,1.63,14.353,0.578,20.523-2.9l130.16-73.304C371.555,251.012,376.418,233.61,368.712,219.925z"></path> <path d="M316.981,13.155h-170c-5.522,0-10,4.477-10,10v45.504c0,5.523,4.478,10,10,10h3.735v96.623c0,5.523,4.477,10,10,10h142.526 c5.523,0,10-4.477,10-10V78.658h3.738c5.522,0,10-4.477,10-10V23.155C326.981,17.632,322.503,13.155,316.981,13.155z M253.016,102.417h-42.072c-4.411,0-8-3.589-8-8c0-4.411,3.589-8,8-8h42.072c4.411,0,8,3.589,8,8 C261.016,98.828,257.427,102.417,253.016,102.417z M306.981,58.658h-3.738H160.716h-3.735V33.155h150V58.658z"></path> </g> </g></svg>
                                            <label className="viewlisting-desc" > Selling for:</label>
                                        </>
                                    ) :
                                    (
                                        <>
                                            <svg className="viewlisting-icon" fill="currentColor" viewBox="0 0 1024 1024" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M379.141 472.367H215.035c-16.962 0-30.72-13.758-30.72-30.72V71.676c0-16.962 13.758-30.72 30.72-30.72h591.8c16.962 0 30.72 13.758 30.72 30.72v364.851c0 16.962-13.758 30.72-30.72 30.72h-96.768v40.96h96.768c39.583 0 71.68-32.097 71.68-71.68V71.676c0-39.583-32.097-71.68-71.68-71.68h-591.8c-39.583 0-71.68 32.097-71.68 71.68v369.971c0 39.583 32.097 71.68 71.68 71.68h164.106v-40.96z"></path><path d="M550.66 165.528c0-23.331-18.909-42.24-42.24-42.24s-42.24 18.909-42.24 42.24c0 23.331 18.909 42.24 42.24 42.24s42.24-18.909 42.24-42.24zm40.96 0c0 45.953-37.247 83.2-83.2 83.2s-83.2-37.247-83.2-83.2 37.247-83.2 83.2-83.2 83.2 37.247 83.2 83.2zm-318.72 160h474.88c11.311 0 20.48-9.169 20.48-20.48s-9.169-20.48-20.48-20.48H272.9c-11.311 0-20.48 9.169-20.48 20.48s9.169 20.48 20.48 20.48zm0 92.16h252.672c11.311 0 20.48-9.169 20.48-20.48s-9.169-20.48-20.48-20.48H272.9c-11.311 0-20.48 9.169-20.48 20.48s9.169 20.48 20.48 20.48zm324.825 95.581c11.311 0 20.48-9.169 20.48-20.48s-9.169-20.48-20.48-20.48H330.942c-11.311 0-20.48 9.169-20.48 20.48v106.26a61.445 61.445 0 006.638 27.787l61.112 120.504a40.922 40.922 0 014.431 18.528v237.189c0 11.311 9.169 20.48 20.48 20.48s20.48-9.169 20.48-20.48V765.868a81.856 81.856 0 00-8.865-37.063L353.63 608.309a20.48 20.48 0 01-2.209-9.261v-85.78h246.303zm40.918 489.788V783.87a51.175 51.175 0 0116.723-37.846l46.466-42.328a81.887 81.887 0 0026.759-60.565V537.772c0-11.311-9.169-20.48-20.48-20.48s-20.48 9.169-20.48 20.48v105.359a40.934 40.934 0 01-13.377 30.28l-46.464 42.327a92.126 92.126 0 00-30.107 68.132v219.187c0 11.311 9.169 20.48 20.48 20.48s20.48-9.169 20.48-20.48z"></path><path d="M625.769 545.158V426.036c0-16.968 13.754-30.72 30.72-30.72 17.396 0 31.15 13.752 31.15 30.72v139.602c0 11.311 9.169 20.48 20.48 20.48s20.48-9.169 20.48-20.48V426.036c0-39.591-32.094-71.68-71.68-71.68-40.016 0-72.11 32.089-72.11 71.68v119.122c0 11.311 9.169 20.48 20.48 20.48s20.48-9.169 20.48-20.48z"></path></g></svg>
                                            <label className="viewlisting-desc" > Buying for:</label>
                                        </>
                                    )
                            }
                        </div>
                        <div>
                            <label className="viewlisting-title" >{listing.price}</label>
                            <label className="viewlisting-title" > {listing.currency}</label>
                        </div>
                        {
                            authState.user.id === userId
                                ?
                            <button onClick={deleteListing} className="viewListing-delete">Delete</button>
                                :
                            null
                        }
                    </div>
                    <div className="viewlisting-details">
                        <CategoryDisplay category={listing.category}/>
                        <ColorDisplay color={listing.color}/>
                        <TagsDisplay tags={listing.tags}/>
                    </div>
                </div>
            </div>
        </main>
    );

}

export default ViewListing;