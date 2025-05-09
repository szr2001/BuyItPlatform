import axios from 'axios';

const Api = axios.create({
    baseURL: 'https://localhost:7054/gateway/',
    withCredentials: true
});

const privateApi = axios.create({
    baseURL: 'https://localhost:7054/gateway/',
    withCredentials: true
});

let isRefreshing = false;

Api.interceptors.response.use(
    async response => {
        // we intercept the response request, we check if it was unauthorized, if it was, we make the
        // refresh token api call to get new tokens and then try again
        if (response?.data?.success === false && response?.data?.message === 'Unauthorized') {
            const originalRequest = response.config;
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

        return response;
    },
    error => {
        console.error(error);
        return Promise.reject(error);
    }
);

export default Api;
