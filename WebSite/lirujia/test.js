var txtuserName = $("#txtuserName").val();//用jquery获取id为txtuserName的页面标记的value，存放在txtuserName变量里
$.ajax({
    type: "POST",                                         //ajax的方式为post(get方式对传送数据长度有限制)
    url: "/AjaxRequest/AddUser.ashx",           //一般处理程序页面AddUser.ashx(在2中会写出该页面内容)
    dataType: "json",                                   //数据传回的格式为json
    data: { adduserName: txtuserName},       //要传送的数据键值对adduserName为键（方便2中的文件用此名称接受数据）txtuserName为值（要传递的变量，例如用户名）
    success: function (data) {                       //成功回传值后触发的方法
        if (data != null && data.IS != "") {        //如果回传的json不为null或json中的IS键对应的值不为空，则触发一下代码，否则弹出“请重新尝试”
            if (data.IS == "-1") {                      //如果json中的IS键对应的值为-1，则说明用户名已在数据库中存在
                alert("添加失败！该名已存在！");
            }
            else if (data.IS == "0") {                 //json中的IS键对应的值为0，则说明用户名没有添加成功
                alert("添加失败!");
            }
            else if (data.IS == "-2") {               //json中的IS键对应的值为-2，则说明数据库返回的主键列不能转换成INT32类型
                alert("数据库连接失败或访问失败!");
            }
            else {
                alert("添加成功！");
                $("#txtuserName").val("");
            }
        }
        else {
            alert("请重新尝试！");
        }
    }
})
