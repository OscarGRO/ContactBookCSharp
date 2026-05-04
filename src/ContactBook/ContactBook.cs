namespace ContactBook;

public class ContactBook
{
    public const string NEXT_PAGE = "+";
    public const string PREV_PAGE = "-";
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

    public ContactBook()
    {
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
        Console.WriteLine("--- LISTA DE CONTACTOS ---");
        Console.WriteLine("\n(No hay contactos para mostrar todavia)");
    }

    private void ShowInputOptions()
    {
        Console.WriteLine("\nOpciones: [+] Siguiente, [-] Anterior, [C] Crear, [M] Fusionar, [X] Salir");
        Console.Write("Seleccione una opcion: ");
    }

    private string GetInput()
    {
        return Console.ReadLine()?.ToUpper() ?? "";
    }

    private bool IsValidInput(string input)
    {
        foreach (string cmd in COMMANDS)
        {
            if (cmd == input) return true;
        }
        return false;
    }
    private void ProcessInput(string input)
    {
       
    }

    private bool ConfirmExit()
    {
    
        return true; 
    }

    private void ShowExitScreen()
    {
        Console.Clear();
        Console.WriteLine("======================================");
        Console.WriteLine("  Gracias por usar la Agenda de Oscar ");
        Console.WriteLine("======================================");
    }

    // Metodo de pausa para que el usuario pueda leer la pantalla
    private void PressEnterContinue()
    {
        Console.Write("Presione ENTER para continuar.");
        
        while (Console.ReadKey(true).Key != ConsoleKey.Enter);
    }
}

