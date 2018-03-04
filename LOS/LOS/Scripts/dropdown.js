var accountDropdownExpanded = false;

function expandAccountDropdown() {
	if (!accountDropdownExpanded) {
		$('.dropdown-menu').show(300);
	}
	else {
		$('.dropdown-menu').hide(300);
	}

	accountDropdownExpanded = !accountDropdownExpanded;
}