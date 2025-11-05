namespace HW_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 2 Задание
            List<Person> listPerson = new List<Person>
            {
                new Person("Алексей", "Земсков"),
                new Person("Иван", "Бочкарев"),
                new Person("Сергей", "Баринов"),
                new Person("Александр", "Лукачев"),
                new Person("Максим", "Трифонов")
            };
            NumberReaderForSort reader = new NumberReaderForSort();
            reader.NumberReaderForSortEvent += number => // Этот способ сортировки я нашел в интернете, который осуществляется через linq
            {
                if (number == 1)
                {
                    var sorted = listPerson.OrderBy(p => p.Surname).ToList();
                    Console.WriteLine("Сортировка А-Я:");
                    foreach (var person in sorted)
                        Console.WriteLine($"{person.Surname} {person.Name}");
                }
                else if (number == 2)
                {
                    var sorted = listPerson.OrderByDescending(p => p.Surname).ToList();
                    Console.WriteLine("Сортировка Я-А:");
                    foreach (var person in sorted)
                        Console.WriteLine($"{person.Surname} {person.Name}");
                }
            };
            while (true) // здесь можно убрать while, чтобы проверить другое задание
            {
                try
                {
                    reader.Reader();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введено не корректное значение!");

                }
                catch (MyException)
                {
                    Console.WriteLine("Это мое исключение");
                }
            }
            // 1 Задание
            Exception[] exceptions = new Exception[] 
            {
                new KeyNotFoundException("Не удается найти указанный ключ для доступа к элементу в коллекции."),
                new ObjectDisposedException("Операция выполняется над объектом, который был ликвидирован"),
                new MyException("Это моя ошибка)"),
                new OverflowException("Арифметическое, приведение или операция преобразования приводят к переполнению."),
                new ArgumentNullException("Аргумент, передаваемый в метод — null")
            };
            foreach (Exception ex in exceptions)
            {
                try
                {
                    throw ex;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Исключение:" + exception.GetType());
                } finally 
                { 
                    Console.WriteLine("Finally!");
                }
            }
        }
    }

    class NumberReaderForSort
    {
        public delegate void NumberEnteredDelegate(int number);
        public event NumberEnteredDelegate? NumberReaderForSortEvent;

        public void Reader()
        {
            Console.WriteLine("Введите число 1 для сортировки от А-Я, либо 2 для Я-А");
            int number = Convert.ToInt32(Console.ReadLine());

            if (number != 1 && number != 2) throw new FormatException();

            NumberEntered(number);
        }
        protected virtual void NumberEntered(int number)
        {
            NumberReaderForSortEvent?.Invoke(number);
        }
    }
    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
    class MyException: Exception
    {
        public MyException(string message) : base(message) 
        { 

        }
    }
}
