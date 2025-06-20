namespace UptimeMonitor.API.Entities
{
    public class UptimeEvent
    {
        public int Id { get; set; }
        public int UptimeCheckId { get; set; }
        public UptimeCheck UptimeCheck { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsUp { get; set; }
        public bool IsFalsePositive { get; set; } = false;
        public string? Category { get; set; } // Internal / External
        public string? Note { get; set; }
        public string? JiraTicket { get; set; }
        public string? MaintenanceType { get; set; } // Planned / Emergency
    }
}