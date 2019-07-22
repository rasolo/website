module.exports = function () {
    const cookiesNotice = document.querySelectorAll(".cookies-notice a")[0];
    if (!cookiesNotice) {
        return;
    }
    const numberOfDaysBeforeExpire = 999;
    const cookieName = "cookiesNotice";
    const cookieValue = "false";

    cookiesNotice.onclick = () => {
        const expireDate = new Date(Date.now() + 3600000 * 24 * numberOfDaysBeforeExpire);
        document.cookie = `${cookieName}=${cookieValue};expires=${expireDate.toUTCString()};path=/`;
    };
}

