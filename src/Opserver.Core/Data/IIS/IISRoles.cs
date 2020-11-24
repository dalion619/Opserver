namespace Opserver.Data.IIS
{
    public static class IISRoles
    {
        public const string Admin = nameof(IISModule)  + ":" + nameof(Admin);
        public const string Viewer = nameof(IISModule) + ":" + nameof(Viewer);
    }
}
