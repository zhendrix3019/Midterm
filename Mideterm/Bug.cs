class BugTicket : Ticket
{
    public string Severity { get; set; }

    public BugTicket(string ticketID, string summary, string status, string priority, string submitter, string assigned, List<string> watching, string severity) 
        : base(ticketID, summary, status, priority, submitter, assigned, watching)
    {
        Severity = severity;
    }

    public override string ToString()
    {
        string watchingString = string.Join("|", Watching);

        return $"{TicketID},{Summary},{Status},{Priority},{Submitter},{Assigned},{watchingString},{Severity}";
    }
}
