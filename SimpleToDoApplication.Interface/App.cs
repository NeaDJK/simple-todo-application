using SimpleToDoApplication.Model;
namespace SimpleToDoApplication.Interface;

public class App
{
    public static TaskService TaskService = new();
    
    public static void Run()
    {
        Console.WriteLine("Добро пожаловать в приложение Трекер задач!");
        Console.WriteLine();
        PrintHelp();
        
        while (true)
        {
            var input = Console.ReadLine();
            Console.WriteLine();

            if (input.Contains("Help"))
                PrintHelp();

            else if (input.Contains("Show"))
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

            else if (input.Contains("Add"))
                Add(input);

            else if(input.Contains("Delete")) 
                Delete(input);

            else
                Console.WriteLine("Команда не распознана!");
        }
    }

    private static void PrintHelp()
    {
        Console.WriteLine("Доступные команды:");
        Console.WriteLine("Команда Help - вывести все доступые команды");
        Console.WriteLine("Команда Show - отобразить все созданные задачи в виде списка");
        Console.WriteLine("Команда Add {название_задачи} {описание} {дата (дд.мм.гггг)} {время (чч:мм)} - добавить задачу ");
        Console.WriteLine("Команда Delete {номер_задачи} - удалить задачу");
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

    private static void Add(string rawInput)
    {
        var input = rawInput.Split();
        
        if (input.Length != 5)
        {
            throw new Exception();
        }

        var date = input[3].Split('.').Select(int.Parse).ToList();
        var time = input[4].Split(':').Select(int.Parse).ToList();
        var dateTime = new DateTime(date[2], date[1], date [0], time[0], time[1], 0);
        
        TaskService.AddTask(input[1], input[2], false, dateTime);

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
// TODO: добавить поддержку команд в любом регистре