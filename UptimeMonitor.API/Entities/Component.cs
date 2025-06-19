namespace UptimeMonitor.API.Entities
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; } = true;

        public int ServiceSystemId { get; set; }
        public ServiceSystem ServiceSystem { get; set; } = null!;
    }
}