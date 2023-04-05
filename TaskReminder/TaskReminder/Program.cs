namespace TaskReminder
{
    internal class Program
    {
        public class Task
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateTime { get; set; }
        }
        static class TaskManager
        {
            public delegate void TaskReminder(string TaskName, DateTime RemindTime);

            public static event TaskReminder ReminderEvent;

            public static readonly List<Task> tasks = new List<Task>();
            public static void AddTasks(Task task)
            {
                tasks.Add(task);
            }
            public static void RemoveTasks(Task task)
            {
                tasks.Remove(task);
            }
            public static void Check()
            {
                foreach(var t in tasks)
                {
                    if (t.DateTime <= DateTime.Now)
                    {
                        if (ReminderEvent!= null)
                        ReminderEvent.Invoke(t.Name,t.DateTime);
                    }
                }
            }
        }
        static void Show(string TaskName, DateTime RemindTime)
        {
            Console.WriteLine($"You must do {TaskName}, at {RemindTime}.");
        }
        static void Main(string[] args)
        {
            TaskManager.ReminderEvent += Show;
            //TaskManager.AddTasks(new Task
            //{
            //    Name = "Task1",
            //    Description = "Desc1",
            //    DateTime = DateTime.Now.AddSeconds(20),
            //});
            TaskManager.AddTasks(new Task
            {
                Name = "Task2",
                Description = "Desc2",
                DateTime = DateTime.Now.AddSeconds(2),
            });
            int count = 0;
            while (count <=3)
            {
                TaskManager.Check();
                Thread.Sleep(1000);
                count++;
            }
        }
    }
}