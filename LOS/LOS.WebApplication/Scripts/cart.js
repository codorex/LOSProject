function checkout() {
	location.reload();
	$('#totalCost').css('display', 'block');
}

var timesRemoved = 0;
var updatedCost = 0;

function removeFromCart(obj) {
	var id = obj.getAttribute('data-productId');
	var cartItemsCount = obj.getAttribute('data-cartItemsCount');
	var price = obj.getAttribute('data-price');
	timesRemoved++;
	updatedCost = parseInt(document.getElementById('totalCost').innerHTML) - price;

	cartItemsCount = cartItemsCount - 1;
	updatedCartCount = (cartItemsCount + 1) - timesRemoved;

	document.getElementById("cart").innerHTML = '<a href="/Cart/ManageCart" id="navbar-button" class="cart">Cart (' + updatedCartCount + ') <span class="glyphicon glyphicon-shopping-cart"></span></a>';
	document.getElementById('totalCost').innerHTML = updatedCost + ".00";
	var row = obj.parentElement.parentElement;

	$.ajax({
		url: '/Cart/Remove',
		type: 'POST',
		data: { "productId": id },
		success: function () {
			$(row).css('pointer-events', 'none');
			$(row).fadeOut(500);
		}
	});

	return false;
}

function clearCart() {
	if (confirm('Remove all products from cart?')) {
		$.ajax({
			url: '/Cart/Clear',
			type: 'POST',
			success: function () {
				location.reload();
			}
		});
		return false;
	}
}