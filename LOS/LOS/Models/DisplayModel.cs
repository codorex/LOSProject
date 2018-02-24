﻿using LOS.Bussiness.Entities;
using LOS.Bussiness.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOS.Models
{
	public class DisplayModel
	{
		public PagedList<Product> ProductsPaged { get; set; }
		public int PageNumber { get; set; }
		public int PageCount { get; set; }
		public Category Category { get; set; }
		public List<SelectListItem> SortByDropdown { get; set; }
		public string keyword { get; set; }
		public string sortBy { get; set; }
		public string sortMethod { get; set; }
		public string sort { get; set; }
		public int CartItemCount { get; set; }
		public List<Image> ProductsImages { get; set; }

		public DisplayModel()
		{
			SortByDropdown = new List<SelectListItem>
			{
				new SelectListItem{ Text = "Name", Value = "name" },
				new SelectListItem{ Text = "Price", Value = "price" },
				new SelectListItem{ Text = "Date", Value = "date" }
			};
		}
	}
}