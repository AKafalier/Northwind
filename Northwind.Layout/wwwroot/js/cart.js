$(document).ready(function () {



    $.ajax({
        url: "https://localhost:7230/api/cart/getitems",
        type: "get",
        xhrFields: {
            withCredentials: true
        },
        success: function (data) {
            console.log(data);
            fillCartTable(data);
        },
        error: function (err) {
            console.log(err);
        }
    })
    $("#cartTableBody").on("click", "button[name='removeitem']", function () {
        var itemId = $(this).attr("id");
        removeFromCart(itemId);
    });

    function removeFromCart(itemId) {
        $.ajax({
            url: "https://localhost:7230/api/cart/deletefromcart/" + itemId,
            type: "delete",
            xhrFields: {
                withCredentials: true
            },
            success: function (data) {
                console.log(data);
                alert("Ürün başarıyla sepetten kaldırıldı!");
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    $("#cartTableBody").on("change", "input[name='cartquantity']", function () {
        var itemId = $(this).attr("id");
        var newQuantity = $(this).val();

        updateCartItemQuantity(itemId, newQuantity);
    });

    function updateCartItemQuantity(itemId, quantity) {
        $.ajax({
            url: "https://localhost:7230/api/cart/updatecartitem/" + itemId,
            type: "put",
            xhrFields: {
                withCredentials: true
            },
            contentType: "application/json",
            data: JSON.stringify(quantity),
            success: function (data) {
                console.log(data);
                alert("Adet başarıyla güncellendi!");
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    $("#productArea").on("click", function (e) {
        if (e.target.name = "addtocart") {

            addToCart(e.target.id);
        }
    })


    function addToCart(id) {
        $.ajax({
            url: "https://localhost:7230/api/cart/addtocart/" + id,
            xhrFields: {
                withCredentials: true
            },
            type: "get",
            success: function (data) {
                console.log(data);
                alert(data.productName + " sepete eklendi!");
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    function fillCartTable(items) {
        var totalPrice = 0;
        const cartTableBody = $("#cartTableBody");
        items.map(function (val, index) {
            totalPrice += val.subTotal;
            const tr = `
                <tr>
<td>${val.productName}</td>
<td>${val.unitPrice}</td>
<td>
<input class="form-control" type="number" value=${val.quantity} name="cartquantity" id=${val.id} />

</td>
<td>${val.subTotal}</td>
<td>
<button class="btn btn-sm btn-danger" name="removeitem" id=${val.id}>Remove</button>
</td>



</tr>
`
            cartTableBody.append(tr);
        })

        $("#totalPrice").text(totalPrice + "₺");
    }
})