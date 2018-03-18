
var accountDropdownExpanded = false;

function expandAccountDropdown() {
	if (!accountDropdownExpanded) {
		$('#signin-dropdown .dropdown-menu').show(300);
	}
	else {
		$('#signin-dropdown .dropdown-menu').hide(300);
	}

	accountDropdownExpanded = !accountDropdownExpanded;
}

$(function () {
	$('#categories-dropdown .dropdown-menu').css("visibility", "visible");
	$("#categories-dropdown .dropdown").hover(		
		function () {
			$('#categories-dropdown .dropdown-menu', this).stop(true, true).slideDown(200);
			$(this).toggleClass('open');
		}
		,
		function () {
			$('#categories-dropdown .dropdown-menu', this).stop(true, true).slideUp(200);
			$(this).toggleClass('open');
		});
});