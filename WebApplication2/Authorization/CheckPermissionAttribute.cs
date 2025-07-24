using WebApplication2.Data;

namespace WebApplication2.Authorization
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]   
    
    public class CheckPermissionAttribute:Attribute
    {

        public CheckPermissionAttribute(Permission permission)
        {
            this.Permission = permission;
        }

        public Permission Permission { get; }
    }
}
