class Employee:
    def __init__(self, name, salary):
        self.__name = name
        self.__salary = salary
    
    def set_salary(self, salary):
        if 0< salary < 50000:
            self.__salary= salary
        else:
            print("Недопустимая зарплата")

    def get_salary(self):
        return self.__salary

    def get_name(self):
        return self.__name

    def print_employee(self):
        print(F"Имя:{self.__name} \tЗарплата:{self.__salary}")

    def work(self):
        return "Я работаю в компании"
    
    def __str__(self):
        return f"Сотрудник: {self.name}, Зарплата: {self.salary}"
class Manager(Employee):
    def __init__(self, name, salary, bonus, experience):
        super().__init__(name, salary)
        self.bonus = bonus
        self.experience = experience

    def work(self):
        return super().work() + ". Я менеджер"
    
    def premia(self):
        if 5 <= self.experience <= 10:
            self.salary += self.bonus * 1.3
        elif self.experience > 11:
            self.salary += self.bonus * 1.7
        else:
            self.salary += self.bonus

    def __str__(self):
        return super().__str__() + f", Стаж: {self.experience} лет."

class Developer(Employee):
    def __init__(self, name, salary, bonus, experience):
        super().__init__(name, salary)
        self.bonus = bonus
        self.experience = experience

    def work(self):
        return super().work() + ". Я разработчик"
    
    def premia(self):
        if 3 <= self.experience <= 7:
            self.salary += self.bonus * 1.4
        elif self.experience > 8:
            self.salary += self.bonus * 1.8
        else:
            self.salary += self.bonus

    def __str__(self):
        return super().__str__() + f", Стаж: {self.experience} лет."


class Intern(Employee):
    def __init__(self, name, salary, bonus, experience):
        super().__init__(name, salary)
        self.bonus = bonus
        self.experience = experience

    def work(self):
        return super().work() + ". Я интерн"
    
    def premia(self):
        if 4 <= self.experience <= 8:
            self.salary += self.bonus * 1.2
        elif self.experience > 9:
            self.salary += self.bonus * 1.6
        else:
            self.salary += self.bonus
        
    def __str__(self):
        return super().__str__() + f", Стаж: {self.experience} лет."


employees = [
    Manager("Ульяна", 5000,200, 9),
    Developer("Борис", 4350,150, 20),
    Intern("Виктор", 4800, 165, 10),
    Developer("Галина", 2000, 95, 2)
]

for emp in employees:
    print(emp)
    print(emp.work())
    print()

employees[0].premia()
employees[2].premia()
employees[3].premia()

print("После премии:")
for emp in employees:
    print(emp)

total_salary = sum(emp.salary for emp in employees)
print(f"Общая зарплата: {total_salary}")

highest_paid = max(employees, key=lambda emp: emp.salary)
print(f"Сотрудник с самой высокой зарплатой: {highest_paid}")


tom = Employee("Tom",500 )
tom.print_employee()
tom.set_salary(-5)
tom.set_salary(600)
tom.print_employee()




