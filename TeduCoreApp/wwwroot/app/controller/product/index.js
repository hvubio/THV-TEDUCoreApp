var productController = function() {
    this.initialize = function() {
        loadData();
    };

    var registerEvent = function() {

    };

    function loadData() {
        var template = $("#table-template").html();
        var render = "";
        $.ajax({
            type: "GET",
            datatype: "json",
            url: "/admin/product/getall",
            success: function(response) {
                $.each(response,
                    function(i, item) {
                        render += Mustache.render(template,
                            {
                                Id: item.Id,
                                Name: item.Name,
                                CategoryName: item.ProductCategory.Name,
                                Image: item.Image == null ? '<img src="/lib/gentelella/production/images/user.png" width=25 />' : '<img src="'+ item.Image +'"width=25 />',
                                Price: tedu.formatNumber(item.Price, 0),
                                CreatedDate: tedu.dateTimeFormatJson(item.DateCreated),
                                Status: tedu.getStatus(item.Status)
                            });
                        if (render !== "") {
                            $("#tbl-content").html(render);
                        }
                    });

            },
            error: function(status) {
                console.log(status);
                tedu.notify("Cannot load product data", "error");
            }
        });
    };
}