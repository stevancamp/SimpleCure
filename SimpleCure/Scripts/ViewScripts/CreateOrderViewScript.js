$(document).ready(function () {
 
});
 
function AddToCart(Type, PricePerUnit, ID) {

    var Quantity = $("#Quantity_" + ID).val();
    var BatchID = $("#BatchID_" + ID).val();
    if (Quantity !== null && Quantity !== undefined && Quantity !== "" && BatchID !== null && BatchID !== undefined && BatchID !== "") {

        var newRowContent = "<tr id=Product_" + ID + "><td>" + Type + "</td><td>" + PricePerUnit + "</td><td>" + Quantity + "</td><td>" + BatchID + "</td><td><button type='button' class='btn btn-outline-danger' onclick='DeleteRow(Product_" + ID + ");'>Remove</button></td></tr>";
        $("#CartTable tbody").append(newRowContent);
        $("#Quantity_" + ID).val("");
        $("#BatchID_" + ID).val("");
        $("#CartDiv").css('display', 'block');        
    }
    else {
        var ErrorMessage = "";

        if (Quantity === null || Quantity === undefined || Quantity === "") {
            ErrorMessage += "You must enter a quantity!\n";
        }
        if (BatchID === null || BatchID === undefined || BatchID === "") {
            ErrorMessage += "You must enter a batch id!\n";
        }
        if (ErrorMessage !== "") {
            alert(ErrorMessage);         
        }
    }

}
function AddDiscount() {
    $("#DiscountDDL").val();
    var DiscountValue = $("#DiscountDDL").children("option:selected").val();
    var DiscountText = $("#DiscountDDL option:selected").text();
    if (DiscountValue !== "0") {
        var newRowContent = "<tr id=Discount_" + DiscountValue + "><td>" + DiscountText + "</td><td><button type='button' class='btn btn-outline-danger' onclick='DeleteRow(Discount_" + DiscountValue + ");'>Remove</button></td></tr>";
        $("#DiscountTbl tbody").append(newRowContent);
    }
}
function DeleteRow(ID) {
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
            product.ProductID = this.id.substring(8, this.id.length);
            product.Quantity = row.find("TD").eq(2).html();
            product.BatchID = row.find("TD").eq(3).html();
            Products.push(product);
        });        
        var Discounts = new Array();
        if ($('#DiscountTbl >tbody >tr').length !== 0) {
            $("#DiscountTbl >tbody >tr").each(function () {
                var discount = {};
                discount.DiscountID = this.id.substring(9, this.id.length);
                Discounts.push(discount);
            });
        }             
        $.ajax({
            type: "POST",
            url: $("#UrlSaveOrder").val(),         
            data: { "Notes": encodeURIComponent($("#orderNotes").val()), "CustomerID": $("#CustomerList").children("option:selected").val(), "ListProductsToSubmit": Products, "ListDiscountIDs": Discounts },
            success: function (data) {               
                window.location = $("#UrlViewOrder").val() + "?ID=" + data;
            }
        });
    }
}