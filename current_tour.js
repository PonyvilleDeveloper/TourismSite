async function build() {
    var id = Number((new URL(window.location.href)).searchParams.get("id"));
    var TOUR = await getJsonFromUrl("/gettours");
    TOUR = TOUR.filter(function(t) {
        return t.id == id;
    })[0];
    var PLACES = await getJsonFromUrl("/getplaces");
    PLACES = PLACES.filter(function(p) {
        return TOUR.places.includes(p.id);
    });
    
    var page = document.getElementsByTagName("main")[0];
    const container = document.createElement("div");

    var h3 = document.createElement("h3");
    h3.textContent = TOUR.title;
    container.appendChild(h3);

    PLACES.forEach(p => {
        var card = document.createElement("div");
        card.className = "text-container";
        var h2 = document.createElement("h2");
        h2.textContent = p.name;
        card.appendChild(h2);
        var img = document.createElement("img");
        img.src = p.photo_url;
        card.appendChild(img);
        var txt = document.createElement("div");
        txt.innerHTML = p.description;
        card.appendChild(txt);
        container.appendChild(card);
    });
    
    page.appendChild(container);
    
    if(document.cookie != "") {
        document.getElementsByClassName("comment-section")[0].style.display ="block";
    }
}

window.addEventListener("DOMContentLoaded", build);