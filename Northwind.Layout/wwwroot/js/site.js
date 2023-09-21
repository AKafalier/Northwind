$(document).ready(function () {
    cartCount();
    categoryGet();
    function cartCount() {
        $.ajax({
            url: "https://localhost:7230/api/cart/getitems",
            xhrFields: {
                withCredentials: true
            },
            type: "get",
            success: function (data) {
                console.log(data);
                cartCountFill(data);
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    function cartCountFill(items) {
        var itemCount = items.length;
        $("#cartCount").empty();
        $("#cartCount").append(`
<span>${itemCount}</span>
`);
    }

    function categoryGet() {
        $.ajax({
            url: "https://localhost:7230/api/category/listcategories",
            xhrFields: {
                withCredentials: true
            },
            type: "get",
            success: function (data) {
                console.log(data);
                dropdownFill(data);
            },
            error: function (err) {
                console.log(err);
            }
        })
    }

    function dropdownFill(items) {
        const navbarDropdownLink = $("#navbarDropdownLink");
        items.map(function (val, index) {
            const li = `
<li><a class="dropdown-item" asp>${val.categoryName}</a></li>
`;
            navbarDropdownLink.append(li);
        })
    }
});
