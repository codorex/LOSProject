﻿using LOS.CategoryModel.Domain;
using LOS.ProducModel.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LOS.WebApplication.Models
{
    public class NavbarModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Username { get; set; }

        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Category Category { get; set; }
        public int CartItemCount { get; set; }
    }
}