using System;

namespace CMS.Data.Model.Entities.User;

public class LoginCaptcha
{
    public LoginCaptcha()
    {
    }

    public LoginCaptcha(string captcha, long expired)
    {
        Captcha = captcha ?? throw new ArgumentNullException(nameof(captcha));
        Expired = expired;
    }

    public string Captcha { get; set; }
    public long Expired { get; set; }
}
