window.onload = function isLogin() {
    $("button.btn").click(function() {
        var user_type = $("#login").attr("value") || false;
        if (!user_type) {
            alert("请登录!");
        } else if (user_type != "Vendor") {
            alert("请以供应商身份登录!");
        }
    });
};
