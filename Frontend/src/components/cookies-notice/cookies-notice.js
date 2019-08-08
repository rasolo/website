module.exports = function () {
    const cookiesAcceptButton = document.querySelectorAll(".cookies_accept")[0];
    const cookiesNotice = document.querySelectorAll(".cookies-notice")[0];
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

