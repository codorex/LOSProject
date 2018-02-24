$(function () {
	
});

$("#login").click(function () {
	var username = document.getElementById("username").value;
	var password = document.getElementById("password").value;

	function validatePassword() {
		var pattern = new RegExp("[A-Za-z0-9]{8,20}");

		if (!pattern.test(password)) {
			document.getElementById("text-danger").innerHTML += "<h5 style='color: darkred;'>Invalid Password</h5>";
			$("#password").css({ "border": "2px solid darkred" });
		}
		else {
			document.getElementById("text-danger").innerHTML = "";
		}
		return false;
	}

		$.ajax({
			url: "/Account/Login",
			type: 'POST',
			data: { "username": username, "password": password },
			success: function (data) {
				location.reload(true);
			},
			error: function (data) {
				document.getElementById("text-danger").innerHTML += "<h5 style='color: darkred;'>Invalid Password</h5>";
				$("#password").css({ "border": "2px solid darkred" });

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

window.onscroll = function () { scrollFunction() };

function scrollFunction() {
	if (document.body.scrollTop > 300 || document.documentElement.scrollTop > 300) {
		document.getElementById("backToTop").style.display = "block";
	} else {
		document.getElementById("backToTop").style.display = "none";
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