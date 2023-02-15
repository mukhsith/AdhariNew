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

clearStorage = () => {
    localStorage.clear();
}