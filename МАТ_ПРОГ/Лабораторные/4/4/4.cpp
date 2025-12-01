#include <iostream>
#include <string>
#include <vector>
#include <cstdlib>
#include <chrono>
#include <ctime>
#include <iomanip>
#include "Levenshtein.h"
#include "LCS.h"

std::string generateRandomString(int length) {
    const std::string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    std::string result;
    result.reserve(length);

    for (int i = 0; i < length; ++i) {
        result += alphabet[rand() % alphabet.size()];
    }

    return result;
}

int main() {
    setlocale(LC_ALL, "Russian");
    std::srand(std::time(0));

    //Задание 1
    std::cout << "Задание 1: Генерация случайных строк\n";
    std::string S1 = generateRandomString(300);
    std::string S2 = generateRandomString(200);
    std::cout << "Сгенерированная строка S1: " << S1 << "\n";
    std::cout << "Сгенерированная строка S2: " << S2 << "\n";

    std::vector<double> ks = { 1.0 / 25, 1.0 / 20, 1.0 / 15, 1.0 / 10, 1.0 / 5, 1.0 / 2, 1.0 };

    /*
    // --- Задание 2: Сравнение методов ---
    std::cout << "\nЗадание 2: Сравнение методов\n";
    std::cout << std::setw(15) << "k"
        << std::setw(25) << "Рекурсивное время (мс)"
        << std::setw(25) << "ДП время (мс)"
        << std::endl;

    for (double k : ks) {
        int len1 = static_cast<int>(k * S1.size());
        int len2 = static_cast<int>(k * S2.size());

        std::string prefix1 = S1.substr(0, len1);
        std::string prefix2 = S2.substr(0, len2);

        const char* x = prefix1.c_str();
        const char* y = prefix2.c_str();

        clock_t t1 = clock();
        int resultRec = levenshtein_r(len1, x, len2, y);
        clock_t t2 = clock();

        clock_t t3 = clock();
        int resultDP = levenshtein(len1, x, len2, y);
        clock_t t4 = clock();

        std::cout << std::setw(15) << k
            << std::setw(25) << (t2 - t1)
            << std::setw(25) << (t4 - t3)
            << std::endl;*/
            // Задание 4: Рекурсивный метод
    std::cout << "\nЗадание 4: Рекурсивный метод\n";
    char x[] = "ALBDACD";
    char y[] = "CDLDCA";

    auto t1 = std::chrono::system_clock::now();
    int l_rec = lcs(strlen(x), x, strlen(y), y);
    auto t2 = std::chrono::system_clock::now();
    std::chrono::duration<double> elapsed_rec = t2 - t1;

    std::cout << "\n-- наибольшая общая подпоследовательность - LCS(рекурсия)\n";
    std::cout << std::endl << "последовательность X: " << x;
    std::cout << std::endl << "последовательность Y: " << y;
    std::cout << std::endl << "          длина LCS: " << l_rec;
    std::cout << std::endl << "     времени прошло: " << elapsed_rec.count() * 1000000 << " мкс.\n";

    // Задание 4: Динамическое программирование
    std::cout << "\nЗадание 4: Динамическое программирование\n";
    char z[100] = "";

    auto t3 = std::chrono::system_clock::now();
    int l_dp = lcsd(x, y, z);
    auto t4 = std::chrono::system_clock::now();
    std::chrono::duration<double> elapsed_dp = t4 - t3;

    std::cout << "\n-- наибольшая общая подпоследовательность - LCS(динамическое программирование)\n";
    std::cout << std::endl << "последовательность X: " << x;
    std::cout << std::endl << "последовательность Y: " << y;
    std::cout << std::endl << "                LCS: " << z;
    std::cout << std::endl << "          длина LCS: " << l_dp;
    std::cout << std::endl << "     времени прошло: " << elapsed_dp.count() * 1000000 << " мкс.\n";

    // Сравнение времени выполнения
    std::cout << "\nСравнение времени выполнения:\n";
    std::cout << std::setw(20) << "Метод" << std::setw(30) << "Время выполнения (мкс)\n";
    std::cout << std::setw(20) << "Рекурсия" << std::setw(30) << elapsed_rec.count() * 1000000 << "\n";
    std::cout << std::setw(20) << "Динамическое программирование" << std::setw(30) << elapsed_dp.count() * 1000000 << "\n";

    return 0;
}