using System;
class Program6
{
    static void Main()
    {
        void CheckedFunction()
        {
            try
            {
                checked
                {
                    int maxValue = int.MaxValue;
                    Console.WriteLine($"Checked: макс.значение int = {maxValue}");

                    // Попытка переполнения
                    int overflow = maxValue + 1;
                    Console.WriteLine($"Checked: После переполнения = {overflow}");
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Checked: Переполнение! {ex.Message}");
            }
        }

        // Локальная функция с блоком unchecked
        void UncheckedFunction()
        {
            unchecked
            {
                int maxValue = int.MaxValue;
                Console.WriteLine($"Unchecked: макс.значение int = {maxValue}");

                // Переполнение игнорируется
                int overflow = maxValue + 1;
                Console.WriteLine($"Unchecked: После переполнения = {overflow}"); //минимальное возм знач-е int
            }
        }

        // Вызов функций
        CheckedFunction();
        UncheckedFunction();
    }
}
