$(document).ready(function () {

});
function AddToCart() {
    var Quantity = $("#QuantityToADD").val();
    var BatchID = $("#BatchIDtoADD").val();
    var ProductID = $("#AddProductIDDDL").val();
    var ProductType = $("#AddProductIDDDL option:selected").text();
    var IsValid = ValidateAddToCart(Quantity, ProductID);
    if (IsValid === "") {
        if (BatchID === null || BatchID === undefined || BatchID === "") {
            BatchID = "N/A";
        }
        var uniqueID = generateUUID();

        $("#CreateNewOrderTotal").html(parseFloat(Number(parseFloat($("#CreateNewOrderTotal").html()).toFixed(2)) + Number(parseFloat(ProductType.slice(ProductType.indexOf("$") + 1, -1)).toFixed(2)) * Quantity).toFixed(2));

        var newRowContent = "<tr id=Product_" + uniqueID + "><td>" + ProductType + "</td><td>" + Quantity + "</td><td>" + BatchID + "</td><td><a href='#' class='text-danger' onclick='DeleteProductRow(Product_" + uniqueID + ", " + Number(parseFloat(ProductType.slice(ProductType.indexOf("$") + 1, -1)).toFixed(2)) * Quantity + " );'><i class='fa fa-times-circle'></i></a> <a href='#' onclick='ShowProductInfo(" + ProductID + ")'><i class='fa fa-eye'></i></a></td><td><input type='hidden' value='" + ProductID + "'></td></tr>";


        $("#CartTable tbody").append(newRowContent);
        $("#QuantityToADD").val("");
        $("#BatchIDtoADD").val("");
        $("#CartDiv").css('display', 'block');
    }
    else { alert(IsValid); }
}
function ValidateAddToCart(Quantity, ProductID) {
    var ErrorMessage = "";
    if (Quantity === null || Quantity === undefined || Quantity === "") {
        ErrorMessage += "You must enter a quantity!\n";
    }
    if (ProductID === null || ProductID === undefined || ProductID === "" || ProductID === "0") {
        ErrorMessage += "You must select a Product!\n";
    }
    return ErrorMessage;
}
function AddDiscount() {

    var DiscountValue = $("#DiscountDDL").children("option:selected").val();
    var DiscountText = $("#DiscountDDL option:selected").text();

    $("#CreateNewOrderTotal").html(parseFloat(Number(parseFloat($("#CreateNewOrderTotal").html()).toFixed(2)) - Number(parseFloat(DiscountText.slice(DiscountText.indexOf("$") + 1, -1)).toFixed(2))).toFixed(2));


    if (DiscountValue !== "0") {

        var uniqueID = generateUUID();

        var newRowContent = "<tr id=Discount_" + uniqueID + "><td>" + DiscountText + "</td><td>Standard</td><td></td><td><button type='button' class='btn btn-outline-danger' onclick='DeleteRow(Discount_" + uniqueID + ");'>Remove</button></td></tr>";



        $("#DiscountTbl tbody").append(newRowContent);
    }



}
function AddCustomDiscount() {
    var DiscountValue = $("#CustomDiscountAmount").val();
    var DiscountType = $("#CustomDiscountType").val();
    if (DiscountValue !== null && DiscountValue !== undefined && DiscountValue !== "" && DiscountType !== null && DiscountType !== undefined && DiscountType !== "") {

        var uniqueID = generateUUID();

        $("#CreateNewOrderTotal").html(parseFloat(Number(parseFloat($("#CreateNewOrderTotal").html()).toFixed(2)) - Number(parseFloat(DiscountValue).toFixed(2))).toFixed(2));

        var newRowContent = "<tr id=Discount_" + uniqueID + "><td>Discount Amount ($" + DiscountValue + ")</td><td>" + DiscountType + "</td><td>" + DiscountValue + "</td><td><a href='#' class='text-danger' onclick='DeleteDiscountRow(Discount_" + uniqueID + ", " + Number(parseFloat(DiscountValue).toFixed(2)).toFixed(2) + ")'><i class='fa fa-times-circle'></i></a></td></tr>";

        $("#DiscountTbl tbody").append(newRowContent);
        $("#CustomDiscountAmount").val('');
        $("#CustomDiscountType").val('');
    }
}

function DeleteProductRow(ID, PriceToRemove) {

    $("#CreateNewOrderTotal").html(parseFloat(parseFloat(Number(parseFloat($("#CreateNewOrderTotal").html()).toFixed(2)) - Number(PriceToRemove).toFixed(2))).toFixed(2));
    $(ID).remove();
}

function DeleteDiscountRow(ID, DiscountToRemove) {
    $("#CreateNewOrderTotal").html(
        parseFloat(
            parseFloat(Number(parseFloat($("#CreateNewOrderTotal").html()).toFixed(2))
                +
                parseFloat(Number(DiscountToRemove).toFixed(2))).toFixed(2)
        ).toFixed(2)
    );
    $(ID).remove();
}

function ValidateOrder() {
    if ($('#CartTable >tbody >tr').length === 0) {
        alert("You must select a product!");
        return false;
    }
    if ($("#CustomerList").children("option:selected").val() === "0") {
        alert("You must select a customer!");
        return false;
    }
    return true;
}
function SubmitOrder() {
    if (ValidateOrder()) {

        var Products = new Array();
        $("#CartTable >tbody >tr").each(function () {
            var row = $(this);
            var product = {};
            product.ProductID = $(this).find("input[type='hidden']").val();
            product.Quantity = row.find("TD").eq(1).html();
            product.BatchID = row.find("TD").eq(2).html();
            Products.push(product);
        });
        var Discounts = new Array();
        if ($('#DiscountTbl >tbody >tr').length !== 0) {
            $("#DiscountTbl >tbody >tr").each(function () {
                var row = $(this);
                var discount = {};

                discount.DiscountID = 1;
                discount.CustomeDiscountType = row.find("TD").eq(1).html();
                discount.CustomAmount = row.find("TD").eq(2).html();

                Discounts.push(discount);
            });
        }


        $.ajax({
            type: "POST",
            url: $("#UrlSaveOrder").val(),
            data: { "Notes": $("#orderNotes").val(), "CustomerID": $("#CustomerList").children("option:selected").val(), "ListProductsToSubmit": Products, "ListDiscountIDs": Discounts },
            success: function (data) {
                window.location = $("#UrlViewOrder").val() + "?ID=" + data;
            }
        });
    }
}
function ShowProductInfo(ProductID) {
    $("#ProductInfo").modal("toggle");
    $("#ViewProductInfoDIV").load($("#UrlViewProductInfo").val() + "?ProductID=" + ProductID);
}

function generateUUID() { // Public Domain/MIT
    var d = new Date().getTime();//Timestamp
    var d2 = (performance && performance.now && (performance.now() * 1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx_xxxx_4xxx_yxxx_xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16;//random number between 0 and 16
        if (d > 0) {//Use timestamp until depleted
            r = (d + r) % 16 | 0;
            d = Math.floor(d / 16);
        } else {//Use microseconds since page-load if supported
            r = (d2 + r) % 16 | 0;
            d2 = Math.floor(d2 / 16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}