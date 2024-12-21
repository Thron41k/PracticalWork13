using static System.Text.RegularExpressions.Regex;

const string filePath = "Text1.txt";

//Считываем текст из файла
if (!File.Exists(filePath))
{
    Console.WriteLine("Файл не найден.");
    return;
}

var text = File.ReadAllText(filePath);

//Разбиваем текст на слова, удаляя знаки препинания и приводя к нижнему регистру
var words = Split(text.ToLower(), @"\W+")
    .Where(word => !string.IsNullOrEmpty(word)) // Исключить пустые строки
    .ToArray();

//Считаем и выводим 10 самых часто встречающихся слов
var top10Words = words
    .GroupBy(word => word)              // Группируем одинаковые слова
    .Select(group => new
    {
        Word = group.Key,
        Count = group.Count()
    })
    .OrderByDescending(x => x.Count)    // Сортируем по убыванию частоты
    .ThenBy(x => x.Word)               // Если частота одинаковая, сортируем по алфавиту
    .Take(10);                         // Берём первые 10 элементов

Console.WriteLine("Топ 10 слов:");
foreach (var item in top10Words)
{
    Console.WriteLine($"{item.Word}: {item.Count}");
}