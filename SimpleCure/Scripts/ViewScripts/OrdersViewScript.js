$(document).ready(function () {
    $("#SearchTerm").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#OrderTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
});