using SimpleToDoApplication.Model;

namespace SimpleToDoApplication.Interface;

public class App
{
    public static TaskService TaskService = new();

    public static void Run()
    {
        try
        {
            TaskService.Load();
        }
        catch (Exception e)
        {
            // ignored
        }

        Console.WriteLine("Добро пожаловать в приложение Трекер задач!");
        Console.WriteLine();
        PrintHelp();

        while (true)
        {
            var input = Console.ReadLine().ToLower();
            Console.WriteLine();

            if (input.Contains("help"))
                PrintHelp();

            else if (input.Contains("show"))
            {
                try
                {
                    Show();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Список задач пуст.");
                    Console.WriteLine();
                }
            }

            else if (input.Contains("add"))
                Add();

            else if (input.Contains("delete"))
                Delete(input);

            else if (input.Contains("exit"))
                break;

            else
                Console.WriteLine("Команда не распознана!");
        }
    }

    private static void PrintHelp()
    {
        Console.WriteLine("Доступные команды:");
        Console.WriteLine("Команда help - вывести все доступые команды");
        Console.WriteLine("Команда show - отобразить все созданные задачи в виде списка");
        Console.WriteLine("Команда add - добавить задачу");
        Console.WriteLine("Команда delete {номер_задачи} - удалить задачу");
        Console.WriteLine("Команда exit - закрыть программу");
        Console.WriteLine();
    }

    private static void Show()
    {
        var count = 1;

        if (TaskService.TaskList.Count == 0)
        {
            throw new Exception();
        }

        Console.WriteLine("Список задач: ");

        foreach (var task in TaskService.TaskList)
        {
            Console.WriteLine($"{count}. {task.Title}");
            Console.WriteLine($"{task.Description}");

            if (task.IsCompleted)
                Console.WriteLine("Статус: выполнено");

            else
            {
                Console.WriteLine("Статус: не выполнено");
                Console.WriteLine($"Необходимо выполнить до: {task.CompletionDateTime}");
            }

            Console.WriteLine();

            count++;
        }
    }

    private static void Add()
    {
        Console.Write("Введите название задачи: ");
        var title = Console.ReadLine();
        
        Console.Write("Введите описание задачи: ");
        var description = Console.ReadLine();

        Console.Write("До какого дня необходимо выполнить (в формате дд.мм.гггг): ");
        var rawDate = Console.ReadLine();

        Console.Write("До какого времени необходимо выполнить (в формате чч:мм): ");
        var rawTime = Console.ReadLine();
        
        var date = rawDate.Split('.').Select(int.Parse).ToList();
        var time = rawTime.Split(':').Select(int.Parse).ToList();
        var dateTime = new DateTime(date[2], date[1], date [0], time[0], time[1], 0);
        
        TaskService.AddTask(title, description, dateTime);
        TaskService.Save();
    
        Console.WriteLine("Задача добавлена!");
        Console.WriteLine();
    }

    private static void Delete(string rawInput)
    {
        var input = rawInput.Split();

        if (input.Length != 2)
        {
            throw new Exception();
        }

        TaskService.DeleteTask(Convert.ToInt32(input[1]) - 1);
        Console.WriteLine($"Задача под номером {input[1]} удалена!");
        Console.WriteLine();
    }
}

// TODO: при добавлении Task указывать его название
// TODO: добавить функцию, чтобы изменять Task