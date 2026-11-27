using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampusServicePortal.Models
{
    [Table("CampusEvents")]
    public class CampusEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

        public DateTime? EventDate { get; set; }

        public string? Location { get; set; }
    }
}