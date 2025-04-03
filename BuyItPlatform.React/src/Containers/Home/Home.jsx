import './Home.css'
function Home() {
  return (
      <>
          <header>
              <h1>Medieval Marketplace</h1>
          </header>

          <nav>
              <ul>
                  <li><a href="#">Home</a></li>
                  <li><a href="#">History</a></li>
                  <li><a href="#">Legends</a></li>
                  <li><a href="#">Gallery</a></li>
              </ul>
          </nav>

          <main>
              <button> ClickMe</button>
              <div class="Holder">
                  <section class="content">
                      <h2>Welcome, Traveler</h2>
                      <p>Explore the tales of knights, castles, and the mysteries of medieval times.</p>
                  </section>
                  <section class="content">
                      <h2>Medieval Lore</h2>
                      <p>From the rise of kingdoms to the battles of honor, uncover the past through ancient scrolls.</p>
                  </section>
                  <section class="content">
                      <h2>Medieval Lore</h2>
                      <p>From the rise of kingdoms to the battles of honor, uncover the past through ancient scrolls.</p>
                  </section>
              </div>
          </main>

          <footer>
              <p>© 2025 Medieval Chronicles. All Rights Reserved.</p>
          </footer>
      </>
  );
}

export default Home;