window.onload = function isLogin() {
    var user_type = $("#login").attr("title") || false;
    if (user_type != "Vendor") {
        $("a.btn").attr("href", "");
    }
    $("button.btn").click(function() {
        if (!user_type) {
            alert("请登录!");
        } else if (user_type != "Vendor") {
            alert("请以供应商身份登录!");
        }
    });
};
