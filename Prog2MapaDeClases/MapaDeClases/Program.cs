using MapaDeClases.Entities;

namespace MapaDeClases
{
    class Program
    {
        static void Main(string[] args)
        {
            Estudiante estudiante = new Estudiante();

            estudiante.Nombre = "Brayner Jose Melo Adames";
            estudiante.Edad = 19;
            estudiante.Matricula = "20250933";
            estudiante.Carrera = "Desarrollo de Software";

            Console.WriteLine(estudiante.Nombre);
            Console.WriteLine( estudiante.Edad);
            Console.WriteLine(estudiante.Matricula);
            Console.WriteLine(estudiante.Carrera);

        }
    }
}
