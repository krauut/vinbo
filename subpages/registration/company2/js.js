$('#yes').click(function(){
  $(".advanced-boxx").slideDown("medium");
  return false;
});

$('#no').click(function(){
  $(".advanced-boxx").slideUp("medium");
  return false;
});



// function CreateJobDo(){
//   if($('#yes input:radio:checked').length > 0){
//   $(".advanced-boxx").slideDown("medium");
//   }
//   else{
//   $(".advanced-boxx").slideup("medium");
// }
