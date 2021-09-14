using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Navigation pages management
    /// </summary>
    public static class ManageNavPages
    {
        /// <summary>
        /// Index
        /// </summary>
        public static string Index => "Index";

        /// <summary>
        /// Email
        /// </summary>
        public static string Email => "Email";

        /// <summary>
        /// ChangePassword
        /// </summary>
        public static string ChangePassword => "ChangePassword";

        /// <summary>
        /// DownloadPersonalData
        /// </summary>
        public static string DownloadPersonalData => "DownloadPersonalData";

        /// <summary>
        /// DeletePersonalData
        /// </summary>
        public static string DeletePersonalData => "DeletePersonalData";

        /// <summary>
        /// ExternalLogins
        /// </summary>
        public static string ExternalLogins => "ExternalLogins";

        /// <summary>
        /// PersonalData
        /// </summary>
        public static string PersonalData => "PersonalData";

        /// <summary>
        /// TwoFactorAuthentication
        /// </summary>
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        /// <summary>
        /// Nav class for Index
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        /// <summary>
        /// Nav class for Email
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);
        
        /// <summary>
        /// Nav class for ChangePassword
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);
        
        /// <summary>
        /// Nav class for DownloadPersonalData
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);
        
        /// <summary>
        /// Nav class for DeletePersonalData
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);
        
        /// <summary>
        /// Nav class for ExternalLogins
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);
        
        /// <summary>
        /// Nav class for PersonalData
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);
        
        /// <summary>
        /// Nav class for TwoFactorAuthentication
        /// </summary>
        /// <param name="viewContext">ViewContext - Context for view execution</param>
        /// <returns>PageNavClass</returns>
        public static string? TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
