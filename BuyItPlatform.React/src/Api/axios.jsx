import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7054/gateway/',
    withCredentials: true
});

const privateApi = axios.create({
    baseURL: 'https://localhost:7054/gateway/',
    withCredentials: true
});

let isRefreshing = false;

api.interceptors.response.use(
    async response => {
        // we intercept the response request, we check if it was unauthorized, if it was, we make the
        // refresh token api call to get new tokens and then try again
        if (response?.data?.success === false && response?.data?.message === 'Unauthorized') {
            const originalRequest = response.config;
            console.log("Main call response", response);
            if (!originalRequest._retry && !isRefreshing) {
                originalRequest._retry = true;
                isRefreshing = true;

                try {
                    const resp = await privateApi.post('authApi/refreshToken');
                    console.log("refreshToken Response" + resp);
                    isRefreshing = false;
                    return api(originalRequest);
                } catch (err) {
                    console.log("refreshToken Response" + err);
                    isRefreshing = false;
                    return Promise.reject(err);
                }
            }
        }

        return response;
    },
    error => {
        return Promise.reject(error);
    }
);

export default api;
