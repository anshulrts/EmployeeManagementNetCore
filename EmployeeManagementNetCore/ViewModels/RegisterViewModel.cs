﻿using EmployeeManagementNetCore.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementNetCore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomainAttribute(allowedDomain: "gmail.com",
            ErrorMessage = "Email domain must be gmail.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password",
            ErrorMessage = "Password and Confirm password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }

    }
}
