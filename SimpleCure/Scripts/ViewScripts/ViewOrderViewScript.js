$(document).ready(function () {
     
});
function ShowCustomerOrderInfo(OrderID) {
    $("#ViewOrderInfoDIV").load($("#UrlViewOrderInfo").val() + "?OrderID=" + OrderID);
}
function ShowOrderStatus(OrderID) {    
    $("#EditCurrentOrderStatusDIV").load($("#UrlEditCurrentOrderStatus").val() + "?OrderID=" + OrderID);
}
function SaveOrderStatus() {
    $.ajax({
        type: "POST",
        url: $("#UrlSaveOrderActivityStatus").val(),
        //data: { "OrderActivityNotes": encodeURIComponent($('#Notes').val()), "OrderActivityStatus": $('#Status').val(), "OrderID": $("#OrderInfo_ID").val() },
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
    if (ProductID !== "0" && BatchID !== "" && BatchID !== undefined && Quantity !== "" && Quantity !== undefined) {
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
function ShowOrderDiscounts(OrderID) {
    $("#OrderDiscountsDIV").load($("#UrlViewOrderDiscounts").val() + "?OrderID=" + OrderID);
}
function ViewAddOrderDiscount(OrderID) {
    $("#ViewAddOrderDiscountModal").modal("toggle");
    $("#ViewAddOrderDiscountDIV").load($("#UrlViewAddOrderDiscount").val() + "?OrderID=" + OrderID);
}
function SaveAddNewDiscountToOrder() {
    if ($("#AddOrderDiscountDDL option:selected").val() !== "0") {
        $.ajax({
            type: "POST",
            url: $("#UrlAddOrderDiscount").val(),
            data: { "OrderID": $("#OrderInfo_ID").val(), "DiscountID": $("#AddOrderDiscountDDL option:selected").val() },
            success: function (data) {
                if (data === "True") {
                    $('#ViewAddOrderDiscountModal').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    $("select#AddOrderDiscountDDL").prop('selectedIndex', 0);
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
        alert('Error occured');
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
function PayOrder(OrderID) {
    $.ajax({
        type: "POST",
        url: $("#UrlOrderPaid").val(),
        data: { "ID": OrderID, "CompletionNotes": CompletionNotes },
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
