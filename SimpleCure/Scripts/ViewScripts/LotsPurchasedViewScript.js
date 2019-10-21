$(document).ready(function () {
    $('#StartDateTime').datetimepicker();
    $('#EndDateTime').datetimepicker(); 
    $("#SearchTerm").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#LotsPurchasedTbl tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
    $("#OpenSearchFilter").click(function () {
        //alert("I made it");
        $("#SearchDiv").toggle();
    });
});


function ApplyLotsPurchasedFilter() {
    var Start = $("#StartDate").val();
    var End = $("#EndDate").val();

    var ErrorMessage = "";
   
    if ($("#StartDate").val() === null || $("#StartDate").val() === undefined || $("#StartDate").val() === "") {
        ErrorMessage += "You must select a Start Date!\n";
    }
    if ($("#EndDate").val() === null || $("#EndDate").val() === undefined || $("#EndDate").val() === "") {
        ErrorMessage += "You must select a End Date!\n";
    }

    if (ErrorMessage === "") {

        //do the redirect public ActionResult LotsPurchased(DateTime? Start, DateTime? End, bool IsComplete = true)
        document.location.replace($("#UrlLotsPurchased").val() + "?Start=" + Start + "&End=" + End);
    }
    else {
        $("#ErrorMessage").text(ErrorMessage);
    }
}





//$("#DiscountsWorkDiv").load($("#UrlEditDiscount").val() + "?ID=" + OrderStatusID);

