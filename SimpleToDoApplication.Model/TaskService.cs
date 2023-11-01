namespace SimpleToDoApplication.Model;

public class TaskService
{
    public List<Task> TaskList = new();

    public void AddTask(string title, string description, DateTime completionDateTime, bool isCompleted = false)
    {
        var newTask = new Task(title, description, isCompleted, completionDateTime);
        TaskList.Add(newTask);
    }

    public void DeleteTask(int index)
    {
        TaskList.RemoveAt(index);
    }

    public void Save()
    {
        var streamWriter =
            new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Data.txt");

        foreach (var task in TaskList)
        {
            streamWriter.WriteLine(task.Title);
            streamWriter.WriteLine(task.Description);
            streamWriter.WriteLine(task.CompletionDateTime);
            streamWriter.WriteLine(task.IsCompleted);
        }

        streamWriter.Close();
    }

    public void Load()
    {
        var streamReader =
            new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Data.txt");

        while (true)
        {
            if (streamReader.Peek() == -1)
            {
                streamReader.Close();
                return;
            }

            var title = streamReader.ReadLine();
            var description = streamReader.ReadLine();
            var dateTime = DateTime.Parse(streamReader.ReadLine());
            var isCompleted = Convert.ToBoolean(streamReader.ReadLine());
            
            AddTask(title, description, dateTime, isCompleted);
        }
    }
}