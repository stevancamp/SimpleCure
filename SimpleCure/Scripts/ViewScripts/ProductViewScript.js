$(document).ready(function () {

});

function ShowCreate() {
    $("#ProductWorkDiv").load($("#UrlCreateProduct").val());
}

function ShowEdit(ProductID) {
    $("#ProductWorkDiv").load($("#UrlEditProduct").val() + "?ID=" + ProductID);
}

