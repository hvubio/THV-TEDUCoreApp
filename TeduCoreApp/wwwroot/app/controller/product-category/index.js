var productCategoryController = function() {
    this.initialize = function() {
        loadData();
    }

    function RegisterEvent() {
        //TODO: lisen event in controller
    }

    function loadData() {
        $.ajax({
            type: 'GET',
            datatype: 'json',
            url: '/Admin/ProductCategory/GetCategoryAll',
            success: function (response) {
                var arr = [];
                $.each(response,
                    function(i, item) {
                        arr.push({
                            id: item.Id,
                            text: item.Name, // name show in page index, it should be name "text"
                            parentId: item.ParentId,
                            sortOrder: item.SortOrder
                        });
                    });
                var treeAtr = tedu.unflattern(arr);
                $('#treeProductCategory').tree({
                    data: treeAtr,
                    dnd: true
                });
            },
            error: function(status) {
                console.log(status);
                tedu.notify("Cannot load product category", "error");
            }
            
        });
    }
}