const menuBar = document.querySelector(".menu-bar");
const mobileNavbarDisabled = document.querySelector(".mobile-navbar-disabled");

menuBar.addEventListener("click", function () {
  mobileNavbarDisabled.classList.toggle("mobile-navbar");
});
