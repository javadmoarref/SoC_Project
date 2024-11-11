
const cookieName = "cart-items";
function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    }
    else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    }
    else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        }
        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_items_count").text(products.length);
    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product = `  <li class="aside-product-list-item">
                        <a class="remove" onclick="removeFromCart('${x.id}')">×</a>
                        <a href="product-details.html">
                            <img src="/uploads/${x.picture}" width="68" height="84" alt="Image">
                            <span class="product-title">${x.name}</span>
                        </a>
                        <span class="product-price">${x.count} × ${x.unitPrice} تومان</span>
                    </li>`;
        cartItemsWrapper.append(product);
    });
}


function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}



function changeCartItemCount(id,count) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    var productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count =count;
    var product = products[productIndex];
    /*const newPrice = parseInt(product.unitPrice) * parseInt(count);*/
    //$(`#${totalId}`).text(newPrice);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function disableButton() {
    document.getElementById("myButtonPay").disable = true;
    document.getElementById("myButtonTracking").disable = false;
}

function deleteCookie() { 
    localStorage.setItem('redirectToHome', 'true');
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $.cookie(cookieName, JSON.stringify(products), { expires: -2, path: "/" });
    //const indicesToRemove = products.map((product, index) => index);
    //for (let i = indicesToRemove.length - 1; i >= 0; i--) {
    //    const index = indicesToRemove[i];
    //    products.splice(index, 1);
    //}
    //$.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart(); 
}

document.addEventListener('DOMContentLoaded', function () {
    debugger;
    if (localStorage.getItem('redirectToHome') === 'true') {
        localStorage.removeItem('redirectToHome');
        window.location.href = '/Index';
    }
});




