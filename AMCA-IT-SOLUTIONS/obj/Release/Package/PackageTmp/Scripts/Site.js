$('.col-md-3,.col-lg-4,.col-md-12,.insdustries-icon,.col-md-6, .col-md-5, .col-md-8,.top-grid').attr({ 'data-aos': 'fade-up', 'data-aos-offset': 300 });
$("form [class*='col-'], #accordionJobs [class*='col-'], footer [class*= 'col-']").attr({ 'data-aos': '' });
AOS.init();
//aos animation end

document.addEventListener("DOMContentLoaded", function () {
    // make it as accordion for smaller screens
    if (window.innerWidth < 992) {

        // close all inner dropdowns when parent is closed
        document.querySelectorAll('.navbar .dropdown').forEach(function (everydropdown) {
            everydropdown.addEventListener('hidden.bs.dropdown', function () {
                // after dropdown is hidden, then find all submenus
                this.querySelectorAll('.submenu').forEach(function (everysubmenu) {
                    // hide every submenu as well
                    everysubmenu.style.display = 'none';
                });
            })
        });

        document.querySelectorAll('.dropdown-menu a').forEach(function (element) {
            element.addEventListener('click', function (e) {
                let nextEl = this.nextElementSibling;
                if (nextEl && nextEl.classList.contains('submenu')) {
                    // prevent opening link if link needs to open dropdown
                    //e.preventDefault();
                    if (nextEl.style.display == 'block') {
                        nextEl.style.display = 'none';
                    } else {
                        nextEl.style.display = 'block';
                    }

                }
            });
        })
    }
    // end if innerWidth
});
// DOMContentLoaded  end
// if (window.innerWidth > 992) {
    window.addEventListener('scroll', function () {
        if (window.scrollY > 70) {
            document.getElementById('makeHeaderSticky').classList.add('fixed-top');
            // add padding top to show content behind navbar
            //navbar_height = document.querySelector('.navbar').offsetHeight;
            //document.body.style.paddingTop = navbar_height + 'px';
            // $("body").animate({ 'paddingTop': navbar_height + 'px'  }, "slow");
        } else {
            //alert();
            document.getElementById('makeHeaderSticky').classList.remove('fixed-top');
            // remove padding top from body
            //document.body.style.paddingTop = '0';
            // $("body").animate({ 'paddingTop': '0' }, "slow");


        }
    });
//}
$("#HomeLink").addClass("active").removeClass("active");
$(".navbar-nav").find('[href="' + window.location.pathname + '"]').parent().addClass("active");
$(".dropdown-content").find('[href="' + window.location.pathname + '"]').addClass("active");
$("#stickySidebar tr td").find('[href="' + window.location.pathname + '"]').addClass("active");
/* toggle menubar*/

/*header menu mobile*/
function toggleMenu(e) {
    $(e).find('span').toggleClass('fa fa-bars fa fa-times');
}
/*to make active and inactive jobs*/
$('.collapse.disabled').remove();
// make sticky sidebar
if ($('#stickySidebar').length > 0) {
    //debugger
    function sticky_relocate() {
        var window_top = $(window).scrollTop();
        var footer_top = $("footer").offset().top - 110;
        var div_top = $('#stickySidebar').offset().top;
        var div_height = $("#makeSticky").height();


        if (window_top + div_height > footer_top)
            $('#makeSticky').removeClass('stick');
        else if (window_top > div_top) {
            $('#makeSticky').addClass('stick');
        } else {
            $('#makeSticky').removeClass('stick');

        }

    }

    $(function () {

        //alert();
        $(window).scroll(sticky_relocate);
        sticky_relocate();
    });
}

    // make sticky sidebar end



// DOMContentLoaded  end
$(".owl-carousel-1").owlCarousel({
    loop: true,
    animateOut: 'fadeOut',
    animateIn: 'fadeIn',
    margin: 10,
    pagination: true,
    autoplaySpeed: 3000,
    autoplayHoverPause: true,
    items: 1,
    dots: false,
    responsive: {
        0: {
            stagePadding: 15,
        },
        600: {
            stagePadding: 30,
        },
        1000: {
            nav: true,
            autoplay: true,
        },
        1200: {
            nav: true,
            autoplay: true,
        }
    }
});
$('.owl-carousel-3').owlCarousel({
    loop: true,
    autoplay: true,
    margin: 10,
    pagination: true,
    autoplayHoverPause: true,
    autoplaySpeed: 3000,
    responsive: {
        0: {
            items: 1,
            stagePadding: 30,
        },
        600: {
            items: 2,
            stagePadding: 30,

        },
        1000: {
            items: 2,
            nav: true,
            stagePadding: 30,
        },
        1200: {
            items: 3,
            nav: true,
        }
    }
});
// slider owl-carousel-3 end
$('.owl-carousel-4').owlCarousel({
    loop: true,
    autoplay: true,
    margin: 10,
    pagination: true,
    autoplayHoverPause: true,
    autoplaySpeed: 3000,
    responsive: {
        0: {
            items: 1,
            stagePadding: 50,
        },
        600: {
            items: 2,
            stagePadding: 50,

        },
        1000: {
            items: 3,
            nav: true,
            stagePadding: 50,
        },
        1200: {
            items: 4,
            nav: true,
        }
    }
});
// slider owl-carousel-4 end
$('#partnersSlide').owlCarousel({
    loop: true,
    autoplay: true,
    margin: 10,
    pagination: true,
    autoplayHoverPause: true,
    autoplaySpeed: 3000,
    responsive: {
        0: {
            items: 2,
            stagePadding: 40,
        },
        600: {
            items: 3,
            stagePadding: 50,
        },
        1000: {
            items: 4,
            nav: true,
            stagePadding: 60,
        },
        1200: {
            items: 5,
            nav: true,
        }
    }
});
//partners - slide end

$("#MessageContact").keyup(function () {
    el = $(this);
    if (el.val().length >= 150) {
        el.val(el.val().substr(0, 150));
    } else {
        $("#charNum").text(150 - el.val().length);
    }
});
//check in textarea counter
/*search in Dropdown*/
$('.search-SelectList').hover(function () {
    var CountryCode = document.getElementById('CountryCode');
    var TradeLicenseAuthority = document.getElementById('TradeLicenseAuthority');
    var Nationality = document.getElementById('Nationality');
    var CurrentLocation = document.getElementById('CurrentLocation');

    if (CountryCode) {
        dselect(document.querySelector('#CountryCode'), {
            search: true

        })
    }
    if (TradeLicenseAuthority) {
        dselect(document.querySelector('#TradeLicenseAuthority'), {
            search: true

        })
    }
    if (Nationality) {
        dselect(document.querySelector('#Nationality'), {
            search: true

        })
    }
    if (CurrentLocation) {
        dselect(document.querySelector('#CurrentLocation'), {
            search: true

        })
    }
})
/*search in Dropdown end*/
/*captcha and number validation*/
var captcha = document.getElementById('captcha');
if (captcha) {
    var textCapSel = captcha.getElementsByTagName("input")[1];
    textCapSel.classList.add("form-control")
    $("#CaptchaImage").attr("alt", "amcadigital");
    $("#CaptchaInputText").attr("Placeholder", "Enter Captcha");
    $('div, a, input').next('br').remove();
    setTimeout(function () {
        captcha.getElementsByTagName("a")[0].removeAttribute("href");
    }, 500);

    if ($('#CapErrorMessage').text().length > 3) {
        // alert();
        var target = document.getElementById("CaptchaInputText");
        target.focus();
        document.getElementById("CaptchaInputText").scrollIntoView();
    }
}
var nanCheck = $('.nanCheck');
if (nanCheck) {
    nanCheck.keyup(function () {
        var value = $(this).val();
        value = value.replace(/^(0*)/, "");
        value = value.replace(/[^0-1-2-3-4-5-6-7-8-9\s]/g, '');
        $(this).val(value);
    });
}
$(document).ready(function () {
    var b = "+971";
    $('#CountryCode option:selected').val(b);
    $('#CountryCode option')[0].value = b;
    $('#CountryCode option')[0].innerHTML = $('#CountryCode option:selected').val();
    $("#CountryCode").val($('#CountryCode option:selected').val());
    $('#CountryCode').change(function () {
        var a = $('#CountryCode option:selected').val();
        $('#CountryCode option')[0].value = a;
        $('#CountryCode option')[0].innerHTML = $('#CountryCode option:selected').val();
        $("#CountryCode").val($('#CountryCode option:selected').val());
    });
});
function ShowSuccess(data) {
    debugger
    var isok = data.isok;
    var msg = data.message;
    if (isok == true) {
        $('#alertMessage').html('<div class="alert alert-success">' + msg + '</div>');
        $("#alertMessage").fadeIn("fast", function () {
            setTimeout(function () {
                $("#SubscribForm")[0].reset();
                $("#alertMessage").fadeOut("slow");
            }, 3000);
        });

    }
    if (isok == false) {
        $('#alertMessage').html('<div class="alert alert-danger">' + msg + '</div>');
        $("#alertMessage").fadeIn("fast", function () {
            setTimeout(function () {
                $("#alertMessage").fadeOut("slow");
            }, 2000);
        });

    }
}
//newsletter subscrib from alert message end




