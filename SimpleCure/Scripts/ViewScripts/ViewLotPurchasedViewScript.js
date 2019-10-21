$(document).ready(function () {
    $('#PurchaseDate').datetimepicker({
        format: 'L'
    });
    $("#Cost").keyup(function () {
        //if cost is greater than 0
        if (!isNaN($("#Cost").val()) && parseInt($("#Cost").val()) > 0) {

            if (!isNaN($("#Pounds").val()) && parseInt($("#Pounds").val()) > 0) {

                //calculate Pricer per pound and price per gram
                if (!isNaN($("#Cost").val()) && parseInt($("#Cost").val()) > 0) {
                    //calculate the PricePerPound                    
                    $("#PricePerPound").val(Math.round(($("#Cost").val() / $("#Pounds").val()) * 100) / 100);
                    //calculate the PricePerGram                    
                    $("#PricePerGram").val(Math.round(($("#Cost").val() / $("#Grams").val()) * 100) / 100);
                }
            }
        }
        else {
            $("#PricePerPound").val("0");
            $("#PricePerGram").val("0");
        }
    });

    $("#Pounds").keyup(function () {
        //if cost is greater than 0
        if (!isNaN($("#Pounds").val()) && parseInt($("#Pounds").val()) > 0) {

            //convert to grams and assign to grams value  https://www.w3schools.com/howto/tryit.asp?filename=tryhow_js_weight_converter_pounds_to_grams
            $("#Grams").val(Math.round(($("#Pounds").val() / 0.0022046) * 100) / 100);

            //if cost is filled out
            if (!isNaN($("#Cost").val()) && parseInt($("#Cost").val()) > 0) {
                //calculate the PricePerPound                    
                $("#PricePerPound").val(Math.round(($("#Cost").val() / $("#Pounds").val()) * 100) / 100);
                //calculate the PricePerGram                    
                $("#PricePerGram").val(Math.round(($("#Cost").val() / $("#Grams").val()) * 100) / 100);
            }
        }
    });

    $("#Grams").keyup(function () {
        //if cost is greater than 0
        if (!isNaN($("#Grams").val()) && parseInt($("#Grams").val()) > 0) {

            //convert to pounds and assign to punds value https://www.w3schools.com/howto/tryit.asp?filename=tryhow_js_weight_converter_grams_to_pounds
            $("#Pounds").val(Math.round(($("#Grams").val() * 0.0022046) * 100) / 100);

            //if cost is filled out
            if (!isNaN($("#Cost").val()) && parseInt($("#Cost").val()) > 0) {
                //calculate the PricePerPound                    
                $("#PricePerPound").val(Math.round(($("#Cost").val() / $("#Pounds").val()) * 100) / 100);
                //calculate the PricePerGram                    
                $("#PricePerGram").val(Math.round(($("#Cost").val() / $("#Grams").val()) * 100) / 100);
            }
        }
    });
});