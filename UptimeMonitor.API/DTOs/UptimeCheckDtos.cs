namespace UptimeMonitor.API.DTOs
{
    public class UptimeCheckCreateDto
    {
        public string Name { get; set; } = null!;
        public int ServiceSystemId { get; set; }
        public int ComponentId { get; set; }
        public string CheckUrl { get; set; } = null!;
        public int CheckInterval { get; set; }
        public int CheckTimeout { get; set; }
        public string? RequestHeaders { get; set; }
        public int DownAlertDelay { get; set; }
        public int DownAlertResend { get; set; }
        public string? DownAlertMessage { get; set; }
        public string AlertEmail { get; set; } = null!;
    }

    public class UptimeCheckResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ServiceSystemId { get; set; }
        public int ComponentId { get; set; }
        public string CheckUrl { get; set; } = null!;
        public int CheckInterval { get; set; }
        public int CheckTimeout { get; set; }
        public string? RequestHeaders { get; set; }
        public int DownAlertDelay { get; set; }
        public int DownAlertResend { get; set; }
        public string? DownAlertMessage { get; set; }
        public string AlertEmail { get; set; } = null!;
    }
}