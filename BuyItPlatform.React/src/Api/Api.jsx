import axios from 'axios';

const Api = axios.create({
    baseURL: 'https://localhost:7000/gateway/',
    withCredentials: true
});

const privateApi = axios.create({
    baseURL: 'https://localhost:7000/gateway/',
    withCredentials: true
});

let isRefreshing = false;

Api.interceptors.response.use(
    async response => {

        return response;
    },
    async error => {
        if (error.status === 401) {
            const originalRequest = error.config;
            if (!originalRequest._retry && !isRefreshing) {
                originalRequest._retry = true;
                isRefreshing = true;
                try {
                    let resp = await privateApi.get('authApi/auth/refreshToken');
                    isRefreshing = false;
                    if (resp?.data?.success === true) {
                        return Api(originalRequest);
                    }
                    return resp;
                } catch (err) {
                    console.error(err);
                    isRefreshing = false;
                    return Promise.reject(err);
                }
            }
        }
        console.error(error);
        return Promise.reject(error);
    }
);

export default Api;
