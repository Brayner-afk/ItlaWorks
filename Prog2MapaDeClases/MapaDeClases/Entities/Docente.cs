namespace MapaDeClases.Entities
{
    public class Docente : Empleado
    {
        public string Especialidad { get; set; } = string.Empty;

        public string Materia { get; set; } = string.Empty;
    }
}
