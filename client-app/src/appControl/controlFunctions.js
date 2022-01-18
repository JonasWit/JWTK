import {COOKIES_NAMES} from "@/enums/portalEnums";

export const readGateAPIAddress = () => {
    return process.env.VUE_APP_API_GATE
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

export const getGDPRConsent = () => {
    return localStorage.getItem("GDPR:accepted") === "true";
};

export const setGDPRConsent = () => {
    localStorage.setItem("GDPR:accepted", "true");
};

export const getLocalStoreItem = (key) => {
    return localStorage.getItem(key);
};

export const setLocalStoreItem = (key, value) => {
    localStorage.setItem(key, value);
};

export const removeLocalStoreItem = (key) => {
    localStorage.removeItem(key);
};