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
    var Description = $("#ProductDescription").val();
    if (ProductID !== "0" && Quantity !== "" && Quantity !== undefined) {

        if (BatchID === undefined || BatchID === null || BatchID === "") { BatchID = "N/A"; }
        $.ajax({
            type: "POST",
            url: $("#UrlAddOrderProdcut").val(),
            data: { "OrderID": OrderID, "BatchID": BatchID, "Quantity": Quantity, "ProductID": ProductID, "Description": Description },
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
        data: {
            "ID": $("#OrderInfo_ID").val(), "CompletionNotes": encodeURIComponent($("#CompletionNotes").val()), "To_From": $("#To_From option:selected").val(), "TransportID": encodeURIComponent($("#TransportID").val()), "TransportLocationStart": encodeURIComponent($("#TransportLocationStart").val()), "TransportLocationEnd": encodeURIComponent($("#TransportLocationEnd").val())
        },
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
    var OrderProductStatus = "";
    if ($("#EditOrderProductStatus option:selected").val() === "0") {      
        OrderProductStatus = $("#OriginalStatus").val();
    }
    else {
        OrderProductStatus = $("#EditOrderProductStatus option:selected").text();
    }
    if (ErrorMessage === "") {
        $.ajax({
            type: "POST",
            url: $("#UrlSaveEditOrderProduct").val(),
            data: { "OrderProductID": $("#EditOrderProductID").val(), "OrderID": $("#EditOrderID").val(), "ProductID": $("#EditProductIDDDL option:selected").val(), "BatchID": $("#EditBatchID").val(), "Quantity": $("#EditQuantity").val(), "Status": OrderProductStatus, "Description": $("#EditProductDescription").val() },
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
 

function EmailInvoice(OrderID) {
    //EmailInvoiceAnchor
    $("#EmailInvoiceAnchor").html("Loading <i class='fa fa-circle-o-notch fa-spin'></i>");
    var Email = $("#EmailInput").val();

    if (Email === "" || Email === undefined || Email === null) {
        $("#EmailErrorMessage").html("You must enter a email address.");
    }
    else {
      
        $.ajax({
            type: "POST",
            url: $("#UrlEmailInvoice").val(),
            data: { "EmailAddress": Email, "ID": OrderID },
            success: function (data) {
                $("#EmailInvoiceAnchor").html("Email Invoice");
                $("#EmailErrorMessage").html("");

                $('#SendEmailModel').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();

                if (data === "True") {
                    alert("The Email was sent successfully to " + Email);
                }
                else {
                    alert('There was an error when trying to send the email to the customer.');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#EmailInvoiceAnchor").html("Email Invoice");
                alert('There was an error when trying to send the email to the customer.');
            }
        });
    }
}
