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
    location.href = $("#UrlOrderActivityTypes").val() + "?ActiveStatus=" + ChangeStatus;
}

function ShowCreate() {
    $("#OrderActivityTypeWorkDiv").load($("#UrlCreateOrderActivityType").val());
}

function ShowEdit(OrderActivityTypeID) {
    $("#OrderActivityTypeWorkDiv").load($("#UrlEditOrderActivityType").val() + "?ID=" + OrderActivityTypeID);
}

