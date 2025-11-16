Agenda agenda = new Agenda();
bool running = true;

Console.WriteLine("Mi Agenda Vacana");
Console.WriteLine("Bienvenido a tu lista de contactos (POO)");

agenda.AgregarContacto("Ana", "García", "Calle 1", "555-1234", "ana@mail.com", 30, true);
agenda.AgregarContacto("Juan", "Perez", "Avenida 5", "555-5678", "juan@mail.com", 25, false);

while (running)
{
    Console.WriteLine("\n-----------------------------------------------------------------");
    Console.Write("1. Agregar | 2. Ver Todos | 3. Buscar | 4. Modificar | 5. Eliminar | 6. Salir\n");
    Console.Write("Elige una opción: ");

    if (!int.TryParse(Console.ReadLine()!, out int choice))
    {
        Console.WriteLine("Entrada inválida.");
        continue;
    }

    try
    {
        switch (choice)
        {
            case 1: AgregarUI(agenda); break;
            case 2: MostrarTodosUI(agenda); break;
            case 3: BuscarUI(agenda); break;
            case 4: ModificarUI(agenda); break;
            case 5: EliminarUI(agenda); break;
            case 6: running = false; break;
            default: Console.WriteLine("Opción no válida"); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }
}
Console.WriteLine("\n¡Agenda finalizada!");

static void AgregarUI(Agenda agenda)
{
    Console.WriteLine("--- AGREGAR CONTACTO ---");
    Console.Write("Nombre: "); string nombre = Console.ReadLine()!;
    Console.Write("Apellido: "); string apellido = Console.ReadLine()!;
    Console.Write("Dirección: "); string direccion = Console.ReadLine()!;

    string telefono;
    do
    {
        Console.Write("Teléfono (solo números): ");
        telefono = Console.ReadLine()!;
        if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d+$"))
        {
            Console.WriteLine("Debe ingresar SOLO dígitos numéricos para el teléfono. Inténtelo de nuevo.");
        }
    } while (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d+$"));

    Console.Write("Email: "); string email = Console.ReadLine()!;

    int edad;
    bool edadValida = false;
    do
    {
        Console.Write("Edad: ");
        if (int.TryParse(Console.ReadLine(), out edad))
        {
            if (edad >= 0)
            {
                edadValida = true;
            }
            else
            {
                Console.WriteLine("La edad no puede ser negativa.");
            }
        }
        else
        {
            Console.WriteLine("Debe ingresar un número entero válido para la edad. Inténtelo de nuevo.");
        }
    } while (!edadValida);

    Console.Write("¿Mejor amigo? (1=Sí, 2=No): ");
    if (!int.TryParse(Console.ReadLine(), out int bfInt)) throw new ArgumentException("Opción inválida.");
    bool esMejorAmigo = bfInt == 1;

    agenda.AgregarContacto(nombre, apellido, direccion, telefono, email, edad, esMejorAmigo);
    Console.WriteLine("Contacto agregado con éxito.");
}

static void MostrarTodosUI(Agenda agenda)
{
    Console.WriteLine("\n--- LISTA DE CONTACTOS ---");
    var contactos = agenda.ObtenerTodos();
    if (contactos.Count == 0)
    {
        Console.WriteLine("No hay contactos para mostrar.");
        return;
    }

    Console.WriteLine("ID | Nombre           | Apellido         | Dirección        | Teléfono         | Email            | Edad | Mejor Amigo");
    Console.WriteLine("---|------------------|------------------|------------------|------------------|------------------|------|------------");
    foreach (var c in contactos)
    {
        Console.WriteLine(c.ToString());
    }
}

static void BuscarUI(Agenda agenda)
{
    Console.Write("Buscar por nombre, apellido o email: ");
    string termino = Console.ReadLine()!;
    var resultados = agenda.BuscarContactos(termino);

    if (resultados.Any())
    {
        Console.WriteLine($"\n--- RESULTADOS DE BÚSQUEDA ({resultados.Count}) ---");
        Console.WriteLine("ID | Nombre           | Apellido         | Dirección        | Teléfono         | Email            | Edad | Mejor Amigo");
        Console.WriteLine("---|------------------|------------------|------------------|------------------|------------------|------|------------");
        foreach (var c in resultados)
        {
            Console.WriteLine(c.ToString());
        }
    }
    else
    {
        Console.WriteLine("No se encontraron resultados.");
    }
}

static void ModificarUI(Agenda agenda)
{
    Console.Write("ID del contacto a modificar: ");
    if (!int.TryParse(Console.ReadLine(), out int id)) return;

    var contacto = agenda.ObtenerPorId(id);
    if (contacto == null) { Console.WriteLine("Contacto no encontrado."); return; }

    Console.WriteLine($"Modificando: {contacto.Nombre} {contacto.Apellido}");
    Console.WriteLine("1. Nombre | 2. Email | 3. Edad | 4. EsMejorAmigo | 5. Teléfono");
    Console.Write("Campo a modificar: ");

    if (!int.TryParse(Console.ReadLine(), out int opcion)) return;

    switch (opcion)
    {
        case 1: Console.Write("Nuevo Nombre: "); contacto.Nombre = Console.ReadLine()!; break;
        case 2: Console.Write("Nuevo Email: "); contacto.Email = Console.ReadLine()!; break;
        case 3:
            Console.Write("Nueva Edad: ");
            if (int.TryParse(Console.ReadLine(), out int nuevaEdad)) contacto.Edad = nuevaEdad;
            else Console.WriteLine("Valor inválido. No se modificó.");
            break;
        case 4:
            Console.Write("¿Es mejor amigo? (1=Sí, 2=No): ");
            if (int.TryParse(Console.ReadLine(), out int bfInt) && (bfInt == 1 || bfInt == 2))
                contacto.EsMejorAmigo = bfInt == 1;
            else Console.WriteLine("Opción inválida. No se modificó.");
            break;
        case 5:
            string nuevoTel;
            do
            {
                Console.Write("Nuevo Teléfono (solo números): ");
                nuevoTel = Console.ReadLine()!;
                if (!System.Text.RegularExpressions.Regex.IsMatch(nuevoTel, @"^\d+$"))
                {
                    Console.WriteLine("Debe ingresar SOLO dígitos numéricos. Inténtelo de nuevo.");
                }
            } while (!System.Text.RegularExpressions.Regex.IsMatch(nuevoTel, @"^\d+$"));
            contacto.Telefono = nuevoTel;
            break;
        default: Console.WriteLine("Opción no válida."); return;
    }
    Console.WriteLine("Contacto modificado.");
}

static void EliminarUI(Agenda agenda)
{
    Console.Write("ID del contacto a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id)) return;

    if (agenda.EliminarContacto(id))
    {
        Console.WriteLine($"Contacto con ID {id} eliminado con éxito.");
    }
    else
    {
        Console.WriteLine("Contacto no encontrado.");
    }
}

// -----------------------------------------------------------------
// CLASE 1: Contacto (Encapsulamiento)
// -----------------------------------------------------------------
public class Contacto
{
    public int Id { get; set; }

    private int _edad;
    private string _nombre = string.Empty;

    public string Nombre
    {
        get { return _nombre; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
            _nombre = value;
        }
    }

    public string Apellido { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EsMejorAmigo { get; set; }

    public int Edad
    {
        get { return _edad; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("La edad no puede ser negativa.");
            }
            _edad = value;
        }
    }

    public Contacto(int id, string nombre, string apellido, string direccion, string telefono, string email, int edad, bool esMejorAmigo)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
        Telefono = telefono;
        Email = email;
        Edad = edad;
        EsMejorAmigo = esMejorAmigo;
    }
    public override string ToString()
    {
        return $"{Id,-2} | {Nombre,-16} | {Apellido,-16} | {Direccion,-16} | {Telefono,-16} | {Email,-16} | {Edad,-4} | {(EsMejorAmigo ? "Sí" : "No"),-10}";
    }
}

// -----------------------------------------------------------------
// CLASE 2: Agenda (Abstracción y Gestión)
// -----------------------------------------------------------------
public class Agenda
{
    private readonly List<Contacto> _contactos = new List<Contacto>();
    private int _nextId = 1;

    public void AgregarContacto(string nombre, string apellido, string direccion, string telefono, string email, int edad, bool esMejorAmigo)
    {
        var nuevoContacto = new Contacto(_nextId++, nombre, apellido, direccion, telefono, email, edad, esMejorAmigo);
        _contactos.Add(nuevoContacto);
    }

    public List<Contacto> ObtenerTodos()
    {
        return _contactos;
    }

    public Contacto? ObtenerPorId(int id)
    {
        return _contactos.FirstOrDefault(c => c.Id == id);
    }

    public List<Contacto> BuscarContactos(string termino)
    {
        string term = termino.ToLower();
        return _contactos
            .Where(c => c.Nombre.ToLower().Contains(term) ||
                        c.Apellido.ToLower().Contains(term) ||
                        c.Email.ToLower().Contains(term) ||
                        c.Direccion.ToLower().Contains(term))
            .ToList();
    }

    public bool EliminarContacto(int id)
    {
        var contacto = ObtenerPorId(id);
        if (contacto == null)
        {
            return false;
        }
        _contactos.Remove(contacto);
        return true;
    }
}