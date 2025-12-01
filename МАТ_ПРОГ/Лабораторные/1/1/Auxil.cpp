#include "Auxil.h" 
#include <ctime>  

namespace auxil
{
    void start()                          
    {
        srand((unsigned)time(NULL));
    };
    double dget(double rmin, double rmax) 
    {
        return ((double)rand() / (double)RAND_MAX) * (rmax - rmin) + rmin;
    };
    int iget(int rmin, int rmax)         
    {
        return (int)dget((double)rmin, (double)rmax);
    };
    long long fibonacci(int n)
    {
        if (n <= 1) return n;
        return fibonacci(n - 1) + fibonacci(n - 2);
    }
}
