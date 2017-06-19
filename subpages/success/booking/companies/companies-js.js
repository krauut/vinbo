$('.company').one("click", function(){
$(".proceed-box").slideDown("fast");
return false;
});

var single_company_container = document.getElementsByClassName("single-company-container");

var myFunction = function() {
    var  company_name = this.getElementsByTagName('h2')[0].innerHTML
    document.getElementById('slide-box').innerHTML = company_name;
};

for (var i = 0; i < single_company_container.length; i++) {
    single_company_container[i].addEventListener('click', myFunction); //false
}



$('.company').on('click', function(){
    $('.company').removeClass('shadow');
    $(this).addClass('shadow');
});

$('.company').on('click', function(){
    $('.company').removeClass('animation');
    $(this).addClass('animation');
});
