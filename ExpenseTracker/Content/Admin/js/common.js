$(document).ready(function () {

    $("form:not(.filter) :input:visible:enabled:first").focus();

    $('input[type="submit"]').click(function () {
        $('input[type=text], textarea').each(function () {
            if ($(this).val() != '') {
                $(this).val($.trim($(this).val()));
            }
        });
    });
    $('.minusremove').bind('cut copy paste', function () {
        if (e.which === 45 || e.which === 47 || e.which === 92)
            return false;
    });
    $('.minusremove').keypress(function (e) {
        if (e.which === 45 || e.which === 47 || e.which === 92)
            return false;
    });
    $(".isrequired").keypress(function () {
        if ($(this).length != 0) {
            $(this).next().css("display", "none");

        } else {
            $(this).next().css("display", "block");
        }
    });
    $('.emailvalidation1').on('keypress', function () {

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
    $('.emailvalidation').on('keypress', function () {

        var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(this.value);
        if (!re) {
            if ($(this).val().trim() != "") {
                $(this).next().css("display", "none");

            }
            $(this).next().next().css("display", "block");
        } else {
            $(this).next().next().css("display", "none");
        }
    });
    $('.phonevalidation').on('keypress', function () {
        var re = /^\d{10}$/.test(this.value);
        if (!re) {
            if ($(this).length != 0) {
                $(this).next().css("display", "none");

            }
            $(this).next().next().css("display", "block");
        } else {
            $(this).next().next().css("display", "none");
        }
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
            $.get(url, { id: $(this).html().trim() }, function (data) {
                current.html(data);
            });

    });
    $('.shippingCountryhtml').each(function () {
        var url = "/customer/Customer_CountrySelect";
        var current = $(this);
        if ($(this).html().trim() != "" || $(this).html().trim() != null)
            $.get(url, { id: $(this).html().trim() }, function (data) {
                current.html(data);
            });

    });
    $('.billingCountryhtml').each(function () {
        var url = "/customer/Customer_CountrySelect";
        var current = $(this);
        if ($(this).html().trim() != "" || $(this).html().trim() != null)
            $.get(url, { id: $(this).html().trim() }, function (data) {
                current.html(data);
            });

    });
    $('input[type=text]').each(function () {
        var placeholder = $(this).prev().text().trim().replace("*", "");
        $(this).attr('Placeholder', placeholder);
    });
    $('textarea').each(function () {
        var placeholder = $(this).prev().text().trim().replace("*", "");
        $(this).attr('Placeholder', placeholder);
    });
    function toTitleCase(str) {
        return str.replace(/(?:^|\s)\w/g, function (match) {
            return match.toUpperCase();
        });
    }
    //Tamil
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
    //Tamil
    $('.decimaltwoplace').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
           ((event.which < 48 || event.which > 57) &&
           (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') == -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 5) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });
    $('.decimaltwoplace').bind("paste", function (e) {
        var text = e.originalEvent.clipboardData.getData('Text');
        if ($.isNumeric(text)) {
            if ((text.substring(text.indexOf('.')).length > 6) && (text.indexOf('.') > -1)) {
                e.preventDefault();
                $(this).val(text.substring(0, text.indexOf('.') + 3));
            }
        }
        else {
            e.preventDefault();
        }
    });
    //Tamil
    $(".singleQuotes").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = (keyCode != 39);
        $(".error").css("display", ret ? "none" : "inline");
        return ret;
    });
    $(".singleQuotes").bind("paste", function (e) {
        return false;
    });
    $(".singleQuotes").bind("drop", function (e) {
        return false;
    });
    $(".btn-pref .btn").click(function () {
        $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
        // $(".tab").addClass("active"); // instead of this do the below 
        $(this).removeClass("btn-default").addClass("btn-primary");
    });
}).ajaxStart(function () {
    preLoader1fadeIn();
}).ajaxStop(function () {
    preLoader1fadeOut();
});;

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function preLoader1fadeIn() {
    setTimeout(function () { $('#preloader').fadeIn() });
    setTimeout(function () { $('#preloader_status').fadeIn() });
    
}

function preLoader1fadeOut() {
    setTimeout(function () { $('#preloader').fadeOut() });
    setTimeout(function () { $('#preloader_status').fadeOut() });
}




$(document).ready(function () {
    var max_fields_limit = 3;
    var x = 1;
    $('.add_more_button').click(function (e) {
        e.preventDefault();
        if (x < max_fields_limit) {
            x++;
            $('.input_fields_container').append('<div><table id="table"><tr><td>code</td><td><input class="form-control" type="text" id="item" placeholder="items"></td><td>type</td><td><input class="form-control" type="text" id="type" placeholder="type"></td><td>Country</td><td><select class="form-control"><option value="volvo">select</option><option value="saab">select1</option><option value="mercedes">select2</option><option value="audi">select3</option></select></td></tr></table><a href="#" class="remove_field btn btn-primary " style=""><span class="glyphicon glyphicon-minus" aria-hidden="true"></span></a></div>');

        }
    });
    $('.input_fields_container').on("click", ".remove_field", function (e) {
        e.preventDefault();
        $(this).parent('div').remove();
        x--;
    })
});

$(document).ready(function () {
    var max_fields_limit = 3;
    var x = 1;
    $('.add_more_button1').click(function (e) {
        e.preventDefault();
        if (x < max_fields_limit) {
            x++;
            $('.form-group.input_fields_container1').append('<div class="form-group"><label class="col-sm-4 control-label" for="textinput">ISO Alpha Level</label><div class="col-sm-5"><input type="text" name="product_name[]" class="form-control"/></div><a href="#" class="remove_field1 col-sm-3 btn btn-sm btn-danger" style=""><span class="glyphicon glyphicon-minus" aria-hidden="true"></span></a></div>'); //add input field
        }
    });
    $('.form-group.input_fields_container1').on("click", ".remove_field1", function (e) {
        e.preventDefault(); $(this).parent('div').remove(); x--;
    })




});















