import random;
import math;

tuple1=tuple(random.randint(1,20)for _ in range(10))
print("Кортеж: ", tuple1)
print("Min: ", min(tuple1))
print("Min: ", max(tuple1))
print("Sum = ", sum(tuple1))
print("average = ", sum(tuple1)/len(tuple1))

list1=list(tuple1)
print("list size in bytes = ", list1.__sizeof__())
print("tuple size in bytes = ", tuple1.__sizeof__())

tuple2 = (11, 12, 13, [10, 20, 30])
print("first element: ", tuple2[3][0])
print("last element: ", tuple2[3][-1])

a, b, c, *rest = tuple2
print("a:", a)
print("b:", b)
print("c:", c)
print("rest:", rest)


employees = {
    "Ivanov" : 10000,
    "Petrov" : 50000,
    "Sergeev" :15000
    }
print("Ivanov salary:", employees["Ivanov"])
print("Smirnov salary:", employees.get("Smirnov", "There is no such person"))
employees["Smirnov"] = 12000
print("quantity of employees", len(employees))
print("list of employees", list(employees.keys()))
average_sal = sum(employees.values())/len(employees)
print("average salary=", average_sal)


order1 = {'apple', 'orange', 'banana'}
order2 = {'apple','pear', 'orange'}
common = order1 & order2
unique = order1 ^ order2
combined = order1 | order2
print("common items", common)
print("unique items:", unique)
print("combined:", combined)


def square(side):
    perimeter = side * 4
    area = side * side
    diagonal = math.sqrt(2) * side
    return perimeter, area, diagonal
side_len = 2
print("square parameters with side=", side_len, ":", square(side_len))


def is_year_leap(year):
    return year % 4 == 0 and (year % 100 != 0 or year % 400 == 0)
print("year 2000 is leap:", is_year_leap(2000))
print("year 2100 is leap:", is_year_leap(2100))
