class EnhancementTicket : Ticket
{
    private double estimate;

    public string Software { get; set; }
    public decimal Cost { get; set; }
    public string Reason { get; set; }
    public int Estimate { get; set; }

    public EnhancementTicket(string ticketID, string summary, string status, string priority, string submitter,
                              string assigned, List<string> watching, string software, decimal cost,
                              string reason, int estimate)
        : base(ticketID, summary, status, priority, submitter, assigned, watching)
    {
        Software = software;
        Cost = cost;
        Reason = reason;
        Estimate = estimate;
    }

    public EnhancementTicket(string ticketID, string summary, string status, string priority, string submitter, string assigned, List<string> watching, string? software, decimal cost, string? reason, double estimate) : base(ticketID, summary, status, priority, submitter, assigned, watching)
    {
        Software = software;
        Cost = cost;
        Reason = reason;
        this.estimate = estimate;
    }

    public override string ToString()
    {
        return base.ToString() + $", {Software}, {Cost}, {Reason}, {Estimate}";
    }
}