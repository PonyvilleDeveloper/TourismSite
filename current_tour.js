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

    var COMMENTS = await getJsonFromUrl("/getcomments");
    console.log(COMMENTS);
    COMMENTS = COMMENTS.filter(function(c) {
        return c.tour_id == id;
    });
    var comments = document.getElementById("comments");
    COMMENTS.forEach(c => {
        var div = document.createElement("div");
        div.className = "comment";
        div.style.display = "flex";
        div.style.alignItems = "center";
        var h4 = document.createElement("h4");
        h4.textContent = c.name;
        h4.style.padding = "5px";
        h4.style.margin = "5px";
        h4.style.border ="1px solid black";
        h4.style.borderRadius = "3px";
        div.appendChild(h4);
        var p = document.createElement("p");
        p.textContent = c.comment;
        p.style.fontStyle = "italic";
        div.appendChild(p);
        comments.appendChild(div);
    });
    
    if(document.cookie != "") {
        document.getElementsByClassName("comment-section")[0].style.display ="block";
        document.getElementById("docomment").textContent = `${sessionStorage.getItem("username")}, оставьте комментарий:`;
        document.getElementById("username").value = sessionStorage.getItem("username");
        document.getElementById("tour_id").value = id;
    }
}

window.addEventListener("DOMContentLoaded", build);