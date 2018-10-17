var RoleController = function() {
    var seft = this;
    this.initialize = function() {
        loadData();
        registerEvents();
    };
    
    function registerEvents() {
        $('#frmMaintainance').validate({
            errorClass: "red",
            ignore: [],
            lang: "en",
            rules: {
                txtName: {required: true}
            }
        });
        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData();
            }
        });
        $("#btn-search").on('click', function () {
            loadData();
        });

        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });


        $('#btn-create').on('click',
            function() {
                resetFormMaintainance();
                $('#modal-add-edit').modal('show');
            });
        $('body').on('click',
            ".btn-edit",
            function(e) {
                e.preventDefault();
                var that = $(this).data('id');
                $.ajax({
                    type: "GET",
                    url: "/Admin/Role/GetById",
                    data: { id: that },
                    dataType: "JSON",
                    beforeSend: function() {
                        tedu.startLoading();
                    },
                    success: function(response) {
                        var data = response;
                        $('#txtName').val(data.Name);
                        $('#hidId').val(data.Id);
                        $('#txtDescription').val(data.Description);
                        $('#modal-add-edit').modal('show');
                        tedu.stopLoading();
                    },
                    error: function(status) {
                        tedu.notify("Has error in process", "error");
                        console.log(status);
                        tedu.stopLoading();
                    }
                });

            });

        $('#btnSave').on('click',
            function(e) {
                if ($('#frmMaintainance').valid()) {
                    e.preventDefault();

                    var id = $('#hidId').val();
                    var name = $('#txtName').val();
                    var description = $('#txtDescription').val();

                
                    $.ajax({
                        type: "POST",
                        url: "/Admin/Role/SaveEntity",
                        data: {
                            Id: id,
                            Name: name,
                            Description: description
                        },
                        success: function(response) {
                            tedu.notify("Update Success", "success");
                            $('#modal-add-edit').modal("hide");
                            resetFormMaintainance();
                            tedu.stopLoading();
                            loadData();
                        }
                    });
                }
                return false;
            });
        $('body').on('click',
            '.btn-delete',
            function(e) {
                e.preventDefault();
                var that = $(this).data('id');
                tedu.confirm("Are you sure delete?",
                    function() {
                        $.ajax({
                            type: "POST",
                            data: { id: that },
                            url: "/Admin/Role/DeleteAsync",
                            dataType: "JSON",
                            beforeSend: function() {
                                tedu.startLoading();
                            },
                            success: function(response) {
                                tedu.notify("Delete success", "success");
                                tedu.stopLoading();
                                loadData();

                            },
                            error: function(status) {
                                tedu.notify("Has an error", "error");
                                Console.log(status);
                                tedu.stopLoading();
                            }
                        });
                    });
            });
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/Admin/Role/GetAllPaging",
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: "JSON",
            beforeSend: function() {
                tedu.startLoading();
            },
            success: function(response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results,
                        function(i, item) {
                            render += Mustache.render(template,
                                {
                                    Name: item.Name,
                                    Id: item.Id,
                                    Description: item.Description
                                });
                        });
                    $('#lbl-total-records').text(response.RowCount);
                    if (render != undefined) {
                        $('#tbl-content').html(render);
                    }

                    wrapPaging(response.RowCount,
                        function() {
                            loadData();
                        },
                        isPageChanged);

                } else {
                    $('#tbl-content').html("");

                }
                tedu.startLoading();
            },
            error: function(status) {
                console.log(status);
            }

        });
    }

    function resetFormMaintainance() {
        $('#hidId').val('');
        $('#txtName').val('');
        $('#txtDescription').val('');
    };

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function(event, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
};
