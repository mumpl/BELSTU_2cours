#include <iostream>
#include <fstream>
#include <vector>
#include <string>

struct Task {
    std::string start;  // Начальная задача
    std::string end;    // Конечная задача
};

// Функция для генерации графа в формате DOT (используется Graphviz)
void generateGraph(const std::vector<Task>& tasks, const std::string& filename) {
    std::ofstream file(filename);
    if (!file.is_open()) {
        std::cerr << "Ошибка открытия файла!" << std::endl;
        return;
    }

    // Начало описания графа
    file << "digraph G {" << std::endl;
    file << "    node [shape=circle, style=filled, color=lightblue];" << std::endl;

    // Добавление задач и связей
    for (const auto& task : tasks) {
        file << "    " << task.start << " -> " << task.end << ";" << std::endl;
    }

    // Завершение описания
    file << "}" << std::endl;
    file.close();

    std::cout << "Граф сохранен в файл: " << filename << std::endl;
    std::cout << "Для визуализации используйте Graphviz (например, команду dot -Tpng " << filename << " -o graph.png)." << std::endl;
}

int main() {
    std::setlocale(LC_ALL, "en_US.UTF-8");
    // Определение задач и их связей
    std::vector<Task> tasks = {
        {"Z1", "Z2"}, {"Z2", "Z3"}, {"Z3", "Z4"},
        {"Z4", "Z5"}, {"Z5", "Z6"}, {"Z6", "Z9"},
        {"Z9", "Z10"}, {"Z10", "Z14"}, {"Z14", "Z15"},
        // Дополнительные работы
        {"Z1", "Z15"}, {"Z1", "Z16"}, {"Z1", "Z17"}, {"Z1", "Z18"}
    };

    // Генерация графа
    generateGraph(tasks, "graph.dot");

    return 0;
}
