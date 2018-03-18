var rated = false;

function rate(obj) {
	var productId = obj.getAttribute('data-productId');
	var rating = obj.getAttribute('data-rating');
	var voteCount = obj.getAttribute('data-voteCount');

	$.ajax({
		url: "/Product/Rate",
		type: 'POST',
		data: { "productId": productId, "rating": rating },
		success: function () {
			document.getElementById('votes').innerHTML = ++voteCount + ' votes';
			document.getElementById('ratingBox').innerHTML = '<h4 style="margin-bottom: 30px;">Thank you for your vote!</h4>';
			return false;
		}
	});
	return false;
};

var updatedCount = 0;
var updatedQuantity = 0;

function addToCart(obj) {
	var id = obj.getAttribute('data-productId');
	var cartItemCount = Number.parseInt(obj.getAttribute('data-cartItemCount'));
	var quantity = obj.getAttribute('data-quantity');

	cartItemCount += 1;

	if (updatedCount < cartItemCount) {
		updatedCount = cartItemCount;
	}
	else {
		updatedCount++;
	}

	document.getElementById("cart").innerHTML = '<a href="/Cart/ManageCart" id="navbar-button" class="cart">Cart (' + updatedCount + ') <span class="glyphicon glyphicon-shopping-cart"></span></a>';

	if (updatedQuantity == 0) {
		updatedQuantity = quantity;
	}
	else if (updatedQuantity > 1) {
		updatedQuantity -= 1;
	}

	if (updatedQuantity == 1) {
		updatedQuantity == 0;
		document.getElementById('addToCartBox').innerHTML =
			`
		<div class="action">
			<button class="btn btn-danger" data-cartItemCount="@cartItems" data-productId="@Model.ProductID" onclick="addToCart(this);" type="button" style="pointer-events: none;  border: none;">Out of Stock</button>
		</div>
		`;
	}

	if (updatedQuantity > 0) {
		$.ajax({
			url: '/Cart/Add',
			type: 'POST',
			data: { "productId": id },
			success: function () {
				$('#notification').slideDown(400).css('display', 'block');
				setTimeout(function () {
					$('#notification').slideUp(600);
				}, 4000);

				return false;
			},
			error: function () {
				$('#notification-error').slideDown(400).css('display', 'block');
				setTimeout(function () {
					$('#notification-error').slideUp(600);
				}, 4000);
				return false;
			}
		});
	}

	return false;
}
