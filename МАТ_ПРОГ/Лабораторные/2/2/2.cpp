#include <iostream>
#include "Combi.h"
#include "Salesman.h"
#include <iomanip>
#include <ctime>

#define N 10
#define INF 0x7fffffff

void TestSubsets()
{
    std::cout << std::endl << " - Генератор множества всех подмножеств -";
    char AA[][2] = { "A", "B", "C", "D" };
    std::cout << std::endl << "Исходное множество: { A, B, C, D }";
    combi::subset s1(sizeof(AA) / 2);
    int n = s1.getfirst();
    while (n >= 0)
    {
        std::cout << std::endl << "{ ";
        for (int i = 0; i < n; i++)
            std::cout << AA[s1.ntx(i)] << ((i < n - 1) ? ", " : " ");
        std::cout << "}";
        n = s1.getnext();
    }
    std::cout << std::endl << "Всего: " << s1.count() << std::endl;
}
void TestCombinations()
{
    std::cout << std::endl << " - Генератор сочетаний -";
    char AA[][2] = { "A", "B", "C", "D" };
    std::cout << std::endl << "Исходное множество: { A, B, C, D }";
    combi::comb c1(sizeof(AA) / 2, 2);
    int n = c1.getfirst();
    while (n >= 0)
    {
        std::cout << std::endl << "{ ";
        for (int i = 0; i < n; i++)
            std::cout << AA[c1.ntx(i)] << ((i < n - 1) ? ", " : " ");
        std::cout << "}";
        n = c1.getnext();
    }
    std::cout << std::endl << "Всего: " << c1.count() << std::endl;
}

void TestPermutations()
{
    std::cout << std::endl << " - Генератор перестановок -";
    char AA[][2] = { "A", "B", "C", "D" };
    std::cout << std::endl << "Исходное множество: { A, B, C, D }";
    combi::permutation p1(sizeof(AA) / 2);
    int n = p1.getfirst();
    while (n >= 0)
    {
        std::cout << std::endl << "{ ";
        for (int i = 0; i < n; i++)
            std::cout << AA[p1.ntx(i)] << ((i < n - 1) ? ", " : " ");
        std::cout << "}";
        n = p1.getnext();
    }
    std::cout << std::endl << "Всего: " << p1.count() << std::endl;
}

void TestArrangements()
{
    std::cout << std::endl << " - Генератор размещений -";
    char AA[][2] = { "A", "B", "C", "D" };
    std::cout << std::endl << "Исходное множество: { A, B, C, D }";
    combi::arr a1(sizeof(AA) / 2, 2);
    int n = a1.getfirst();
    while (n >= 0)
    {
        std::cout << std::endl << "{ ";
        for (int i = 0; i < n; i++)
            std::cout << AA[a1.ntx(i)] << ((i < n - 1) ? ", " : " ");
        std::cout << "}";
        n = a1.getnext();
    }
    std::cout << std::endl << "Всего: " << a1.count() << std::endl;
}

void generateDistances(int n, int* d)
{
    srand(time(0));
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (i == j)
                d[i * n + j] = 0;
            else
                d[i * n + j] = (rand() % 291) + 10;
        }
    }

    d[(rand() % n) * n + (rand() % n)] = INF;
    d[(rand() % n) * n + (rand() % n)] = INF;
    d[(rand() % n) * n + (rand() % n)] = INF;
}

void printDistances(int n, int* d)
{
    std::cout << "-- матрица расстояний --" << std::endl;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (d[i * n + j] == INF)
                std::cout << std::setw(4) << "INF";
            else
                std::cout << std::setw(4) << d[i * n + j];
        }
        std::cout << std::endl;
    }
}

int main()
{
    setlocale(LC_ALL, "rus");

    std::cout << "Задание 1. Разобрать и разработать генератор подмножеств заданного множества" << std::endl;
    TestSubsets();

    std::cout << std::endl << "Задание 2. Разобрать и разработать генератор сочетаний" << std::endl;
    TestCombinations();

    std::cout << std::endl << "Задание 3. Разобрать и разработать генератор перестановок" << std::endl;
    TestPermutations();

    std::cout << std::endl << "Задание 4. Разобрать и разработать генератор размещений" << std::endl;
    TestArrangements();

    std::cout << std::endl << "Задание 5. Решить задачу коммивояжера и результат занести в отчет:" << std::endl;
    std::cout << "расстояния сгенерировать случайным образом: 10 городов, расстояния 10 – 300 км, 3 расстояния между городами задать бесконечными" << std::endl;

    int d[N * N];
    generateDistances(N, d);

    std::cout << "-- Задача коммивояжера -- " << std::endl;
    std::cout << "-- количество городов: " << N << std::endl;
    printDistances(N, d);

    int r[N];
    int s = salesman(N, d, r);

    std::cout << "-- оптимальный маршрут: ";
    for (int i = 0; i < N; i++) std::cout << r[i] << "-->";
    std::cout << 0 << std::endl;
    std::cout << "-- длина маршрута: " << s << std::endl;

    std::cout << std::endl << "Задание 6. Исследовать зависимость времени вычисления необходимое для решения задачи коммивояжера от размерности задачи:" << std::endl;
    std::cout << "6 – 12 городов" << std::endl;

    const int minCities = 6;
    const int maxCities = 12;

    for (int n = minCities; n <= maxCities; n++)
    {
        int* d = new int[n * n];
        generateDistances(n, d);

        std::cout << std::endl << "-- количество городов: " << n << std::endl;
        printDistances(n, d);

        int* r = new int[n];
        clock_t startTime = clock();
        int s = salesman(n, d, r);
        clock_t endTime = clock();

        double duration = double(endTime - startTime) / CLOCKS_PER_SEC;
        std::cout << "-- оптимальный маршрут: ";
        for (int i = 0; i < n; i++) std::cout << r[i] << "-->";
        std::cout << 0 << std::endl;
        std::cout << "-- длина маршрута: " << s << std::endl;
        std::cout << "-- время выполнения: " << duration << " секунд" << std::endl;

        delete[] d;
        delete[] r;
    }

    system("pause");
    return 0;
}

