function BindState(countryId, stateid) {
    var json = { id: countryId };
    $.ajax({
        type: "POST",
        url: "/Customer/BindState",
        data: json,
        dataType: "json",
        error: function (xhr, status, error) {
        },
        success: function (data) {
            
            $(stateid).empty();

            if (data.length != 0) {

                var markup = "<option value=''>Choose option</option>";

                $.each(data, function (key, val) {
                    markup += "<option value=" + val.Value + ">" + val.Text + "</option>";
                });

                $(stateid).append(markup).data('fastselect').destroy();
                $(stateid).fastselect();
            }
            else {
                var markup = "<option value=''>Choose option</option>";
                $(stateid).append(markup).data('fastselect').destroy();
                $(stateid).fastselect();
            }
        }
    });
}
function BindState2(countryId, stateid, selectedvalue) {
    var json = { id: countryId };
    $.ajax({
        type: "POST",
        url: "/Customer/BindState",
        data: json,
        dataType: "json",
        error: function (xhr, status, error) {
        },
        success: function (data) {

            $(stateid).empty();

            if (data.length != 0) {

                var markup = "<option value=''>Choose option</option>";

                $.each(data, function (key, val) {
                    markup += "<option value=" + val.Value + ">" + val.Text + "</option>";
                });
                $(stateid).append(markup);
                $(stateid).val(selectedvalue);
                $(stateid).data('fastselect').destroy();
                $(stateid).fastselect();
            }
            else {
                var markup = "<option value=''>Choose option</option>";
                $(stateid).append(markup).data('fastselect').destroy();
                $(stateid).fastselect();
            }
        }
    });
}
//Billing
function AddBilling() {

    var length = $('.validate-size-block-billing').children().length;

    var innerBlockLength = $('.validate-size-block-billing').children().last().attr('id');

    innerBlockLength = innerBlockLength.replace('inner-validate-size-block-billing_', '');

    var Address1 = $('#customerbillingdetails_' + innerBlockLength + '__BILLING_ADDRESS1').val();
    var Address2 = $('#customerbillingdetails_' + innerBlockLength + '__BILLING_ADDRESS2').val();
    var City = $('#customerbillingdetails_' + innerBlockLength + '__BILLING_CITY').val();
    var Country = $('#customerbillingdetails_' + innerBlockLength + '__BILLING_COUNTRY').val();
    var State = $('#customerbillingdetails_' + innerBlockLength + '__BILLING_STATE').val();
    if (State == -1) {
        State = $('#customerbillingdetails_' + innerBlockLength + '__BILLING_OTHER_STATE').val();
    }
    var Zip = $('#customerbillingdetails_' + innerBlockLength + '__BILLING_ZIP').val();

    if (Address1 != "" && City != "" && Country != "" && Zip != "" && State != "") {
        $('#customerbillingdetails_0__IS_DEFAULT').removeAttr("disabled");
        var billingCountryItemOptions = $('#customerbillingdetails_0__BILLING_COUNTRY').html();
        var billingCountry = billingCountryItemOptions.replace('selected="selected"', '');

        var Html = "<div id='inner-validate-size-block-billing_" + (Number(innerBlockLength) + 1) + "' class='inner-validate-size-block-billing'>";

        Html += " <div class='col-md-11 col-sm-11 billing_divider'><div class='row'><div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Billing Address1 <span class='required'> *</span></label>";
        Html += "<input class='form-control isrequiredBilling isrequired ' data-val='true' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__BILLING_ADDRESS1' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].BILLING_ADDRESS1' type='text'>";
        Html += "<span class='error-text ' style='display:none'>The Billing Address1 field is required</span>";
        Html += "</div>";
        Html += " <div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Billing Address2</label>";
        Html += "<input class='form-control' data-val='true' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__BILLING_ADDRESS2' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].BILLING_ADDRESS2'  type='text'>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Billing City<span class='required'> *</span></label>";
        Html += "<input class='form-control isrequiredBilling isrequired' data-val='true' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__BILLING_CITY' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].BILLING_CITY'  type='text'>";
        Html += " <span class='error-text' style='display:none'>The Billing City field is required</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3  subcat-sec'>";
        Html += "<label style='font-weight:bold;'>Billing Country<span class='required'> *</span></label>";
        Html += "<select class='form-control fselect BILLINGCOUNTRY billingCountrycheck isrequiredfastBILLING isrequiredfast' data-val='true' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__BILLING_COUNTRY' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].BILLING_COUNTRY'>";
        Html += billingCountry + "</select>";
        Html += " <span class='error-text' style='display:none'>The Billing Country field is required</span>";
        Html += "</div>";
        Html += "</div>";
        Html += "<div class='row'>";
        Html += " <div class='form-group col-md-3 col-sm-3 subcat-sec'>";
        Html += "<label style='font-weight:bold;'>Billing State<span class='required'> *</span></label>";
        Html += "<select class='form-control fselect BILLINGSTATE billingStatecheck isrequiredfastBILLING isrequiredfast' data-val='true' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__BILLING_STATE' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].BILLING_STATE'>";
        Html += "<option value=''>Choose option</option></select>";
        Html += " <span class='error-text' style='display:none'>The Billing State field is required</span>";
        Html += "</div>";
        Html += " <div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Other State<span class='required'> *</span></label>";
        Html += "<input class='form-control BILLINGOTHERSTATE isrequiredBilling' data-val='true' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__BILLING_OTHER_STATE' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].BILLING_OTHER_STATE' disabled type='text'>";
        Html += " <span class='error-text ' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__errBILLING_OTHER_STATE'>";
        Html += "</div>";
        Html += " <div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Billing Zip<span class='required'> *</span></label>";
        Html += "<input class='form-control isrequiredBilling isrequired numeric' data-val='true' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__BILLING_ZIP' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].BILLING_ZIP' type='text'>";
        Html += " <span class='error-text' style='display:none'>The Billing Zip field is required</span>";
        Html += "</div>";
        Html += "</div>";
        Html += "</div>";
        Html += "<div class='col-md-1 col-sm-1'>";
        Html += "<div class='btn-group col-md-12 col-sm-12'>";
        //Html += "<label>Action</label>";
        Html += "<a onclick='RemoveBilling(this);' id='BillingRemove_" + (Number(innerBlockLength) + 1) + "' class='btn btn-primary btn-xs BillingRemove btn-remove'>Remove</a>";
        Html += "</div>";
        //Html += "<div class='form-group col-md-3 col-sm-3 '>";
        //Html += "<label style='font-weight:bold;'>Default</label>";
        //Html += "<div class='chklist'>";
        //Html += "<label class='settings-check'>";
        //Html += "<input class='chk-value chksubmenu chkcustomerbillingdetails' id='customerbillingdetails_" + (Number(innerBlockLength) + 1) + "__IS_DEFAULT' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].IS_DEFAULT' type='checkbox' value='true'>";
        //Html += " <span class='checkmark'></span>";
        //Html += "</label>";
        //Html += "</div>";
        Html += "</div>";
        Html += "</div>";
        Html += "</div>";
        $('.validate-size-block-billing').append(Html);
        $(".fselect").fastselect();
        var specialKeys = new Array();
        specialKeys.push(8);

        startupfuctionBILLING();






    }
    else {
        $(".isrequiredfastBILLING").each(function () {

            if ($(this).val().trim() == "") {
                $(this).parent().next().css("display", "block");
            } else {
                $(this).parent().next().css("display", "none");
            }
        });

        $(".isrequiredBilling").each(function () {

            if ($(this).val().trim() == "") {
                $(this).next().css("display", "block");
            } else {
                $(this).next().css("display", "none");
            }
        });
    }
}
function startupfuctionBILLING() {
    $(".BILLINGCOUNTRY").change(function () {
        var CountryHtmlId = $(this).attr('id').replace("customerbillingdetails_", "").replace("__BILLING_COUNTRY", "");
        var countryId = $(this).val();
        if (countryId != "") {

            BindState(countryId, "#customerbillingdetails_" + CountryHtmlId + "__BILLING_STATE");
        }
        else {
            $("#customerbillingdetails_" + CountryHtmlId + "__BILLING_STATE").empty();
            var markup = "<option value=''>Choose option</option>";
            $("#customerbillingdetails_" + CountryHtmlId + "__BILLING_STATE").append(markup).data('fastselect').destroy();
            $("#customerbillingdetails_" + CountryHtmlId + "__BILLING_STATE").fastselect();
        }
    });


    $('.chkcustomerbillingdetails').on('change', function () {



        $('.chkcustomerbillingdetails').not(this).prop('checked', false);

    });
    $(".BILLINGSTATE").change(function () {
        var StateHtmlId = $(this).attr('id').replace("customerbillingdetails_", "").replace("__BILLING_STATE", "");
        if ($(this).val() == "-1") {
            $('#customerbillingdetails_' + StateHtmlId + '__BILLING_OTHER_STATE').parent().show();
            $('#customerbillingdetails_' + StateHtmlId + '__errBILLING_OTHER_STATE').html("The Other State field is required");
            $('#customerbillingdetails_' + StateHtmlId + '__errBILLING_OTHER_STATE').show();
            $('#customerbillingdetails_' + StateHtmlId + '__BILLING_OTHER_STATE').removeAttr("disabled");
            IsValid = false;
        }
        else {
            $('#customerbillingdetails_' + StateHtmlId + '__BILLING_OTHER_STATE').val("");
            $('#customerbillingdetails_' + StateHtmlId + '__BILLING_OTHER_STATE').attr('disabled', 'disabled');
            $('#customerbillingdetails_' + StateHtmlId + '__errBILLING_OTHER_STATE').hide();
        }

    });

    $(".BILLINGOTHERSTATE").change(function () {
        var StateHtmlId = $(this).attr('id').replace("customerbillingdetails_", "").replace("__BILLING_OTHER_STATE", "");

        if ($('#customerbillingdetails_' + StateHtmlId + '__BILLING_OTHER_STATE').val() == "") {
            $('#customerbillingdetails_' + StateHtmlId + '__errBILLING_OTHER_STATE').html("The Other State field is required");
            $('#customerbillingdetails_' + StateHtmlId + '__errBILLING_OTHER_STATE').css("visibility", "visible");

        }
        else {
            $('#customerbillingdetails_' + StateHtmlId + '__errBILLING_OTHER_STATE').css("visibility", "hidden");
        }

    });
    $(".isrequiredBilling").keyup(function () {
        if ($(this).val().trim() == "") {
            $(this).next().css("display", "block");
        } else {
            $(this).next().css("display", "none");
        }
    });
    $(".isrequiredfast").change(function () {

        if ($(this).val().trim() == "") {
            $(this).parent().next().css("display", "block");
        } else {
            $(this).parent().next().css("display", "none");
        }
    });
    startupcommon();

}
function RemoveBilling(data) {

    var Id = $(data).attr('id').replace("BillingRemove_", "");
    var num = 0;
    var bind = 0;

    if ($('.validate-size-block-billing').children().length > 1) {

        $('#inner-validate-size-block-billing_' + Id).remove();
        $('#inner-validate-size-block-billing_' + Id).remove();
        $(".inner-validate-size-block-billing").each(function () {

            var mid = $(this).attr('id');

            $('#' + $(this).attr('id')).find('select').each(function () {
                var ids = $(this).attr('id');
                var name = $(this).attr('name');
                var newid = ids.replace(/[0-9]+/, num);
                var newname = name.replace(/[0-9]+/, num);
                $(this).attr('id', newid);
                $(this).attr('name', newname);

            });

            $('#' + mid + ' input[type=text]').each(function () {
                var ids = $(this).attr('id');
                var name = $(this).attr('name');
                var newid = ids.replace(/[0-9]+/, num);
                var newname = name.replace(/[0-9]+/, num);
                $(this).attr('id', newid);
                $(this).attr('name', newname);

            });
            $('#' + mid + ' input[type=checkbox]').each(function () {
                var ids = $(this).attr('id');
                var name = $(this).attr('name');
                var newid = ids.replace(/[0-9]+/, num);
                var newname = name.replace(/[0-9]+/, num);
                $(this).attr('id', newid);
                $(this).attr('name', newname);

            });
            $('#' + $(this).attr('id')).find('a').each(function () {

                var ids = $(this).attr('id');

                var newid = ids.replace(/[0-9]+/, num);

                $(this).attr('id', newid);


            });

            num++;

            var newbind = mid.replace(/[0-9]+/, bind);
            $(this).attr('id', newbind);
            bind++;

        });
    }
}

//Common
function PreviousPage() {
    window.location = "/Customer/Customer_list";
}
function valueChanged() {
    if ($('.TAX').is(":checked"))
        $("#TaxFile").show();
    else
        $("#TaxFile").hide();
}
function startupcommon() {
    var specialKeys = new Array();
    specialKeys.push(8);
    $(".numeric").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = ((keyCode >= 48 && keyCode <= 57 || keyCode == 9) || specialKeys.indexOf(keyCode) != -1);
        $(".error").css("display", ret ? "none" : "inline");
        return ret;
    });
    $(".numeric").bind("paste", function (e) {
        return false;
    });
    $(".numeric").bind("drop", function (e) {
        return false;
    });

    $(".numeric1").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = ((keyCode >= 48 && keyCode <= 57 || keyCode == 9) || specialKeys.indexOf(keyCode) != -1);
        $(".error").css("display", ret ? "none" : "inline");
        return ret;
    });
    $(".numeric1").bind("paste", function (e) {
        return true;
    });
    $(".numeric1").bind("drop", function (e) {
        return false;
    });

    $('.emailvalidation').on('keypress', function () {
        var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
        if ($(this).val().trim() != "") {
            if (!re) {
                $('#spshipping_emailid').hide();
                $(this).next().next().css("display", "block");
            } else {
                $(this).next().next().css("display", "none");
            }
        }
        else {
            $(this).next().css("display", "block");
            $(this).next().next().css("display", "none");
        }
    }).on('keydown', function (e) {

        if (e.keyCode == 8)
            $('.emailvalidation').trigger('keypress');
    });

    $(".fselect").fastselect();
}
//Shipping
function AddShipping() {

    var length = $('.validate-size-block-shipping').children().length;

    var innerBlockLength = $('.validate-size-block-shipping').children().last().attr('id');

    innerBlockLength = innerBlockLength.replace('inner-validate-size-block-shipping_', '');

    var Name = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_NAME').val();
    var LastName = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_LAST_NAME').val();
    var Address1 = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_ADDRESS1').val();
    var Address2 = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_ADDRESS2').val();
    var Address1 = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_ADDRESS1').val();
    var Phone = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_PHONE').val();
    var Email = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_EMAIL').val();
    var City = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_CITY').val();
    var Country = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_COUNTRY').val();
    var State = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_STATE').val();

    if (State == -1) {
        State = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING_OTHER_STATE').val();
    }

    var Zip = $('#customershippingdetails_' + innerBlockLength + '__SHIPPING').val();

    if (Email != "" && Phone != "" && LastName != "" && Name != "" && Address1 != "" && City != "" && Country != "" && Zip != "" && State != "") {
        $('#customershippingdetails_0__IS_DEFAULT').removeAttr("disabled");
        var billingCountryItemOptions = $('#customershippingdetails_0__SHIPPING_COUNTRY').html();
        var shippingcountry = billingCountryItemOptions.replace('selected="selected"', '');

        var Html = "<div id='inner-validate-size-block-shipping_" + (Number(innerBlockLength) + 1) + "' class='inner-validate-size-block-shipping'>";

        Html += " <div class='col-md-11 col-sm-11 billing_divider'><div class='row'>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>First Name <span class='required'> *</span></label>";
        Html += "<input autocomplete='off' class='form-control isrequiredShipping isrequired' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_NAME' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_NAME' type='text' />";
        Html += "<span class='error-text ' style='display:none'>The First Name field is required</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Last Name <span class='required'> *</span></label>";
        Html += "<input autocomplete='off' class='form-control isrequiredShipping isrequired' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_LAST_NAME' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_LAST_NAME' type='text' />";
        Html += "<span class='error-text ' style='display:none'>The Last Name field is required</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Address 1 <span class='required'> *</span></label>";
        Html += "<input autocomplete='off' class='form-control isrequiredShipping isrequired' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_ADDRESS1' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_ADDRESS1' type='text' />";
        Html += "<span class='error-text ' style='display:none'>The Address 1 field is required</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Address 2</label>";
        Html += "<input autocomplete='off' class='form-control' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_ADDRESS2' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_ADDRESS2' type='text' />";
        Html += "</div>";
        Html += "</div>";
        Html += "<div class='row'>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Mobile No<span class='required'> *</span></label>";
        Html += "<input autocomplete='off' class='form-control isrequiredShipping isrequired numeric' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_PHONE' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_PHONE' type='text'  />";
        Html += "<span class='error-text ' style='display:none'>The Mobile No field is required</span>";
        Html += "<span id='spshippingphone' class='error-text ' style='display:none'>Please Enter a Valid Mobile No</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Email<span class='required'> *</span></label>";
        Html += "<input autocomplete='off' class='form-control isrequired isrequiredShippingMail emailvalidation' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_EMAIL' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_EMAIL' type='text' />";
        Html += "<span class='error-text ' style='display:none' id='spshipping_emailid' >The Email field is required</span>";
        Html += "<span  id='spshipping_email' class='error-text ' style='display:none'>Please Enter a Valid Email</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Company Name</label>";
        Html += "<input autocomplete='off' class='form-control' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_COMPANY_NAME' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_COMPANY_NAME' type='text' value='' />";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>Zip<span class='required'> *</span></label>";
        Html += "<input autocomplete='off' class='form-control isrequiredShipping isrequired numeric' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_ZIP' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_ZIP' type='text' value='' />";
        Html += "<span class='error-text ' style='display:none'>The Zip field is required</span>";
        Html += "</div>";
        Html += "</div>";
        Html += "<div class='row'>";
        Html += "<div class='form-group col-md-3 col-sm-3'>";
        Html += "<label style='font-weight:bold;'>City<span class='required'> *</span></label>";
        Html += "<input autocomplete='off' class='form-control isrequiredShipping isrequired' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_CITY' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_CITY' type='text'  />";
        Html += "<span class='error-text ' style='display:none'>The City field is required</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3 subcat-sec'>";
        Html += "<label style='font-weight:bold;'>Country<span class='required'> *</span></label>";
        Html += "<select class='form-control fselect isrequiredfastSHIPPING isrequiredfast SHIPPINGCOUNTRY' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_COUNTRY' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_COUNTRY'>" + shippingcountry + "</select>";
        Html += "<span class='error-text ' style='display:none'>The Country field is required</span>";
        Html += "</div>";
        Html += "<div class='form-group col-md-3 col-sm-3 subcat-sec'>";
        Html += "<label style='font-weight:bold;'>State<span class='required'> *</span></label>";
        Html += "<select class='form-control fselect isrequiredfastSHIPPING isrequiredfast SHIPPINGSTATE' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_STATE' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_STATE'><option value=''>Choose option</option>";
        Html += "</select>";
        Html += "<span class='error-text ' style='display:none'>The State field is required</span>";
        Html += "</div>";
        //Html += "<div class='form-group col-md-3 col-sm-3'>";
        //Html += "<label style='font-weight:bold;'>Other State<span class='required'> *</span></label>";
        //Html += "<input autocomplete='off' class='form-control' disabled='disabled' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_OTHER_STATE' name='customershippingdetails[" + (Number(innerBlockLength) + 1) + "].SHIPPING_OTHER_STATE' type='text' />";
        //Html += "<span class='error-text' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__errSHIPPING_OTHER_STATE'>";
        //Html += "</span>";
        //Html += "</div>";

        Html += "</div>";
        Html += "</div>";
        Html += "<div class='col-md-1 col-sm-1'>";
        Html += "<div class='btn-group col-md-12 col-md-12'>";
        //Html += "<label>Action</label>";
        Html += "<a onclick='RemoveShipping(this);' id='ShippingRemove_" + (Number(innerBlockLength) + 1) + "' class='btn btn-primary btn-xs btn-remove'>Remove</a>";
        Html += "</div>";
        //Html += "<div class='col-md-12 col-sm-12 '>";
        //Html += "<label style='font-weight:bold;'>Default</label>";
        //Html += "<div class='chklist'>";
        //Html += "<label class='settings-check'>";
        //Html += "<input class='chk-value chksubmenu chkcustomershipping' id='customershippingdetails_" + (Number(innerBlockLength) + 1) + "__IS_DEFAULT' name='customerbillingdetails[" + (Number(innerBlockLength) + 1) + "].IS_DEFAULT' type='checkbox' value='true'>";
        //Html += " <span class='checkmark'></span>";
        //Html += "</label>";
        //Html += "</div>";
        Html += "</div>";
        Html += "</div>";
        Html += "</div>";
        $('.validate-size-block-shipping').append(Html);


        $('#customershippingdetails_' + (Number(innerBlockLength) + 1) + '__SHIPPING_COUNTRY').change(function () {
            var CountryHtmlId = $(this).attr('id').replace("customershippingdetails_", "").replace("__SHIPPING_COUNTRY", "");
            var countryId = $(this).val();
            if (countryId != "") {

                BindState(countryId, "#customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_STATE");
            }
            else {

                $("#customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_STATE").empty();
                var markup = "<option value=''>Choose option</option>";
                $("#customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_STATE").append(markup).data('fastselect').destroy();
                $("#customershippingdetails_" + (Number(innerBlockLength) + 1) + "__SHIPPING_STATE").fastselect();
            }
        });

        $('#customershippingdetails_' + (Number(innerBlockLength) + 1) + '__SHIPPING_COUNTRY').val(1);
        $('#customershippingdetails_' + (Number(innerBlockLength) + 1) + '__SHIPPING_COUNTRY').trigger('change');

        $(".fselect").fastselect();

        var specialKeys = new Array();
        specialKeys.push(8);

        $('.emailvalidation').on('keypress', function () {
            var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
            if ($(this).val().trim() != "") {
                if (!re) {
                    $('#spshipping_emailid').hide();
                    $(this).next().css("display", "none");
                    $(this).next().next().css("display", "block");
                } else {
                    $(this).next().next().css("display", "none");
                }
            }
            else {
                $(this).next().css("display", "block");
                $(this).next().next().css("display", "none");
            }
        }).on('keydown', function (e) {

            if (e.keyCode == 8)
                $('.emailvalidation').trigger('keypress');
        });

        startupfuctionSHIPPING();
    }
    else {
        $(".isrequiredfastSHIPPING").each(function () {

            if ($(this).val().trim() == "") {
                $(this).parent().next().css("display", "block");
            } else {
                $(this).parent().next().css("display", "none");
            }
        });

        $(".isrequiredShipping").each(function () {

            if ($(this).val().trim() == "") {
                $(this).next().css("display", "block");
            } else {
                $(this).next().css("display", "none");
            }
        });

        $('.isrequiredShippingMail').each(function () {
            var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
            if ($(this).val().trim() != "") {
                if (!re) {
                    $(this).next().next().css("display", "block");
                } else {
                    $(this).next().next().css("display", "none");
                }
            }
            else {
                $(this).next().css("display", "block");
                $(this).next().next().css("display", "none");
            }
        });
    }
}
function startupfuctionSHIPPING() {
    $(".SHIPPINGCOUNTRY").change(function () {
        var CountryHtmlId = $(this).attr('id').replace("customershippingdetails_", "").replace("__SHIPPING_COUNTRY", "");
        var countryId = $(this).val();
        if (countryId != "") {

            BindState(countryId, "#customershippingdetails_" + CountryHtmlId + "__SHIPPING_STATE");
        }
        else {

            $("#customershippingdetails_" + CountryHtmlId + "__SHIPPING_STATE").empty();
            var markup = "<option value=''>Choose option</option>";
            $("#customershippingdetails_" + CountryHtmlId + "__SHIPPING_STATE").append(markup).data('fastselect').destroy();
            $("#customershippingdetails_" + CountryHtmlId + "__SHIPPING_STATE").fastselect();
        }
    });


    $('.chkcustomershipping').on('change', function () {
        $('.chkcustomershipping').not(this).prop('checked', false);
    });

    $(".SHIPPINGSTATE").change(function () {
        var StateHtmlId = $(this).attr('id').replace("customershippingdetails_", "").replace("__SHIPPING_STATE", "");
        if ($(this).val() == "-1") {
            $('#customershippingdetails_' + StateHtmlId + '__SHIPPING_OTHER_STATE').parent().show();
            $('#customershippingdetails_' + StateHtmlId + '__errSHIPPING_OTHER_STATE').html("The Other State field is required");
            $('#customershippingdetails_' + StateHtmlId + '__errSHIPPING_OTHER_STATE').show();
            $('#customershippingdetails_' + StateHtmlId + '__SHIPPING_OTHER_STATE').removeAttr("disabled");
            IsValid = false;
        }
        else {
            $('#customershippingdetails_' + StateHtmlId + '__SHIPPING_OTHER_STATE').val("");
            $('#customershippingdetails_' + StateHtmlId + '__SHIPPING_OTHER_STATE').attr('disabled', 'disabled');
            $('#customershippingdetails_' + StateHtmlId + '__errSHIPPING_OTHER_STATE').hide();
        }

    });

    $(".SHIPPINGOTHERSTATE").change(function () {
        var StateHtmlId = $(this).attr('id').replace("customershippingdetails_", "").replace("__SHIPPING_OTHER_STATE", "");

        if ($('#customershippingdetails_' + StateHtmlId + '__SHIPPING_OTHER_STATE').val() == "") {
            $('#customershippingdetails_' + StateHtmlId + '__errSHIPPING_OTHER_STATE').html("The Other State field is required");
            $('#customershippingdetails_' + StateHtmlId + '__errSHIPPING_OTHER_STATE').css("visibility", "visible");

        }
        else {
            $('#customershippingdetails_' + StateHtmlId + '__errSHIPPING_OTHER_STATE').css("visibility", "hidden");
        }

    });
    $(".isrequiredShipping").keyup(function () {
        if ($(this).val().trim() == "") {
            $(this).next().css("display", "block");
        } else {
            $(this).next().css("display", "none");
        }
    });
    $(".isrequiredfastSHIPPING").change(function () {

        if ($(this).val().trim() == "") {
            $(this).parent().next().css("display", "block");
        } else {
            $(this).parent().next().css("display", "none");
        }
    });

    startupcommon();

}
function RemoveShipping(data) {

    var Id = $(data).attr('id').replace("ShippingRemove_", "");
    var num = 0;
    var bind = 0;

    if ($('.validate-size-block-shipping').children().length > 1) {

        $('#inner-validate-size-block-shipping_' + Id).remove();
        $('#inner-validate-size-block-shipping_' + Id).remove();
        $(".inner-validate-size-block-shipping").each(function () {

            var mid = $(this).attr('id');

            $('#' + $(this).attr('id')).find('select').each(function () {
                var ids = $(this).attr('id');
                var name = $(this).attr('name');
                var newid = ids.replace(/[0-9]+/, num);
                var newname = name.replace(/[0-9]+/, num);
                $(this).attr('id', newid);
                $(this).attr('name', newname);

            });

            $('#' + mid + ' input[type=text]').each(function () {
                var ids = $(this).attr('id');
                var name = $(this).attr('name');
                var newid = ids.replace(/[0-9]+/, num);
                var newname = name.replace(/[0-9]+/, num);
                $(this).attr('id', newid);
                $(this).attr('name', newname);

            });
            $('#' + $(this).attr('id')).find('a').each(function () {

                var ids = $(this).attr('id');

                var newid = ids.replace(/[0-9]+/, num);

                $(this).attr('id', newid);


            });
            $('#' + mid + ' input[type=checkbox]').each(function () {
                var ids = $(this).attr('id');
                var name = $(this).attr('name');
                var newid = ids.replace(/[0-9]+/, num);
                var newname = name.replace(/[0-9]+/, num);
                $(this).attr('id', newid);
                $(this).attr('name', newname);

            });
            num++;

            var newbind = mid.replace(/[0-9]+/, bind);
            $(this).attr('id', newbind);
            bind++;

        });
    }
}
