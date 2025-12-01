#include "Auxil.h"
#include <ctime>
#include <iostream>
#include <locale>
#include <vector>

#define CYCLE 1000000

using namespace auxil;
using namespace std;

int main()
{
	setlocale(LC_ALL, "rus");
	cout << "Задание 1. Демонстрация генерации случайных чисел" << endl;
	start();

	double random_double = dget(0.0, 1.0);
	cout << "Случайное действительное число от 0.0 до 1.0 = " << random_double << endl;
	
	int random_int = iget(1, 10);
	cout << "Случайное число от 1 до 10 = " << random_int << endl;

	cout << endl << "Задание 2. Замер продолжительности процесса вычисления" << endl;

	double av1 = 0, av2 = 0;
	clock_t t1 = 0, t2 = 0;
	start();

	t1 = clock();
	for (int i = 0; i < CYCLE; i++)
	{
		av1 += static_cast<double>(iget(-100, 100));
		av2 += dget(-100, 100);
	}

	t2 = clock();

	cout << endl << "количество циклов:          " << CYCLE;
	cout << endl << "среднее значение (int):     " << av1 / CYCLE;
	cout << endl << "среднее значение (double):  " << av2 / CYCLE;
	cout << endl << "продолжительность (у.е):    " << (t2 - t1);
	cout << endl << "                  (сек):    " << static_cast<double>(t2 - t1) / CLOCKS_PER_SEC;
	cout << endl;

	cout << "Задание 3. Исследование рекурсивного алгоритма (вычисление чисел Фибоначчи)" << endl;

	vector<int> fibonacci_indices = { 20, 25, 30, 35, 40 };
	for (int idx : fibonacci_indices)
	{
		t1 = clock();
		long long result = fibonacci(idx);
		t2 = clock();
		cout << "Фибоначчи (" << idx << "): " << result;
		cout << " | Время(у.е): " << (t2 - t1);
		cout << " | Время(сек): " << static_cast<double>(t2 - t1) / CLOCKS_PER_SEC;
		cout << endl;
	}
	system("pause");
	return 0;
}