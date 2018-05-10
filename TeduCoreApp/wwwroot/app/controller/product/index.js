var productController = function() {
    this.initialize = function() {
        loadCategory();
        loadData();
        registerEvent();
        registerControls();
    };

    function registerEvent() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            debug: false,
            lang: 'en',
            rules: {
                txtNameM: {required: true},
                txtPriceM: {
                    number: true
                },
                txtPromotionPriceM: {
                    number: true
                },
                txtOriginalPriceM: {
                    number: true
                },
                //ddlCategoryIdM: {
                //    required: true
                //},
                txtContentM: {
                    required: function() {
                        CKEDITOR.instances.txtContentM.updateElement();
                    },
                    minlength: 10
                }
            },
            messages: {
                txtContentM: {
                    required:'Content can not empty',
                    minlength: 'Input minimun 100 character'
                }
            }
        });
       
        $('#ddlShowPage').on('change',
            function() {
                tedu.configs.pageSize = $(this).val();
                tedu.configs.pageIndex = 1;
                loadData(true);
            });
        $("#ddlCategorySearch").on('change',
            function() {
                loadData(true);
            });
        $('#btnSearch').on('click',
            function() {
                loadData(true);
            });
        $('#txtKeyword').keypress(function(e) {
            if (e.keyCode === 13)
                loadData(true);
        });

        $('#btnCreate').on('click',
            function () {
                intTreeDropDownCategory();
                resetFormMaintainance();
                
                // check combotree is required 
                $('#ddlCategoryIdM').combotree({
                    validateOnCreate: false,
                    required: true,
                    missingMessage:'Please choose the category',
                    prompt:'Choose a category'
                });

                $('#modal-add-edit').modal('show');
            });
        $('#btnCancel').on('click', function() {
            $('#frmMaintainance').validate().resetForm();
        });

        $('body').on('click',
            '#btnSave',
            function(e) {
                if ($('#frmMaintainance').valid() && $('#ddlCategoryIdM').combotree('isValid')) {

                    var id = $('#hidIdM').val();
                    var name = $('#txtNameM').val();
                    var description = $('#txtDescM').val();
                    var categoryId = $('#ddlCategoryIdM').combotree('getValue');
                    var unit = $('#txtUnitM').val();
                    var price = $('#txtPriceM').val();
                    var originalPrice = $('#txtOriginalPriceM').val();
                    var promotionPrice = $('#txtPromotionPriceM').val();
                    var content = CKEDITOR.instances.txtContentM.getData();
                    var seoTitle = $('#txtSeoPageTitleM').val();
                    var seoUrl = $('#txtSeoAliasM').val();
                    var seoKeyword = $('#txtSeoKeywordM').val();
                    var seoDescription = $('#txtSeoDescriptionM').val();
                    var tags = $('#txtTagM').val();
                    var active = $('#ckStatusM').prop('checked') === true ? 1 : 0;
                    var homeShow = $('#ckShowHomeM').prop('checked');
                    var hotFlag = $('#ckHotM').prop('checked');

                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        url: '/Admin/Product/SaveEntity',
                        data: {
                            Id: id,
                            Name: name,
                            Price: price,
                            PromotionPrice: promotionPrice,
                            OriginalPrice: originalPrice,
                            Description: description,
                            Unit: unit,
                            SeoPageTitle: seoTitle,
                            SeoKeywords: seoKeyword,
                            SeoDescription: seoDescription,
                            SeoAlias: seoUrl,
                            Tag: tags,
                            Image: 'adfsadf',
                            Content: content,
                            CategoryId: categoryId,
                            Status: active,
                            HomeFlag: homeShow,
                            HotFlag: hotFlag
                        },
                        beforeLoading: function() {
                            tedu.startLoading();
                        },
                        success: function(response) {
                            tedu.notify('Success create new product', 'success');
                            tedu.stopLoading();
                            $('#modal-add-edit').modal('hide');

                            resetFormMaintainance();
                            loadData(true);
                        },
                        error: function(status) {
                            console.log(status);
                            tedu.notify('Error process make Save product', 'error');
                        }


                    });
                }
                return false;
            });

        $('body').on('click',
            '.btn-edit',
            function(e) {
                e.preventDefault();
                var that = $(this).data('id');
                $.ajax({
                    type: 'post',
                    dataType: 'json',
                    data: {
                        id: that
                    },
                    url: '/Admin/Product/GetById',
                    beforeSend: function() {
                        tedu.startLoading();
                    },
                    success: function(response) {
                        var data = response;
                        $('#hidIdM').val(data.Id);
                        $('#txtNameM').val(data.Name);
                        intTreeDropDownCategory(data.CategoryId);

                        $('#txtDescM').val(data.Description);
                        $('#txtUnitM').val(data.Unit);

                        $('#txtPriceM').val(data.Price);
                        $('#txtOriginalPriceM').val(data.OriginalPrice);
                        $('#txtPromotionPriceM').val(data.PromotionPrice);

                        // $('#txtImageM').val(data.ThumbnailImage);

                        $('#txtTagM').val(data.Tag);
                        $('#txtMetakeywordM').val(data.SeoKeywords);
                        $('#txtMetaDescriptionM').val(data.SeoDescription);
                        $('#txtSeoPageTitleM').val(data.SeoPageTitle);
                        $('#txtSeoAliasM').val(data.SeoAlias);

                        CKEDITOR.instances.txtContentM.setData(data.Content);
                        $('#ckStatusM').prop('checked', data.Status === 1);
                        $('#ckHotM').prop('checked', data.HotFlag);
                        $('#ckShowHomeM').prop('checked', data.HomeFlag);

                        $('#modal-add-edit').modal('show');
                        tedu.stopLoading();
                    },
                    error(status) {
                        console.log(status);
                        tedu.notify('Error process load data', 'error');
                        tedu.stopLoading();
                    }
                });
            });

        $('body').on('click',
            '.btn-delete',
            function(e) {
                var that = $(this).data('id');
                tedu.confirm('Are you sure delete product',
                    function() {
                        $.ajax({
                            type: "post",
                            dataType: "json",
                            data: {
                                id: that
                            },
                            url: '/Admin/Product/Delete',
                            beforeSend: function () {
                                tedu.startLoading();
                            },
                            success: function (response) {
                                tedu.stopLoading();
                                tedu.notify('Delete success data', 'success');
                                loadData(true);
                            },
                            error: function (status) {
                                console.log(status);
                                tedu.notify('Can not delete process error', 'error');
                            }
                        });
                    });
            });
    };
    function registerControls() {
        CKEDITOR.replace('txtContentM', {});

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                            // CKEditor compatibility fix start.
                            && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };
    };
    function loadCategory() {
        var render = "<option> Select Category </option>";
        $.ajax({
            type: 'GET',
            datatype: 'json',
            url: '/admin/product/getcategoryall',
            success: function (response) {
                $.each(response,
                    function(i, item) {
                        render += "<option value='" +item.Id + "'> "+ item.Name +"  </option>";
                    });
                if (render !== null) {
                    $('#ddlCategorySearch').html(render);
                }
            },
            error: function(status) {
                console.log(status);
                tedu.notify("Can not load product category", "error");
            }
        });
    }

    function loadData(isPageChanged) {
        var template = $("#table-template").html();
        var render = "";
        $.ajax({
            type: "GET",
            data: {
                categoryId: $('#ddlCategorySearch').val(),
                keyword: $('#txtKeyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            datatype: "json",
            url: "/admin/product/getallpaging",
            success: function(response) {
                console.log(response);
                if (response.RowCount === 0) {
                    tedu.notify("Can not found data", "error");
                    $('#paginationUL').empty();
                    $('#tbl-content').html(" ");
                }
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
                        
                    });

                $('#lblTotalRecords').text(response.RowCount);
                if (render !== "") {
                    $("#tbl-content").html(render);
                }

                wrapPaging(response.RowCount, function () {
                    loadData();
                }, isPageChanged);
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

    function intTreeDropDownCategory(selectId) {
        $.ajax({
            url: '/Admin/ProductCategory/GetCategoryAll',
            dataType: 'json',
            type: 'GET',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response,
                    function (i, item) {
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
            error: function (status) {
                console.log(status);
                tedu.notify("Can not load data to dropdown tree", 'error');
            }
        });
    }

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        intTreeDropDownCategory(' ');

        $('#txtDescM').val('');
        $('#txtUnitM').val('');

        $('#txtPriceM').val('0');
        $('#txtOriginalPriceM').val('');
        $('#txtPromotionPriceM').val('');

        //$('#txtImageM').val('');

        $('#txtTagM').val('');
        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        CKEDITOR.instances.txtContentM.setData('');
        $('#ckStatusM').prop('checked', true);
        $('#ckHotM').prop('checked', false);
        $('#ckShowHomeM').prop('checked', false);

    }
}