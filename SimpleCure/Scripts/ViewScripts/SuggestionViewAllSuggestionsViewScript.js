$(document).ready(function () {
    $("#SearchTerm").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SuggestionTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
});

function DeleteSuggestion(ID) {
    $.ajax({
        type: "POST",
        url: $("#UrlDeleteSuggestion").val(),
        data: { "ID": ID },
        success: function (data) {
            if (data.ResponseSuccess) {
                window.location = $("#UrlViewAllSuggestions").val();
            }
            else {
                $("#DeleteMessage").text(data.ResponseMessage);
                
            }                     
        }
    });
}