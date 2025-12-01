using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main()
    {
        Dictionary<int, char> myDictionary = new Dictionary<int, char>
        {
            {1, 'a'},
            {2, 'b'},
            {3, 'c'},
            {4, 'd'},
            {5, 'e'},
            {6, 'f'},
            {7, 'g'},
            {8, 'h'}
        };
        Console.WriteLine("Начальная коллекция");
        foreach (var item in myDictionary)
        {
            Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
        }
        int n = 2;
        Console.WriteLine($"Удаление первых {n}-х элементов");
        var keysToRemove = new List<int>(myDictionary.Keys);
        for (int i = 0; i < n && i < keysToRemove.Count; i++)
        {
            myDictionary.Remove(keysToRemove[i]);
        }
        Console.WriteLine("Коллекция псле удаления");
        foreach (var item in myDictionary)
        {
            Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
        }
        myDictionary.Add(9, 'i');
        myDictionary[10] = 'j';
        Console.WriteLine("Коллекция после добавления элементов");
        foreach (var item in myDictionary)
        {
            Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
        }
        ArrayList myArrayList = new ArrayList();
        foreach (var item in myDictionary)
        {
            myArrayList.Add(item.Value);
        }
        Console.WriteLine("Коллекция ArrayList, заполненная данными из Dictionary");
        foreach(var item in myArrayList)
        {
            Console.WriteLine($"value: {item}");
        }
        char searchValue = 'h';
        bool search = myArrayList.Contains(searchValue);
        Console.WriteLine($"Поиск значения {searchValue} в ArrayList");
        if (search)
        {
            Console.WriteLine($"Значение '{searchValue}' найдено в ArrayList.");
        }
        else
        {
            Console.WriteLine($"Значение '{searchValue}' не найдено в ArrayList.");
        }
    }
    
}
