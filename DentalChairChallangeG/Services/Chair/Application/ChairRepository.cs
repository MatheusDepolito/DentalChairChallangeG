
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;

namespace DentalChairChallangeG.Services
{
    public class ChairRepository(AppDbContext context) : IChairRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Chair> CreateChair(Chair chair)
        {
            _context.Chair.Add(chair);
            await _context.SaveChangesAsync();
            return chair;
        }

        public async Task DeleteChair(int id)
        {
            var chair = await GetChairById(id);
            if (chair != null)
            {
                _context.Chair.Remove(chair);
                await _context.SaveChangesAsync();
            } else
            {
                throw new KeyNotFoundException("Chair not found");
            }
        }

        public async Task<IEnumerable<Chair>> GetAllChairs()
        {
            return await _context.Chair.ToListAsync();
        }

        public async Task<Chair?> GetChairById(int id)
        {
            return await _context.Chair.FindAsync(id);
        }

        public async Task<Chair> UpdateChair(Chair chair)
        {
            var existingChair = await GetChairById(chair.Id) ?? throw new KeyNotFoundException("Chair not found");
            existingChair.Number = chair.Number;
            existingChair.Description = chair.Description;
            existingChair.IsAvailable = chair.IsAvailable;

            _context.Chair.Update(existingChair);
            await _context.SaveChangesAsync();
            return existingChair;
        }
        public async Task<IEnumerable<ChairAllocationOutputDTO>> AllocateChairs(ChairAllocationInputDTO allocationInput)
        {
            var startTime = allocationInput.StartTime;
            var endTime = allocationInput.EndTime;
            var intervalMinutes = allocationInput.IntervalMinutes;

            if (startTime >= endTime || intervalMinutes <= 0)
            {
                throw new ArgumentException("Invalid time range or interval.");
            }

            // Obtém todas as cadeiras disponíveis
            var chairs = await _context.Chair.Where(c => c.IsAvailable).ToListAsync();

            if (!chairs.Any())
            {
                throw new Exception("No available chairs.");
            }

            // Calcula o número total de intervalos
            var totalIntervals = (int)((endTime - startTime).TotalMinutes / intervalMinutes);

            if (totalIntervals == 0)
            {
                throw new ArgumentException("Time range is too short for the given interval.");
            }

            // Inicializa a lista de alocações
            var chairAllocations = new List<ChairAllocationOutputDTO>();

            // Fila de cadeiras para alocação rotativa
            var chairQueue = new Queue<Chair>(chairs);

            // Realiza a alocação para cada intervalo
            for (int i = 0; i < totalIntervals; i++)
            {
                var currentStartTime = startTime.AddMinutes(i * intervalMinutes);
                var currentEndTime = currentStartTime.AddMinutes(intervalMinutes);

                // Se todas as cadeiras já foram alocadas, reinicie a fila
                if (chairQueue.Count == 0)
                {
                    chairQueue = new Queue<Chair>(chairs);
                }

                // Aloca a próxima cadeira disponível
                var allocatedChairs = new List<Chair> { chairQueue.Dequeue() };

                chairAllocations.Add(new ChairAllocationOutputDTO
                {
                    StartTime = currentStartTime,
                    EndTime = currentEndTime,
                    Chairs = allocatedChairs
                });
            }

            return chairAllocations;
        }



    }
}
