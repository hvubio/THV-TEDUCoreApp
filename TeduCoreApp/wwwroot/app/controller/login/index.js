var loginController = function() {
    this.initialize = function() {
        registerEvents();
    };

    var registerEvents = function () {
        $('#frmLogin').validate({
            errorClass: 'red',
            ignore: [],
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                }
            },
            messages: {
                username: "Bạn cần nhập dữ liệu",
                password: "Bạn cần nhập dữ liệu"
            }

        });
        $("#btnLogin").on("click",
            function (e) {
                if ($('#frmLogin').valid()) {
                    e.preventDefault();
                    var user = $("#txtUserName").val();
                    var password = $("#txtPassword").val();
                    login(user, password);
                }
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
            url: 'admin/login/authen',
            success: function(res) {
                if (res.Success) {
                    window.location.href = "/Admin/Home/Index";
                } else {
                    tedu.notify("Đăng nhập không thành công", "error");
                }
            }
        });
    };
};