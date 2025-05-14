using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }

        //One-to-one with ArtistProfile
        public virtual ArtistProfile ArtistProfile { get; set; }


    }
}
