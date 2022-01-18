import {axiosInstance} from "@/services/API";


export const register = (payload) => {
    return axiosInstance().post("/api/auth/register", payload)
}
export const login = (payload) => {
    return axiosInstance().post("/api/auth/register", payload)
}
