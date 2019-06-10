$(document).ready(function () {
    
});


//function ShowCreate() {
//    $("#DiscountsWorkDiv").load($("#UrlCreateDiscount").val());
//}

//function ShowEdit(OrderStatusID) {
//    $("#DiscountsWorkDiv").load($("#UrlEditDiscount").val() + "?ID=" + OrderStatusID);
//}


//display cart partial view

//add product to cart
function AddToCart(Type, PricePerGram, ID) {

    var Quantity = $("#Quantity_" + ID).val();
    if (Quantity !== null && Quantity !== undefined && Quantity !== "") {

        var newRowContent = "<tr id=Product_" + ID + "><td>" + Type + "</td><td>" + PricePerGram + "</td><td>" + Quantity + "</td><td><button type='button' class='btn btn-outline-danger' onclick='DeleteRow(Product_" + ID + ");'>Remove</button></td></tr>";
        $("#CartTable tbody").append(newRowContent);
        $("#Quantity_" + ID).val("");
    }
    else {
        alert("You must enter a quantity!");
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
function SubmitOrder() {
    //validate everything first
    if (ValidateOrder()) {

        var Products = new Array();     
        $("#CartTable >tbody >tr").each(function () {
            var row = $(this);
            var product = {};
            product.ProductID = this.id.substring(8, this.id.length);
            product.Quantity = row.find("TD").eq(2).html();
            
            Products.push(product);
            
        });
        console.log("Product IDs" + JSON.stringify(Products));
        var Discounts = new Array();
        if ($('#DiscountTbl >tbody >tr').length !== 0) {
            $("#DiscountTbl >tbody >tr").each(function () {
                var discount = {};
                discount.DiscountID = this.id.substring(9, this.id.length);
                 
                Discounts.push(discount);
             
            });
           
        }
        console.log("Discount IDs" + JSON.stringify(Discounts));
        var Notes = $("#orderNotes").val();
        console.log("Notes " + Notes);
        var CustomerID = $("#CustomerList").children("option:selected").val();
        console.log("Customer ID " + CustomerID);
       
        var Data = [{ "Notes": encodeURIComponent(Notes), "CustomerID": CustomerID, "ListProductsToSubmit": Products, "ListDiscountIDs": Discounts}];

        $.ajax({
            type: "POST",
            url: $("#UrlSaveOrder").val(),
            //contentType: "application/json; charset=utf-8",
            data: { "Notes": encodeURIComponent(Notes), "CustomerID": CustomerID, "ListProductsToSubmit": Products, "ListDiscountIDs": Discounts },
            success: function () {
                alert("Success");
            } 
        });
        
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