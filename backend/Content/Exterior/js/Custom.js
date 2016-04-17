$("#facebookLoginClick").click(function () {
    //$.post("/Account/ExternalLogin?ReturnUrl=", { provider: "facebook", novalidate: "novalidate" });
    $("#facebookLoginForm").submit();
});
