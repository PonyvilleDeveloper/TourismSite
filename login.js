const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});
function isValidEmail(email) {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
}
async function getusername() {
    try {
        const response = await fetch('/username?login=' + document.getElementById("login_enter").value, {
            credentials: 'include' // Добавление куки к запросу
        });
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.text(); // Используйте .text() для получения тела ответа как строки
        // Запись данных в SessionStorage
        sessionStorage.setItem('username', data);
    } catch (error) {
        console.error('Произошла проблема с операцией fetch: ', error);
    }
}
async function memberuser() {
    sessionStorage.setItem('username', document.getElementById("inputusername").value);
}