namespace SimpleToDoApplication.Model;

public class TaskService
{
    public List<Task> TaskList = new();

    public void AddTask(string title, string description, bool isCompleted, DateTime completionDateTime)
    {
        var newTask = new Task(title, description, isCompleted, completionDateTime);
        TaskList.Add(newTask);
    }

    public void DeleteTask(int index)
    {
        TaskList.RemoveAt(index);
    }
}
