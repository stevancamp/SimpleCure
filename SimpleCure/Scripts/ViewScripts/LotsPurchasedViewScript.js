$(document).ready(function () {

    $('#LotsPurchasedTable').DataTable();

    $('#StartDateTime').datetimepicker();
    $('#EndDateTime').datetimepicker();
    //$("#SearchTerm").on("keyup", function () {
    //    var value = $(this).val().toLowerCase();
    //    $("#LotsPurchasedTbl tr").filter(function () {
    //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
    //    });
    //});
    $("#OpenSearchFilter").click(function () {
        //alert("I made it");
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

    
    var Provider = $("#GenericClass_Provider option:selected").val();//$("#Provider").val();
    var Lot_Set = $("#GenericClass_Lot_Set").val();
    //set the filter button to show loading
    //ApplyFilter
    $("#ApplyFilter").text("Loading...");
    document.location.replace($("#UrlLotsPurchased").val() + "?Start=" + Start + "&End=" + End + "&IsComplete=" + IsComplete + "&Provider=" + Provider + "&Lot_Set=" + Lot_Set);
}





//$("#DiscountsWorkDiv").load($("#UrlEditDiscount").val() + "?ID=" + OrderStatusID);

