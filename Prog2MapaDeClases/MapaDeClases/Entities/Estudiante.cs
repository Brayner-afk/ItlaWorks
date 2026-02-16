namespace MapaDeClases.Entities
{
    public class Estudiante : MiembroDeLaComunidad
    {
        public string Matricula { get; set; } = string.Empty;

        public string Carrera { get; set; } = string.Empty;
    }
}
