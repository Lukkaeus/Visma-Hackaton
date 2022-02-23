namespace Hackaton.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string DateOfCreation { get; set; }
        public string DateOfExpiration { get; set; }
        public string ExpectedDuration { get; set; }


    }
}
