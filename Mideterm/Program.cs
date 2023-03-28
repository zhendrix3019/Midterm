class Program
{
    static void Main(string[] args)
    {
        string file = "tickets.txt";
        string choice;

        do
        {
            Console.WriteLine("\n1) Read data from file.");
            Console.WriteLine("2) Create file from data.");
            Console.WriteLine("Enter any other key to exit.");

            choice = Console.ReadLine();

            if (choice == "1")
            {
                if (File.Exists(file))
                {
                    List<Ticket> tickets = ReadTicketsFromFile(file);
                    foreach (Ticket ticket in tickets)
                    {
                        Console.WriteLine(ticket);
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist");
                }
            }
            else if (choice == "2")
            {
                List<Ticket> tickets = new List<Ticket>();

                do
                {
                    Console.WriteLine("Enter a new ticket (Y/N)?");
                    choice = Console.ReadLine().ToUpper();
                    if (choice != "Y") { break; }

                    Console.WriteLine("Enter the type of ticket (Bug/Defect, Enhancement, Task):");
                    string ticketType = Console.ReadLine();

                    Ticket ticket;
                    switch (ticketType)
                    {
                        case "Bug/Defect":
                            ticket = CreateBugTicket();
                            break;
                        case "Enhancement":
                            ticket = CreateEnhancementTicket();
                            break;
                        case "Task":
                            ticket = CreateTaskTicket();
                            break;
                        default:
                            Console.WriteLine("Invalid ticket type, defaulting to Bug/Defect");
                            ticket = CreateBugTicket();
                            break;
                    }
                    tickets.Add(ticket);

                } while (choice == "Y");

                WriteTicketsToFile(file, tickets);
            }

        } while (choice == "1" || choice == "2" || choice == "N");
    }

    static BugTicket CreateBugTicket()
    {
        Console.WriteLine("Enter the TicketID.");
        string ticketID = Console.ReadLine();

        Console.WriteLine("Enter the summary.");
        string summary = Console.ReadLine();

        Console.WriteLine("Enter the status.");
        string status = Console.ReadLine();

        Console.WriteLine("Enter the priority.");
        string priority = Console.ReadLine();

        Console.WriteLine("Enter the submitter.");
        string submitter = Console.ReadLine();

        Console.WriteLine("Enter who was assigned.");
        string assigned = Console.ReadLine();

        Console.WriteLine("Enter the severity.");
        string severity = Console.ReadLine();

        List<string> watching = new List<string>();
        string choice;

        do
        {
            Console.WriteLine("Enter who is watching.");
            string watcher = Console.ReadLine();
            watching.Add(watcher);

            Console.WriteLine("Is there another person watching (Y/N)?");
            choice = Console.ReadLine().ToUpper();

        } while (choice == "Y");

        return new BugTicket(ticketID, summary, status, priority, submitter, assigned, watching, severity);
    }

   static EnhancementTicket CreateEnhancementTicket()
    {
        Console.WriteLine("Enter the TicketID.");
        string ticketID = Console.ReadLine();

        Console.WriteLine("Enter the summary.");
        string summary = Console.ReadLine();

        Console.WriteLine("Enter the status.");
        string status = Console.ReadLine();

        Console.WriteLine("Enter the priority.");
        string priority = Console.ReadLine();

        Console.WriteLine("Enter the submitter.");
        string submitter = Console.ReadLine();

        Console.WriteLine("Enter who was assigned.");
        string assigned = Console.ReadLine();

        List<string> watching = new List<string>();
        string choice;

        do
        {
            Console.WriteLine("Enter who is watching.");
            string watcher = Console.ReadLine();
            watching.Add(watcher);

            Console.WriteLine("Is there another person watching (Y/N)?");
            choice = Console.ReadLine().ToUpper();

        } while (choice == "Y");

        Console.WriteLine("Enter the software affected by this enhancement.");
        string software = Console.ReadLine();

        Console.WriteLine("Enter the cost estimate for this enhancement.");
        decimal cost;
        while (!decimal.TryParse(Console.ReadLine(), out cost))
        {
            Console.WriteLine("Invalid cost. Please enter a valid decimal number:");
        }

        Console.WriteLine("Enter the reason for this enhancement.");
        string reason = Console.ReadLine();

        Console.WriteLine("Enter the time estimate for this enhancement (in hours).");
        double estimate;
        while (!double.TryParse(Console.ReadLine(), out estimate))
        {
            Console.WriteLine("Invalid time estimate. Please enter a valid number:");
        }

        return new EnhancementTicket(ticketID, summary, status, priority, submitter, assigned, watching, software, cost, reason, estimate);
    }
    static TaskTicket CreateTaskTicket()
{
    Console.WriteLine("Enter the TicketID.");
    string ticketID = Console.ReadLine();

    Console.WriteLine("Enter the summary.");
    string summary = Console.ReadLine();

    Console.WriteLine("Enter the status.");
    string status = Console.ReadLine();

    Console.WriteLine("Enter the priority.");
    string priority = Console.ReadLine();

    Console.WriteLine("Enter the submitter.");
    string submitter = Console.ReadLine();

    Console.WriteLine("Enter who was assigned.");
    string assigned = Console.ReadLine();

    List<string> watching = new List<string>();
    string choice;

    do
    {
        Console.WriteLine("Enter who is watching.");
        string watcher = Console.ReadLine();
        watching.Add(watcher);

        Console.WriteLine("Is there another person watching (Y/N)?");
        choice = Console.ReadLine().ToUpper();

    } while (choice == "Y");

    Console.WriteLine("Enter the project name.");
    string projectName = Console.ReadLine();

    Console.WriteLine("Enter the due date (mm/dd/yyyy).");
    DateTime dueDate = DateTime.Parse(Console.ReadLine());

    return new TaskTicket(ticketID, summary, status, priority, submitter, assigned, watching, projectName, dueDate);
}


static void WriteTicketsToFile(string fileName, List<Ticket> tickets)
{
    using (StreamWriter sw = new StreamWriter(fileName))
    {
        foreach (Ticket ticket in tickets)
        {
            if (ticket is BugTicket)
            {
                BugTicket bugTicket = ticket as BugTicket;
                sw.WriteLine($"{bugTicket.TicketID},{bugTicket.Summary},{bugTicket.Status},{bugTicket.Priority},{bugTicket.Submitter},{bugTicket.Assigned},{string.Join("|", bugTicket.Watching)},{bugTicket.Severity}");
            }
            else if (ticket is EnhancementTicket)
            {
                EnhancementTicket enhancementTicket = ticket as EnhancementTicket;
                sw.WriteLine($"{enhancementTicket.TicketID},{enhancementTicket.Summary},{enhancementTicket.Status},{enhancementTicket.Priority},{enhancementTicket.Submitter},{enhancementTicket.Assigned},{string.Join("|", enhancementTicket.Watching)},{enhancementTicket.Software},{enhancementTicket.Cost},{enhancementTicket.Reason},{enhancementTicket.Estimate}");
            }
            else if (ticket is TaskTicket)
            {
                TaskTicket taskTicket = ticket as TaskTicket;
                sw.WriteLine($"{taskTicket.TicketID},{taskTicket.Summary},{taskTicket.Status},{taskTicket.Priority},{taskTicket.Submitter},{taskTicket.Assigned},{string.Join("|", taskTicket.Watching)},{taskTicket.ProjectName},{taskTicket.DueDate}");
            }
        }
    }
}

static List<Ticket> ReadTicketsFromFile(string fileName)
{
    List<Ticket> tickets = new List<Ticket>();

    if (!File.Exists(fileName))
    {
        Console.WriteLine("File does not exist");
        return tickets;
    }

    using (StreamReader sr = new StreamReader(fileName))
    {
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] parts = line.Split(',');

            string ticketID = parts[0];
            string summary = parts[1];
            string status = parts[2];
            string priority = parts[3];
            string submitter = parts[4];
            string assigned = parts[5];
            List<string> watching = new List<string>(parts[6].Split('|'));

            if (parts.Length == 8)
            {
                // BugTicket
                string severity = parts[7];
                BugTicket bugTicket = new BugTicket(ticketID, summary, status, priority, submitter, assigned, watching, severity);
                tickets.Add(bugTicket);
            }
            else if (parts.Length == 11)
            {
                // EnhancementTicket
                string software = parts[7];
                decimal cost = decimal.Parse(parts[8]);
                string reason = parts[9];
                int estimate = int.Parse(parts[10]);
                EnhancementTicket enhancementTicket = new EnhancementTicket(ticketID, summary, status, priority, submitter, assigned, watching, software, cost, reason, estimate);
                tickets.Add(enhancementTicket);
            }
            else if (parts.Length == 9)
            {
                // TaskTicket
                string projectName = parts[7];
                DateTime dueDate = DateTime.Parse(parts[8]);
                TaskTicket taskTicket = new TaskTicket(ticketID, summary, status, priority, submitter, assigned, watching, projectName, dueDate);
                tickets.Add(taskTicket);
            }
        }
    }

    return tickets;
}
}
