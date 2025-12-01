using System;
using System.Collections.Generic;

public delegate void MoveHandler(int offsetX, int offsetY);
public delegate void CompressHandler(double compression);

class User
{
    public string Name { get; private set; }
    public (int X, int Y) Position { get; private set; } = (0, 0);
    public double Scale { get; private set; } = 1.0;
    public event MoveHandler Move;
    public event CompressHandler Compress;
    public User (string name)
    { 
        Name = name;
    }
    public void OnMove(int  offsetX, int offsetY)
    {
        Move?.Invoke(offsetX, offsetY);
    }
    public void OnCompress(double compression)
    {
        Compress?.Invoke(compression);
    }
    public void SubscribeToMove()
    {
        Move += (offsetX, offsetY) =>
        {
            Position = (Position.X + offsetX, Position.Y + offsetY);
            Console.WriteLine($"{Name} перемещён в позицию {Position}");
        };
    }
    public void SubscribeToCompress()
    {
        Compress += compression =>
        {
            Scale *= compression;
            Console.WriteLine($"{Name} сжат с коэффициентом {Scale}");
        };
    }
    public override string ToString()
    {
        return $"Пользователь - {Name}, позиция - {Position}, масштаб - {Scale}";
    }
}
class Program
{
    static void Main()
    {
        var user1 = new User("Merlin");
        var user2 = new User("Arthur");
        var user3 = new User("Morgana");

        user1.SubscribeToMove();
        user2.SubscribeToCompress();
        user3.SubscribeToMove();
        user3.SubscribeToCompress();

        Console.WriteLine(">>>Перемещение>>>");
        user1.OnMove(3, 9);
        user3.OnMove(12, 5);

        Console.WriteLine("***Сжатие***");
        user2.OnCompress(0.2);
        user3.OnCompress(1.7);

        Console.WriteLine("#Состояние пользователей#");
        var users = new List<User> { user1, user2, user3 };   
        users.ForEach(user => Console.WriteLine(user.ToString()));
    }
}
