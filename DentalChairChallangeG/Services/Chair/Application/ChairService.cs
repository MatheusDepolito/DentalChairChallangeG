using BaseLibrary.DTOs;
using BaseLibrary.Entities;

namespace DentalChairChallangeG.Services
{
    public class ChairService(IChairRepository chairRepository)
    {
        public async Task<Chair> CreateChair(CreateChairDTO createChairDTO)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(createChairDTO.Number))
            {
                throw new ArgumentException("Chair number is required.");
            }

            // Cria a entidade Chair
            var chair = new Chair
            {
                Number = createChairDTO.Number,
                Description = createChairDTO.Description,
                IsAvailable = createChairDTO.IsAvailable
            };

            // Usa o repositório para salvar a cadeira
            return await chairRepository.CreateChair(chair);
        }

        public async Task<IEnumerable<Chair>> GetAllChairs()
        {
            return await chairRepository.GetAllChairs();
        }

        public async Task<Chair?> GetChairById(int id)
        {
            Chair? chair = await chairRepository.GetChairById(id);

            if (chair != null)
            {
                return chair;
            } else
            {
                throw new Exception("Chair not found");
            }
        }

        public async Task<Chair> UpdateChair(Chair chair)
        {
            return await chairRepository.UpdateChair(chair);
        }

        public async Task DeleteChair(int id)
        {
            await chairRepository.DeleteChair(id);
        }
        public async Task<IEnumerable<ChairAllocationOutputDTO>> AllocateChairs(ChairAllocationInputDTO allocationInput)
        {
            // Valida os parâmetros
            if (allocationInput.StartTime >= allocationInput.EndTime || allocationInput.IntervalMinutes <= 0)
            {
                throw new ArgumentException("Invalid time range or interval.");
            }

            // Chama o método do repositório para alocação de cadeiras
            return await chairRepository.AllocateChairs(allocationInput);
        }
    }
}
