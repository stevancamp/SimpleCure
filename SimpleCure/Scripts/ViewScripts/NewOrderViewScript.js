$(document).ready(function () {

});


function AddOrderToTable(ProductID, Product, Price) {
    var Quantity = $('#Quantity_' + ProductID).val();
    if (Quantity !== '' && Quantity !== undefined) {
        var markup = "<tr id=" + ProductID + "><td>" + Product + "</td><td>" + ProductID+"</td><td>" + Price + "</td><td>" + $('#Quantity_' + ProductID).val() + "</td><td>" + Price * $('#Quantity_' + ProductID).val() + "</td><td><button class='btn btn-primary btnDelete'>Delete</button></td></tr>";

        $("#ProductTable").append(markup);
        $('#Quantity_' + ProductID).val('');
    }
    else {
        $('#Quantity_' + ProductID).focus();
    }
}
$("#ProductTable").on('click', '.btnDelete', function () {
    $(this).closest('tr').remove();
});

function ValidateProductOrders() {
    var Valid = false;
    if ($('#ProductTable tbody').find('tr').length > 0) {
        Valid = true;
    }
    else {
        alert("You must make at least one product order.");
        Valid = false;
    }
    return Valid;
}

function ValidateForm() {
    var Valid = false;

    if ($('#CompanyName').val() !== '' && $('#CompanyName').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a Company Name.");
        return false;
    }
    if ($('#ContactName').val() !== '' && $('#ContactName').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a Contact Name.");
        return false;
    }
    if ($('#OMMANumber').val() !== '' && $('#OMMANumber').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a OMMA Number.");
        return false;
    }
    if ($('#EINNumber').val() !== '' && $('#EINNumber').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a EIN Number.");
        return false;
    }
    if ($('#OBNDDNumber').val() !== '' && $('#OBNDDNumber').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a OBNDD Number.");
        return false;
    }
    if ($('#PhoneNumber').val() !== '' && $('#PhoneNumber').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a Phone Number.");
        return false;
    }
    if ($('#EmailAddress').val() !== '' && $('#EmailAddress').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a Email Address.");
        return false;
    }
    if ($('#StreetAddress').val() !== '' && $('#StreetAddress').val() !== undefined) {
        Valid = true;
    }
    else {
        alert("You must enter a Street Address.");
        return false;
    }
    var isChecked = $("input[type=checkbox]").is(":checked");
    if (isChecked) {
        Valid = true;
    }
    else {
        alert("You must select at least one Business Type.");
        return false;
    }

    return Valid;
}

//submit entire form
function SubmitOrder() {
    if (ValidateProductOrders() && ValidateForm()) {

        SubmitOrderInfo();
        
    }
}

function SubmitOrderInfo() {


    $.ajax({
        type: 'POST',
        url: $('#UrlSubmitOrderInfo').val(),
        data: {
            CompanyName: $('#CompanyName').val(),
            ContactName: $('#ContactName').val(),
            OMMANumber: $('#OMMANumber').val(),
            EINNumber: $('#EINNumber').val(),
            OBNDDNumber: $('#OBNDDNumber').val(),
            PhoneNumber: $('#PhoneNumber').val(),
            EmailAddress: $('#EmailAddress').val(),
            StreetAddress: $('#StreetAddress').val(),
            //BusinessTypes: $('#BusinessTypes').val(),
            BusinessTypes: '',
            Notes: $('#Notes').val()
        },
        success:      function (data, status) {
            console.log(status);
            if (status === "success") {

                $('#NewOrderID').val(data);
                SubmitProductInfo();
            }
            else {
                return false;
            }

        },
        async: false
    });

     
}

function SubmitProductInfo() {

    //var Valid = false;
    $('#ProductTable > tbody  > tr').each(function (row, tr) {
        var OrderID = $('#NewOrderID').val();        
        var ProductID = encodeURIComponent($(this).closest('tr').find('td:eq(1)').text());
        var Quantity = $(this).closest('tr').find('td:eq(3)').text();
        
        $.post($('#UrlSubmitProductInfo').val(),
            {
                OrderInfoID: OrderID,
                Type: ProductID,
                Quantity: Quantity 
            },
            function (data, status) {
                SubmitOrderHistory();
            });
    });

    //return Valid;
}

function SubmitOrderHistory() {
    //var Valid = false;
    $.post($('#UrlSubmitOrderHistory').val(),
        {
            OrderID: $('#NewOrderID').val()
           
        },
        function (data, status) {
            window.location.href = $('#UrlNewOrder').val();
        });

    
}