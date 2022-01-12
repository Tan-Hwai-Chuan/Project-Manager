// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function hideShowToggle(hideShowId) {
    var x = document.getElementById(hideShowId);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}

function switchVisible(div1, div2) {
    var x = document.getElementById(div1);
    var y = document.getElementById(div2);

    if (x) {

        if (x.style.display == 'none') {
            x.style.display = 'block';
            y.style.display = 'none';
        }
        else {
            x.style.display = 'none';
            y.style.display = 'block';
        }
    }
}