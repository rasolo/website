(function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
let cookiesNotice = require('./cookies-notice');
cookiesNotice();



},{"./cookies-notice":2}],2:[function(require,module,exports){
module.exports = function () {
    const cookiesAcceptButton = document.querySelectorAll(".cookies_accept")[0];
    const cookiesNotice = document.querySelectorAll(".cookies-notice")[0];

    if (!cookiesNotice) {
        return;
    }

    //Activates animation of cookies notice slide up.
    cookiesNotice.style.marginBottom = "initial";

    if (!cookiesAcceptButton) {
        return;
    }
    const numberOfDaysBeforeExpire = 999;
    const cookieName = "cookiesNotice";
    const cookieValue = "false";

    cookiesAcceptButton.onclick = () => {
        const expireDate = new Date(Date.now() + 3600000 * 24 * numberOfDaysBeforeExpire);
        cookiesNotice.style.display = "none";
        document.cookie = `${cookieName}=${cookieValue};expires=${expireDate.toUTCString()};path=/`;
    };
}


},{}]},{},[1]);
