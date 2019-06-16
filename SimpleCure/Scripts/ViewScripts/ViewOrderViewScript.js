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
        data: { "OrderActivityNotes": encodeURIComponent($('#Notes').val()), "OrderActivityStatus": $('#Status').val(), "OrderID": $("#OrderInfo_ID").val() },
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
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}

function ShowOrderDiscounts(OrderID) {
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}

function ShowCustomerInfo(CustomerID) {
    //popup the modal
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}

function ShowCustomerOrderHistory(CustomerID) {
    //popup the modal
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}

function ShowOrderActivity(OrderID) {
    //popup the modal
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}

function ShowOrderProducts(OrderID) {
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}

function AddOrderProduct(OrderID) {
    //popup the modal
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}

function ShowSpecificProductInfo(OrderProductID) {
    //popup the modal
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val() + "ID=" + OrderID);
}