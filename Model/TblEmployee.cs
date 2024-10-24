using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebAPI.Model
{
    public class TblEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and be exactly 10 digits.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int EmpAge { get; set; }

        [Required]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid Gender value.")]
        public Gender Gender { get; set; }

        [Required]
        public bool IsMarried { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Designation")]
        public int? DesId { get; set; }
        public TblDesignation? Designation { get; set; }

    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }

   
}