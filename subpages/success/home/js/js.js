$('.burger-button').click(function(){
  $('span:nth-child(2)').toggleClass('transparent');
  $('span:nth-child(1)').toggleClass('rotate-top');
  $('span:nth-child(3)').toggleClass('rotate-bottom');
  $(".burger-links").slideToggle("fast");
  return false;
});

// $("a[href='#top']").click(function() {
//   $("html, body").animate({ scrollTop: 0 }, "slow");
//   return false;
// });
