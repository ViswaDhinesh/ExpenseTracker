var PageState;
var Address1;
var Address2;
var City
var State;
var Zip5;
var Zip4;
var ReturnText;
var Description = "";
var USPSREQUESTUSERAPIID;
var cartWeightPounds;
var cartWeightOunces;

function sweetalertwithButton(title, message, type) {
    if (type == 'success') {


        swal({
            title: title,
            text: message,
            type: 'success'

        });
    }
    else if (type == 'error') {
        swal(
                title,
                message,
                'error'
              )
    }
    else if (type == 'warning') {
        swal(
                title,
                message,
                'warning'
              )
    }
    else if (type == 'info') {
        swal(
                title,
                message,
                'info'
              )
    }
    else if (type == 'question') {
        swal(
                title,
                message,
                'question'
              )
    } else if (type == 'success_btn') {
        swal(
              title,
              message,
              'success'
            )
    }
}

function sweetalert(title, message, type) {
    if (type == 'success') {

        //swal(
        //      title,
        //      message,
        //      'success',{
        //          timer: 1000,
        //      }
        //    )
        ////$('.swal2-buttonswrapper').hide();
        ////setTimeout(function(){ $('.swal2-popup').hide() }, 2000);

        //swal({
        //    title: title,
        //    text: message,
        //    icon: "success",
        //    timer: 2000,
        //    buttons: false
        //});

        swal({
            title: title,
            text: message,
            type: 'success',
            timer: 1500,
            showCancelButton: false,
            showConfirmButton: false
        });
    }
    else if (type == 'error') {
        swal(
                title,
                message,
                'error'
              )
        //swal({
        //    title: title,
        //    text: message,
        //    type: 'error',
        //    timer: 1500,
        //    showCancelButton: false,
        //    showConfirmButton: false
        //});
    }
    else if (type == 'warning') {
        swal(
                title,
                message,
                'warning'
              )
    }
    else if (type == 'info') {
        swal(
                title,
                message,
                'info'
              )
    }
    else if (type == 'question') {
        swal(
                title,
                message,
                'question'
              )
    } else if (type == 'success_btn') {
        swal(
              title,
              message,
              'success'
            )
    }
}

function sweetalertwithurl(title, message, type, url) {
    if (type == 'success') {
        swal(title,
        message,
        'success')
    .then(function (value) {
        window.location.href = url;
    });
    }
}

function toastalert(title, message, type) {
    $.toast().reset('all');
    if (type == 'success') {
        $.toast({
            heading: title,
            text: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: 'success'
        })
    }
    else if (type == 'error') {
        $.toast({
            heading: title,
            text: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: 'error'
        })
    }
    else if (type == 'warning') {
        $.toast({
            heading: title,
            text: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: 'warning'
        })
    }

}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function theAnimation(id, oldval, newval, decimal) {
    Animation = new countUp(id, oldval, newval, decimal, 0);
    Animation.start();
}

function theAnimationShipping(id, oldval, newval, decimal) {
    var newvalue = parseFloat(newval).toFixed(2);
    $("#" + id).html(newvalue);
}

$(document).ready(function () {

    $('input[type=text],input[type=email],input[type=password],input[type=number], textarea').each(function () {
        //alert();
        $(this).prop('autocomplete', 'off');
    });

    var specialKeys = new Array();
    specialKeys.push(8);
    $(".numeric").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = ((keyCode >= 48 && keyCode <= 57 || keyCode == 9) || specialKeys.indexOf(keyCode) != -1);
        $(".error").css("display", ret ? "none" : "inline");
        return ret;
    });
    //$(".numeric").bind("paste", function (e) {
    //    return false;
    //});
    $(".numeric").bind("drop", function (e) {
        return false;
    });
    $('.billingStateshtml').each(function () {
        var url = "/customer/Customer_StateSelect";
        var current = $(this);
        if ($(this).html().trim() != "" || $(this).html().trim() != null)
            $.get(url, { id: $(this).html().trim() }, function (data) {
                current.html(data);
            });

    });
    $('.shippingStateshtml').each(function () {
        var url = "/customer/Customer_StateSelect";
        var current = $(this);
        if ($(this).html().trim() != "" || $(this).html().trim() != null)
            $.get(url, {
                id: $(this).html().trim()
            }, function (data) {
                current.html(data);
            });

    });
    $('.shippingCountryhtml').each(function () {
        var url = "/customer/Customer_CountrySelect";
        var current = $(this);
        if ($(this).html().trim() != "" || $(this).html().trim() != null)
            $.get(url, {
                id: $(this).html().trim()
            }, function (data) {
                current.html(data);
            });

    });
    $('.billingCountryhtml').each(function () {
        var url = "/customer/Customer_CountrySelect";
        var current = $(this);
        if ($(this).html().trim() != "" || $(this).html().trim() != null)
            $.get(url, {
                id: $(this).html().trim()
            }, function (data) {
                current.html(data);
            });

    });

    $('#BILLING_STATE').change(function (e) {

        var val = $("#BILLING_STATE").val();
        GetStateCode($("#BILLING_STATE").val(), 2);
        $('#BILLING_OTHER_STATE').hide();

        if ($("#BILLING_STATE").val() == -1) {
            $('#BILLING_OTHER_STATE').show();
        }
    });
    $("#SHIPPING_ZIP").change(function () {
        if (PageState != "Manage_address") {
            if ($(this).val() != "") {
                var xml = "<CityStateLookupRequest USERID='" + USPSREQUESTUSERAPIID + "'>";
                xml += "<Revision>1</Revision>";
                xml += "<ZipCode ID='0'>";
                xml += "<Zip5>" + $(this).val() + "</Zip5>";
                xml += "<Zip4>0000</Zip4>";
                xml += "</ZipCode>";
                xml += "</CityStateLookupRequest>";


                var xmlDoc = $.parseXML(xml);
                var Isrequired = true;

                $.ajax({
                    type: "GET",
                    url: "http://production.shippingapis.com/ShippingAPI.dll?API=CityStateLookup&XML=" + xml,
                    dataType: "xml",
                    success: function (xml1) {
                        var doc = $(xml1.documentElement);
                        doc.find('ZipCode').each(function () {
                            City = $(this).find('City').text();
                            State = $(this).find('State').text();
                            Zip5 = $(this).find('Zip5').text();
                            Description = $(this).find('Description').text();
                            ReturnText = $(this).find('ReturnText').text();

                        });

                        if (Description == "" && ReturnText == "") {

                            $(".isrequired").removeClass("errorBorder");
                            $('#SHIPPING_CITY').val(City);
                            $('#SHIPPING_ZIP').val(Zip5);
                            GetCountryid('US', 1, State);



                            if ($('#cknsamebillingadd').prop("checked") == true) {
                                GetCountryid('US', 2, State);
                                $('#BILLING_CITY').val(City);
                                $('#BILLING_ZIP').val(Zip5);
                            }

                        }




                    },

                    error: function () {
                        toastalert('', "An error occurred while processing XML file.", 'error');
                        Isrequired = false;


                    }
                });
            }
        }

    });
    $('#SHIPPING_STATE').change(function (e) {

        var val = $("#SHIPPING_STATE").val();
        GetStateCode(val, 1);
        $('#SHIPPING_OTHER_STATE').hide();

        if ($("#SHIPPING_STATE").val() == -1) {
            $('#SHIPPING_OTHER_STATE').show();
        }
        if (PageState == "Shipping_Guest") {
            if ($('#cknsamebillingadd').prop("checked") == true) {

                $("#BILLING_STATE").val($("#SHIPPING_STATE").val());

                $('#BILLING_STATE').prop('disabled', true);
                $('#BILLING_STATE').trigger('change');
            }
        }

    });
}).ajaxStart(function () {
    preLoader1fadeIn();
}).ajaxStop(function () {
    preLoader1fadeOut();
});

function getTaxFromAvalaraTax(line1, city, region, country, postalCode) {
    var url = "/Shipping/GetTaxUsingavatax";
    $.get(url, { line1: line1, city: city, region: region, country: country, postalCode: postalCode, price: $('#subTotalwithDiscount').val() }, function (data) {

        taxvalue = data.tax
        var oldEstimatedTotal = parseFloat($('#estimatedTotal').text() - parseFloat($('#shippingCharges').text()) - parseFloat($('#taxCharges').text()));
        var oldTax = $('#taxCharges').text();
        var newTax = taxvalue;
        theAnimationShipping("taxCharges", oldTax, newTax, 2);

        $('#shippingCharges').text('0.00');
        var oldShippingCharges = $('#shippingCharges').text();
        var newShippingCharge = $('#shippingCharges').text();
        theAnimationShipping("shippingCharges", oldShippingCharges, newShippingCharge, 2);


        var newEstimatedTotal = parseFloat(newTax) + parseFloat(oldEstimatedTotal);
        theAnimationShipping("estimatedTotal", oldEstimatedTotal, newEstimatedTotal, 2);

    });
}

function GetCountryid(countryCode, type, StateCode) {



    jQuery.ajax({
        type: "POST",
        url: "/Shipping/GetCountryId",
        data: {
            code: countryCode,

        },
        success: function (data) {

            if (type == 1) {
                $("#SHIPPING_COUNTRY").data('fastselect').destroy();
                $("#SHIPPING_COUNTRY").val(data);
                $("#SHIPPING_COUNTRY").fastselect();
                $("#SHIPPING_COUNTRY").trigger('change');

            }
            else if (type == 2) {
                if ($('#cknsamebillingadd').prop("checked") == true) {
                    $("#BILLING_COUNTRY").prop('disabled', true);
                    $("#BILLING_COUNTRY").val(data);
                    $("#BILLING_COUNTRY").trigger('change');
                }
                else {
                    $("#BILLING_COUNTRY").data('fastselect').destroy();
                    $("#BILLING_COUNTRY").prop('disabled', true);
                    $("#BILLING_COUNTRY").val(data);
                    $("#BILLING_COUNTRY").fastselect();
                    $("#BILLING_COUNTRY").trigger('change');
                }
            }
            setTimeout(function () {

                jQuery.ajax({
                    type: "POST",
                    url: "/Shipping/GetStateid",
                    data: {
                        code: StateCode,
                        countryCode: $('#SHIPPING_COUNTRY').val()

                    },
                    success: function (data) {

                        if (type == 1) {
                            $("#SHIPPING_STATE").data('fastselect').destroy();
                            $("#SHIPPING_STATE").val(data);
                            $("#SHIPPING_STATE").fastselect();
                            $('#SHIPPING_STATE').trigger('change');
                        }
                        else if (type == 2) {

                            if ($('#cknsamebillingadd').prop("checked") == true) {
                                if ($('#BILLING_STATE').prop('disabled') == true) {
                                    $("#BILLING_STATE").prop('disabled', true);
                                    $("#BILLING_STATE").val(data);
                                    $('#BILLING_STATE').trigger('change');
                                }
                                else {
                                    $("#BILLING_STATE").data('fastselect').destroy();
                                    $("#BILLING_STATE").prop('disabled', true);
                                    $("#BILLING_STATE").val(data);
                                    $('#BILLING_STATE').trigger('change');

                                }

                            }
                            else {
                                $("#BILLING_STATE").data('fastselect').destroy();
                                $("#BILLING_STATE").prop('disabled', true);
                                $("#BILLING_STATE").val(data);
                                $("#BILLING_STATE").fastselect();
                                $('#BILLING_STATE').trigger('change');
                            }

                        }


                    }
                });
            }, 2000);

        }
    });
}

function GetStateCode(stateCode, type) {
    jQuery.ajax({
        type: "POST",
        url: "/Shipping/GetStateCode",
        data: {
            id: stateCode,

        },
        success: function (data) {
            if (type == 1) {
                shippingStateCode = data;
            }
            else if (type == 2) {
                billingStateCode = data;
            }


        }
    });
}

function GetCountryCode(countryCode, type) {
    jQuery.ajax({
        type: "POST",
        url: "/Shipping/GetCountryCode",
        data: {
            id: countryCode,

        },
        success: function (data) {

            if (type == 1) {
                shippingCountryCode = data;
            }
            else if (type == 2) {
                billingCountryCode = data;
            }


        }
    });
}

function preLoader1fadeIn() {
    setTimeout(function () { $('#preloader1').fadeIn() });
    setTimeout(function () { $('#preloader_status1').fadeIn() });
}

function preLoader1fadeOut() {
    setTimeout(function () { $('#preloader1').fadeOut() });
    setTimeout(function () { $('#preloader_status1').fadeOut() });
}

function getShippingRate(Address, City, State, Country, ZipCode, Subtotal) {

    var url = "/Shipping/getShippingRate";
    $.get(url, {
        Address: Address, City: City, State: State, Country: Country, ZipCode: ZipCode, PackageWeight: cartWeightOunces, TotalPrice: Subtotal
    }, function (data) {
        if (data.STATUS == true) {
            getBuildshippingdetails(data.HTMLRETURN);
        }
        else if (data.STATUS == false) {
            swal("Error Occured!", "Something went wrong, Please try again later.", "error");
        }
    });


}

function getBuildshippingdetails(val) {
    preLoader1fadeIn();
    $("#shipping_method_list").empty();
    $("#shipping_method_list").html(val);

    $("input[name='method']").click(function () {

        var favorite = [];
        $.each($("input[name='method']:checked"), function () {
            favorite.push($(this).val());
        });

        var res = favorite.join(", ").split(",");

        var oldTax = $('#taxCharges').text();
        var newTax = $('#taxCharges').text();
        //$("#taxCharges").text(newTax);
        theAnimationShipping("taxCharges", oldTax, newTax, 2);

        var oldShippingCharges = $('#shippingCharges').text();
        var newShippingCharge = parseFloat(res[1]);
        theAnimationShipping("shippingCharges", oldShippingCharges, newShippingCharge, 2);
        //$("#shippingCharges").text(newShippingCharge);
        var oldEstimatedTotal = parseFloat($('#estimatedTotal').text()) - parseFloat(oldShippingCharges) - parseFloat(oldTax);
        var newEstimatedTotal = parseFloat(newShippingCharge) + parseFloat(newTax) + parseFloat(oldEstimatedTotal);

        theAnimationShipping("estimatedTotal", oldEstimatedTotal, newEstimatedTotal, 2);
        //$("#estimatedTotal").text(newEstimatedTotal);

    });

    $("#div_shipping_method_list").show();
    if (PageState == "Shipping_Guest") {
        $(".shippingreadonlycheck").each(function () {
            $(this).attr('readonly', true);
        });
        $(".billingreadonlycheck").each(function () {
            $(this).attr('readonly', true);
        });
        $("#BILLING_COUNTRY").prop('disabled', true);
        $('#BILLING_COUNTRY').trigger('change');
        $('#BILLING_STATE').prop('disabled', true);
        $('#BILLING_STATE').trigger('change');
        $("#SHIPPING_COUNTRY").data('fastselect').destroy();
        $("#SHIPPING_COUNTRY").prop('disabled', true);
        $("#SHIPPING_STATE").data('fastselect').destroy();
        $('#SHIPPING_STATE').prop('disabled', true);
        $("#btnUpdate").html("Update Address");
    }
    preLoader1fadeOut();
}

function getUSPSAddressXml(addr1, addr2, City, State, ZipCode) {

    var xml = "";
    var url = "http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=";
    xml += "<AddressValidateRequest USERID='" + USPSREQUESTUSERAPIID + "'>";
    xml += "<Revision>1</Revision>";
    xml += "<Address ID='0'>";
    xml += "<Address1>" + addr1.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, ' ') + "</Address1>";
    xml += "<Address2>" + addr2.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, ' ') + "</Address2>";
    xml += "<City>" + City + "</City>";
    xml += "<State>" + State + "</State>";
    xml += "<Zip5>" + ZipCode + "</Zip5>";
    xml += "<Zip4>0000</Zip4>";
    xml += "</Address>";
    xml += "</AddressValidateRequest>";
    return url + xml;
}

function getVerifyUSPSAddress(addr1, addr2, City1, State1, ZipCode, s_addr1, s_addr2, s_City, s_ZipCode, b_addr1, b_addr2, b_City, b_ZipCode) {

    var Isrequired = true;
    var result1 = []
    $.when($.ajax({
        type: "GET",
        url: getUSPSAddressXml(addr1, addr2, City1, State1, ZipCode),
        dataType: "xml",
        success: function (xml1) {




        },
        error: function () {
            toastalert('', "An error occurred while processing XML file.", 'error');
            Isrequired = false;
        }
    })).done(function (result) {



        var doc = $(result.documentElement);
        doc.find('Address').each(function () {
            Address2 = $(this).find('Address1').text();
            Address1 = $(this).find('Address2').text();
            City = $(this).find('City').text();
            State = $(this).find('State').text();
            Zip5 = $(this).find('Zip5').text();
            Zip4 = $(this).find('Zip4').text();
            Description = $(this).find('Description').text();
            ReturnText = $(this).find('ReturnText').text();

        });

        if (Description == "" && ReturnText == "") {
            toastalert('', 'Address Verified', 'success');
            s_addr1.val(Address1);
            s_addr2.val(Address2);
            s_City.val(City);
            s_ZipCode.val(Zip5);
            if ($('#cknsamebillingadd').prop("checked") == true) {
                b_addr1.val(Address1);
                b_addr2.val(Address2);
                b_City.val(City);
                b_ZipCode.val(Zip5);
                Isrequired = true;
            }

        }
        else if (Description != "") {

            if (Description.trim() == "Invalid Zip Code.".trim()) {
                toastalert('', Description, 'error');
            }
            else if (Description.trim() == "Invalid City.".trim()) {
                toastalert('', Description, 'error');
            }
            else {
                var temphtml = "<p>We were not able to match your address with the USPS address database.</p>"
                              + "<p>Invalid Address May be getting Delay for Delivery.</p>"
                              + "<p><b>" + Description + "</b></p><p>You Entered</p>"
                              + "<p>" + s_addr1.val() + ","
                              + s_addr2.val() + "</p>"
                              + "<p>" + b_ZipCode.val() + ",</p>"
                              + "<p>" + b_City.val() + ",</p>"
                              + "<p>" + State1 + ",</p>"
                              + "<p>" + "USA. </p>";

                $('#verifiymodalbody').empty();
                $("#verifiymodalbody").append(temphtml);
                $('#myModal').modal('toggle');
            }
            Isrequired = false;
        }
        else if (ReturnText != "") {
            toastalert('', ReturnText, 'error');
            Isrequired = false;
        }
        result1 = [Address1, Address2, City, State, Zip5, Isrequired];



    });


    //setTimeout(function () { console.log(result1); return result1 }, 3000);


}

function keyPress(shippingtextbox, billingtextbox) {

    $("#" + shippingtextbox + "").keypress(function () {
        var $this = $(this);

        window.setTimeout(function () {

            if ($('#cknsamebillingadd').prop("checked") == true) {
                $("#" + billingtextbox + "").val($this.val());

            }
        }, 0);
    }).on('keydown', function (e) {

        if (e.keyCode == 8)
            $("#" + shippingtextbox + "").trigger('keypress');
    });
}

function InitLoadkeyup() {
    $("#Shipping-Validate .isrequired").keyup(function () {
        var data = $(this).val();
        if (data != "") {
            $(this).removeClass("errorBorder")
        }
        else {
            $(this).addClass("errorBorder");
        }
    });
    $("#Billing-Validate .isrequired").keyup(function () {
        var data = $(this).val();
        if (data != "") {
            $(this).removeClass("errorBorder");
        }
        else {
            $(this).addClass("errorBorder");
        }
    });
    $('#Shipping-Validate').find('select').change(function () {
        if ($(this).val().trim() == "") {
            $(this).parent().addClass("errorBorder");
            Isrequired = false;
        } else {
            $(this).parent().removeClass("errorBorder");
        }
    });

    $('#Billing-Validate').find('select').change(function () {
        console.log($(this).attr("id") + "---" + $(this).val());
        if ($(this).val().trim() == "") {
            $(this).parent().addClass("errorBorder");
            Isrequired = false;
        } else {
            $(this).parent().removeClass("errorBorder");
        }
    });
    $(".emailvalidation").keyup(function () {


        var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
        if (!re) {
            if ($(this).val().trim() != "") {
                $(this).removeClass("errorBorder");

            }
            $(this).addClass("errorBorder");
        } else {
            $(this).removeClass("errorBorder");
        }


    });
}

function getUSPSNotMatchDes(Description, SHIPPING_ADDRESS1, SHIPPING_ADDRESS2, SHIPPING_ZIP, SHIPPING_CITY, shippingStateCode) {
    var temphtml =
    "<p>We were not able to match your address with the USPS address database.</p>"
    + "<p>Invalid Address May be getting Delay for Delivery.</p>"
    + "<p><b>" + Description + "</b></p><p>You Entered</p>"
    + "<p>" + SHIPPING_ADDRESS1 + ","
    + SHIPPING_ADDRESS2 + "</p>"
    + "<p>" + SHIPPING_ZIP + ",</p>"
    + "<p>" + SHIPPING_CITY + ",</p>"
    + "<p>" + shippingStateCode + ",</p>"
    + "<p>" + "USA. </p>";
    return temphtml;
}


