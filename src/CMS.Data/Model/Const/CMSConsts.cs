namespace CMS.Data.Model.Const
{
    public static class CMSConsts
    {
        public static class Group
        {
            public const int Admin = 1;
            public const int CmsAdmin = 2;
            public const int User = 3;
        }

        public static class Claims
        {
            public const string Bio = "urn:github:bio";
            public const string AvatarUrl = "urn:github:avatar_url";
            public const string HtmlUrl = "urn:github:html_url";
            public const string BlogAddress = "urn:github:blog";
        }

    }
}
