using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tranning_Apps_Management.Models.DB;

[Table("User_Infor")]
public partial class UserInfor
{
    [Key]
    [Column("UsID")]
    [StringLength(50)]
    [Unicode(false)]
    public string UsId { get; set; } = null!;

    [Column("FName")]
    [StringLength(50)]
    public string? Fname { get; set; }

    [Column("SName")]
    [StringLength(100)]
    public string? Sname { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PhoneNo { get; set; }

    [StringLength(100)]
    public string? Address1 { get; set; }

    [StringLength(100)]
    public string? Address2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Country { get; set; }
}
