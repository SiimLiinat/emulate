namespace WebApp.Models
{
    /// <summary>
    /// Error viewmodel
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Request ID for error
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Show request ID
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}