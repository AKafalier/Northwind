$(document).ready(function () {
    function getProducts() {
        $.ajax({
            url: "https://localhost:7230/api/product/getproducts",
            type: "get",
            success: function (data) {
                console.log(data);
                fillProducts(data)
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
    function fillProducts(datas) {
        const productArea = $("#productArea");
        datas.map(function (val, index) {
            const div = `
            <div class="col mb-5">
            <div class="card h-100 bg-secondary">
                <!-- Product image-->
                <img class="card-img-top" src="https://dummyimage.com/450x300/343a40/6c757d.jpg" alt="...">
                <!-- Product details-->
                <div class="card-body p-4">
                    <div class="text-center">
                        <!-- Product name-->
                        <h5 class="fw-bolder">${val.productName}</h5>
                        <!-- Product price-->
                        $ ${val.unitPrice}
                    </div>
                </div>
                <!-- Product actions-->
                <div class="card-footer p-4 pt-0 border-top-0 bg-transparent">
                    <div class="text-center"><a class="btn btn-outline-dark mt-auto" name="addtocart" id=${val.productId}>Add To Cart</a>
                    </div>
                </div>
            </div>
        </div>
            `;
            productArea.append(div);
        })
    }
    getProducts();
})