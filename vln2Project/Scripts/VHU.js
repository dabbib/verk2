var $output = $('#output');
$(window).on('scroll', function () {
    var scrollTop = $(window).scrollTop(),
        elementOffset = $('#foo').offset().top,
        distance = (elementOffset - scrollTop);

    $output.prepend('<p>' + distance + '</p>');
});