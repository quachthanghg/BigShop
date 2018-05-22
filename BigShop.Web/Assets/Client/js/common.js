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

var common2 = {
    init: function () {
        common2.registerEvents();
    },
    registerEvents: function () {
        $("#txtName1").autocomplete({
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
                $("#txtName1").val(ui.item.Name);
                return false;
            },
            select: function (event, ui) {
                $("#txtName1").val(ui.item.Name);
               $.ajax({
                    type: "GET",
                    url: "/Product/CompareProduct1",
                    data: {
                        id: ui.item.ID
                    },
                    success: function (res) {
                        $("#compareproduct1").html(res);
                    }
                });
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div>" + item.Name + "</div>")
                .appendTo(ul);
        };
    }
};
common2.init();

var common3 = {
    init: function () {
        common3.registerEvents();
    },
    registerEvents: function () {
        $("#txtName2").autocomplete({
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
                $("#txtName2").val(ui.item.Name);
                return false;
            },
            select: function (event, ui) {
                $("#txtName2").val(ui.item.Name);
                $.ajax({
                    type: "GET",
                    url: "/Product/CompareProduct2",
                    data: {
                        id: ui.item.ID
                    },
                    success: function (res) {
                        $("#compareproduct2").html(res);
                    }
                });
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div>" + item.Name + "</div>")
                .appendTo(ul);
        };
    }
};
common3.init();

