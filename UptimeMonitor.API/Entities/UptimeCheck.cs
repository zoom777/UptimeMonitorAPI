namespace UptimeMonitor.API.Entities
{
    public class UptimeCheck
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int ServiceSystemId { get; set; }
        public ServiceSystem ServiceSystem { get; set; } = null!;

        public int ComponentId { get; set; }
        public Component Component { get; set; } = null!;

        public string CheckUrl { get; set; } = null!;
        public int CheckInterval { get; set; } // minutos
        public int CheckTimeout { get; set; } // milisegundos

        public string? RequestHeaders { get; set; } // uno por línea
        public int DownAlertDelay { get; set; } // minutos
        public int DownAlertResend { get; set; } // ciclos
        public string? DownAlertMessage { get; set; }
        public string AlertEmail { get; set; } = null!;
    }
}
