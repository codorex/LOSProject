$('#submit').click(function () {
	var patternPass = new RegExp("[A-Za-z0-9]{8,20}");
	var patternUsername = new RegExp("[A-Za-z0-9_]{6,20}");
	var password = document.getElementById("newPassword").value;
	var username = document.getElementById("usernameNew").value;
	if (!$('#username').val() === '') {
		$('#errors').addClass('alert alert-danger');
		$('#errors').text('You have not filled the fields');
		return false;
	}
	else if (!patternUsername.test(username)) {
		$('#errors').addClass('alert alert-danger');
		$('#errors').text('Username must contain at least 6 characters!');
		return false;
	}
    else if (!$('#firstname').val() === '' || $.trim($('#firstname').val()) === '') {
        $('#errors').addClass('alert alert-danger');
        $('#errors').text('You have not filled the fields');
        return false;
    }
    else if (!$('#lastname').val() === '') {
        $('#errors').addClass('alert alert-danger');
        $('#errors').text('You have not filled the fields');
        return false;
	}
    else if (!$('#email').val() === '') {
        $('#errors').addClass('alert alert-danger');
        $('#errors').text('You have not filled the fields');
        return false;
	}
	else if (!patternPass.test(password)) {
		$('#errors').addClass('alert alert-danger');
		$('#errors').text('Password must contain between 8 and 20 characters (latin letters and digits only)');
		return false;
	}
	else if (confirmPassword() === false) {
        $('#confirmpassword').val('');
        $('#errors').addClass('alert alert-danger');
        $('#errors').text('Check if passwords match or are empty!');
        return false;
    }
});

function confirmPassword() {
	var password = $('#newPassword').val();
	var confirmPassword = $('#confirmpassword').val();
	var match = (password === confirmPassword);

    return match;
}



/// BELOW IS THE LOGIN VALIDATION | ABOVE - REGISTRATION VALIDATION



function validateEmail(email) {
	var pattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	if (!pattern.test(email)) {
		$("#email").css({ "border": "2px solid darkred" });
	}
	else {
		document.getElementsByClassName("text-danger").innerHTML = "";
		return true;
	}
	return false;
}

function validatePassword(password) {
	var pattern = new RegExp("[A-Za-z0-9]{8,20}");

	if (!pattern.test(password)) {
		$("#password").css({ "border": "2px solid darkred" });
	}
	else {
		return true;
	}
	return false;
}

$("#login").on('click', function () {
	var password = document.getElementById("password").value;

	if (validatePassword(password) === false) {
		document.getElementById("text-danger-password").innerHTML = "<h5 style='color: darkred;'>Invalid Password</h5>";
	}
	else {
		$("#password").css({ "border": "1px solid lightgrey" });
		document.getElementById("text-danger-password").innerHTML = "";
	}

	if (validatePassword(password)) {
		return true;
	}
	return false;
});

$("#changePasswordBtn").click(function () {
	var newPassword = document.getElementById("newPass").value;
	var newPasswordConfirm = document.getElementById("newPassConfirm").value;
	var oldPassword = document.getElementById("oldPass").value;

	if (newPassword !== newPasswordConfirm) {
		$("#text-danger-password").css("display","block");
		document.getElementById("text-danger-password").innerHTML = "<div class='alert alert-danger' style='display: block; margin-top: 0px; margin-left: 10px; width: 97%;'><h5 style='border-radius: 5px; text-align:center;'>Invalid Password</h5></div>";
		
		document.getElementById("newPass").value = "";
		document.getElementById("newPassConfirm").value = "";
		document.getElementById("oldPass").value = "";
		return false;
	}
	else {
		return true;
	}
});