using System.Diagnostics;


//Считываем текст из файла Text1.txt
const string filePath = "Text1.txt";
string text;
const int numOfRepetitions = 10;
try
{
    text = File.ReadAllText(filePath);
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
    return;
}

//Разбиваем текст на слова
var words = text.Split([' ', '\n', '\r', '\t', ',', '.', ';', ':', '!', '?'], StringSplitOptions.RemoveEmptyEntries);

var stopwatch = new Stopwatch();
var time = new long[numOfRepetitions];

//Добавляем слова в List<T> с замером времени
var wordList = new List<string>();

for (var i = 0; i < numOfRepetitions; i++)
{
    stopwatch.Restart();
    wordList = words.ToList();
    stopwatch.Stop();
    time[i] = stopwatch.ElapsedMilliseconds;
}
Console.WriteLine($"Медианное значение времени добавления слов в List<T> с помощью ToList(): {CalculateMedian(time)} мс");

wordList.Clear();
for (var i = 0; i < numOfRepetitions; i++)
{
    stopwatch.Restart();
    foreach (var word in words)
    {
        wordList.Add(word);
    }
    stopwatch.Stop();
    time[i] = stopwatch.ElapsedMilliseconds;
}
Console.WriteLine($"Медианное значение времени добавления слов в List<T> с помощью цикла: {CalculateMedian(time)} мс");

wordList.Clear();
for (var i = 0; i < numOfRepetitions; i++)
{
    stopwatch.Restart();
    wordList.AddRange(words);
    stopwatch.Stop();
    time[i] = stopwatch.ElapsedMilliseconds;
}
Console.WriteLine($"Медианное значение времени добавления слов в List<T> с помощью AddRange(): {CalculateMedian(time)} мс");

//Добавляем слова в LinkedList<T> с замером времени
var wordLinkedList = new LinkedList<string>();
for (var i = 0; i < numOfRepetitions; i++)
{
    stopwatch.Restart();
    foreach (var word in words)
    {
        wordLinkedList.AddLast(word);
    }
    stopwatch.Stop();
    time[i] = stopwatch.ElapsedMilliseconds;
}

Console.WriteLine($"Медианное значение времени добавления слов в LinkedList<T>: {CalculateMedian(time)} мс");

return;

// Метод для вычисления медианного значения
static long CalculateMedian(long[] numbers)
{
    if (numbers == null || numbers.Length == 0)
        throw new ArgumentException("Массив не должен быть пустым.");

    // Сортируем массив
    Array.Sort(numbers);
    var count = numbers.Length;
    var middle = count / 2;

    // Если количество элементов нечётное, возвращаем центральный элемент
    if (count % 2 == 1)
        return numbers[middle];

    // Если чётное, возвращаем среднее арифметическое двух центральных
    return (numbers[middle - 1] + numbers[middle]) / 2;
}