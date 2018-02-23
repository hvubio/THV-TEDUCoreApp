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
                username: "You must input data",
                password: "You must input data"
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
                    tedu.notify("Login false, Username or Password war wrong", "error");
                }
            }
        });
    };
};