function sendVerificationCode() {
    var email = document.getElementById("email").value;
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (email === "") {
        alert("Пожалуйста, введите ваш электронный адрес.");
    } else if (!emailPattern.test(email)) {
        alert("Пожалуйста, введите корректный электронный адрес.");
    } else {
        document.getElementById("verificationCodeSection").style.display = "block";
        document.getElementById("email").readOnly = true;
    }
}

function checkVerificationCode() {
    var code = document.getElementById("verificationCode").value;

    // Здесь должна быть ваша логика проверки кода, например:
    if (code === "1234") { // Пример: проверяем, равен ли введенный код "1234"
        window.location.href = "new_password.html"; // Переход на страницу с установкой нового пароля
    } else {
        alert("Код введен неверно. Пожалуйста, введите корректный код.");
    }
}
