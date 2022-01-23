import {axiosInstanceAuthorized} from "@/services/API";

export const testAuth = () => {
    return axiosInstanceAuthorized().get("/auth/authorized-check")
}