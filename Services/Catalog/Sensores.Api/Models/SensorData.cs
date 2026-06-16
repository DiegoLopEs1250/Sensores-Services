namespace Sensores.Api.Models
{
    public class SensorData
    {
        public Guid Id { get; set; }
        public double Temperatura { get; set; }
        public double Humedad {  get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? IpServidor { get; set; }
        public string? IpContenedor { get; set; }
    }
}
