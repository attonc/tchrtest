using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TcHrTest.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
        public string Name { get; set; }
        public string SIN { get; set; }
        public double Salary { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int Seniority
        {
            get
            {
                if (EnrollmentDate != DateTime.MinValue)
                {
                    return (int)DateTime.Now.Subtract(EnrollmentDate).Days / 365;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public Range SalaryRange { get; set; }
    }

    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Range
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
    }
}
