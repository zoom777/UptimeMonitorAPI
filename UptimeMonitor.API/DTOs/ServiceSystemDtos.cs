namespace UptimeMonitor.API.DTOs
{
    public class ServiceSystemCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class ServiceSystemResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}