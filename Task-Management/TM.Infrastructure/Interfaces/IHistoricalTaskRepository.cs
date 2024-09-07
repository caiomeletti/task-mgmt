using TM.Domain.Entities;

namespace TM.Infrastructure.Interfaces
{
    public interface IHistoricalTaskRepository
    {
        Task<HistoricalTask> CreateAsync(HistoricalTask historicalTask);
    }
}
