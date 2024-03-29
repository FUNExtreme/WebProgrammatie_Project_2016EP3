﻿using System;
using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Database.Entities
{
    public class DbUser
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
}
