using AutoMapper;
using CMS.Model.DTO;
using CMS.Model.Entity;
using CMS.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CMS.Model.Profiles
{
	public class LinkProfiles : Profile
	{
		public LinkProfiles()
		{
			// 这里就不赘述了
			base.CreateMap<Link, LinkDto>();
			base.CreateMap<LinkParam, LinkDto>();
			base.CreateMap<LinkParam, Link>();
			base.CreateMap<LinkDto, Link>();
		}
	}
}
