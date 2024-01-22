function addCard(data) {
    var container = document.getElementById("tours_container");
    // Create elements
    const card = document.createElement('div');
    var img = document.createElement('img');
    var ul = document.createElement('ul');
    var a = document.createElement('a');

    // Set attributes and content
    card.className = "card";
    card.dataset.title = data.title;
    img.src = data.imgSrc;
    img.alt = data.imgAlt;
    a.href = data.link;
    a.textContent = "Подробнее";

    // Add list items
    data.listItems.forEach(function(item) {
        var li = document.createElement('li');
        li.textContent = item;
        ul.appendChild(li);
    });

    // Append elements to card
    card.appendChild(img);
    card.appendChild(ul);
    card.appendChild(a);

    // Append card to body (or another container)
    container.appendChild(card);

}

async function build() {
    var PLACES = await getJsonFromUrl("/getplaces");
    var TOURS = await getJsonFromUrl("/gettours");
    var buff = [];
    TOURS.forEach(t => {
        buff = PLACES.filter(function(p) {
            return t.places.includes(p.id);
        });
        addCard({
            title: t.title,
            imgSrc: buff[0].photo_url,
            imgAlt: "Фото-обложка тура",
            listItems: buff.map(p => p.name),
            link: `tour?id=${t.id}`
        });
        buff = [];
    });
}

window.addEventListener("DOMContentLoaded", build);
/* 
Usage
addBlock({
    title: "Что посмотреть за 1 день",
    imgSrc: "/resources/img/creml.jpg",
    imgAlt: "Тур 1",
    listItems: [
        "Ярославский музей-заповедник",
        "Парк на Стрелке",
        "Волжская набережная",
        "Губернаторский дом и сад",
        "Красная площадь",
        "Первомайский бульвар",
        "Знаменская башня",
        "Гостиный двор",
        "Улица Кирова"
    ],
    link: "turs1"
}); */
