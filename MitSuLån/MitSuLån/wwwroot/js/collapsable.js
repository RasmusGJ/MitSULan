﻿//Collapsable views controller
var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {
            content.style.display = "none";
        } else {
            content.style.display = "block";
        }
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.display = "inherit";
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
}