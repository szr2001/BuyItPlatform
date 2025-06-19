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
    const isFirstRender = useRef(true); // because useEffect runs twitce due to StrictMode component
    const listingRequestingCount = 10;
    const listingPageDisplayCount = 4;

    const handleSearch = ({ title, location }) => {
        setName(title);
        setLocation(location);
        readListings();
    };
    const readListings = async () => {
        try {
            const listingFilter = { categorry: category, name: null }; //problem
            console.log(listingFilter);
            const listingResponse = await Api.post(`listingsApi/getListings?count=${listingRequestingCount}&offset=${0}`, listingFilter);

            if (!listingResponse.data.success) {
                toast.error(listingResponse.data.message, {
                    autoClose: 2000 + listingResponse.data.message.length * 50,
                });
                console.error(listingResponse);
            }

            console.log(listingResponse);
            setListings(listingResponse.data.result);

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
    };
    useEffect(() => {

        if (isFirstRender.current) {
            isFirstRender.current = false;
            readListings();
        }

    }, []);
  return (
        <main>
          <div className = "holder">
              <ListingSearch onSearch={handleSearch} />
              <Categories onCategorySelected={(c) => { setCategory(c); } } />
              <ListingsDisplay listingsPerPageCount={listingPageDisplayCount} onPageChangedCallback={(e) => { }}
                  onReachedTheEndCallback={() => { }} totalListings={listingsCount} listingsChunk={listings} />
          </div>
        </main>
  );
}

export default Home;