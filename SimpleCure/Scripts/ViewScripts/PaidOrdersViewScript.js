$(document).ready(function () {
    $.fn.dataTable.moment('M/D/YYYY h:mm:ss A');   
    $('#PaidOrdersTable').DataTable();
});