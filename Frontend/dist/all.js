module.exports=function(){const e=document.querySelectorAll(".cookies_accept")[0],o=document.querySelectorAll(".cookies-notice")[0];if(!o)return;if(o.style.marginBottom="initial",!e)return;e.onclick=()=>{const e=new Date(Date.now()+863136e5);o.style.display="none",document.cookie=`cookiesNotice=false;expires=${e.toUTCString()};path=/`}};
//# sourceMappingURL=all.js.map