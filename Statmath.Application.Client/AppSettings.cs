namespace Statmath.Application.Client
{
    /// <summary>
    /// Model of the client application settings
    /// </summary>
    public class AppSettings
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
    }
}