namespace UptimeMonitor.API.Entities
{
    public class ServiceSystem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; } = true;

        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}