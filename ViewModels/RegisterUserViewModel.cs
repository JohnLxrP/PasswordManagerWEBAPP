﻿using System.ComponentModel.DataAnnotations;

namespace PasswordManagerWEBAPP.ViewModels
{
    public class RegisterUserViewModel
    {
        // view validations
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Invalid name format. Only alphabets, spaces, hyphens, and apostrophes are allowed.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Invalid name format. Only alphabets, spaces, hyphens, and apostrophes are allowed.")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and confirm password doesnt match")]
        public string  ConfirmPassword { get; set; }
    }
}
