$(document).ready(function () {

});

function ToggleActive() {
    var CurrentStatus = $("#ToggleActiveStatus").val();
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
    location.href = $("#UrlBusinessTypes").val() + "?ActiveStatus=" + ChangeStatus;
}

function ShowCreate() {
    $("#BusinessTypeWorkDiv").load($("#UrlCreateBusinessType").val());
}

function ShowEdit(BusinessTypeID) {
    $("#BusinessTypeWorkDiv").load($("#UrlEditBusinessType").val() + "?ID=" + BusinessTypeID);
}

