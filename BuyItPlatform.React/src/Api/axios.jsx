import axios from 'axios';

export default axios.create({   
    baseURL: 'https://localhost:7054/gateway/authApi',
    withCredentials: true
    });