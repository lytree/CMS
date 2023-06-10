﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Repository;
using CMS.Web.Data;
using CMS.Web.Service;
using CMS.Web.Service.Blog.Classifys;


namespace CMS.Web.Service.Blog.Classifies;

public class ClassifyService : CrudAppService<Classify, ClassifyDto, ClassifyDto, long, ClassifySearchDto, CreateUpdateClassifyDto, CreateUpdateClassifyDto>, IClassifyService
{
	private readonly IFileRepository _fileRepository;

	public ClassifyService(IAuditBaseRepository<Classify, long> repository, IFileRepository fileRepository) : base(repository)
	{
		_fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
	}

	public override async Task<PagedResultDto<ClassifyDto>> GetListAsync(ClassifySearchDto input)
	{
		List<ClassifyDto> classify = (await Repository.Select
				.WhereIf(input.ClassifyName.IsNotNullOrEmpty(), r => r.ClassifyName.Contains(input.ClassifyName))
				.OrderByDescending(r => r.CreateTime)
				.ToPagerListAsync(input, out long totalCount))
			.Select(r =>
			{
				ClassifyDto classifyDto = Mapper.Map<ClassifyDto>(r);
				classifyDto.ThumbnailDisplay = _fileRepository.GetFileUrl(classifyDto.Thumbnail);
				return classifyDto;
			}).ToList();

		return new PagedResultDto<ClassifyDto>(classify, totalCount);
	}

	public List<ClassifyDto> GetListByUserId(long? userId)
	{
		if (!userId.HasValue)
		{
			userId = CurrentUser.FindUserId();
		}

		List<ClassifyDto> classify = Repository.Select
			.Where(r => r.CreateUserId == userId)
			.OrderByDescending(r => r.SortCode)
			.ToList()
			.Select(r =>
			{
				ClassifyDto classifyDto = Mapper.Map<ClassifyDto>(r);
				classifyDto.ThumbnailDisplay = _fileRepository.GetFileUrl(classifyDto.Thumbnail);
				return classifyDto;
			}).ToList();

		return classify;
	}

	public override async Task<ClassifyDto> GetAsync(long id)
	{
		Classify classify = await Repository.Select.Where(a => a.Id == id).ToOneAsync();
		ClassifyDto classifyDto = Mapper.Map<ClassifyDto>(classify);
		classifyDto.ThumbnailDisplay = _fileRepository.GetFileUrl(classifyDto.Thumbnail);
		return classifyDto;
	}

	public override async Task<ClassifyDto> CreateAsync(CreateUpdateClassifyDto createClassify)
	{
		bool exist = await Repository.Select.AnyAsync(r =>
			r.ClassifyName == createClassify.ClassifyName && r.CreateUserId == CurrentUser.FindUserId());
		if (exist)
		{
			throw new CMSException($"分类专栏[{createClassify.ClassifyName}]已存在");
		}

		Classify classify = Mapper.Map<Classify>(createClassify);
		await Repository.InsertAsync(classify);
		return Mapper.Map<ClassifyDto>(classify);
	}

	public override async Task<ClassifyDto> UpdateAsync(long id, CreateUpdateClassifyDto updateInput)
	{
		Classify classify = await Repository.Select.Where(r => r.Id == id).ToOneAsync();
		if (classify == null)
		{
			throw new CMSException("该数据不存在");
		}

		if (classify.CreateUserId != CurrentUser.FindUserId())
		{
			throw new CMSException("您无权编辑他人的分类专栏");
		}

		bool exist = await Repository.Select.AnyAsync(r => r.ClassifyName == updateInput.ClassifyName && r.Id != id && r.CreateUserId == CurrentUser.FindUserId());
		if (exist)
		{
			throw new CMSException($"分类专栏[{updateInput.ClassifyName}]已存在");
		}

		Mapper.Map(updateInput, classify);

		await Repository.UpdateAsync(classify);
		return Mapper.Map<ClassifyDto>(classify);
	}

	public override async Task DeleteAsync(long id)
	{
		Classify classify = await Repository.Select.Where(a => a.Id == id).ToOneAsync();
		if (classify.CreateUserId != CurrentUser.FindUserId())
		{
			throw new CMSException("您无权删除他人的分类专栏");
		}

		await Repository.DeleteAsync(new Classify { Id = id });
	}

	public async Task UpdateArticleCountAsync(long? id, int inCreaseCount)
	{
		if (id == null)
		{
			return;
		}
		Classify classify = await Repository.Select.Where(r => r.Id == id).ToOneAsync();
		classify.UpdateArticleCount(inCreaseCount);
		await Repository.UpdateAsync(classify);
	}
}