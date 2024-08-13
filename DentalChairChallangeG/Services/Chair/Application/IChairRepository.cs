using BaseLibrary.DTOs;
using BaseLibrary.Entities;

namespace DentalChairChallangeG.Services
{
    public interface IChairRepository
    {
        Task<Chair?> GetChairById(int id);
        Task<IEnumerable<Chair>> GetAllChairs();
        Task<Chair> CreateChair(Chair chair);
        Task<Chair> UpdateChair(Chair chair);
        Task DeleteChair(int id);
        Task<IEnumerable<ChairAllocationOutputDTO>> AllocateChairs(ChairAllocationInputDTO allocationInput);
    }
}


