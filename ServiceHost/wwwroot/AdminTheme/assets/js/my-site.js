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

jQuery.validator.addMethod("maxFileSize",
    function (value, element, params) {
        var size = element.files[0].size;
        var maxSize = 1 * 1024*1024;
        if (size > maxSize)
            return false;
        else {
            return true;
        }
    });
jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");

jQuery.validator.addMethod("fileExtensionLimit",
    function (value, element, params) {
        var ext = element.files[0].type;
        if (!(ext === "image/png" || ext === "image/jpeg" || ext ==="image/jpg"))
            return false;
        else {
            return true;
        }
    });
jQuery.validator.unobtrusive.adapters.addBool("fileExtensionLimit");