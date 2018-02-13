var loginController = function() {
    this.initialize = function() {
        registerEvents();
    };

    var registerEvents = function() {
        $("#btnLogin").on("click",
            function(e) {
                e.preventDefault();
                var userName = $("#txtUserName").val();
                var password = $("#txtPassword").val();
                login(userName, password);
            });
    };

    var login = function(user, pass) {
        $.ajax({
            type: "POST",
            data: {
                UserName: user,
                Password: pass
            },
            datatype: "json",
            url: "admin/login/authen",
            success: function(res) {
                if (res.Seccess) {
                    window.location.href = "/Admin/Home/Index";
                } else {
                    tedu.notify("Đăng nhập không thành công", "error");
                }
            }
        });
    };
};