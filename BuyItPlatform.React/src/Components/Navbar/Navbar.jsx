import './Navbar.css'
import { Link } from "react-router-dom";

function Navbar() {
  return (
      <>
          <header>
              <h1>Medieval Marketplace</h1>
          </header>

          <nav className="navbar">
              <ul>
                  <Link to="/">Home</Link>
                  <Link to="/Village/">Village</Link>
                  <Link to="/Account/">Account</Link>
              </ul>
          </nav>
      </>
  );
}

export default Navbar;