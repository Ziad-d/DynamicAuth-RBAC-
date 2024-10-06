using DynamicAuth_RBAC_.Enums;

namespace DynamicAuth_RBAC_.DTOs.RoleFeatureDTOs
{
    public class FeaturesToRoleDTO
    {
        public List<Feature> Features { get; set; }
        public int RoleId { get; set; }
    }
}
