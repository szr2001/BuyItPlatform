import './Home.css'
import { toast } from 'react-toastify';
import Api from '../../Api/Api';
import { useState, useEffect, useRef } from 'react';
import { ListingSearch, Categories, ListingsDisplay } from '../../Components';
function Home() {
    const [listings, setListings] = useState([]);
    const [category, setCategory] = useState(null);
    const isFirstRender = useRef(true); // because useEffect runs twitce due to StrictMode component
    const displayedListingsCount = 10;

    const handleSearch = ({ title, location }) => {
        console.log(title + location);
    };

    useEffect(() => {

        const readListings = async () => {
            try {
                const listingFilter = {};
                const response = await Api.post(`listingsApi/getListings?count=${displayedListingsCount}&offset=${0}`, listingFilter);

                if (!response.data.success) {
                    toast.error(response.data.message, {
                        autoClose: 2000 + response.data.message.length * 50,
                    });
                    console.error(response);
                }

                console.log(response);
                setListings(response.data.result);
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
              <ListingsDisplay listingsPerPageCount={displayedListingsCount} listings={listings} />
          </div>

        </main>
  );
}

export default Home;