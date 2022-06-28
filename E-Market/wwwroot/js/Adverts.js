
function filterCats() {
    let region = $("#region-filter").val()
    let href = window.location.origin
    console.log(href)

    window.location.replace(href + "?region=" + region);
}