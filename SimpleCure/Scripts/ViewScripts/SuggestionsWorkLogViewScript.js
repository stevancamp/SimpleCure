$(document).ready(function () {
    $('#StartDateTime').datetimepicker(); 
    $('#EndDateTime').datetimepicker(); 
});

function CreateLogItem() {
 
    var IsValid = ValidateLogItem();
    if (IsValid === "") {
        
        var item = {};
        item.SuggestionID = $("#SuggestionID").val();
        item.Comment = $("#Comments").val();
        item.StartDateTime = $("#StartDate").val();
        item.EndDateTime = $("#EndDate").val();
        item.SuggestionStatus = $("#SuggestionStatusDDL option:selected").val();
        if ($("#CompleteSuggestion").prop("checked") === true) {
            item.IsComplete = true;
        }
        else if ($("#CompleteSuggestion").prop("checked") === false) {
            item.IsComplete = false;
        }
       
        
        $.ajax({
            type: "POST",
            url: $("#UrlSaveSuggestionWorkLog").val(),
            contentType: 'application/json',
            data: JSON.stringify(item),
            success: function (data) {
                if (data.ResponseSuccess) {
                    LoadWorkLogSuggestions($("#SuggestionID").val());
                    $('#NewWorkLogItem').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
                else {
                    $("#ErrorMessage").text(data.ResponseMessage);
                }                               
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#ErrorMessage").text("There was an error please try again.");
            }
        });

    }
    else {
        $("#ErrorMessage").text(IsValid);
    }
}

function ValidateLogItem() {

    var ErrorMessage = "";

    if ($("#StartDate").val() === null || $("#StartDate").val() === undefined || $("#StartDate").val() === "") {
        ErrorMessage += "You must enter a start date time!\n";
    }

    if ($("#EndDate").val() === null || $("#EndDate").val() === undefined || $("#EndDate").val() === "") {
        ErrorMessage += "You must enter a end date time!\n";
    }

    if ($("#SuggestionStatusDDL option:selected").val() === null || $("#SuggestionStatusDDL option:selected").val() === undefined || $("#SuggestionStatusDDL option:selected").val() === "") {
        ErrorMessage += "You must select the status!\n";
    }    

    if ($("#Comments").val() === null || $("#Comments").val() === undefined || $("#Comments").val() === "") {
        ErrorMessage += "You must enter your comments!\n";
    }
    return ErrorMessage;

}

function LoadWorkLogSuggestions(SuggestionID) {
    $("#WorkLogDIV").load($("#UrlViewSuggestionWorkLogsBySuggestionID").val() + "?SuggestionID=" + SuggestionID);
}