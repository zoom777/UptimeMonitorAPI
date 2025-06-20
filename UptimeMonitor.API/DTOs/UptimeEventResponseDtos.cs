namespace UptimeMonitor.API.DTOs
{
    public class UptimeEventResponseDto
    {
        public int Id { get; set; }
        public int UptimeCheckId { get; set; }
        public int ComponentId { get; set; }
        public int ServiceSystemId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsUp { get; set; }
        public bool IsFalsePositive { get; set; }
        public string? Category { get; set; }
        public string? Note { get; set; }
        public string? JiraTicket { get; set; }
        public string? MaintenanceType { get; set; }
    }

    public class UptimeEventExportDto
    {
        public int Id { get; set; }
        public string UptimeCheckName { get; set; } = string.Empty;
        public string ComponentName { get; set; } = string.Empty;
        public string ServiceSystemName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsUp { get; set; }
        public bool IsFalsePositive { get; set; }
        public string? Category { get; set; }
        public string? Note { get; set; }
        public string? JiraTicket { get; set; }
        public string? MaintenanceType { get; set; }
    }

    public class UptimeEventUpdateDto
    {
        public bool IsFalsePositive { get; set; }
        public string? Category { get; set; }
        public string? Note { get; set; }
        public string? JiraTicket { get; set; }
        public string? MaintenanceType { get; set; }
    }

    public class UptimeEventUpdateResponseDto
    {
        public int Id { get; set; }
        public bool IsFalsePositive { get; set; }
        public string? Category { get; set; }
        public string? Note { get; set; }
        public string? JiraTicket { get; set; }
        public string? MaintenanceType { get; set; }
    }
}