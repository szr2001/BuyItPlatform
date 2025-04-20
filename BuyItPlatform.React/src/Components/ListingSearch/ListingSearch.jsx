import './ListingSearch.css'

function ListingSearch({ onSearch }) {

    const sendQuery = () => {
        onSearch({ title: "Purse", location: "Germany" }); //test data
    };

  return (
      <div className="searchbar">
          <div className="searchbar-element searchbar-element-obj rounded-left">
              <img className="searchbar-img" />
              <input className="searchbar-text" autoComplete="off" type="text" id="searchItem" placeholder="Searching for Treasure?" />
          </div>
          <div className="searchbar-element rounded-right">
              <img className="searchbar-img" />
              <input className="searchbar-text" autoComplete="off" type="text" id="searchLocation" placeholder="Village?" />
          </div>
          <button className="searchbar-button" onClick={sendQuery} >Search</button>
      </div>
  );
}

export default ListingSearch;