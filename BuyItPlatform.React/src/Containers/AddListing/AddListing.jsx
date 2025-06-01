import './AddListing.css'
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import { Loading, Categories, Tags, Colors } from '../../Components';
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
    const [activePrice, setPrice] = useState(0);
    const [activeCurrency, setCurrency] = useState("Eur");
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
            formData.append("Price", String(1500));
            formData.append("Currency", "Eur");
            formData.append("ListingType", "Sell");
            formData.append("Category", activeCategory);
            formData.append("SubCategory", "");
            formData.append("Color", activeColor);

            // Append multiple tags
            activeTags.forEach(tag => {
                formData.append("Tags", tag);
            });

            // Append multiple files
            [file1, file2, file3].forEach(file => {
                formData.append("ImageFiles", file);
            });

            const response = await Api.post(
                'listingsApi/uploadListing',
                formData, // <-- Make sure this is NOT being replaced accidentally!
                {
                    // ⛔ DO NOT set 'Content-Type' manually!
                }
            );


            if (!response.data.success) {
                toast.error(response.data.message, {
                    autoClose: 2000 + response.data.message.length * 50,
                });
                console.error(response);
            }
            navigate(`/Profile/${userId}`);
        }
        catch (error) {
            toast.error("error.response.data.message", {
                autoClose: 2000,
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

    const categories = [
        "Furniture",
        "Animals",
        "Electronics",
        "Fashion",
        "HomeGarden",
        "Motors",
        "Sports",
        "CollectiblesArt",
        "HealthBeauty",
        "ToysHobbies",
        "BusinessIndustrial",
        "MusicMoviesGames"
    ];
    const tags = [
        "New",
        "Used",
        "SlightlyUsed",
        "Refurbished",
        "EcoFriendly",
        "Handmade",
        "Vintage",
        "Wood",
        "Plastic",
        "Ceramic",
        "Metal",
        "Glass",
        "Leather",
        "Cotton",
        "Organic",
        "Recycled",
        "LimitedEdition",
        "CustomMade",
        "Rare",
        "Collectible",
        "Imported",
        "LocallyMade",
        "Artisanal",
        "Waterproof",
        "Shockproof",
        "Wireless",
        "SmartTechnology",
        "Lightweight",
        "HeavyDuty",
        "Foldable",
        "Compact",
        "Expandable",
        "MultiFunctional",
        "ChildSafe",
        "PetFriendly",
        "EnergyEfficient",
        "SolarPowered",
        "Biodegradable",
        "Hypoallergenic",
        "FastCharging",
        "HighPerformance",
        "NoiseCancelling",
        "VintageInspired",
        "HandmadeCrafted",
        "DIYKit",
        "Electronic",
        "Digital",
        "Analog",
        "BatteryOperated",
        "Rechargeable",
        "SmartHomeCompatible"
    ];
    const subCategories = [
        "Smartphones",
        "Laptops",
        "Cameras",
        "Audio",
        "GamingConsoles",
        "Accessories",
        "Clothing",
        "Shoes",
        "Watches",
        "Jewelry",
        "BagsAccessories",
        "Furniture",
        "HomeDecor",
        "Appliances",
        "Tools",
        "OutdoorGarden",
        "Cars",
        "Motorcycles",
        "AutoParts",
        "Boats",
        "Fitness",
        "Cycling",
        "CampingHiking",
        "TeamSports",
        "Fishing",
        "Antiques",
        "Coins",
        "Memorabilia",
        "ArtPaintings",
        "Makeup",
        "Skincare",
        "Haircare",
        "Fragrances",
        "ActionFigures",
        "BoardGames",
        "Dolls",
        "ModelKits",
        "OfficeEquipment",
        "HeavyMachinery",
        "LabEquipment",
        "VinylRecords",
        "VideoGames",
        "DVDs",
        "MusicalInstruments"
    ];
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
                        maxLength={250} type="text" value={newDesc}
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
                <label className="addlisting-title"> A Category Perhaps? </label>
                <Categories onCategorySelected={(e) => { setCategory(e); }} />
                <Tags maxTags={5} onTagsChanged={(e) => { setTags(e); } } />
                <label className="addlisting-title"> What Color Is It My King? </label>
                <Colors onColorsChanged={(e) => { setColor(e); }} />
                <button className="addlisting-button" onClick={uploadListing}>Publish</button>
            </div>
        </main>
  );
}

export default AddListing;