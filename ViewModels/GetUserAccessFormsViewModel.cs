namespace DeviceManager.Api.Data.Views
{
    public partial class GetUserAccessFormsViewModel
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public int? SubMenuId { get; set; }
        public string Name { get; set; }
        public string ModuleName { get; set; }
        public string Url { get; set; }
        public bool? IsActive { get; set; }
        public int? FkProfileId { get; set; }
        public string Icon { get; set; }
    }
}
