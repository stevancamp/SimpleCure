$(document).ready(function () {
    $.fn.dataTable.moment('M/D/YYYY h:mm:ss A');   
    $('#LotsPurchasedTable').DataTable();
    $('#StartDateTime').datetimepicker();
    $('#EndDateTime').datetimepicker();   
    $("#OpenSearchFilter").click(function () {       
        $("#SearchDiv").toggle();
    });
});

function ApplyLotsPurchasedFilter() {
    var Start = $("#StartDate").val();
    var End = $("#EndDate").val();
    var IsComplete = false;
    if ($("#GenericClass_Complete").is(":checked")) {
        IsComplete = true;
    }
    else {
        IsComplete = false;
    }
    var Provider = $("#GenericClass_Provider option:selected").val();
    var Lot_Set = $("#GenericClass_Lot_Set").val();
    $("#ApplyFilter").text("Loading...");
    document.location.replace($("#UrlLotsPurchased").val() + "?Start=" + Start + "&End=" + End + "&IsComplete=" + IsComplete + "&Provider=" + Provider + "&Lot_Set=" + Lot_Set);
}