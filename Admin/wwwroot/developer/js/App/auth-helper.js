 
var tokenKey = "AuthenticationToken";

setToken = idToken => {
    // Saves user token to localStorage
    localStorage.setItem(tokenKey, idToken);
};

getToken = () => {
    // Retrieves the user token from localStorage
    var token = getCookie(tokenKey);
    return token; 
};

removeToken = () => {
    // Clear user token and profile data from localStorage
    localStorage.removeItem(tokenKey);
};

isLoggedIn = () => {
    // Checks if there is a saved token and it's still valid
    const token = this.getToken(); // Getting token from localstorage
    //const loggedIn = !!token && !this.isTokenExpired(); // handwaiving here
    //console.log(loggedIn);
    return !!token && !this.isTokenExpired(); // handwaiving here
};

isTokenExpired = () => {
    try {
        const decoded = jwt_decode(getToken());
        
        if (decoded.exp < Math.floor(Date.now() / 1000)) {
            // Checking if token is expired.
            showLog('token has expired');
            return true;
        } else return false;
    } catch (err) {
        showLog("expired check failed! " + err);
        return true;
    }
};

setUserCookie = (name, data, days) => {

    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }

    document.cookie = name + "=" + JSON.stringify(data) + expires + "; path=/";

}

getUserCookie = (name) => {
    var result = document.cookie.match(new RegExp(name + '=([^;]+)'));
    result && (result = JSON.parse(result[1]));
    return result;
}

setCookie = (name, value, days) => {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value | '') + expires + "; path=/";
}

getCookie = (name) => {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

 
var userEmail = getCookie("user_name"); 

setParam = (page, name, value) => {
    const key = page + '_' + name;
    localStorage.setItem(key, value);
}

getParam = (page, name) => {
    const key = page + '_' + name;
    const data = localStorage.getItem(key);
    return data;
}

removeParam = (page, name) => {
    localStorage.removeItem(page + '_' + name);
}
addStorage = (key, value) => {
    localStorage.setItem(key, JSON.stringify(value));
}
getStorage = (key) => {
    var data = JSON.parse(localStorage.getItem(key));
    return data;
}
removeStorage = (key) => {
    localStorage.removeItem(key);
}
doLogOut = () => {
    removeToken();
    window.location.href = appFolder; 
};


//validateAPIResponse = (statusCode, IsSuccess) => {
//    showLog(statusCode + " , " + IsSuccess);
//    if (IsSuccess === false && (statusCode == 401 || statusCode == 0)) {
//        removeToken();
//        window.location.href = '/Account/Login';  
//        return;
//    }
//};

 