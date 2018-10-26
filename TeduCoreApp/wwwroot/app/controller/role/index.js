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

        //Grant permission
        $('body').on('click', '.btn-dark', function () {
            $('#hidRoleId').val($(this).data('id'));
            getRoleName($('#hidRoleId').val());
            $.when(loadFunctionList()).done(fillPermission($('#hidRoleId').val()));
            $('#modal-grantpermission').modal('show');
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
        
        // Save Permission
        $('#btnSavePermission').off('click').on('click',
            function() {
                var listPermission = [];
                $.each($('#tblFunction tbody tr'),
                    function(i, item) {
                        listPermission.push({
                            RoleId: $('#hidRoleId').val(),
                            FunctionId: $(item).data('id'),
                            CanRead: $(item).find('.ckView').first().prop('checked'),
                            CanCreate: $(item).find('.ckAdd').first().prop('checked'),
                            CanUpdate: $(item).find('.ckEdit').first().prop('checked'),
                            CanDelete: $(item).find('.ckDelete').first().prop('checked')
                        });
                    });
                $.ajax({
                    type: "POST",
                    url: "/Admin/Role/SavePermission",
                    data: {
                        listPermission: listPermission,
                        roleId: $('#hidRoleId').val()
                    },
                    beforeSend: function() {
                        tedu.startLoading();
                    },
                    success: function(response) {
                        tedu.notify('Save permission successful', 'success');
                        $('#modal-grantpermission').modal('hide');
                        tedu.stopLoading();
                    },
                    error: function(status) {
                        tedu.notify('Has an error in save permission', 'error');
                        console.log(status);
                    }
                });
            });

    }

    function getRoleName(roleId) {
        var nameRoleTemplate = $('#result-name-role').html();
        var nameRender = "";
        return $.ajax({
            type:"GET",
            url: "/Admin/Role/GetById",
            dataType: "JSON",
            data: {
                id: roleId
            },
            beforeSend: function() {
                tedu.startLoading();
            },
            success: function(response) {
                nameRender = Mustache.render(nameRoleTemplate,
                    {
                        NameRole: response.Name
                    });

                if (nameRender !== undefined) {
                    $('.modal-title').html(nameRender);
                }

            },
            error: function(status) {
                    console.log(status);
            }
        });
    }

    function loadFunctionList(callback) {
        var strUrl = "/admin/Function/GetAll";
        return $.ajax({
            type: "GET",
            url: strUrl,
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var template = $('#result-data-function').html();
                var render = "";
                $.each(response, function (i, item) {
                    
                    render += Mustache.render(template, {
                        Name: item.Name,
                        treegridparent: item.ParentId !== null ? "treegrid-parent-" + item.ParentId : "",
                        Id: item.Id,
                        AllowView: item.AllowView ? "checked" : "",
                        AllowCreate: item.AllowCreate ? "checked" : "",
                        AllowEdit: item.AllowEdit ? "checked" : "",
                        AllowDelete: item.AllowDelete ? "checked" : "",
                        Status: tedu.getStatus(item.Status)
                    });
                });
                if (render !== undefined) {
                    $('#lst-data-function').html(render);
                }
               
                $('.tree').treegrid({
                    //'initialState': 'collapsed'
                });

                $('#ckCheckAllView').on('click', function () {
                    $('.ckView').prop('checked', $(this).prop('checked'));
                });

                $('#ckCheckAllCreate').on('click', function () {
                    $('.ckAdd').prop('checked', $(this).prop('checked'));
                });
                $('#ckCheckAllEdit').on('click', function () {
                    $('.ckEdit').prop('checked', $(this).prop('checked'));
                });
                $('#ckCheckAllDelete').on('click', function () {
                    $('.ckDelete').prop('checked', $(this).prop('checked'));
                });

                $('.ckView').on('click', function () {
                    if ($('.ckView:checked').length === response.length) {
                        $('#ckCheckAllView').prop('checked', true);
                    } else {
                        $('#ckCheckAllView').prop('checked', false);
                    }
                });
                $('.ckAdd').on('click', function () {
                    if ($('.ckAdd:checked').length === response.length) {
                        $('#ckCheckAllCreate').prop('checked', true);
                    } else {
                        $('#ckCheckAllCreate').prop('checked', false);
                    }
                });
                $('.ckEdit').on('click', function () {
                    if ($('.ckEdit:checked').length === response.length) {
                        $('#ckCheckAllEdit').prop('checked', true);
                    } else {
                        $('#ckCheckAllEdit').prop('checked', false);
                    }
                });
                $('.ckDelete').on('click', function () {
                    if ($('.ckDelete:checked').length === response.length) {
                        $('#ckCheckAllDelete').prop('checked', true);
                    } else {
                        $('#ckCheckAllDelete').prop('checked', false);
                    }
                });
                if (callback !== undefined) {
                    callback();
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    }

    function fillPermission(roleId) {
        var strUrl = "/Admin/Role/ListAllFunction";
        return $.ajax({
            type: "POST",
            url: strUrl,
            data: {
                roleId: roleId
            },
            dataType: "json",
            beforeSend: function() {
                tedu.startLoading();
            },
            success: function(response) {
                var listPermission = response;
                $.each($('#tblFunction tbody tr'),
                    function(i, item) {
                        $.each(listPermission,
                            function(j, jItem) {
                                if (jItem.FunctionId === $(item).data('id')) {
                                    $(item).find('.ckView').first().prop('checked', jItem.CanRead);
                                    $(item).find('.ckAdd').first().prop('checked', jItem.CanCreate);
                                    $(item).find('.ckEdit').first().prop('checked', jItem.CanUpdate);
                                    $(item).find('.ckDelete').first().prop('checked', jItem.CanDelete);

                                }
                            });
                    });

                if ($('.ckView:checked').length === $('#tblFunction tbody tr .ckView').length) {
                    $('#ckCheckAllView').prop('checked', true);
                } else {
                    $('#ckCheckAllView').prop('checked', false);
                }
                if ($('.ckAdd:checked').length === $('#tblFunction tbody tr .ckAdd').length) {
                    $('#ckCheckAllCreate').prop('checked', true);
                } else {
                    $('#ckCheckAllCreate').prop('checked', false);
                }
                if ($('.ckEdit:checked').length === $('#tblFunction tbody tr .ckEdit').length) {
                    $('#ckCheckAllEdit').prop('checked', true);
                } else {
                    $('#ckCheckAllEdit').prop('checked', false);
                }
                if ($('.ckDelete:checked').length === $('#tblFunction tbody tr .ckDelete').length) {
                    $('#ckCheckAllDelete').prop('checked', true);
                } else {
                    $('#ckCheckAllDelete').prop('checked', false);
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
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
