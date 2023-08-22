namespace Km56.MyStore.Application.Service
{
    public class ItemService : IItemService
    {
        private readonly Domain.Service.IItemRepository _itemRepository;
        private readonly Infrastructure.Common.ObjectMapper _objectMapper;

        public ItemService(Domain.Service.IItemRepository itemRepository)
        { 
            _itemRepository = itemRepository;
            _objectMapper = new Infrastructure.Common.ObjectMapper();
        }

        public async Task<IEnumerable<Dto.ItemDto>> GetAllAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return items.Select(i => _objectMapper.Map<Domain.Entity.Item, Dto.ItemDto>(i));
        }
    }
}
