using DynamicAuth_RBAC_.Enums;

namespace DynamicAuth_RBAC_.Models
{
    public class RoleFeature : BaseModel
    {
        public int RoleID { get; set; }
        public Role Role { get; set; }

        public Feature Feature { get; set; }
    }
}
