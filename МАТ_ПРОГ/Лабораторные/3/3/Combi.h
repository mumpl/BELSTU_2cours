#pragma once

namespace combi
{
    struct perm
    {
        short n;
        short* sset;
        perm(short n);
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned __int64 count();
        void reset();
    };
    struct permutation
    {
        short n;
        short* sset;
        permutation(short n);
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned __int64 count();
        void reset();
    };
    struct arr
    {
        short n;
        short k;
        short* sset;
        arr(short n, short k);
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned __int64 count();
        void reset();
    };
}
