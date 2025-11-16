Console.WriteLine("Bienvenido a mi lista de Contactos");

bool running = true;
List<int> ids = new List<int>();
Dictionary<int, string> names = new Dictionary<int, string>();
Dictionary<int, string> lastnames = new Dictionary<int, string>();
Dictionary<int, string> addresses = new Dictionary<int, string>();
Dictionary<int, string> telephones = new Dictionary<int, string>();
Dictionary<int, string> emails = new Dictionary<int, string>();
Dictionary<int, int> ages = new Dictionary<int, int>();
Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();

while (running)
{
    Console.WriteLine();
    Console.WriteLine("-----------------------------------------------------------------");
    Console.WriteLine(@"1. Agregar Contacto | 2. Ver Contactos | 3. Buscar Contactos | 4. Modificar Contacto | 5. Eliminar Contacto | 6. Salir");
    Console.WriteLine("Digite el número de la opción deseada:");

    if (!int.TryParse(Console.ReadLine(), out int typeOption))
    {
        Console.WriteLine("Entrada inválida. Por favor, digite un número de opción.");
        continue;
    }

    switch (typeOption)
    {
        case 1:
            AddContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;
        case 2:
            ViewContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;
        case 3:
            SearchContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;
        case 4:
            ModifyContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;
        case 5:
            DeleteContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
            break;
        case 6:
            running = false;
            break;
        default:
            Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
            break;
    }
}

Console.WriteLine("¡Gracias por usar la lista de contactos!");

// -----------------------------------------------------------------
// MÉTODOS ESTÁTICOS
// -----------------------------------------------------------------

/// Añadir un nuevo contacto a las colecciones.
static void AddContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("--- AGREGAR CONTACTO ---");
    Console.Write("Digite el nombre: ");
    string name = Console.ReadLine()!;

    Console.Write("Digite el apellido: ");
    string lastname = Console.ReadLine()!;

    Console.Write("Digite la dirección: ");
    string address = Console.ReadLine()!;

    Console.Write("Digite el teléfono: ");
    string phone = Console.ReadLine()!;

    Console.Write("Digite el email: ");
    string email = Console.ReadLine()!;

    Console.Write("Digite la edad (número): ");
    if (!int.TryParse(Console.ReadLine(), out int age))
    {
        Console.WriteLine("Edad inválida. Contacto no agregado.");
        return;
    }

    Console.Write("¿Es mejor amigo? (1. Si, 2. No): ");
    if (!int.TryParse(Console.ReadLine(), out int isBestFriendInt))
    {
        Console.WriteLine("Opción inválida. Asumiendo 'No'.");
        isBestFriendInt = 2;
    }

    bool isBestFriend = isBestFriendInt == 1;

    var id = ids.Count > 0 ? ids.Max() + 1 : 1;
    ids.Add(id);
    names.Add(id, name);
    lastnames.Add(id, lastname);
    addresses.Add(id, address);
    telephones.Add(id, phone);
    emails.Add(id, email);
    ages.Add(id, age);
    bestFriends.Add(id, isBestFriend);

    Console.WriteLine($"Contacto {name} {lastname} (ID: {id}) agregado con éxito.");
}

/// Muestrar todos los contactos registrados.
static void ViewContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("\n--- LISTA DE CONTACTOS ---");
    if (ids.Count == 0)
    {
        Console.WriteLine("No hay contactos para mostrar.");
        return;
    }

    Console.WriteLine("ID | Nombre           | Apellido         | Dirección        | Teléfono         | Email            | Edad | Mejor Amigo");
    Console.WriteLine("---|------------------|------------------|------------------|------------------|------------------|------|------------");

    foreach (var id in ids)
    {
        string isBestFriendStr = bestFriends[id] ? "Sí" : "No";

        Console.WriteLine($"{id,-2} | {names[id],-16} | {lastnames[id],-16} | {addresses[id],-16} | {telephones[id],-16} | {emails[id],-16} | {ages[id],-4} | {isBestFriendStr,-10}");
    }
    Console.WriteLine("--------------------------");
}

/// Buscar un contacto por nombre o apellido.
static void SearchContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("\n--- BUSCAR CONTACTO ---");
    Console.Write("Digite el nombre o apellido a buscar: ");
    string searchTerm = Console.ReadLine()!.ToLower();

    var matchingIds = ids
        .Where(id => names[id].ToLower().Contains(searchTerm) || lastnames[id].ToLower().Contains(searchTerm))
        .ToList();

    if (matchingIds.Count == 0)
    {
        Console.WriteLine($"No se encontraron contactos que coincidan con '{searchTerm}'.");
        return;
    }

    Console.WriteLine($"Se encontraron {matchingIds.Count} contactos:");
    Console.WriteLine("ID | Nombre           | Apellido         | Teléfono");
    Console.WriteLine("---|------------------|------------------|------------------");

    foreach (var id in matchingIds)
    {
        Console.WriteLine($"{id,-2} | {names[id],-16} | {lastnames[id],-16} | {telephones[id],-16}");
    }
    Console.WriteLine("--------------------------");
}
/// Modificar un contacto.
static void ModifyContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("\n--- MODIFICAR CONTACTO ---");
    Console.Write("Digite el ID del contacto a modificar: ");

    if (!int.TryParse(Console.ReadLine(), out int idToModify) || !ids.Contains(idToModify))
    {
        Console.WriteLine("❌ ID inválido o no encontrado.");
        return;
    }

    Console.WriteLine($"Modificando contacto ID: {idToModify} ({names[idToModify]} {lastnames[idToModify]})");
    Console.WriteLine(@"Seleccione el campo a modificar: 1. Nombre | 2. Apellido | 3. Dirección | 4. Teléfono | 5. Email | 6. Edad | 7. Mejor Amigo");

    if (!int.TryParse(Console.ReadLine(), out int fieldOption))
    {
        Console.WriteLine("❌ Opción de campo inválida.");
        return;
    }

    string? newValue;

    switch (fieldOption)
    {
        case 1:
            Console.Write($"Nombre actual ({names[idToModify]}). Nuevo nombre: ");
            newValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(newValue)) names[idToModify] = newValue!;
            break;
        case 2:
            Console.Write($"Apellido actual ({lastnames[idToModify]}). Nuevo apellido: ");
            newValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(newValue)) lastnames[idToModify] = newValue!;
            break;
        case 3:
            Console.Write($"Dirección actual ({addresses[idToModify]}). Nueva dirección: ");
            newValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(newValue)) addresses[idToModify] = newValue!;
            break;
        case 4:
            Console.Write($"Teléfono actual ({telephones[idToModify]}). Nuevo teléfono: ");
            newValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(newValue)) telephones[idToModify] = newValue!;
            break;
        case 5:
            Console.Write($"Email actual ({emails[idToModify]}). Nuevo email: ");
            newValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(newValue)) emails[idToModify] = newValue!;
            break;
        case 6:
            Console.Write($"Edad actual ({ages[idToModify]}). Nueva edad (número): ");
            if (int.TryParse(Console.ReadLine(), out int newAge) && newAge >= 0)
            {
                ages[idToModify] = newAge;
            }
            else
            {
                Console.WriteLine("❌ Edad inválida o negativa. No se modificó la edad.");
                return;
            }
            break;
        case 7:
            Console.Write($"¿Es mejor amigo actualmente? ({bestFriends[idToModify]}). Nuevo valor (1. Si, 2. No): ");
            if (int.TryParse(Console.ReadLine(), out int newBestFriend) && (newBestFriend == 1 || newBestFriend == 2))
            {
                bestFriends[idToModify] = newBestFriend == 1;
            }
            else
            {
                Console.WriteLine("❌ Opción inválida. No se modificó el estado de mejor amigo.");
                return;
            }
            break;
        default:
            Console.WriteLine("❌ Opción de campo no válida.");
            return;
    }

    Console.WriteLine("✅ Contacto modificado con éxito.");
}
/// Eliminar un contacto.
static void DeleteContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("\n--- ELIMINAR CONTACTO ---");
    Console.Write("Digite el ID del contacto a eliminar: ");

    if (!int.TryParse(Console.ReadLine(), out int idToDelete) || !ids.Contains(idToDelete))
    {
        Console.WriteLine("❌ ID inválido o no encontrado.");
        return;
    }

    // Obtener el nombre para el mensaje de confirmación
    string contactName = names[idToDelete];

    // Eliminar de la lista de IDs
    ids.Remove(idToDelete);

    // Eliminar de todos los Diccionarios (Remove devuelve true/false, pero podemos ignorarlo)
    names.Remove(idToDelete);
    lastnames.Remove(idToDelete);
    addresses.Remove(idToDelete);
    telephones.Remove(idToDelete);
    emails.Remove(idToDelete);
    ages.Remove(idToDelete);
    bestFriends.Remove(idToDelete);

    Console.WriteLine($" Contacto {contactName} (ID: {idToDelete}) eliminado con éxito.");
}