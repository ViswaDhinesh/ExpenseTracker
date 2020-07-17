$(window).on("load", function () {
    $(".mega_menu").mCustomScrollbar();
    // $(".cart_box .dropdown-menu").mCustomScrollbar();    
});
$(document).ready(function () {
    //$("#home").carousel({interval: 5000});
    //	wow = new WOW(
    //      {
    //      boxClass:     'wow',      // default
    //      animateClass: 'animated', // default
    //      offset:       0,          // default
    //      mobile:       true,       // default
    //      live:         true        // default
    //    }
    //    )
    //    wow.init();
    var wow = new WOW({
        boxClass: 'wow', // animated element css class (default is wow)
        animateClass: 'animated', // animation css class (default is animated)
        offset: 0, // distance to the element when triggering the animation (default is 0)
        mobile: true, // trigger animations on mobile devices (default is true)
        live: true, // act on asynchronously loaded content (default is true)
        callback: function (box) {
            // the callback is fired every time an animation is started
            // the argument that is passed in is the DOM node being animated
        },
        scrollContainer: null, // optional scroll container selector, otherwise use window,
        resetAnimation: true, // reset animation on end (default is true)
    });
    wow.init();
    //New Arrival
    $('#new_arrival_list').owlCarousel({
        loop: false,
        margin: 20,
        responsiveClass: true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 450,
        autoplay: true,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1,
                nav: true
            },
            600: {
                items: 3,
                nav: false
            },
            1000: {
                items: 3,
                nav: true,
                navText: ["<div class='nav-btn prev-slide'><img src='../../Content/Front/images/thumb_left_arrow.png'></div>", "<div class='nav-btn next-slide'><img src='../../Content/Front/images/thumb_right_arrow.png'></div>"],
                loop: false,
                margin: 20
            }
        }
    });
    //Daily Special 
    $('#daily_special_list').owlCarousel({
        loop: false,
        margin: 20,
        responsiveClass: true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 450,
        autoplay: true,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1,
                nav: true
            },
            600: {
                items: 3,
                nav: false
            },
            1000: {
                items: 3,
                nav: true,
                navText: ["<div class='nav-btn prev-slide'><img src='../../Content/Front/images/thumb_left_arrow.png'></div>", "<div class='nav-btn next-slide'><img src='../../Content/Front/images/thumb_right_arrow.png'></div>"],
                loop: false,
                margin: 20
            }
        }
    });
    //Thumbnail 
    $('#featured_category_list').owlCarousel({
        loop: false,
        margin: 20,
        responsiveClass: true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 450,
        autoplay: true,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1,
                nav: true
            },
            600: {
                items: 5,
                nav: false
            },
            1000: {
                items: 5,
                nav: true,
                navText: ["<div class='nav-btn prev-slide'><img src='../../Content/Front/images/thumb_left_arrow.png'></div>", "<div class='nav-btn next-slide'><img src='../../Content/Front/images/thumb_right_arrow.png'></div>"],
                loop: false,
                margin: 10
            }
        }
    });
    //Home Slide
    $('#home').owlCarousel({
        items: 1,
        nav: true,
        navText: ["<div class='nav-btn prev-slide home-prev'><img src='../../Content/Front/images/prev_left.png'></div>", "<div class='nav-btn next-slide home-next'><img src='../../Content/Front/images/prev_right.png'></div>"],
        loop: true,
        autoplay: true,
        autoplayHoverPause: true,
    });
    $('#home1').owlCarousel({
        items: 1,
        nav: true,
        navText: ["<div class='nav-btn prev-slide home-prev'><img src='images/prev_left.png'></div>", "<div class='nav-btn next-slide home-next'><img src='images/prev_right.png'></div>"],
        loop: false,
        autoplay: true,
        autoplayHoverPause: true,
    });
    //nice select
    // $('select').niceSelect();
    // $('.dropdown-menu').click(function(e) {
    //    e.stopPropagation();
    //});

    //signup 

    $("#sign_up").click(function () {
        $(this).hide();
    });

    //Multilevel submenu
    //  $( '.dropdown-menu a.dropdown-toggle' ).on( 'click', function ( e ) {
    //     var $el = $( this );
    //     $el.toggleClass('active-dropdown');
    //     var $parent = $( this ).offsetParent( ".dropdown-menu" );
    //     if ( !$( this ).next().hasClass( 'show' ) ) {
    //         $( this ).parents( '.dropdown-menu' ).first().find( '.show' ).removeClass( "show" );
    //     }
    //     var $subMenu = $( this ).next( ".dropdown-menu" );
    //     $subMenu.toggleClass( 'show' );

    //     $( this ).parent( "li" ).toggleClass( 'show' );

    //     $( this ).parents( 'li.nav-item.dropdown.show' ).on( 'hidden.bs.dropdown', function ( e ) {
    //         $( '.dropdown-menu .show' ).removeClass( "show" );
    //         $el.removeClass('active-dropdown');
    //     } );

    //      if ( !$parent.parent().hasClass( 'navbar-nav' ) ) {
    //         $el.next().css( { "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 } );
    //     }

    //     return false;
    // } ); 
    // $('.navbar .dropdown').hover(function () {
    //       $(this).find('.dropdown-menu').first().stop(true, true).slideDown(150);
    //   }, function () {
    //       $(this).find('.dropdown-menu').first().stop(true, true).slideUp(105)
    // });
    //Payment
    $(".payment").click(function () {
        $(".payment").removeClass('payment_selected');
        $(this).addClass('payment_selected');
    });

    //Custom Upload
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
                $('#imagePreview').hide();
                $('#imagePreview').fadeIn(650);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#imageUpload").change(function () {
        readURL(this);
    });
    $(function () {
        //$('#main_navbar').bootnavbar();
    })
    //Headroom
    $(function () {
        let header = document.getElementById('header');
        let headroom = new Headroom(header);
        headroom.init();

        // Menu Responsive
        // Calculamos el ancho de la página
        let ancho = $(window).width(),
			enlaces = $('#enlaces'),
			btnMenu = $('#btn-menu'),
			icono = $('#btn-menu .icono');

        if (ancho < 700) {
            enlaces.hide();
            icono.addClass('fa-bars');
        }

        btnMenu.on('click', function (e) {
            enlaces.slideToggle();
            icono.toggleClass('fa-bars');
            icono.toggleClass('fa-times');
        });

        $(window).on('resize', function () {
            if ($(this).width() > 700) {
                enlaces.show();
                icono.addClass('fa-times');
                icono.removeClass('fa-bars');
            } else {
                enlaces.hide();
                icono.addClass('fa-bars');
                icono.removeClass('fa-times');
            }
        });

    });
    $("#customer").click(function () {
        $(".seller").toggle();
        $("#business_tab").removeClass("password");
    });
    $(".track_order_form .btn").click(function () {
        $(".fixed-top").css("z-index", "999");
    });
    $(".unsubscribe-newsletter").click(function () {
        $(".fixed-top").css("z-index", "999");
    });
    //$("#forgot").click(function () {
    //    $(".signin_col").hide();
    //   // $(".forgot_col").show();
    //});


    $("#video").click(function () {
        $(".fixed-top").css("z-index", "999");
    });

    $("#reorder").click(function () {
        $(".fixed-top").css("z-index", "999");
    });

    $(".more_pieces_list").hide();
    $(".card_desc span").hide();
    //$(".readmore").click(function () {
    //    $(".card_desc span").toggle();
    //});
    $(".more_pieces").click(function () {
        $(".more_pieces_list").toggle();
    });
    //    $('.flip').hover(
    //        function(){
    //            $(this).find('.back').slideDown(400); //.fadeIn(250)
    //        },
    //        function(){
    ////            $(this).find('.back').slideUp(400); //.fadeOut(205)
    //        }
    //    ); 
    //Loader...
    setTimeout(function () { $('#preloader').fadeOut() }, 1500);
    setTimeout(function () { $('#preloader_status').fadeOut() }, 1500);
    //Custom Scroll...
    //$("body").niceScroll({
    //    cursorcolor: "#000",
    //    cursorwidth: "9px",
    //    cursorborder: "none",
    //    cursorborderradius: "0px",
    //    autohidemode: false,
    //    height:"100%",
    //    railpadding: { top: 0, right: -2, left: 3, bottom: 0 }
    //});
});