export const setCookie = (cname, cvalue, exdays, samesite = "strict") => {
  let d = new Date();
  d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
  document.cookie = `${cname}=${cvalue};expires=${d.toUTCString()};samesite=${samesite};path=/`;
};

function getCookieFromRequest(cookieName, stringCookie) {
  let strCookie = new RegExp('' + cookieName + '[^;]+').exec(stringCookie)[0];
  return unescape(strCookie ? strCookie.toString().replace(/^[^=]+./, '') : '');
}

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
  if (process.browser) {
    return localStorage.getItem("GDPR:accepted") === "true";
  }
};
