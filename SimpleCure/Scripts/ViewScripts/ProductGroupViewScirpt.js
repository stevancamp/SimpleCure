$(document).ready(function () {

}); 
function ShowCreate() {
    $("#ProductGroupWorkDiv").load($("#UrlCreateProductGroup").val());
}
function ShowEdit(BusinessTypeID) {
    $("#ProductGroupWorkDiv").load($("#UrlEditProductGroup").val() + "?ID=" + BusinessTypeID);
}

