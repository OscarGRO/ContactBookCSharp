namespace ContactBook;

public class ContactBook
{    public const string NEXT_PAGE = "N";
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

            do
            {
                ShowInputOptions();
                input = GetInput();
            } 
            while (!IsValidInput(input));

            ProcessInput(input);

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
        Console.WriteLine("============================================================================");
        Console.WriteLine("  Lista de Contactos: ");
        Console.WriteLine("============================================================================");

        if (allContacts.Count <= 0)
        {
            Console.WriteLine(" No se encontraron contactos.");
        }
        else
        {
            int indexCol = -Math.Max("#".Length, allContacts.Count.ToString().Length);
            int fnameCol = -Math.Max("Nombre".Length, allContacts.Max(c => c.Nombre?.Length ?? 0));
            int lnameCol = -Math.Max("Apellido".Length, allContacts.Max(c => c.Apellido?.Length ?? 0));
            int phoneCol = -Math.Max("Telefono".Length, allContacts.Max(c => c.Telefono?.Length ?? 0));
            int emailCol = -Math.Max("Email".Length, allContacts.Max(c => c.Email?.Length ?? 0));

            string format = $" {{0,{indexCol}}} | {{1,{fnameCol}}} | {{2,{lnameCol}}} | {{3,{phoneCol}}} | {{4,{emailCol}}}";

            Console.WriteLine(format, "#", "Nombre", "Apellido", "Telefono", "Email");
            
            Console.WriteLine(new string('-', Math.Abs(indexCol + fnameCol + lnameCol + phoneCol + emailCol) + 15));


            int n = allContacts.Count;
            int page = 1;
            int size = 10;
            int pageCount = (int) Math.Max(1, Math.Ceiling(n / (double) size));
            int s = Math.Clamp((page - 1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);
            for (int i = s; i < e; i++)
            {
                Contact c = allContacts[i];
                
                Console.WriteLine(format, i + 1, c.Nombre, c.Apellido, c.Telefono, c.Email);
            }

            Console.WriteLine();
            Console.WriteLine($" Pagina {page} of {pageCount} ({s + 1}-{e} of {n})");
        }
        Console.WriteLine("============================================================================");
    }

    private void ShowInputOptions()
    {
        Console.WriteLine("\nOpciones: [N] Siguiente, [P] Anterior, [C] Crear, [M] Fusionar, [X] Salir");
        Console.Write("Seleccione una opcion: ");
    }

    private string GetInput() => Console.ReadLine()?.ToUpper() ?? "";

    private bool IsValidInput(string input) => Array.Exists(COMMANDS, cmd => cmd == input);

    private void ProcessInput(string input)
    {

    }

    private void ShowExitScreen()
    {
        Console.Clear();
        Console.WriteLine("======================================");
        Console.WriteLine("  Gracias por usar la Agenda de Oscar ");
        Console.WriteLine("======================================");
    }

    private void PressEnterContinue()
    {
        Console.Write("Presione ENTER para continuar.");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter);
    }
}