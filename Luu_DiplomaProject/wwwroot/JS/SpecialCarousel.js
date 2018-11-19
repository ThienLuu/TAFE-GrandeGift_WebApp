﻿var $item = $('.carousel-item');
//var $wHeight = $(window).height();
$item.eq(0).addClass('active');
$item.height(250);
$item.addClass('full-screen');

$('.carousel img').each(function () {
    var $src = $(this).attr('src');
    var $color = $(this).attr('data-color');
    $(this).parent().css({
        'background-image': 'url(' + $src + ')',
        'background-color': $color
    });
    $(this).remove();
});

//$(window).on('resize', function () {
//    $wHeight = $(window).height();
//    $item.height($wHeight);
//});

$('.carousel').carousel({
    interval: 6000,
    pause: "false"
});

//$(document).ready(function () {
//    // This will fire when document is ready:
//    $(window).resize(function () {
//        // This will fire each time the window is resized:
//        if ($(window).width() >= 720) {
//            // if larger or equal
//            $('.main-text').css('top', '320px');
//        } else {
//            $('.main-text').css('top', '200px');
//        }
//    }).resize(); // This will simulate a resize to trigger the initial run.
//});