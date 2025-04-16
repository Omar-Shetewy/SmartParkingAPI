namespace SmartParking.API.Services.Interface;

public interface IAiService
{
    public Task<bool> ValidatePlateNumber(string plateNumber);
    public Task<string> SendToAIModel(byte[] imageBytes);
    //public Task SavePlateData(byte[] imageBytes, string plateNumber);
}
