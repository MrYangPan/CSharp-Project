var viewModel = function () {
    var self = this;
    this.form = {
        usercode: ko.observable(),
        password: ko.observable(),
        remember: ko.observable(false),
        ip: null,
        city: null
    };
    this.message = ko.observable();
    this.loginClick = function (form) {
        if (!self.form.password())
            self.form.password($('[type=password]').val());
       $.ajax({
            type: "POST",
            url: "api/Login",
            dataType: "json",
            contentType: "application/json",
            data: ko.toJSON(self.form),
            success: function (d) {
                if (d.Success) {
                    self.message($.fn.login.loginSuccess);
                    window.location.href = '/';
                } else {
                    self.message(d.Message);
                }
            },
            error: function (e) {
                var msg = e.responseText.length > 20 ? $.fn.login.loginError : e.responseText;
                self.message(msg);
            },
            beforeSend: function () {
                $(form).find("input").attr("disabled", true);
                self.message($.fn.login.loginLogining);
            },
            complete: function () {
                $(form).find("input").attr("disabled", false);
            }
        });
    };

    this.resetClick = function () {
        self.form.usercode("");
        self.form.password("");
        self.form.remember(false);
    };

    this.init = function () {
        var ilData = ilData || [];
        self.form.ip = ilData[0];
        $.getJSON("http://api.map.baidu.com/location/ip?ak=F454f8a5efe5e577997931cc01de3974&callback=?", function (d) {
            self.form.city = d.content.address;
        });
        if (top != window) top.window.location = window.location;
    };

    this.init();
};

$(function () { ko.applyBindings(new viewModel());});