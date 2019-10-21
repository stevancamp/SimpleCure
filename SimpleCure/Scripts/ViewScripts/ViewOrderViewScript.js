$(document).ready(function () {

});
function ShowCustomerOrderInfo(OrderID) {
    $("#ViewOrderInfoDIV").load($("#UrlViewOrderInfo").val() + "?OrderID=" + OrderID);
}
function ShowOrderStatus(OrderID) {
    $("#EditCurrentOrderStatusDIV").load($("#UrlEditCurrentOrderStatus").val() + "?OrderID=" + OrderID);
}
function SaveOrderStatus() {

    if ($('#Status').val() !== "0") {
        $.ajax({
            type: "POST",
            url: $("#UrlSaveOrderActivityStatus").val(),
            data: { "OrderActivityNotes": $('#Notes').val(), "OrderActivityStatus": $('#Status').val(), "OrderID": $("#OrderInfo_ID").val() },
            success: function (data) {
                if (data === "True") {
                    ShowOrderStatus($("#OrderInfo_ID").val());
                    ShowCustomerOrderInfo($("#OrderInfo_ID").val());
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Error occured');
            }
        });
    }
    else {
        alert('You must select a status');
    }
}
function ShowOrderProducts(OrderID) {
    $("#ListProductsDIV").load($("#UrlViewOrderProducts").val() + "?OrderID=" + OrderID);
}
function SaveNewCustomerInformation() {
    if ($("#CustomerList option:selected").val() !== "0") {
        $.ajax({
            type: "POST",
            url: $("#UrlSaveCustomerInfoForOrder").val(),
            data: { "OrderID": $("#OrderInfo_ID").val(), "CustomerID": $("#CustomerList option:selected").val() },
            success: function (data) {
                if (data === "True") {
                    $("#ChangeCustomerInfoModal").modal("toggle");
                    $("select#CustomerList").prop('selectedIndex', 0);
                    ShowCustomerOrderInfo($("#OrderInfo_ID").val());

                }
                else {
                    alert('Error occured');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Error occured');
            }
        });
    }
    else {
        $("#CustomerList").focus();
    }
}
function RemoveOrderedItem(OrderProductID) {
    $.ajax({
        type: "POST",
        url: $("#UrlRemoveOrderProduct").val(),
        data: { "OrderProductID": OrderProductID },
        success: function (data) {
            if (data === "True") {
                ShowOrderProducts($("#OrderInfo_ID").val());
            }
            else {
                alert('Error occured');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error occured');
        }
    });
}
function ShowProductInfo(ProductID) {
    $("#ProductInfo").modal("toggle");
    $("#ViewProductInfoDIV").load($("#UrlViewProductInfo").val() + "?ProductID=" + ProductID);
}
function ShowAddNewProduct() {
    $("#AddProduct").modal("toggle");
    $("#ViewAddOrderProductDIV").load($("#UrlViewAddOrderProduct").val());
}
function SaveNewProductToOrder() {
    var ProductID = $("#AddProductIDDDL option:selected").val();
    var OrderID = $("#OrderInfo_ID").val();
    var BatchID = $("#BatchIDtoADD").val();
    var Quantity = $("#QuantityToADD").val();
    if (ProductID !== "0" && Quantity !== "" && Quantity !== undefined) {

        if (BatchID === undefined || BatchID === null || BatchID === "") { BatchID = "N/A"; }
        $.ajax({
            type: "POST",
            url: $("#UrlAddOrderProdcut").val(),
            data: { "OrderID": OrderID, "BatchID": BatchID, "Quantity": Quantity, "ProductID": ProductID },
            success: function (data) {
                if (data === "True") {
                    $('#AddProduct').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    $("select#AddProductIDDDL").prop('selectedIndex', 0);
                    ShowOrderProducts($("#OrderInfo_ID").val());
                }
                else {
                    alert('Error occured');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Error occured');
            }
        });
    }
}
function EditProductInfo(OrderID, OrderProductID) {
    $("#EditProductInfoModal").modal("toggle");
    $("#ViewEditOrderProductDIV").load($("#UrlViewEditOrderProduct").val() + "?OrderID=" + OrderID + "&OrderProductID=" + OrderProductID);
}
function ShowOrderDiscounts(OrderID) {
    $("#OrderDiscountsDIV").load($("#UrlViewOrderDiscounts").val() + "?OrderID=" + OrderID);
}
function ViewAddOrderDiscount(OrderID) {
    $("#ViewAddOrderDiscountModal").modal("toggle");
    $("#ViewAddOrderDiscountDIV").load($("#UrlViewAddOrderDiscount").val() + "?OrderID=" + OrderID);
}
function SaveAddNewDiscountToOrder() {

    var DiscountType = $("#CustomDiscountType").val();//CustomDiscountType
    var DiscountAmount = $("#CustomDiscountAmount").val();//CustomDiscountAmount

    if (DiscountType !== null && DiscountType !== undefined && DiscountType !== "" && DiscountAmount !== null && DiscountAmount !== undefined && DiscountAmount !== "") {
        $.ajax({
            type: "POST",
            url: $("#UrlAddOrderDiscount").val(),
            data: { "OrderID": $("#OrderInfo_ID").val(), "DiscountID": "1", "CustomAmount": DiscountAmount, "CustomType": DiscountType },
            success: function (data) {
                if (data === "True") {
                    $('#ViewAddOrderDiscountModal').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();                  
                    $("#CustomDiscountType").val(''); 
                    $("#CustomDiscountAmount").val('');
                    ShowOrderDiscounts($("#OrderInfo_ID").val());
                    ShowOrderProducts($("#OrderInfo_ID").val());
                }
                else {
                    alert('Error occured');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Error occured');
            }
        });
    }
    else {
        alert('You must enter a discount and a discount amount.');
    } 
}
function RemoveOrderDiscount(OrderDiscountID) {
    $.ajax({
        type: "POST",
        url: $("#UrlRemoveOrderDiscount").val(),
        data: { "ID": OrderDiscountID },
        success: function (data) {
            if (data === "True") {
                ShowOrderDiscounts($("#OrderInfo_ID").val());
                ShowOrderProducts($("#OrderInfo_ID").val());
            }
            else {
                alert('Error occured');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error occured');
        }
    });
}
function DeleteOrder(OrderID) {
    $.ajax({
        type: "POST",
        url: $("#UrlDeleteOrder").val(),
        data: { "ID": OrderID },
        success: function (data) {
            if (data === "True") {
                //window.location();
                document.location.replace($("#UrlOrders").val());
            }
            else {
                alert('Error occured');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error occured');
        }
    });
}
function ShowPayOrder() {
    $("#PayOrderModal").modal("toggle");
}
function PayOrder() {
    $.ajax({
        type: "POST",
        url: $("#UrlOrderPaid").val(),
        data: { "ID": $("#OrderInfo_ID").val(), "CompletionNotes": encodeURIComponent($("#CompletionNotes").val()) },
        success: function (data) {
            if (data === "True") {
                document.location.replace($("#UrlOrders").val());
            }
            else {
                alert('Error occured');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error occured');
        }
    });
}
function SaveEditOrderProduct() {
    var ErrorMessage = "";
    if ($("#EditProductIDDDL option:selected").val() === "0") {
        ErrorMessage += "You must select a Product!\n";
    }
    if ($("#EditBatchID").val() === null || $("#EditBatchID").val() === undefined || $("#EditBatchID").val() === "") {
        ErrorMessage += "You must enter a Batch ID!\n";
    }
    if ($("#EditQuantity").val() === null || $("#EditQuantity").val() === undefined || $("#EditQuantity").val() === "") {
        ErrorMessage += "You must enter a Quantity!\n";
    }
    if ($("#EditOrderProductStatus option:selected").val() === "0") {
        ErrorMessage += "You must select a Status!\n";
    }
    if (ErrorMessage === "") {
        $.ajax({
            type: "POST",
            url: $("#UrlSaveEditOrderProduct").val(),
            data: { "OrderProductID": $("#EditOrderProductID").val(), "OrderID": $("#EditOrderID").val(), "ProductID": $("#EditProductIDDDL option:selected").val(), "BatchID": $("#EditBatchID").val(), "Quantity": $("#EditQuantity").val(), "Status": $("#EditOrderProductStatus option:selected").text() },
            success: function (data) {
                if (data.ResponseSuccess) {
                    $("#EditProductInfoModal").modal("toggle");
                    $("#EditOrderProductErrorMessage").text("");
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    ShowOrderProducts($("#EditOrderID").val());
                }
                else {
                    $("#EditOrderProductErrorMessage").text(data.ResponseMessage);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#EditOrderProductErrorMessage").text(errorThrown);
            }
        });
    }
    else {
        $("#EditOrderProductErrorMessage").text(ErrorMessage);

    }

}

function EmailInvoice(EmailAddress, OrderID) {

    $.ajax({
        type: "POST",
        url: $("#UrlEmailInvoice").val(),
        data: { "EmailAddress": EmailAddress, "ID": OrderID },
        success: function (data) {
            if (data === "True") {               
                alert("Email address is " + EmailAddress + " the OrderID is " + OrderID);
            }
            else {
                alert('There was an error when trying to send the email to the customer.');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('There was an error when trying to send the email to the customer.');
        }
    });
}
