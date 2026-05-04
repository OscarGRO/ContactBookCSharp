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
            int pageCount = (int)Math.Max(1, Math.Ceiling(n / (double)pageSize));
            currentPage = Math.Clamp(currentPage, 1, pageCount);

            int s = (currentPage - 1) * pageSize;
            int e = Math.Min(s + pageSize, n);

            for (int i = s; i < e; i++)
            {
                Contact c = allContacts[i];
                Console.WriteLine(format, i + 1, c.Nombre, c.Apellido, c.Telefono, c.Email);
            }

            Console.WriteLine();
            Console.WriteLine($" Pagina {currentPage} de {pageCount} ({s + 1}-{e} de {n})");
        }
        Console.WriteLine("============================================================================");
    }

    private void ShowInputOptions()
    {
        Console.WriteLine(string.Format("[{0}] Next Page      | [{1}] Create Contact | [{2}] Delete Contact  | [{3}] Deduplicate Contacts", 
            NEXT_PAGE, CREATE_CONTACT, DELETE_CONTACT, DEDUPLICATE_CONTACTS));
        
        Console.WriteLine(string.Format("[{0}] Prev Page      | [{1}] Review Contact | [{2}] Find Contacts    | [{3}] Change Page Size", 
            PREV_PAGE, REVIEW_CONTACT, FIND_CONTACTS, PAGE_SIZE));
        
        Console.WriteLine(string.Format("[{0}] Goto Page      | [{1}] Update Contact | [{2}] Order Contacts   | [{3}] Exit", 
            GOTO_PAGE, UPDATE_CONTACT, ORDER_CONTACTS, EXIT));

        Console.WriteLine();
        Console.Write("> ");
    }

    private string GetInput() => Console.ReadLine()?.ToUpper() ?? "";

    private bool IsValidInput(string input) => Array.Exists(COMMANDS, cmd => cmd == input);

    private void ProcessInput(string input)
    {
        int n = allContacts.Count;
        int pageCount = (int)Math.Max(1, Math.Ceiling(n / (double)pageSize));

        switch (input)
        {
            case NEXT_PAGE:
                if (currentPage < pageCount) currentPage++;
                break;
            case PREV_PAGE:
                if (currentPage > 1) currentPage--;
                break;
        }
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
        Console.WriteLine("\nPresione ENTER para continuar.");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter);
    }
}