using IGeekFan.FreeKit.Extras.AuditEntity;

namespace CMS.Web.Service.Cms.Files;

public class FileDto : Entity<long>
{
	public string Key { get; set; }
	public string Path { get; set; }
	public string Url { get; set; }
}