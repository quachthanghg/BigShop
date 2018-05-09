var cart = {
    init: function () {
        cart.loadData(),
            cart.registerEvents();
    },
    registerEvents: function () {
        $('#frmPayment').validate({
            rules: {
                FullName: 'required',
                Email: {
                    required: true,
                    email: true
                },
                Address: 'required',
                PhoneNumber: {
                    required: true,
                    number: true
                },
                txtMessage: 'required',
            },
            message: {
                FullName: "Tên người dùng không được để trống",
                Email: {
                    required: "Email không được để trống",
                    email: "Email cần phải đúng định dạng"
                },
                Address: "Địa chỉ không được để trống",
                PhoneNumber: {
                    required: "Số điện thoại không được để trống",
                    number: "Phải là số"
                },
                txtMessage: "Nội dung không được để trống"
            }
        });

        $(".btnAddToCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productID = parseInt($(this).data('id'));
            cart.addItem(productID);
            var quatity = $('.totalQuantity').val();
            quatity++;
            $('.totalQuantity').val(quatity);
            $('.totalQuantity').text(quatity);
            var cart1 = $('.simpleCart_empty');
            var imgtodrag = $(this).parent('.simpleCart_shelfItem').find(".mask").find("img").eq(0);
            if (imgtodrag) {
                var imgclone = imgtodrag.clone()
                    .offset({
                        top: imgtodrag.offset(),
                        left: imgtodrag.offset()
                    })
                    .css({
                        'opacity': '0.5',
                        'position': 'absolute',
                        'height': '150px',
                        'width': '150px',
                        'z-index': '100'
                    })
                    .appendTo($('body'))
                    .animate({
                        'top': cart1.offset().top + 10,
                        'left': cart1.offset().left + 10,
                        'width': 75,
                        'height': 75
                    }, 1000, 'easeInOutExpo');

                setTimeout(function () {
                    cart1.effect("shake", {
                        times: 2
                    }, 200);
                }, 1500);

                imgclone.animate({
                    'width': 0,
                    'height': 0
                }, function () {
                    $(this).detach()
                });
            }
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
            cart.updateAll();
        })

        $("#btnContinue").off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        })

        $("#btnCheckOut").off('click').on('click', function (e) {
            e.preventDefault();
            $('#checkout').show();
        })

        $("#cbkUserLogin").change(function () {
            //if ($(this).is('check')) {
            cart.getUserLoginInfo();
            //}
            //else {
            //    $('#txtFullName').val('');
            //    $('#txtAddress').val('');
            //    $('#txtEmail').val('');
            //    $('#txtPhoneNumber').val('');
            //}

        });

        $("#btnDeleteAll").off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        })

        $('#btnCreateOrder').click(function () {
            var isValid = $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }
            
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
                    if (tem.length == '') {
                        $('.cartContent').html("Không tồn tại sản phẩm nào trong giỏ hàng");
                    }
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
                    console.log("them thanh cong");
                }
            }

        });
    },
    updateAll: function () {
        var lstCart = [];
        $.each($(".txtQuantity"), function (i, item) {
            lstCart.push({
                ProductID: $(item).data('id'),
                Quantity: $(item).val()
            })
        })
        $.ajax({
            url: '/ShoppingCart/Update',
            type: "POST",
            dataType: "Json",
            data: {
                cartData: JSON.stringify(lstCart)
            },
            success: function (res) {
                if (res.status == true) {
                    cart.loadData();
                    console.log("OK");
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
    },
    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: "POST",
            dataType: "Json",
            success: function (res) {
                if (res.status) {
                    if (response.status) {
                        cart.loadData();
                    }
                }
            }
        });
    },
    getUserLoginInfo: function () {
        $.ajax({
            url: '/ShoppingCart/GetUserLoginInfo',
            type: "POST",
            dataType: "Json",
            success: function (res) {
                if (res.status) {
                    var user = res.data;
                    $('#txtFullName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhoneNumber').val(user.PhoneNumber);
                }
            }
        });
    },
    createOrder: function () {
        var order = {
            CustomerName: $('#txtFullName').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerMessage: $('#txtMessage').val(),
            CustomerMobile: $('#txtPhoneNumber').val(),
            PaymentMethod: 'Thanh toán tiền mặt',
            Status: false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: "POST",
            dataType: "Json",
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (res) {
                if (res.status) {
                    $('.ckeckout').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('.cartContent').html('Đặt hàng thành công! chúng tôi sẽ liên hệ lại để xác nhận đơn đặt hàng');
                    }, 1000);
                }
            }
        });
    }

};
cart.init();