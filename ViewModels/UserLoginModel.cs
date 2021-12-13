namespace DeviceManager.Api.ViewModels
{
    public class UserLoginModel
    {
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public int TenantId { get; set; }
    }
}
