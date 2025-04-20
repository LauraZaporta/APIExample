using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}