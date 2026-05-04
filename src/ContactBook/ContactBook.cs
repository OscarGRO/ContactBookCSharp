namespace ContactBook;

public class ContactBook
{   
    public const string NEXT_PAGE = "N";
    public const string PREV_PAGE = "P";
    public const string GOTO_PAGE = "G";
    public const string PAGE_SIZE = "S";

    public const string CREATE_CONTACT = "C";
    public const string REVIEW_CONTACT = "R";
    public const string UPDATE_CONTACT = "U";
    public const string DELETE_CONTACT = "D";
    public const string FIND_CONTACTS = "F";
    public const string ORDER_CONTACTS = "O";
    public const string DEDUPLICATE_CONTACTS = "M";
    public const string EXIT = "X";

    public readonly string[] COMMANDS = new string[]
    {
        NEXT_PAGE, PREV_PAGE, GOTO_PAGE, PAGE_SIZE,
        CREATE_CONTACT, REVIEW_CONTACT, UPDATE_CONTACT, 
        DELETE_CONTACT, FIND_CONTACTS, ORDER_CONTACTS, 
        DEDUPLICATE_CONTACTS, EXIT
    };

    private List<Contact> allContacts;
    private int currentPage = 1;
    private int pageSize = 10;

    public ContactBook(List<Contact> contacts = null!)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
    }

    public void Start()
    {
        ShowWelcomeScreen();

        string input;
        do
        {
            ShowContacts();

            bool isInvalid;
            do
            {
                ShowInputOptions();
                input = GetInput();
                
                isInvalid = !IsValidInput(input) && input != EXIT;

                if (isInvalid)
                {
                    Console.WriteLine("ERROR: Input invalido. Por favor, intentelo de nuevo.");
                    PressEnterContinue();
                    ShowContacts();
                }

            } while (isInvalid);

            if (input == EXIT)
            {
                Console.Write("\nEstas seguro de que quieres salir? (Y/N): ");
                string confirm = Console.ReadLine()?.ToUpper() ?? "";
                
                if (confirm == "Y")
                {
                    ProcessInput(input);
                }
                else
                {
                    input = ""; 
                }
            }
            else
            {
                ProcessInput(input);
            }

        } while (input != EXIT);

        ShowExitScreen();
    }

    private void ShowWelcomeScreen()
    {
        Console.Clear();
        Console.WriteLine("======================================");
        Console.WriteLine("  Bienvenido al ContactBook de Oscar!    ");
        Console.WriteLine("======================================");
        PressEnterContinue();
    }

    private void ShowContacts()
    {
        Console.Clear();
        Console.WriteLine("================================================================================================");
        Console.WriteLine("  Lista de Contactos: ");
        Console.WriteLine("================================================================================================");

        int indexCol = -Math.Max("#".Length, allContacts.Count.ToString().Length);
        int fnameCol = -Math.Max("Nombre".Length, allContacts.Any() ? allContacts.Max(c => c.Nombre?.Length ?? 0) : 0);
        int lnameCol = -Math.Max("Apellido".Length, allContacts.Any() ? allContacts.Max(c => c.Apellido?.Length ?? 0) : 0);
        int phoneCol = -Math.Max("Telefono".Length, allContacts.Any() ? allContacts.Max(c => c.Telefono?.Length ?? 0) : 0);
        int emailCol = -Math.Max("Email".Length, allContacts.Any() ? allContacts.Max(c => c.Email?.Length ?? 0) : 0);

        string format = $" {{0,{indexCol}}} | {{1,{fnameCol}}} | {{2,{lnameCol}}} | {{3,{phoneCol}}} | {{4,{emailCol}}}";

        Console.WriteLine(format, "#", "Nombre", "Apellido", "Telefono", "Email");
        Console.WriteLine(new string('-', Math.Abs(indexCol + fnameCol + lnameCol + phoneCol + emailCol) + 15));

        int n = allContacts.Count;
        int pageCount = (int)Math.Max(1, Math.Ceiling(n / (double)pageSize));
        currentPage = Math.Clamp(currentPage, 1, pageCount);

        int s = (currentPage - 1) * pageSize;
        int e = Math.Min(s + pageSize, n);

        for (int i = s; i < e; i++)
        {
            Contact c = allContacts[i];
            Console.WriteLine(format, i + 1, c.Nombre, c.Apellido, c.Telefono, c.Email);
        }

        int renderedLines = e - s;
        for (int i = renderedLines; i < pageSize; i++)
        {
            Console.WriteLine(format, "", "", "", "", "");
        }

        Console.WriteLine();
        Console.WriteLine($" Pagina {currentPage} de {pageCount} ({ (n > 0 ? s + 1 : 0) }-{e} de {n})");
        Console.WriteLine("================================================================================================");
    }

    private void ShowInputOptions()
    {
        Console.WriteLine(string.Format("[{0}] Pagina Siguiente | [{1}] Crear Contacto      | [{2}] Eliminar Contacto | [{3}] Eliminar Duplicados", 
            NEXT_PAGE, CREATE_CONTACT, DELETE_CONTACT, DEDUPLICATE_CONTACTS));
        
        Console.WriteLine(string.Format("[{0}] Pagina Anterior  | [{1}] Revisar Contacto    | [{2}] Buscar Contactos  | [{3}] Tamaño de Pagina", 
            PREV_PAGE, REVIEW_CONTACT, FIND_CONTACTS, PAGE_SIZE));
        
        Console.WriteLine(string.Format("[{0}] Ir a Pagina      | [{1}] Actualizar Contacto | [{2}] Ordenar Contactos | [{3}] Salir", 
            GOTO_PAGE, UPDATE_CONTACT, ORDER_CONTACTS, EXIT));

        Console.WriteLine();
        Console.Write("> ");
    }

    private string GetInput() => Console.ReadLine()?.ToUpper() ?? "";

    private bool IsValidInput(string input) => Array.Exists(COMMANDS, cmd => cmd == input);

    private void ProcessInput(string input)
    {
        switch (input)
        {
            case NEXT_PAGE: NextPage(); break;
            case PREV_PAGE: PrevPage(); break;
            case GOTO_PAGE: GotoPage(); break;
            case PAGE_SIZE: PageSize(); break;
            case CREATE_CONTACT: CreateContact(); break;
            case REVIEW_CONTACT: ReviewContact(); break;
            case UPDATE_CONTACT: UpdateContact(); break;
            case DELETE_CONTACT: DeleteContact(); break;
            case FIND_CONTACTS: FindContacts(); break;
            case ORDER_CONTACTS: OrderContacts(); break;
            case DEDUPLICATE_CONTACTS: DeduplicateContacts(); break;
            case EXIT: Exit(); break;
            default: break;
        }
    }

    private void ShowExitScreen()
    {
        Console.Clear();
        Console.WriteLine("===========================================");
        Console.WriteLine("  Gracias por usar el ContactBook de Oscar! ");
        Console.WriteLine("===========================================");
    }

    private void PressEnterContinue()
    {
        Console.WriteLine("\nPresione ENTER para continuar.");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter);
    }

    private void NextPage() 
    { 
        int pageCount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)pageSize));
        if (currentPage < pageCount) currentPage++;
    }

    private void PrevPage() 
    { 
        if (currentPage > 1) currentPage--;
    }

    private void GotoPage() 
    { 
        int pageCount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)pageSize));
        Console.Write($"Ingrese el numero de pagina (1-{pageCount}): ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int targetPage) && targetPage >= 1 && targetPage <= pageCount)
        {
            currentPage = targetPage;
        }
        else
        {
            Console.WriteLine("ERROR: Input invalido. Por favor, intentelo de nuevo.");
            PressEnterContinue();
        }
    }

    private void PageSize()
    {
        PageSize(ref pageSize);
    }

    private void PageSize(ref int size)
    {
        Console.Write("Entre el tamaño de pagina [1-20]: ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int newSize) && newSize >= 1 && newSize <= 20)
        {
            size = newSize;
            
            // Recalcular si la pagina actual sigue siendo valida con el nuevo tamaño
            int pageCount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)size));
            if (currentPage > pageCount)
            {
                currentPage = 1;
            }
        }
        else
        {
            Console.WriteLine("ERROR: Input invalido. Por favor, intentelo de nuevo.");
            PressEnterContinue();
        }
    }

    private void CreateContact() 
    { 
        Console.WriteLine("Crear Contacto"); 
        PressEnterContinue();
    }

    private void ReviewContact() 
    { 
        Console.WriteLine("Revisar Contacto"); 
        PressEnterContinue();
    }

    private void UpdateContact() 
    { 
        Console.WriteLine("Actualizar Contacto"); 
        PressEnterContinue();
    }

    private void DeleteContact() 
    { 
        Console.WriteLine("Eliminar Contacto"); 
        PressEnterContinue();
    }

    private void FindContacts() 
    { 
        Console.WriteLine("Buscar Contactos"); 
        PressEnterContinue();
    }

    private void OrderContacts() 
    { 
        Console.WriteLine("Ordenar Contactos"); 
        PressEnterContinue();
    }

    private void DeduplicateContacts() 
    { 
        Console.WriteLine("Eliminar Contactos Duplicados"); 
        PressEnterContinue();
    }

    private void Exit() 
    { 
        Console.WriteLine("Salir"); 
    }
}