namespace CMS.Web.Tools.Captcha.Dto;

/// <summary>
/// 滑动验证
/// </summary>
public class SlideJigsawCaptchaDto
{
	/// <summary>
	/// 滑块图
	/// </summary>
	public string BlockImage { get; set; }

	/// <summary>
	/// 底图
	/// </summary>
	public string BaseImage { get; set; }
}
