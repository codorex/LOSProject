var clicked = false;
$('#expandCategories').click(
	function () {
		if (!clicked) {
			$('#categoriesFooter').slideDown(400);
			clicked = true;
		}
		else {
			$('#categoriesFooter').slideUp(400);
			clicked = false;
		}
	});