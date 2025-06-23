import './Home.css'
import { toast } from 'react-toastify';
import Api from '../../Api/Api';
import { useState, useEffect, useRef } from 'react';
import { ListingSearch, Categories, ListingsDisplay } from '../../Components';
function Home() {
    const [listings, setListings] = useState([]);
    const [listingsCount, setListingsCount] = useState(0);
    const [category, setCategory] = useState(null);
    const [name, setName] = useState(null);
    const [location, setLocation] = useState(null);
    const isFirstRender = useRef(true); // because useRef runs twitce due to StrictMode component
    const listingRequestingCount = 5;

    const loadMoreListings = async (listingCount) => {
        if (listingsCount === listingCount) return;

        try {
            const listingFilter = { category, name }; //problem
            console.log(listingFilter);
            const listingResponse = await Api.post(`listingsApi/getListings?count=${listingRequestingCount}&offset=${listingCount}`, listingFilter);

            if (!listingResponse.data.success) {
                toast.error(listingResponse.data.message, {
                    autoClose: 2000 + listingResponse.data.message.length * 50,
                });
                console.error(listingResponse);
            }

            console.log(listingResponse);
            setListings(listings.concat(listingResponse.data.result));
        }
        catch (error) {
            if (error.status === 401) {
                window.localStorage.setItem('user', null);
                dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                dispatch({ type: "SET_USER", payload: { user: null } });
                navigate('/Login/');
                return;
            }
            const errorText = error?.response?.data?.message || error.message || "An unexpected error occurred";
            toast.error(errorText, {
                autoClose: 2000 + errorText.length * 50,
            });
            console.log(error);
        }
    }

    const handleSearch = ({ title, location }) => {
        setName(title);
        setLocation(location);
        readListings(category, title);
    };
    const readListings = async (newCategory, newName) => {
        try {
            const listingFilter = { category: newCategory, name: newName };
            console.log(listingFilter);
            const listingResponse = await Api.post(`listingsApi/getListings?count=${listingRequestingCount}&offset=${0}`, listingFilter);

            if (!listingResponse.data.success) {
                toast.error(listingResponse.data.message, {
                    autoClose: 2000 + listingResponse.data.message.length * 50,
                });
                console.error(listingResponse);
            }

            console.log(listingResponse);
            if (listingResponse.data.result.length > 0) {
                setListings(listingResponse.data.result);
            }
            else {
                setListings(null);
            }

            const countResponse = await Api.post(`listingsApi/countListings`, listingFilter);

            if (!countResponse.data.success) {
                toast.error(listingResponse.data.message, {
                    autoClose: 2000 + listingResponse.data.message.length * 50,
                });
                console.error(listingResponse);
            }

            console.log(countResponse);
            setListingsCount(countResponse.data.result);
        }
        catch (error) {
            if (error.status === 401) {
                window.localStorage.setItem('user', null);
                dispatch({ type: "SET_AUTH", payload: { isAuthenticated: false } });
                dispatch({ type: "SET_USER", payload: { user: null } });
                navigate('/Login/');
                return;
            }
            const errorText = error?.response?.data?.message || error.message || "An unexpected error occurred";
            toast.error(errorText, {
                autoClose: 2000 + errorText.length * 50,
            });
            console.log(error);
        }
    };
    useEffect(() => {

        if (isFirstRender.current) {
            isFirstRender.current = false;
            readListings(category, name);
        }

    }, []);
  return (
        <main>
          <div className = "holder">
              <ListingSearch onSearchCallback={handleSearch} />
              <Categories onCategorySelected={(c) => { setCategory(c); } } />
              <ListingsDisplay listingsChunk={listings} onScrolledToBottomCallback={loadMoreListings} />
          </div>
        </main>
  );
}

export default Home;