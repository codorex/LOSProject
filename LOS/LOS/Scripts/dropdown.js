$(function () {
	$('.dropdown-menu').css("visibility", "visible");
	$(".dropdown").hover(		
		function () {
			$('.dropdown-menu', this).stop(true, true).slideDown(200);
			$(this).toggleClass('open');
		}
		,
		function () {
			$('.dropdown-menu', this).stop(true, true).slideUp(200);
			$(this).toggleClass('open');
		});
});