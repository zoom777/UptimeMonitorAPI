namespace UptimeMonitor.API.DTOs
{
    public class UptimeEventResponseDto
    {
        public int Id { get; set; }
        public int UptimeCheckId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsUp { get; set; }
        public bool IsFalsePositive { get; set; }
        public string? Category { get; set; }
        public string? Note { get; set; }
        public string? JiraTicket { get; set; }
        public string? MaintenanceType { get; set; }
    }
}