﻿var productController = function() {
    this.initialize = function() {
        loadData();
        registerEvent();
    };

    function registerEvent(){
        $('#ddlShowPage').on('change',
            function() {
                tedu.configs.pageSize = $(this).val();
                tedu.configs.pageIndex = 1;
                loadData(true);
            });
    };

    function loadData(isPageChanged) {
        var template = $("#table-template").html();
        var render = "";
        $.ajax({
            type: "GET",
            data: {
                categoryId: null,
                keyword: $('#txtKeyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            datatype: "json",
            url: "/admin/product/getallpaging",
            success: function(response) {
                console.log(response);
                $.each(response.Results,
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
                        $('#lblTotalRecords').text(response.RowCount);
                        if (render !== "") {
                            $("#tbl-content").html(render);
                        }
                        wrapPaging(response.RowCount, function() {
                            loadData();
                        }, isPageChanged);
                    });

            },
            error: function(status) {
                console.log(status);
                tedu.notify("Cannot load product data", "error");
            }
        });
    };

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //unbind pagination if it exited or click change pagesize
        if ($('#paginationUL').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData('twbs-pagination');
            $('#paginationUL').unbind('page');
        }

        // Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: "Đầu",
            prev: "Trước",
            next: "Tiếp",
            last: "Cuối",
            onPageClick: function(even, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}