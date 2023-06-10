using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities;
using CMS.Data.Repository;

namespace CMS.Web.Service.Base.BaseItems;

public class BaseItemService : ApplicationService, IBaseItemService
{
	private readonly IAuditBaseRepository<BaseItem,long> _baseItemRepository;
	private readonly IAuditBaseRepository<BaseType,long> _baseTypeRepository;

	public BaseItemService(IAuditBaseRepository<BaseItem,long> baseItemRepository, IAuditBaseRepository<BaseType,long> baseTypeRepository)
	{
		_baseItemRepository = baseItemRepository;
		_baseTypeRepository = baseTypeRepository;
	}

	public async Task DeleteAsync(int id)
	{
		await _baseItemRepository.DeleteAsync(new BaseItem { Id = id });
	}

	public async Task<List<BaseItemDto>> GetListAsync(string typeCode)
	{
		long baseTypeId = _baseTypeRepository.Select.Where(r => r.TypeCode == typeCode).ToOne(r => r.Id);

		List<BaseItemDto> baseItems = (await _baseItemRepository.Select
				.OrderBy(r => r.SortCode)
				.OrderBy(r => r.Id)
				.Where(r => r.BaseTypeId == baseTypeId)
				.ToListAsync())
			.Select(r => Mapper.Map<BaseItemDto>(r)).ToList();

		return baseItems;
	}

	public async Task<BaseItemDto> GetAsync(int id)
	{
		BaseItem baseItem = await _baseItemRepository.Select.Where(a => a.Id == id).ToOneAsync();
		return Mapper.Map<BaseItemDto>(baseItem);
	}

	public async Task CreateAsync(CreateUpdateBaseItemDto createBaseItem)
	{
		bool exist = await _baseItemRepository.Select.AnyAsync(r =>
			r.BaseTypeId == createBaseItem.BaseTypeId && r.ItemCode == createBaseItem.ItemCode);
		if (exist)
		{
			throw new CMSException($"编码[{createBaseItem.ItemCode}]已存在");
		}

		BaseItem baseItem = Mapper.Map<BaseItem>(createBaseItem);
		await _baseItemRepository.InsertAsync(baseItem);
	}

	public async Task UpdateAsync(int id, CreateUpdateBaseItemDto updateBaseItem)
	{
		BaseItem baseItem = await _baseItemRepository.Select.Where(r => r.Id == id).ToOneAsync();
		if (baseItem == null)
		{
			throw new CMSException("该数据不存在");
		}

		bool typeExist = await _baseTypeRepository.Select.AnyAsync(r => r.Id == updateBaseItem.BaseTypeId);
		if (!typeExist)
		{
			throw new CMSException("请选择正确的类别");
		}

		bool exist = await _baseItemRepository.Select.AnyAsync(r =>
			r.BaseTypeId == updateBaseItem.BaseTypeId && r.ItemCode == updateBaseItem.ItemCode && r.Id != id);

		if (exist)
		{
			throw new CMSException($"编码[{updateBaseItem.ItemCode}]已存在");
		}

		Mapper.Map(updateBaseItem, destination: baseItem);
		await _baseItemRepository.UpdateAsync(baseItem);
	}
}