$(document).ready(function () {

});

function ShowCreate() {
    $("#OrderStatusWorkDiv").load($("#UrlCreateOrderStatus").val());
}

function ShowEdit(OrderStatusID) {
    $("#OrderStatusWorkDiv").load($("#UrlEditOrderStatus").val() + "?ID=" + OrderStatusID);
}

