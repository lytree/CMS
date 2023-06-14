﻿using Owl.reCAPTCHA;

namespace CMS.Web.Config;

public class GooglereCAPTCHAConfig : reCAPTCHAOptions
{
    public static string RecaptchaSettings = "RecaptchaSettings";

    public string HeaderKey { get; set; } = "Google-RecaptchaToken";
    public bool Enabled { get; set; } = false;
    public string Version { get; set; } = reCAPTCHAConsts.V3;
    public float MinimumScore { get; set; } = 0.9F;
}