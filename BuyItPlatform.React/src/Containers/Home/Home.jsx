import './Home.css'
function Home() {
  return (
        <main>
          <div className = "holder">
              <form className="searchbar">
                  <div className="searchbar-element searchbar-element-obj rounded-left">
                        <img className = "searchbar-img"/>
                        <input className="searchbar-text" autoComplete="off" type="text" id="searchItem" placeholder="Searching for Treasure?"/>
                  </div>
                  <div className="searchbar-element rounded-right">
                      <img className="searchbar-img" />
                      <input className="searchbar-text" autoComplete="off" type="text" id="searchLocation" placeholder="Village?" />
                  </div>
                  <button className="searchbar-button" type="submit">Search</button>
              </form>
          </div>

        </main>
  );
}

export default Home;