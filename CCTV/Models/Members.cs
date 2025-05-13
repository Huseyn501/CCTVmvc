using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CCTV.Models
{
    public class Members
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string? ImageUrl { get; set; }
    }
}
