namespace SmartParking.API.Services.Implementation;
public class ANBRService : IANBRService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;
    public ANBRService(HttpClient httpClient, ApplicationDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<string> SendToAIModel(byte[] imageBytes)
    {
        var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(imageBytes), "image", "plate.jpg" }
        };

        var response = await _httpClient.PostAsync(" https://cyan-otters-work.loca.lt/detect", content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> ValidatePlateNumber(string plateNumber)
    {
        var user = await _context.Cars
        .Where(c => c.PlateNumber == plateNumber)
        .Select(c => c.UserId)
        .FirstOrDefaultAsync();

        if (user == 0) return false; 

        return await _context.ReservationRecords.AnyAsync(r => r.UserId == user);

        //return await _context.Cars.AnyAsync(r => r.PlateNumber == plateNumber);
    }


    //public async Task SavePlateData(byte[] imageBytes, string plateNumber)
    //{
    //    var plateRecord = new PlateRecord
    //    {
    //        PlateNumber = plateNumber,
    //        Image = imageBytes
    //    };
    //    _context.PlateRecords.Add(plateRecord);
    //    await _context.SaveChangesAsync();
    //}
}
