﻿import './Home.css'
import { useState } from 'react';
import { ListingSearch, Categories, ListingsDisplay, ListingOverview } from '../../Components';
function Home() {
    const [listings, setListings] = useState([]);
    const handleSearch = ({ title, location }) => {
        console.log(title + location); // do the api call to get the listings
    };

  return (
        <main>
          <div className = "holder">
              <ListingSearch onSearch={handleSearch} />
              <Categories />
              <ListingsDisplay listings={listings} />
              <ListingOverview />
          </div>

        </main>
  );
}

export default Home;