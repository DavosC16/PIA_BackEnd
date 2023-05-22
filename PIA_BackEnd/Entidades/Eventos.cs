namespace PIA_BackEnd.Entidades
{
    public class Eventos
    {
        public int Id { get; set; }

        public int IdOrganizador { get; set; }

        public string Nombre { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

        public string Ubicacion { get; set; }

        public int MaxCapacidad { get; set; }

    }
}
