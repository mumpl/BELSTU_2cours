#include <algorithm>
#include "Combi.h"

namespace combi
{
    subset::subset(short n)
    {
        this->n = n;
        this->sset = new short[n];
        this->reset();
    };

    void subset::reset()
    {
        this->sn = 0;
        this->mask = 0;
    };

    short subset::getfirst()
    {
        __int64 buf = this->mask;
        this->sn = 0;
        for (short i = 0; i < n; i++)
        {
            if (buf & 0x1) this->sset[this->sn++] = i;
            buf >>= 1;
        }
        return this->sn;
    };

    short subset::getnext()
    {
        int rc = -1;
        this->sn = 0;
        if (++this->mask < this->count()) rc = getfirst();
        return rc;
    };

    short subset::ntx(short i)
    {
        return this->sset[i];
    };

    unsigned __int64 subset::count()
    {
        return (unsigned __int64)(1 << this->n);
    };
};
namespace combi
{
    comb::comb(short n, short k)
    {
        this->n = n;
        this->k = k;
        this->sset = new short[k];
        this->reset();
    }

    void comb::reset()
    {
        for (short i = 0; i < k; i++)
            this->sset[i] = i;
    }

    short comb::getfirst()
    {
        this->reset();
        return k;
    }

    short comb::getnext()
    {
        int i;
        for (i = k - 1; i >= 0; i--)
        {
            if (sset[i] < n - k + i)
            {
                sset[i]++;
                for (int j = i + 1; j < k; j++)
                    sset[j] = sset[j - 1] + 1;
                return k;
            }
        }
        return -1;
    }

    short comb::ntx(short i)
    {
        return this->sset[i];
    }

    unsigned __int64 comb::count()
    {
        unsigned __int64 result = 1;
        for (short i = 1; i <= k; i++)
            result = result * (n - i + 1) / i;
        return result;
    }
};

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