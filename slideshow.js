var slideIndex = 0;
showSlides(slideIndex);

function plusSlides(n) {
  showSlides(slideIndex -= n);
}

function currentSlide(n) {
  showSlides(slideIndex = n);
}

function showSlides(n) {
  var i;
  var slides = document.getElementsByClassName("mySlides");
  var dots = document.getElementsByClassName("dot");
  
  if (n > slides.length) { slideIndex = 1; }    
  if (n < 1) { slideIndex = slides.length; }
  
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";  
  }
  
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  
  slides[slideIndex - 1].style.display = "block";  
  dots[slideIndex - 1].className += " active";
}

var nextBtn = document.querySelector('.next');
var prevBtn = document.querySelector('.prev');

nextBtn.addEventListener('click', function() {
  plusSlides(1);
});

prevBtn.addEventListener('click', function() {
  plusSlides(-1);
});

var dots = document.getElementsByClassName('dot');
for (var i = 0; i < dots.length; i++) {
  dots[i].addEventListener('click', function() {
    currentSlide(Array.from(dots).indexOf(this) + 1);
  });
}
function autoSlide() {
  plusSlides(-1);
  setTimeout(autoSlide, 5000); // Вызываем снова через 5 секунд
}

autoSlide(); // Запустить автопереключение