import axios from 'axios'
import {getLocalStoreItem, readGateAPIAddress} from "@/appControl/controlFunctions";
import {LOCAL_STORE_NAMES} from "@/enums/portalEnums";

export const axiosInstance = () => {
    return axios.create({
        baseURL: readGateAPIAddress()
    })
}

export const axiosInstanceAuthorized = () => {
    return axios.create({
        baseURL: readGateAPIAddress(),
        timeout: 2000,
        headers: {
            'X-ClientId-Header': 'systemywp-client',
            'Authorization' : `Bearer ${getLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN)}`
        }
    })
}

    
