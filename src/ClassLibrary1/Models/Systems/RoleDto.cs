using Core.Domain.Identity;

namespace Core.Models.Systems
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public class autoMapperProfiles : AutoMapper.Profile
        {
            public autoMapperProfiles()
            {
                CreateMap<AppRole, RoleDTO>();
            }
        }
    }
}
