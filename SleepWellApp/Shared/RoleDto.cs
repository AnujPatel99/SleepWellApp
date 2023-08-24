using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPCTech2023FavoriteMovie.Shared;

public class RoleDto
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}