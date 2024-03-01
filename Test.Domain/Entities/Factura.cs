namespace Test.Domain.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
