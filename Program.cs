using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Подключение библиотеки Newtonsoft.Json для работы с JSON

class Program
{
    static void Main(string[] args)
    {
        StudentManager manager = new StudentManager(); // Создание экземпляра класса менеджера студентов
        bool running = true; // Флаг для управления основным циклом программы

        while (running)
        {
            Console.Clear(); // Очистка консоли
            // Вывод пояснений для пользователя
            Console.WriteLine("Задание 1. Консольное приложение для управления списком студентов.");
            Console.WriteLine("\n1. Загрузить список студентов  \n2. Вывести список студентов  \n3. Поиск студента  \n4. Сортировка студентов  \n5. Добавить студента" +
                "\n6. Редактировать студента  \n7. Удалить студента \n8. Сохранить список студентов  \n9. Выйти");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear(); // Очистка консоли
                    manager.LoadStudentsFromFile(); // Загрузка студентов из файла JSON
                    Console.ReadKey(); // Ожидание нажатия клавиши
                    break;
                case "2":
                    Console.Clear();
                    manager.DisplayStudents(); // Отображение списка студентов
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    manager.SearchStudent(); // Поиск студента по имени
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    manager.SortStudents(); // Сортировка списка студентов
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    manager.AddStudent(); // Добавление нового студента
                    Console.ReadKey();
                    break;
                case "6":
                    Console.Clear();
                    manager.EditStudent(); // Редактирование существующего студента
                    Console.ReadKey();
                    break;
                case "7":
                    Console.Clear();
                    manager.RemoveStudent(); // Удаление студента
                    Console.ReadKey();
                    break;
                case "8":
                    Console.Clear();
                    manager.SaveStudentsToFile(); // Сохранение списка студентов в файл JSON
                    Console.ReadKey();
                    break;
                case "9":
                    running = false; // Выход из программы
                    break;
                default:
                    Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                    break;
            }
        }
    }
}

class Student // Класс Student, представляющий отдельного студента с его характеристиками
{
    public int ID { get; set; } // ID студента
    public string Name { get; set; } // Имя студента
    public string Group { get; set; } // Группа студента
    public double AverageGrade { get; set; } // Средний балл студента

    public override string ToString()
    {
        return $"ID: {ID}, Имя: {Name}, Группа: {Group}, Средний балл: {AverageGrade}"; // Форматированный вывод информации о студенте
    }
}

class StudentManager // Класс StudentManager, управляющий операциями с данными студентов
{
    private List<Student> students = new List<Student>(); // Лист для хранения списка студентов

    public void AddStudent()
    {
        try
        {
            Student student = new Student(); // Создание экземпляра класса студента
            Console.Write("Введите ID: ");
            student.ID = int.Parse(Console.ReadLine()); // Ввод и парсинг ID
            Console.Write("Введите имя: ");
            student.Name = Console.ReadLine(); // Ввод имени
            Console.Write("Введите группу: ");
            student.Group = Console.ReadLine(); // Ввод группы
            Console.Write("Введите средний балл: ");
            student.AverageGrade = double.Parse(Console.ReadLine().Replace('.', ',')); // Парсинг среднего балла с учетом ввода . и ,
            students.Add(student); // Добавление студента в список
            Console.WriteLine("Студент добавлен."); // Вывод подтверждения
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Произошла ошибка. {ex.Message}");
        }
    }

    public void RemoveStudent()
    {
        Console.Write("Введите ID студента для удаления: ");
        int id = int.Parse(Console.ReadLine()); // Ввод и парсинг ID студента для удаления
        Student student = students.Find(s => s.ID == id); // Поиск студента по ID
        if (student != null)
        {
            students.Remove(student); // Удаление студента, если найден
            Console.WriteLine("Студент удален.");
        }
        else
        {
            Console.WriteLine("Студент не найден."); // Сообщение, если студент не найден
        }
    }

    public void EditStudent()
    {
        Console.Write("Введите ID студента для редактирования: ");
        int id = int.Parse(Console.ReadLine()); // Ввод ID студента для редактирования
        Student student = students.Find(s => s.ID == id);

        if (student != null)
        {
            Console.Write("Введите новое имя: ");
            student.Name = Console.ReadLine(); // Ввод нового имени
            Console.Write("Введите новую группу: ");
            student.Group = Console.ReadLine(); // Ввод новой группы
            Console.Write("Введите новый средний балл: ");
            student.AverageGrade = double.Parse(Console.ReadLine().Replace('.', ',')); // Ввод нового среднего балла
            Console.WriteLine("Данные студента обновлены.");
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }

    public void SearchStudent()
    {
        Console.Write("Введите имя студента для поиска: ");
        string name = Console.ReadLine(); // Ввод имени для поиска
        var result = students.FindAll(s => s.Name.Contains(name)); // Поиск студентов по имени

        if (result.Count > 0)
        {
            Console.WriteLine("Найденные студенты:");
            foreach (var student in result)
            {
                Console.WriteLine(student); // Вывод найденных студентов
            }
        }
        else
        {
            Console.WriteLine("Студенты не найдены.");
        } 
    }

    public void SortStudents()
    {
        Console.WriteLine("Сортировать по: 1 - ID, 2 - Имя, 3 - Средний балл");
        string choice = Console.ReadLine(); // Ввод критерия сортировки
        switch (choice)
        {
            case "1":
                students.Sort((x, y) => x.ID.CompareTo(y.ID)); // Сортировка по ID
                break;
            case "2":
                students.Sort((x, y) => x.Name.CompareTo(y.Name)); // Сортировка по имени
                break;
            case "3":
                students.Sort((x, y) => y.AverageGrade.CompareTo(x.AverageGrade)); // Сортировка по среднему баллу (по убыванию)
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                return;
        }
        Console.WriteLine("Студенты отсортированы.");
    }

    // Сохранение списка студентов в JSON
    public void SaveStudentsToFile()
    {
        string json = JsonConvert.SerializeObject(students, Formatting.Indented); // Преобразование списка студентов в JSON
        File.WriteAllText("students.json", json); // Запись JSON в файл
        Console.WriteLine("Список студентов сохранен в файл students.json.");
    }

    // Загрузка списка студентов из JSON
    public void LoadStudentsFromFile()
    {
        if (File.Exists("students.json"))
        {
            string json = File.ReadAllText("students.json"); // Чтение JSON из файла
            students = JsonConvert.DeserializeObject<List<Student>>(json); // Десериализация JSON в список студентов
            Console.WriteLine("Список студентов загружен из файла students.json.");
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    // Отображение списка студентов
    public void DisplayStudents()
    {
        if (students.Count > 0)
        {
            Console.WriteLine("Список студентов:");
            foreach (var student in students)
            {
                Console.WriteLine(student); // Вывод информации о каждом студенте
            }
        }
        else
        {
            Console.WriteLine("Список студентов пуст.");
        }
    }
}
