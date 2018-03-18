$("#addComment").click(function () {
	var content = document.getElementById("userComment").value;
	var userId = $("#data-userId").val();
	var userName = $("#data-userName").val();
	var productId = $("#data-productId").val();
	var datePosted = $("#data-datePosted").val();

	var comment = {

		Content: content,
		DatePosted: datePosted,
		UserId: userId,
		ProductID: productId
	};

	if (userId === "") {
		$('#logInToComment').slideDown(400).css('display', 'block');
		setTimeout(function () {
			$('#logInToComment').slideUp(600);
		}, 5000);
	}
	else if (content === "") {
		$("#userComment").css("border", "3px solid darkred");
		setTimeout(function () {
			$('#userComment').css("border", "none");
		}, 3000);
	}
	else {
		$("#noComments").css("display", "none");
		document.getElementById("addCommentResult").innerHTML = `
				<div style="padding: 5vh; background-color: #4a5878;">
					<strong class="pull-left primary-font" style="color: #8bfe74;">` + userName + `</strong>
					<br> 
					<small class="pull-right text-muted" style="color: white;">
						<span class="glyphicon glyphicon-time"></span>` + ' ' + datePosted + `
					</small>
					<li class="ui-state-default" style="padding-top: 20px;">` + content + `</li>
				</div>
				<br>` + document.getElementById("addCommentResult").innerHTML;
	}

	document.getElementById("userComment").value = "";
	$.ajax({
		url: "/Product/AddComment",
		type: 'POST',
		dataType: 'json',
		success: function (data) {
			return false;
		},
		data: comment
	});
	return false;
});

function deleteComment(obj) {
	var id = obj.getAttribute('data-commentId');

	$.ajax({
		url: '/Product/DeleteComment',
		type: 'POST',
		data: { "commentId": id },
		success: function () {
			location.reload();
		}
	});
	return false;
};