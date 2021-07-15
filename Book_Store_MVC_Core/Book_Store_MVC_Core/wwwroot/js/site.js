// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AddToCart(bookId) {

    var addToCartId = "addtoCartBtn-".concat(bookId);
    var addToWishId = "wishlistBtn-".concat(bookId);
    var addedToCart = "addedtocartBtn-".concat(bookId);

    var requestObject = {};
    requestObject.UserId = 1;
    requestObject.BookId = bookId;
    requestObject.Quantity = 1;
    console.log(JSON.stringify(requestObject));
    $.ajax({
        type: "POST",
        url: 'https://localhost:44325/Cart/AddToCart',
        data: JSON.stringify(requestObject),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            //Onclick AddToCart button hide AddToCart button
            var AddToCartButton = document.getElementById(addToCartId);
            AddToCartButton.style.display = "none";

            //Onclick AddToCart button hide WishList button
            var AddToWishListButton = document.getElementById(addToWishId);
            AddToWishListButton.style.display = "none";

            //Onclick AddToCart button show AddedToCart button
            var AddedToCartButton = document.getElementById(addedToCart);
            AddedToCartButton.style.display = "block"
            // alert("Data has been added successfully.");  

        },
        error: function () {
            alert("Error while inserting data");
        }
    });
}

function Remove_Cart(CartId) {
    var removeFromCart = "remove-".concat(CartId);
    var requestObject = {};
    requestObject.cartId = CartId;

    $.ajax({
        type: "POST",
        url: 'https://localhost:44325/Cart/RemoveFromCart',
        data: JSON.stringify(requestObject.cartId),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success:
            function () {
                //Onclick REMOVE button hide AddToCart button
                var RemoveButton = document.getElementById('placeid');
                RemoveButton.style.display = "none";
                window.location.reload();
            },
        error: function () {
            alert("Error while REMOVING data");
        }
    });
}
function place_order() {
    var place_order = document.getElementById('placeid');
    place_order.style.display = "none";

    var form_name = document.getElementById('form-div-cart');
    form_name.style.display = "block";
}
function Checkout() {
    var requestObject = {};
    requestObject.CartId = 38;
    requestObject.UserId = 1;
    $.ajax({
        type: "POST",
        url: 'https://localhost:44325/Orders/Checkout',
        data: JSON.stringify(requestObject),
        // dataType: "json",
        contentType: "application/json",
        success: function (data) {
            $("body").html(data);
        },
        error: function () {
            alert("Error ");
        }
    });
}

function Remove_WishList(WishListId) {
    var removeFromCart = "remove-".concat(WishListId);
    var requestObject = {};
    requestObject.WishListId = WishListId;

    $.ajax({
        type: "POST",
        url: 'https://localhost:44325/WishList/RemoveFromWishList',
        data: JSON.stringify(requestObject.WishListId),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            console.log("REMOVED from WishList");
            window.location.reload();
        },

        error: function () {
            alert("Error while REMOVING data");
        }
    });
}
function Login() {
    requestObject = {},
        requestObject.Email = $("#email").val(),
        requestObject.Password = $("#password").val()
    $.ajax({
        method: "POST",
        url: 'https://localhost:44325/Account/Login',
        data: JSON.stringify(requestObject),
        contentType: "application/json",       
        success: function (data) {
            console.log(data);
            sessionStorage.setItem("token",data.data),//save token in session storage
            window.location.href = ('https://localhost:44325/Books/GetAllBooks')
        },
        error: function () {
            alert("Error ");
        }
    });
}

function GoToCart() {
    if (sessionStorage.getItem("token") != null && sessionStorage.getItem("token") != undefined) {

        $.ajax({
            type: "GET",
            url: 'https://localhost:44325/Cart/GetCart',
            //data: JSON.stringify(requestObject),
            headers: {
                Authorization: "bearer " + sessionStorage.getItem("token")
            },
            dataType: "html",

            success: function (data) {
                console.log("success");
            },
            error: function () {
                alert("Error ");
            }
        });
    }
    else {
        window.location.href = ('https://localhost:44325/Account/Login');
    }
}
