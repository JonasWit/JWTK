import {axiosInstance} from "@/services/API";

export const register = (payload) => {
    return axiosInstance().post("/auth/register", payload)
}
export const authenticate = (payload) => {
    return axiosInstance().post("/auth/authenticate", payload)
}
