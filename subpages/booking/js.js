$('.type').on('click', function(){
    $('.type.active').removeClass('active');
    $(this).addClass('active');
});

$('.advanced-button .btn-lbl').click(function(){
  $(".advanced-box").slideToggle("medium");
  return false;
});

$('.type').on('click', function(){
    $('.tyype').toggleClass('visible');
});
