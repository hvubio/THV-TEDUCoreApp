var productCategoryController = function() {
    this.initialize = function() {
        loadData();
        registerEvent();
    };

    function registerEvent() {
        //TODO: lisen event in controller
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtNameM: {required: true},
                txtOrderM: {number: true},
                txtHomeOrderM: {number: true}
            }
        });

        $('#btnCreate').off('click').on('click',
            function() {
                intTreeDropDownCategory();
                $('#modal-add-edit').modal('show');
                
            });
        $('body').on('click',
            '#btnEdit',
            function(e) {
                e.preventDefault();
                var that = $('#hidIdM').val();
                $.ajax({
                    type: 'GET',
                    url: '/Admin/ProductCategory/GetCategoryById',
                    data: { id : that },
                    dataType: 'json',
                    beforeSend: function() {
                        tedu.startLoading();
                    },
                    success: function(response) {
                        var data = response;
                        $('#hidIdM').val(data.Id);
                        $('#txtNameM').val(data.Name);
                        intTreeDropDownCategory(data.ParentId);
                        $('#txtAliasM').val(data.Alias);
                        $('#txtDescM').val(data.Description);
                        $('#txtImageM').val(data.Image);
                        $('#txtSeoPageTitleM').val(data.SeoPageTitle);
                        $('#txtSeoKeywordM').val(data.SeoKeywords);
                        $('#txtSeoAliasM').val(data.SeoAlias);
                        $('#txtSeoDescriptionM').val(data.SeoDescription);
                        $('#ckStatusM').prop('checked', data.Status === 1);
                        $('#ckShowHomeM').prop('checked', data.HomeFlag);
                        $('#txtOrderM').val(data.SortOrder);
                        $('#txtHomeOrderM').val(data.HomeOrder);

                        $('#modal-add-edit').modal('show');
                        tedu.stopLoading();
                    },
                    error: function(status) {
                        console.log(status);
                        tedu.notify("Can not load data", "error");
                    }
                });
            });

        $('body').on('click',
            '#btnSave',
            function (e) {
                if ($('#frmMaintainance').valid())
                {
                    e.preventDefault();
                    var id = parseInt($('#hidIdM').val());
                    var name = $('#txtNameM').val();
                    var parentId = $('#ddlCategoryIdM').combotree('getValue');
                    var description = $('#txtDescM').val();
                    var alias = $('#txtAliasM').val();

                    var image = $('#txtImageM').val();
                    var order = parseInt($('#txtOrderM').val());
                    var homeOrder = $('#txtHomeOrderM').val();

                    var seoKeyword = $('#txtSeoKeywordM').val();
                    var seoMetaDescription = $('#txtSeoDescriptionM').val();
                    var seoPageTitle = $('#txtSeoPageTitleM').val();
                    var seoAlias = $('#txtSeoAliasM').val();
                    var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;
                    var showHome = $('#ckShowHomeM').prop('checked');

                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: {
                            Id: id,
                            Name: name,
                            Description: description,
                            ParentId: parentId,
                            HomeOrder: homeOrder,
                            SortOrder: order,
                            HomeFlag: showHome,
                            Image: image,
                            Status: status,
                            SeoPageTitle: seoPageTitle,
                            SeoAlias: seoAlias,
                            SeoKeywords: seoKeyword,
                            SeoDescription: seoMetaDescription
                        },
                        beforeSend: function () {
                            tedu.startLoading();
                        },
                        url: '/Admin/ProductCategory/SaveEntity',
                        success: function (response) {
                            tedu.notify("Update success", "success");
                            $('#modal-add-edit').modal('hide');

                            resetFormMaintainance();

                            tedu.stopLoading();
                            loadData();
                        },
                        error: function (status) {
                            console.log(status);
                            tedu.notify('Can not edit category', 'error');
                        }
                    });
                }
                return false;

            });
        $('body').on('click',
            '#btnDelete',
            function(e) {
                e.preventDefault();
                var id = parseInt($('#hidIdM').val());
                tedu.confirm('Are your sure delele this category?',
                    function() {
                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            data: {
                                Id: id
                            },
                            url: '/Admin/ProductCategory/Delete',
                            success: function (response) {
                                tedu.notify('Delete success', 'success');
                                loadData();
                            },
                            error: function (status) {
                                console.log(status);
                                tedu.notify('Delete Proccess Error', 'error');

                            }
                        });
                    });
            });

        $('#btnClose').on('click',
            function(e) {
                e.preventDefault();
                resetFormMaintainance();
                loadData();
            });


    }
    // reset data in modal 
    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        intTreeDropDownCategory();
        //initTreeDropDownCategory(' ');

        $('#txtDescM').val('');
        $('#txtOrderM').val('');
        $('#txtHomeOrderM').val('');
        $('#txtImageM').val('');

        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');
        $('#txtSeoKeywordM').val('');
        $('#txtSeoDescriptionM').val('');


        $('#ckStatusM').prop('checked', true);
        $('#ckShowHomeM').prop('checked', false);
    }
    // get data to dropdown tree
    function intTreeDropDownCategory(selectId) {
        $.ajax({
            url: '/Admin/ProductCategory/GetCategoryAll',
            dataType: 'json',
            type: 'GET',
            async: false,
            success: function(response) {
                var data = [];
                $.each(response,
                    function(i, item) {
                        data.push({
                            id: item.Id,
                            text: item.Name,
                            parentId: item.ParentId,
                            sortOrder: item.SortOrder
                        });
                    });
                var arr = tedu.unflattern(data);
                $('#ddlCategoryIdM').combotree({
                    data: arr
                });
                if (selectId != undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectId);
                }
            },
            error: function(status) {
                console.log(status);
                tedu.notify("Can not load data to dropdown tree", 'error');
            }
        });
    }

    function loadData() {
        $.ajax({
            type: 'GET',
            datatype: 'json',
            url: '/Admin/ProductCategory/GetCategoryAll',
            success: function(response) {
                var arr = [];
                $.each(response,
                    function(i, item) {
                        arr.push({
                            id: item.Id,
                            text: item.Name +"-"+ item.SortOrder, // name show in page index, it should be name "text"
                            parentId: item.ParentId,
                            sortOrder: item.SortOrder
                        });
                    });
                var treeAtr = tedu.unflattern(arr);
                treeAtr.sort(function(a, b) {
                    return a.sortOrder - b.sortOrder;
                });
                console.log(treeAtr);

                $('#treeProductCategory').tree({
                    data: treeAtr,
                    dnd: true,
                    onContextMenu: function(e,node) {
                        $('#hidIdM').val(node.id);
                        e.preventDefault();
                        //display context menu
                        $('#contextMenu').menu('show',
                            {
                                left: e.pageX,
                                top: e.pageY
                            });
                    },
                    onDrop: function(target, source, point) {
                        console.log(target);
                        console.log(source);
                        console.log(point);
                        var targetNode = $(this).tree('getNode', target);
                        if (point === 'append') {
                            var children = [];
                            $.each(targetNode.children,
                                function(i, item) {
                                    children.push({
                                        key: item.id,
                                        value: i
                                    });
                                });
                            // update to database
                            $.ajax({
                                type: 'post',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id,
                                    items: children
                                },
                                url: '/Admin/ProductCategory/UpdateParentId',
                                success: function() {
                                    loadData();
                                }
                            });
                        } else if (point === 'top' || point === 'bottom') {
                            $.ajax({
                                type: 'post',
                                datatype: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id
                                },
                                url: '/Admin/ProductCategory/ReOrder',
                                success: function() {
                                    loadData();
                                }
                            });
                        }
                    }
                });
            },
            error: function(status) {
                console.log(status);
                tedu.notify("Cannot load product category", "error");
            }

        });
    }
};