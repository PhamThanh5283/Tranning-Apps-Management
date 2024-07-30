using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tranning_Apps_Management.Models.DB;

[Table("UserSignup")]
public partial class UserSignup
{
    [Key]
    [Column("EmployeeID")]
    [StringLength(20)]
    [Unicode(false)]
    public string EmployeeId { get; set; } = null!;

    [StringLength(50)]
    public string? Fullname { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("DeptID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DeptId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Password { get; set; }
}
