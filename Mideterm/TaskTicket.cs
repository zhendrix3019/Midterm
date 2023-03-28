class TaskTicket : Ticket
{
    public string ProjectName { get; set; }
    public DateTime DueDate { get; set; }

    public TaskTicket(string ticketID, string summary, string status, string priority, string submitter,
                        string assigned, List<string> watching, string projectName, DateTime dueDate)
        : base(ticketID, summary, status, priority, submitter, assigned, watching)
    {
        ProjectName = projectName;
        DueDate = dueDate;
    }

    public override string ToString()
    {
        return base.ToString() + $", {ProjectName}, {DueDate}";
    }
}
