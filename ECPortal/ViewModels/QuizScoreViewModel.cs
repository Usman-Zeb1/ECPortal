public class QuizScoreViewModel
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public DateTime QuizDate { get; set; }
    public int asTarget { get; set; }
    public int AgentSatisfaction { get; set; }
    public int asPercentage { get; set; }
    public int vsTarget { get; set; }
    public int visitSatisfaction { get; set; }
    public int vsPercentage { get; set; }
    public int QuizTarget { get; set; }
    public int QuizOnline { get; set; }
    public int QuizPercentage { get; set; }
    public int RamTarget { get; set; }
    public int FatalError { get; set; }
    public int RamPercentage { get; set; }
    public int ResponsesCount { get; set; }
}
