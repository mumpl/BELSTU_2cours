#include <algorithm>
#include "Combi.h"

namespace combi
{
    perm::perm(short n)
    {
        this->n = n;
        this->sset = new short[n];
        this->reset();
    }

    void perm::reset()
    {
        for (short i = 0; i < n; i++)
            this->sset[i] = i;
    }

    short perm::getfirst()
    {
        this->reset();
        return n;
    }

    short perm::getnext()
    {
        if (std::next_permutation(sset, sset + n))
            return n;
        return -1;
    }

    short perm::ntx(short i)
    {
        return this->sset[i];
    }

    unsigned __int64 perm::count()
    {
        unsigned __int64 result = 1;
        for (short i = 2; i <= n; i++)
            result *= i;
        return result;
    }
};

namespace combi
{
    arr::arr(short n, short k)
    {
        this->n = n;
        this->k = k;
        this->sset = new short[k];
        this->reset();
    }

    void arr::reset()
    {
        for (short i = 0; i < k; i++)
            this->sset[i] = i;
    }

    short arr::getfirst()
    {
        this->reset();
        return k;
    }

    short arr::getnext()
    {
        for (short i = k - 1; i >= 0; i--)
        {
            if (sset[i] < n - 1)
            {
                sset[i]++;
                for (short j = i + 1; j < k; j++)
                    sset[j] = 0;
                return k;
            }
        }
        return -1;
    }

    short arr::ntx(short i)
    {
        return this->sset[i];
    }

    unsigned __int64 arr::count()
    {
        unsigned __int64 result = 1;
        for (short i = 0; i < k; i++)
            result *= n;
        return result;
    }

    permutation::permutation(short n)
    {
        this->n = n;
        this->sset = new short[n];
        this->reset();
    }

    void permutation::reset()
    {
        for (short i = 0; i < n; i++)
            this->sset[i] = i;
    }

    short permutation::getfirst()
    {
        this->reset();
        return n;
    }

    short permutation::getnext()
    {
        if (std::next_permutation(sset, sset + n))
            return n;
        return -1;
    }

    short permutation::ntx(short i)
    {
        return this->sset[i];
    }

    unsigned __int64 permutation::count()
    {
        unsigned __int64 result = 1;
        for (short i = 2; i <= n; i++)
            result *= i;
        return result;
    }
};