namespace CMS.Web.Helper;


public static class MultipartRequestHelper
{
	public static bool IsMultipartContentType(string? contentType)
	{
		return !string.IsNullOrEmpty(contentType)
			   && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
	}
}
