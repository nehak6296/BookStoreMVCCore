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
        url: 'https://localhost:44325/Books/AddToCart',
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
