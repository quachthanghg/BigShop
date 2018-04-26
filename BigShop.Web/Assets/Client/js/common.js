var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#txtName").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    type: "GET",
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    data: {
                        search: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtName").val(ui.item.Name);
                return false;
            },
            select: function (event, ui) {
                $("#txtName").val(ui.item.Name);
                window.location.href = "/san-pham/" + ui.item.ProductCategory.Alias + "/" + ui.item.Alias + "/" + ui.item.ID;
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<div>" + item.Name + "</div>")
                    .appendTo(ul);
            };
    }
};
common.init();