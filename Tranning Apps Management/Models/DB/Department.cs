using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tranning_Apps_Management.Models.DB;

[Table("Department")]
public partial class Department
{
    public int Id { get; set; }

    [Key]
    [Column("DeptID")]
    [StringLength(50)]
    [Unicode(false)]
    public string DeptId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Deptname { get; set; }

    [Column("VDeptname")]
    [StringLength(50)]
    public string? Vdeptname { get; set; }
}
