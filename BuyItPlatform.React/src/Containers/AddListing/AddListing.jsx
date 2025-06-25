import './AddListing.css'
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import { Loading, Categories, Tags, Colors, CurrencyType, NumberInput, ListingType } from '../../Components';
import Api from '../../Api/Api';
import { useParams } from 'react-router-dom';
import { useContext, useState, useEffect } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
function AddListing() {
    const [newName, setNewName] = useState("");
    const [newDesc, setNewDesc] = useState("");
    const { slotIndex } = useParams();
    const [authState, dispatch] = useContext(AuthContext);
    const navigate = useNavigate();

    const [file1, setFile1] = useState(null);
    const [file2, setFile2] = useState(null);
    const [file3, setFile3] = useState(null);

    const [preview1, setPreview1] = useState(null);
    const [preview2, setPreview2] = useState(null);
    const [preview3, setPreview3] = useState(null);
    const [activePrice, setPrice] = useState(25);
    const [activeCurrency, setCurrency] = useState("Eur");
    const [activeListingType, setListingType] = useState("Sell");
    const [activeCategory, setCategory] = useState("");
    const [activeTags, setTags] = useState([]);
    const [activeColor, setColor] = useState([]);
    const [activeSubCategory, setSubCategory] = useState("");

    useEffect(() => {
        return () => {
            //clear resources
            if (preview1) {
                URL.revokeObjectURL(preview1);
            }
            if (preview2) {
                URL.revokeObjectURL(preview2);
            }
            if (preview3) {
                URL.revokeObjectURL(preview3);
            }
        };
    }, [preview1, preview2, preview3]);

    const uploadListing = async () => {
        try {
            const formData = new FormData();

            formData.append("SlotId", String(slotIndex));
            formData.append("Name", newName);
            formData.append("Description", newDesc);
            formData.append("Price", activePrice);
            formData.append("Currency", activeCurrency);
            formData.append("ListingType", activeListingType);
            formData.append("Category", activeCategory);
            formData.append("Color", activeColor);

            // Append multiple tags
            activeTags.forEach(tag => {
                formData.append("Tags", tag);
            });

            // Append multiple files
            [file1, file2, file3].forEach(file => {
                formData.append("ImageFiles", file);
            });

            const response = await Api.post('listingsApi/uploadListing', formData);
            //I'm losing my mind with this formData... the gateway doesn't forrward it correctly or some shit
            //may god or even allah at this point I can't be picky, any god, please, save me


            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.error(response);
            }
            navigate(`/Profile/${authState.user.id}`);
        }
        catch (error) {
            if (error.status === 401) return;
            const errorText = error?.response?.data?.message || error.message || "An unexpected error occurred";
            toast.error(errorText, {
                autoClose: 2000 + errorText.length * 50,
            });
            console.log(error);
            if (error.status === 403) {
                window.localStorage.setItem('user', null);
                dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                dispatch({ type: "SET_USER", payload: { user: null } });
                navigate('/Login/');
            }
        }
    }
    const handleFile1Change = (e) => {
        const file = e.target.files[0];
        if (file) {
            setFile1(file);

            if (preview1) {
                //clear resources
                URL.revokeObjectURL(preview1);
            }
            // Create a preview URL
            const objectUrl = URL.createObjectURL(file);
            setPreview1(objectUrl);
        }
    };
    const handleFile2Change = (e) => {
        const file = e.target.files[0];
        if (file) {
            setFile2(file);

            if (preview2) {
                //clear resources
                URL.revokeObjectURL(preview2);
            }
            // Create a preview URL
            const objectUrl = URL.createObjectURL(file);
            setPreview2(objectUrl);
        }
    };
    const handleFile3Change = (e) => {
        const file = e.target.files[0];
        if (file) {
            setFile3(file);

            if (preview3) {
                //clear resources
                URL.revokeObjectURL(preview3);
            }
            // Create a preview URL
            const objectUrl = URL.createObjectURL(file);
            setPreview3(objectUrl);
        }
    };


    return (
        <main>
            <div className="holder">
                <label className="addlisting-title"> Some Details M'lord? </label>
                <input className="addlisting-name-input addlisting-text" autoComplete="off"
                    maxLength={15} type="text" value={newName}
                    onChange={(e) => { setNewName(e.target.value); }}
                    placeholder="Listing name..."></input>
                <div className="addlisting-desc-holder">
                    <textarea className="addlisting-desc-input addlisting-text" autoComplete="off"
                        maxLength={200} type="text" value={newDesc}
                        onChange={(e) => { setNewDesc(e.target.value); }}
                        placeholder="Listing description..."></textarea>
                </div>

                <div className="addlisting-holder">

                    <label htmlFor="userpic-file1" className="addlisting-img-holder">
                        {
                            preview1 == null ?
                            <svg className="addlisting-img-icon" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path opacity="0.5" d="M2 12C2 7.28595 2 4.92893 3.46447 3.46447C4.92893 2 7.28595 2 12 2C16.714 2 19.0711 2 20.5355 3.46447C22 4.92893 22 7.28595 22 12C22 16.714 22 19.0711 20.5355 20.5355C19.0711 22 16.714 22 12 22C7.28595 22 4.92893 22 3.46447 20.5355C2 19.0711 2 16.714 2 12Z" stroke="currentColor" stroke-width="1.5"></path> <path d="M15 12L12 12M12 12L9 12M12 12L12 9M12 12L12 15" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path> </g></svg>
                            :
                            <img className="addlisting-img"
                                src={preview1}
                            />
                        }
                        <input
                            id="userpic-file1"
                            className="userpic-input"
                            type="file"
                            accept="image/*"
                            onChange={handleFile1Change}
                        />
                    </label>

                    <label htmlFor="userpic-file2" className="addlisting-img-holder">
                        {
                            preview2 == null ?
                                <svg className="addlisting-img-icon" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path opacity="0.5" d="M2 12C2 7.28595 2 4.92893 3.46447 3.46447C4.92893 2 7.28595 2 12 2C16.714 2 19.0711 2 20.5355 3.46447C22 4.92893 22 7.28595 22 12C22 16.714 22 19.0711 20.5355 20.5355C19.0711 22 16.714 22 12 22C7.28595 22 4.92893 22 3.46447 20.5355C2 19.0711 2 16.714 2 12Z" stroke="currentColor" stroke-width="1.5"></path> <path d="M15 12L12 12M12 12L9 12M12 12L12 9M12 12L12 15" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path> </g></svg>
                                :
                                <img className="addlisting-img"
                                    src={preview2}
                                />
                        }
                        <input
                            id="userpic-file2"
                            className="userpic-input"
                            type="file"
                            accept="image/*"
                            onChange={handleFile2Change}
                        />
                    </label>

                    <label htmlFor="userpic-file3" className="addlisting-img-holder">
                        {
                            preview3 == null ?
                                <svg className="addlisting-img-icon" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path opacity="0.5" d="M2 12C2 7.28595 2 4.92893 3.46447 3.46447C4.92893 2 7.28595 2 12 2C16.714 2 19.0711 2 20.5355 3.46447C22 4.92893 22 7.28595 22 12C22 16.714 22 19.0711 20.5355 20.5355C19.0711 22 16.714 22 12 22C7.28595 22 4.92893 22 3.46447 20.5355C2 19.0711 2 16.714 2 12Z" stroke="currentColor" stroke-width="1.5"></path> <path d="M15 12L12 12M12 12L9 12M12 12L12 9M12 12L12 15" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path> </g></svg>
                                :
                                <img className="addlisting-img"
                                    src={preview3}
                                />
                        }
                        <input
                            id="userpic-file3"
                            className="userpic-input"
                            type="file"
                            accept="image/*"
                            onChange={handleFile3Change}
                        />
                    </label>
                </div>
                <label className="addlisting-title"> What Price M'lord? </label>
                <div className="addlisting-price-holder">
                    <ListingType onTypeChangedCallback={(newType) => { setListingType(newType); }} />
                    <NumberInput maxChar={6} onNumberChangedCallback={(newNr) => { setPrice(newNr); }} />
                    <CurrencyType onCurrencyChangedCallback={(newCur => { setCurrency(newCur); }) } />
                </div>
                <label className="addlisting-title"> A Category Perhaps? </label>
                <Categories onCategorySelected={(e) => { setCategory(e); }} />
                <Tags maxTags={5} onTagsChanged={(e) => { setTags(e); } } />
                <label className="addlisting-title"> What Color Is It My King? </label>
                <Colors onColorChanged={(e) => { setColor(e); }} />
                <button className="addlisting-button" onClick={uploadListing}>Publish</button>
            </div>
        </main>
  );
}

export default AddListing;