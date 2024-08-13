
namespace BaseLibrary.DTOs
{
    public class CreateChairDTO
    {
        public string Number { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
