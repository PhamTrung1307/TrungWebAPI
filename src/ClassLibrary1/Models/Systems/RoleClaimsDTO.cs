using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Systems
{
    public class RoleClaimsDTO
    {
        public required string Type { get; set; }
        public required string Value { get; set; }
        public string? DisplayName { get; set; }
        public bool Selected { get; set; }
    }
}
