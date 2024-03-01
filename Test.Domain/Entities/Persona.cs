namespace Test.Domain.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string ApellidoPaterno { get; set; } = default!;
        public string ApellidoMaterno { get; set; } = default!;
        public string Identificacion { get; set; } = default!;
    }
}
