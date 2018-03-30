var statsExpanded = false;
var ratedProductsExpanded = false;
var menuIsExpanded = false;
var reviewedProductsExpanded = false;

$(function () {
	$('.ratedProduct').on('click', function (e) {
		e.stopPropagation();
	});

	$('#dropdown-menu').on('click', function (e) {
		e.stopPropagation();
	});

	$("#login").on('click', function () {
		var username = document.getElementById("username").value;
		var password = document.getElementById("password").value;

		function validatePassword() {
			var pattern = new RegExp("[A-Za-z0-9]{8,20}");

			if (!pattern.test(password)) {
				document.getElementById("text-danger-password").innerHTML += "<h5 style='color: darkred;'>Invalid Password</h5>";
				$("#password").css({ "border": "2px solid darkred" });
			}
			else {
				document.getElementById("text-danger").innerHTML = "";
			}
			return false;
		}

		$.ajax({
			url: "/account/login",
			type: 'POST',
			data: { "username": username, "password": password },
			success: function (data) {
				location.reload(true);
			},
			error: function (data) {
				document.getElementById("text-danger-email").innerHTML = "<h5 style='color: red;'>Login Failed. Invalid Credentials!</h5>";
				$("#password").css({ "border": "2px solid darkred" });
				$("#username").css({ "border": "2px solid darkred" });

				return false;
			}
		});
		return false;
	});

	$("#logout").click(function () {
		$.ajax({
			url: "/Account/Logout",
			type: 'POST',
			success: function () {
				location.reload();
			}
		});
		return false;
	});
});
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
	if (document.getElementById("backToTop")) {
		if (document.body.scrollTop > 300 || document.documentElement.scrollTop > 300) {
			document.getElementById("backToTop").style.display = "block";
		} else {
			document.getElementById("backToTop").style.display = "none";
		}
	}
}

function backToTop() {
	//document.body.scrollTop = 0; // For Chrome, Safari and Opera 
	//document.documentElement.scrollTop = 0; // For IE and Firefox
	window.scroll({
		top: 0,
		behavior: 'smooth'
	});
}

$('.star1').css('color', 'black').prevAll('.star1').css('color', 'black');
$('.star1').hover(function () {
	$(this).css('color', 'gold').prevAll('.star1').css('color', 'gold');
}, function () {
	$(this).css('color', 'black').prevAll('.star1').css('color', 'black');
});

function showStats() {
	if (!statsExpanded) {
		$('.user-stats-wrapper').show(200);
	}
	else {
		$('.user-stats-wrapper').hide(200);
	}

	statsExpanded = !statsExpanded;
}

function expandRatedProducts() {
	if (!ratedProductsExpanded) {
		$('.user-stats-ratedProducts-container').slideDown(300);
		$('.ratedProducts-button .product-list-header').css('padding-bottom', '10px');
		$('.ratedProducts-button .glyphicon').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
	}
	else {
		$('.user-stats-ratedProducts-container').slideUp(300);
		$('.ratedProducts-button .product-list-header').css('padding-bottom', '0px');
		$('.ratedProducts-button .glyphicon').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
	}

	ratedProductsExpanded = !ratedProductsExpanded;
}

function expandReviewedProducts() {
	if (!reviewedProductsExpanded) {
		$('.user-stats-reviewedProducts-container').slideDown(300);
		$('.reviewedProduct-button .product-list-header').css('padding-bottom', '10px');
		$('.reviewedProduct-button .glyphicon').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
	}
	else {
		$('.user-stats-reviewedProducts-container').slideUp(300);
		$('.reviewedProduct-button .product-list-header').css('padding-bottom', '0px');
		$('.reviewedProduct-button .glyphicon').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
	}

	reviewedProductsExpanded = !reviewedProductsExpanded;
}

function openProduct(id) {
	location.href = '/product/details/' + id;
}

function expandMenu() {
	if (menuIsExpanded) {
		$('#navbar-wrap').fadeOut(200);
	}
	else {
		$('#navbar-wrap').fadeIn(200);
	}

	menuIsExpanded = !menuIsExpanded;
}