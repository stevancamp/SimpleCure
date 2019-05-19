$(document).ready(function () {

});

function ShowCreate() {
    $("#OrderInfoProductGroupWorkDiv").load($("#UrlCreateOrderInfoProductGroup").val());
}

function ShowEdit(BusinessTypeID) {
    $("#OrderInfoProductGroupWorkDiv").load($("#UrlEditOrderInfoProductGroup").val() + "?ID=" + BusinessTypeID);
}

