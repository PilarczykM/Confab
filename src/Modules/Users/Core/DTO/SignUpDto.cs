﻿using System.ComponentModel.DataAnnotations;

namespace Confab.Modules.Users.Core.DTO
{
    public class SignUpDto
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
        public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}

