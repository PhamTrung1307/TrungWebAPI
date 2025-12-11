namespace Core.Models.Systems
{
    public class PermissionDTO
    {
        public string RoleId { get; set; }
        public IList<RoleClaimsDTO> RoleClaims { get; set; }
    }
}
