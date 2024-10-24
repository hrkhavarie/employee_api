using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoWebAPI.Model
{
    public class TblDesignation
    {
        [Key]
        public int DesId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [StringLength(100, ErrorMessage = "Department name cannot be longer than 100 characters.")]
        public string Department { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public double? Salary { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Required]
        //public int EmpId { get; set; }


        public ICollection<TblEmployee> Employees { get; set; } = new List<TblEmployee>();

    }
}
