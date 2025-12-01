#include <iostream>
#include <vector>
#include <limits>
#include <algorithm>
#include <locale.h>
#include "Combi.h" 

const int INF = std::numeric_limits<int>::max();
const int N = 5;

int dist[N][N] = {
    {INF, 2, 22, INF, 1},
    {1, INF, 16, 67, 83},
    {3, 3, INF, 86, 50},
    {18, 57, 4, INF, 3},
    {92, 67, 52, 14, INF}
};

std::vector<bool> visited(N, false);
std::vector<int> path(N);
int bestCostBranchAndBound = INF;
std::vector<int> bestPathBranchAndBound;

void branchAndBound(int currPos, int count, int cost) {
    if (count == N && dist[currPos][0] != INF) {
        int totalCost = cost + dist[currPos][0];
        if (totalCost < bestCostBranchAndBound) {
            bestCostBranchAndBound = totalCost;
            bestPathBranchAndBound = path;
        }
        return;
    }

    for (int i = 0; i < N; ++i) {
        if (!visited[i] && dist[currPos][i] != INF) {
            visited[i] = true;
            path[count] = i;
            branchAndBound(i, count + 1, cost + dist[currPos][i]);
            visited[i] = false;
        }
    }
}

long long calculateRouteCost(const std::vector<int>& route, const int dist[N][N]) {
    long long totalCost = 0;
    for (size_t i = 0; i < route.size() - 1; ++i) {
        if (dist[route[i]][route[i + 1]] == INF) return INF;
        totalCost += dist[route[i]][route[i + 1]];
    }
    if (dist[route.back()][route.front()] == INF) return INF;
    totalCost += dist[route.back()][route.front()];
    return totalCost;
}

int main() {
    setlocale(LC_ALL, "Russian");

    combi::permutation p(N);
    int n = p.getfirst();
    std::vector<int> currentRoute(N);
    for (int i = 0; i < N; ++i) {
        currentRoute[i] = p.ntx(i);
    }

    long long bestCostPermutation = INF;
    std::vector<int> bestRoutePermutation;

    while (n >= 0) {
        long long currentCost = calculateRouteCost(currentRoute, dist);
        if (currentCost < bestCostPermutation) {
            bestCostPermutation = currentCost;
            bestRoutePermutation = currentRoute;
        }
        n = p.getnext();
        for (int i = 0; i < N; ++i) {
            currentRoute[i] = p.ntx(i);
        }
    }

    std::cout << "Минимальная стоимость (перестановки): " << bestCostPermutation << std::endl;
    std::cout << "Оптимальный маршрут: ";
    for (int i : bestRoutePermutation) {
        std::cout << i + 1 << " ";
    }
    std::cout << bestRoutePermutation.front() + 1 << std::endl;

    return 0;
}