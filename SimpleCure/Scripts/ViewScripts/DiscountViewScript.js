$(document).ready(function () {

});

function ShowCreate() {
    $("#DiscountsWorkDiv").load($("#UrlCreateDiscount").val());
}

function ShowEdit(OrderStatusID) {
    $("#DiscountsWorkDiv").load($("#UrlEditDiscount").val() + "?ID=" + OrderStatusID);
}

