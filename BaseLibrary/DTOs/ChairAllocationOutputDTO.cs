using BaseLibrary.Entities;

namespace BaseLibrary.DTOs
{
    public class ChairAllocationOutputDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Chair> Chairs { get; set; } = [];
    }
}
