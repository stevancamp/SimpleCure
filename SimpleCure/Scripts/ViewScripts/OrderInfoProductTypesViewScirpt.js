$(document).ready(function () {

});

function ToggleActive() {
    var CurrentStatus = $('#ToggleActiveStatus').val();
    var ChangeStatus;
    if (CurrentStatus === "True") {
        ChangeStatus = false;
    }
    else if (CurrentStatus === "False") {
        ChangeStatus = true;
    }
    else {
        ChangeStatus = true;
    }
    location.href = $("#UrlOrderInfoProductTypes").val() + "?ActiveStatus=" + ChangeStatus;
}

function ShowCreate() {
    $('#OrderInfoProductTypeWorkDiv').load($("#UrlCreateOrderInfoProductType").val());
}

function ShowEdit(BusinessTypeID) {
    $('#OrderInfoProductTypeWorkDiv').load($("#UrlEditOrderInfoProductType").val() + "?ID=" + BusinessTypeID);
}

