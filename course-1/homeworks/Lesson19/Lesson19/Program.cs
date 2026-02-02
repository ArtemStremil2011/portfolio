using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lesson19
{
    class PRogram
    {
        //Задание 1. Работа со списками 
        //Шаг 1. Создайте список чисел.
        //var numbers = new List<int> { 12, 5, 8, 19, 3 };
        //Шаг 2. Выведите исходный список.
        //Console.WriteLine("Исходный список: " + string.Join(" ", numbers));
        //Шаг 3. Отсортируйте.
        //numbers.Sort();Console.WriteLine("Отсортированный: " + string.Join(" ", numbers));
        //Шаг 4. Найдите минимум и максимум.
        //int min = numbers[0]; int max = numbers[^1]; // последний элементConsole.WriteLine($"Минимум: {min}, Максимум: {max}");
        //Шаг 5. Переверните порядок.
        //numbers.Reverse(); Console.WriteLine("После Reverse: " + string.Join(" ", numbers));

        //public static void Main(string[] args)
        //{
        //    var numbers = new List<int>() { 12, 5, 8, 19, 3 };

        //    Console.WriteLine($"Исходный список: {string.Join(" ", numbers)}");

        //    numbers.Sort(); 

        //    Console.WriteLine("Отсортированный: " + string.Join(" ", numbers));

        //    int min = numbers[0]; 
        //    int max = numbers[^1]; // последний элемент

        //    Console.WriteLine($"Минимум: {min}, Максимум: {max}");

        //    numbers.Reverse(); 

        //    Console.WriteLine("После Reverse: " + string.Join(" ", numbers));
        //}


        //Задание 2. Работа со словарем
        //Шаг 1. Создайте словарь.
        //var phoneBook = new Dictionary<string, string>();
        //Шаг 2. Добавьте записи.
        //phoneBook.Add("Анна", "8921-123-45-67"); phoneBook.Add("Иван", "8931-555-77-88");phoneBook.Add("Ольга", "8905-111-22-33");
        //Шаг 3. Получите телефон существующего контакта.
        //string name = "Иван";if (phoneBook.ContainsKey(name))    Console.WriteLine($"Телефон {name}: {phoneBook[name]}");
        //Шаг 4. Безопасно обратитесь к несуществующему контакту.
        //name = "Пётр";if (phoneBook.TryGetValue(name, out var phone))    Console.WriteLine($"Телефон {name}: {phone}");else    Console.WriteLine($"Контакт {name} не найден");

        //public static void Main(string[] args)
        //{
        //    var phoneBook = new Dictionary<string, string>();

        //    phoneBook.Add("Анна", "8921-123-45-67"); 
        //    phoneBook.Add("Иван", "8931-555-77-88"); 
        //    phoneBook.Add("Ольга", "8905-111-22-33"); 

        //    string name = "Иван";
        //    if (phoneBook.ContainsKey(name))
        //    {
        //        Console.WriteLine($"Телефон {name}: {phoneBook[name]}");
        //    }

        //    name = "Пётр";
        //    if (phoneBook.TryGetValue(name, out var phone))
        //    {
        //        Console.WriteLine($"Телефон {name}: {phone}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Контакт {name} не найден");
        //    }
        //}

        //Задание 3. Работа с очередью
        //Шаг 1. Создайте очередь.
        //var clients = new Queue<string>();
        //Шаг 2. Поставьте в очередь имена.
        //clients.Enqueue("Анна"); clients.Enqueue("Иван");clients.Enqueue("Мария");clients.Enqueue("Олег");
        //Шаг 3. Посмотрите, кто первый.
        //Console.WriteLine($"Первый в очереди: {clients.Peek()}");
        //Шаг 4. Обслужите всех по порядку.
        //while (clients.Count > 0) { string c = clients.Dequeue(); Console.WriteLine($"Обслужен клиент: {c}"); }
        //Console.WriteLine("Очередь пуста");

        //public static void Main(string[] args)
        //{
        //    var clients = new Queue<string>();

        //    clients.Enqueue("Анна");
        //    clients.Enqueue("Иван");
        //    clients.Enqueue("Мария");
        //    clients.Enqueue("Олег");

        //    Console.WriteLine($"Первый в очереди: {clients.Peek()}");

        //    while (clients.Count > 0) 
        //    {
        //        string c = clients.Dequeue();
        //        Console.WriteLine($"Обслужен клиент: {c}"); 
        //    }
        //}

        //Задание 4. Работа со стеком
        //Шаг 1. Создайте стек.
        //var actions = new Stack<string>();
        //Шаг 2. Добавьте действия.
        //actions.Push("Открыт документ"); actions.Push("Написан текст");actions.Push("Удалён абзац");
        //Шаг 3. Посмотрите верхнее действие.
        //Console.WriteLine($"Верхнее действие: {actions.Peek()}");
        //Шаг 4. Делайте отмену по одному действию.
        //while (actions.Count > 0){    string act = actions.Pop(); Console.WriteLine($"Отмена действия: {act}");    Console.WriteLine($"Осталось действий: {actions.Count}");}

        //public static void Main(string[] args)
        //{
        //    var actions = new Stack<string>();

        //    actions.Push("Открыт документ"); 
        //    actions.Push("Написан текст"); 
        //    actions.Push("Удалён абзац");

        //    Console.WriteLine($"Верхнее действие: {actions.Peek()}");

        //    while (actions.Count > 0) 
        //    {
        //        string act = actions.Pop();
        //        Console.WriteLine($"Отмена действия: {act}");
        //        Console.WriteLine($"Осталось действий: {actions.Count}"); 
        //    }
        //}

        //Задание 5. List + Queue + Dictionary
        //Шаг 1. Создайте меню с ценами.
        //var prices = new Dictionary<string, int> { { "Кофе", 150 }, { "Чай", 100 }, { "Сэндвич", 250 } };
        //Шаг 2. Создайте очередь клиентов.
        //var customers = new Queue<string>(); customers.Enqueue("Анна");customers.Enqueue("Иван");
        //Шаг 3. Определите список заказа(для примера одинаковый для всех).
        //var orderItems = new List<string> { "Кофе", "Сэндвич" };
        //Шаг 4. Обслужите каждого клиента и посчитайте сумму.
        //Console.WriteLine("Обслуживание клиентов:");while (customers.Count > 0){    var client = customers.Dequeue(); Console.WriteLine($"\nКлиент {client}:");    int total = 0;    foreach (var item in orderItems)    {        int price = prices[item]; total += price;        Console.WriteLine($"  {item} -- {price} руб.");    }
        //Console.WriteLine($"Итого: {total} руб.");}

        //public static void Main(string[] args)
        //{
        //    var prices = new Dictionary<string, int>
        //    {
        //        { "Кофе", 150 },
        //        { "Чай", 100 },
        //        { "Сэндвич", 250 }
        //    };

        //    var customers = new Queue<string>();

        //    customers.Enqueue("Анна");
        //    customers.Enqueue("Иван");

        //    var orderItems = new List<string>
        //    {
        //        "Кофе",
        //        "Сэндвич"
        //    };

        //    Console.WriteLine("Обслуживание клиентов:");
        //    while (customers.Count > 0)
        //    {
        //        var client = customers.Dequeue();
        //        Console.WriteLine($"\nКлиент {client}:");
        //        int total = 0;

        //        foreach (var item in orderItems)
        //        {
        //            int price = prices[item];
        //            total += price;
        //            Console.WriteLine($"  {item} -- {price} руб.");
        //        }

        //        Console.WriteLine($"Итого: {total} руб.");
        //    }
        //
        //}
    }
}