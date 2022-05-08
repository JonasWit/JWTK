import {axiosInstance, axiosInstanceAuthorized} from "@/services/API";

export const createIngredient = (payload) => {
    return axiosInstanceAuthorized().post("/GastronomyIngredients", payload)
}

export const getIngredient = () => {
    return axiosInstanceAuthorized().get("/GastronomyIngredients")
}

export const getIngredients = () => {
    return axiosInstanceAuthorized().get("/GastronomyIngredients/list")
}

export const getPaginatedIngredients = (cursor, take) => {
    return axiosInstanceAuthorized().post(`/GastronomyIngredients/list/${cursor}/${take}`)
}

export const removeIngredient = (id) => {
    return axiosInstanceAuthorized().delete(`/GastronomyIngredients/${id}`)
}

export const updateIngredient = (payload) => {
    return axiosInstanceAuthorized().put("/GastronomyIngredients", payload)
}

// todo: JW add rest of them

