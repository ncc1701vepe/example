namespace Km56.MyStore.Application.Service
{
    public interface IItemService
    {
        Task<IEnumerable<Application.Dto.ItemDto>> GetAllAsync();
    }
}
