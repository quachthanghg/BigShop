var cart = {
    init: function () {
        cart.loadData(),
            cart.registerEvents();
    },
    registerEvents: function () {
        $(".btnAddToCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productID = parseInt($(this).data('id'));
            cart.addItem(productID);
        });

        $(".btnDeleteItem").click(function (e) {
            e.preventDefault();
            var productID = parseInt($(this).data('id'));
            cart.deleteItem(productID);
        });

        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productid = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (quantity < 0) {
                quantity = 0;
            }
            if (isNaN(quantity) == false) {
                var amount = quantity * price;
                $('#amount_' + productid).text(numeral(amount).format('0,0'));
            }
            else {
                $('#amount_' + productid).text(0);
            }
            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));

        })
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: "GET",
            dataType: "json",
            success: function (res) {
                console.log(res.status)
                if (res.status == true) {
                    var template = $('#templateCart').html();
                    var tem = '';
                    var data = res.data;

                    $.each(data, function (i, item) {
                        tem += Mustache.render(template, {
                            ProductID: item.ProductID,
                            ProductName: item.Product.Name,
                            ProductImage: item.Product.Image,
                            ProductPrice: item.Product.Price,
                            ProductPriceF: numeral(item.Product.Price).format('0,0'),
                            ProductQuantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });
                    });

                    $('#cartBody').html(tem);
                    cart.registerEvents();
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                }
            }
        });
    },
    addItem: function (productID) {
        $.ajax({
            url: '/ShoppingCart/Add',
            type: "POST",
            dataType: "Json",
            data: {
                productID: productID
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Thêm thành công");
                }
            }

        });
    },
    deleteItem: function (productID) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            type: "POST",
            dataType: "Json",
            data: {
                productID: productID
            },
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }

        });
    },
    getTotalOrder: function () {
        var lstTextBox = $('.txtQuantity');
        var total = 0;
        $.each(lstTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(this).data('price'));
        })
        return total;
    }

};
cart.init();