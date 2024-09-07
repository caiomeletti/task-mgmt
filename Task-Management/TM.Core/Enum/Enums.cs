namespace TM.Core.Enum
{
    public enum ResultDisabling 
    {
        Disabled = 0,
        NotFound = 1,
        HasPendingTask = 2
    }

    public enum Priority
    {
        High = 0,
        Medium = 1,
        Low = 2
    }

    public enum CurrentTaskStatus
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2
    }
}
