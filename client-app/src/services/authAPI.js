import {axiosInstance, axiosInstanceAuthorized} from "@/services/API";

export const register = (payload) => {
    return axiosInstance().post("/auth/register", payload)
}
export const authenticate = (payload) => {
    return axiosInstance().post("/auth/authenticate", payload)
}
export const changePassword = (payload) => {
    return axiosInstanceAuthorized().post("/auth/change-password", payload)
}
export const deleteUserAccount = () => {
    return axiosInstanceAuthorized().post("/auth/delete-account")
}
