import './Navbar.css'
import { Link } from "react-router-dom";

function Navbar() {
  return (
      <>
          <header>
              <h1>Medieval Marketplace</h1>
          </header>

          <div className="navbar">
              <Link className="navbar-option" to="/">Home</Link>
              <Link className="navbar-option" to="/Account/">Account</Link>
              <Link className="navbar-option" to="/Village/">Village</Link>
          </div>
      </>
  );
}

export default Navbar;