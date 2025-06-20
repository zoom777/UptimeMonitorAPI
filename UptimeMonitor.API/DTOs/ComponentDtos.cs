namespace UptimeMonitor.API.DTOs
{
    public class ComponentCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ServiceSystemId { get; set; }
    }

    public class ComponentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public int ServiceSystemId { get; set; }
    }
}