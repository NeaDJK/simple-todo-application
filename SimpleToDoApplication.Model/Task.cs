namespace SimpleToDoApplication.Model;

public class Task
{
    public string Title;
    public string Description;
    public bool IsCompleted;
    public DateTime CompletionDateTime;

    public Task(string title, string description, bool isCompleted, DateTime completionDateTime)
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
        CompletionDateTime = completionDateTime;
    }
}