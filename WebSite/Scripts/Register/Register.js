$(document).ready(function () {
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();

        $.each(a, function () {
            if (o[this.name] != undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || "");
            }
            else {
                o[this.name] = this.value || "";
            }
        });
        return o;
    };
        
    var Register = {
        "events": {
            "#register_form submit" : "deny_submit",
            '.changeCode click': 'changeCode',
            "#inputCode blur": "codeAuth",
            "#inputPassword2 blur": "testPsw",
            "#user_name blur" : "testUserName"
        },
        "authStatus": {},
        testUserName : function(e) {
            var _this = this,
                formEle = _this.FormEle;

            var userType = formEle.find("#user_type").val();
            var userName = e.target.value;
            $.post(URL.userName,{
                user_type: userType,
                user_name: userName
            }, function(data) {
                if (data.toString() === "true") {
                    _this.authStatus['userName'] = true;
                    _this.ShowTest(1, true);
                } else {
                    _this.authStatus['userName'] = false;
                    _this.ShowTest(1, false);
                }
            });
        },
        FormEle: $("#register-main"),
        form: "#register_form",
        "changeCode": function () {
            var _this = this,
                formEle = _this.FormEle;
            var random = Math.random();
            var url = formEle.find('img').attr('src');
            formEle.find('img').attr('src', url + "?version=" + random);
        },
        "deny_submit" : function(e) {
            var _this = this,
                flag = true,
                authStatus = _this.authStatus,
                formEle = _this.FormEle;

            var data = $(e.target).serializeObject();

            formEle.find('.form-control').removeClass("has_error");

            data = $.extend(data, authStatus);
            console.log(data);
            for (var item in data) {
                if (data.hasOwnProperty(item) && (data[item] === "" || data[item].toString() === 'false')) {
                    flag = false;
                    formEle.find("[name='" + item + "']").addClass("has_error");
                }
            }
            if (!flag) {
                return false;
            }

            console.log(123);
        },
        "ShowTest": function (index, data) {
            var _this = this,
                formEle = _this.FormEle,
                icon = formEle.find(".form-group").eq(index).find('i');

            if (data.toString() === "true") {
                icon[0].className = "fa fa-2x fa-check";
                icon.removeClass("error");
                icon.addClass("passed");
            }
            else {
                icon[0].className = "fa fa-2x fa-times";
                icon.removeClass("passwd");
                icon.addClass("error");
            }

            icon.show();
        },
        "testPsw": function (e) {
            var _this = this,
                FormEle = _this.FormEle;

            var data = FormEle.find(_this.form).serializeObject();

            if (data['user_password'] && data['repeat_password'] && data["user_password"] === data["repeat_password"]) {
                _this.ShowTest(6, true);
                _this.authStatus['repeatPassword'] = true;
            }
            else {
                _this.ShowTest(6, false);
                _this.authStatus['repeatPassword'] = false;
            }
        },
        "codeAuth": function (e) {
            var _this = Register,
                value = $(e.target).val(),
                index = $(e.target).parents(".form-group").index();
            $.post(URL.authCode, { validCode: value }, function (data) {
                _this.ShowTest(index, data.toString());
                _this.authStatus["validCode"] = data.toString();
            });
        },
        init: function () {
            var _this = this,
                events = _this.events,
                formEle = _this.FormEle,
                event_name = null,
                ele = null;
            $.each(events, function (name) {
                ele = name.split(" ")[0];
                event_name = name.split(" ")[1];

                if (typeof events[name] === 'string') {
                    formEle.find(ele).on(event_name, function (e) {
                        return _this[events[name]].call(_this, e);
                    });
                }
                else if (typeof events[name] === 'function') {
                    formEle.find(ele).on(event_name, function(e){
                        return  events[name].call(_this, e);
                    });
                }
            });
        }
    };
    Register.init();
});
   