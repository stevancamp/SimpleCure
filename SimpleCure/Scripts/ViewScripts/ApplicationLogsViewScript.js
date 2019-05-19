$(document).ready(function () {
    $('#LogDate').datetimepicker({
        format: 'L'
    });   
});

function SearchLogByDate() {   
    location.href = $("#UrlApplicationLogs").val() + "?dateTime=" + $("#AppLogDate").val();    
}