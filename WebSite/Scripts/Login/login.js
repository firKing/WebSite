$(document).ready(function(){

    /**
     * 登录模块
     * @type {{
     *     log: Function,
     *     form_ele: (*|jQuery|HTMLElement),
     *     events: {submit: string},
     *     ERROR: {0: string, 1: string, 2: string, 3: string, 4: string},
     *     RegChecker: {username: RegExp, password: RegExp},
     *     submit_func: Function,
     *     post_ajax: Function,
     *     init: Function
     * }}
     */
    var Login = {

        // 弹出提示框
        log : function log(msg){
            var topbar = $("#top_bar");
            topbar.html(msg);
            topbar.animate({
                top: '0'
            });

            setTimeout(function(){
                topbar.animate({
                    top : "-50px"
                });
            }, 3000);
        },

        // 表单form 元素
        form_ele : $("#form_wrapper"),

        // 事件绑定
        events : {
            "#login_form submit" : "submit_func"
        },

        // 错误信息
        ERROR : {
            0: "success",
            1: "have not user",
            2: "password is not correct",
            3: "vr_num is not correct",
            4: "other error "
        },

        // 表单验证
        RegChecker:  {
            username : /\w+/,
            password : /\w+/
        },

        // submit事件函数
        "submit_func" : function(e){
            e.preventDefault();

            var _this = this,
                flag = true,
                data = $(e.target).serializeObject(),
                errormsg,
                checker = _this.RegChecker,
                form_ele = _this.form_ele,
                input_ele = null;


            form_ele.find('input').removeClass('has_error');

            $.each(data, function(name){
                input_ele = form_ele.find("input[name='" + name + "']");
                errormsg = input_ele.attr('data-error');

                if(!checker[name].test(this)){
                    flag = false;
                    input_ele.attr('placeholder', errormsg);
                    input_ele.addClass('has_error');
                }
            });

            if(!flag){
                return false;
            }
        },
        // 绑定事件函数
        init : function(){
            var _this = this,
                events = _this.events,
                formEle = _this.form_ele,
                ele = null,
                event_name = null;

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
                        return events[name].call(_this, e);
                    });
                }
            });
        }
    };
    Login.init();
});