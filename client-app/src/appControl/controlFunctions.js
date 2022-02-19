import {COOKIES_NAMES, LOCAL_STORE_NAMES} from "@/enums/portalEnums";
import jwt_decode from "jwt-decode";
import {UserModel} from "@/models/general/userModel";

export const readGateAPIAddress = () => {
    console.warn("API Endpoint: ", process.env.VUE_APP_API_GATE)
    return process.env.VUE_APP_API_GATE
};

export const readDarkMode = () => {
    const darkTheme = getLocalStoreItem(LOCAL_STORE_NAMES.DARK_THEME);
    return !!darkTheme;
};

export const readGDPRConsent = () => {
    const consent = getLocalStoreItem(LOCAL_STORE_NAMES.GDPR_CONSENT);
    console.warn("GDPR Consent: ", !!consent)
    return !!consent;
};

export const setTokenCookie = (tokenValue) => {
    console.log('setting cookie')
    let d = new Date();
    d.setTime(d.getTime() + (7 * 24 * 60 * 60 * 1000));
    document.cookie = `${COOKIES_NAMES.TOKEN_COOKIE}=${tokenValue};expires=${d.toUTCString()};samesite=strict;path=/;Secure`;
};

export const setCookie = (cname, cvalue, exdays, samesite = "strict") => {
    let d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    document.cookie = `${cname}=${cvalue};expires=${d.toUTCString()};samesite=${samesite};path=/`;
};

export const getCookieFromRequest = (cookieName, stringCookie) => {
    if (!stringCookie) return null;
    let expression = new RegExp('' + cookieName + '[^;]+').exec(stringCookie);
    if (!expression) return null;
    let strCookie = expression[0];
    return unescape(strCookie ? strCookie.toString().replace(/^[^=]+./, '') : '');
};

export const getCookie = (cname) => {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
};

export const checkCookie = (cookieName) => {
    const cookie = getCookie(cookieName);
    return cookie !== "";
};

export const getLocalStoreItem = (key) => {
    return localStorage.getItem(key);
};

export const deserializeUserObject = () => {
    const token = getLocalStoreItem(LOCAL_STORE_NAMES.ID_TOKEN);
    if (token) {
        const user = new UserModel();
        Object.entries(jwt_decode(token)).forEach(([key, value]) => {
            if (key.includes("emailaddress")) {
                user.email = value
            }
            if (key.includes("role")) {
                user.role = value
            }
            if (key.includes("nameidentifier")) {
                user.id = value
            }
            if (key === "exp") {
                user.expire = new Date(value * 1000)
            }
        })
        return user
    }
    return null
};

export const setLocalStoreItem = (key, value) => {
    localStorage.setItem(key, value);
};

export const removeLocalStoreItem = (key) => {
    localStorage.removeItem(key);
};

export const toggleNavBar = () => {
    const menu = document.querySelector('#mainNavBar');
    if (menu.classList.contains('hidden')) {
        menu.classList.remove('hidden');
    } else {
        menu.classList.add('hidden');
    }
};

export const hideNavBar = () => {
    const menu = document.querySelector('#mainNavBar');
    if (!menu.classList.contains('hidden')) {
        menu.classList.add('hidden');
    }
};

