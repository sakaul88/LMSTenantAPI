namespace DeviceManager.Api.Services
{
    /// <summary>
    /// Device service interface
    /// </summary>
    public interface IUserLogDetailsService<T>
    {
        void logout(int fkEmployeeId);
    }
}