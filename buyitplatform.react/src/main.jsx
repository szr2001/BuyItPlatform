import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'

import { Navbar, Footer } from './Components';
import { Home } from './Containers';

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <Navbar/>
        <Home/>
        <Footer/>
    </StrictMode>,
)
