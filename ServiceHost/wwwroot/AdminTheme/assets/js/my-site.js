function makeSlug(source, dist) {

    var firstEntry = document.querySelector('#' + source).value;
    document.querySelector("#" + dist).value = convertToSlug(firstEntry);
}

function convertToSlug(str) {
    var slug = '';
    const trimmed = str.trim();
    slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return slug.toLowerCase();
}


function fillField(source, dist) {
    var firstEntry = document.querySelector('#' + source).value;
    document.querySelector("#" + dist).value = firstEntry;
}
