import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'

import { Footer } from './Components';
import { App } from './Containers';

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <App />
        <Footer/>
    </StrictMode>,
)
