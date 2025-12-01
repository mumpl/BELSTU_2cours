#pragma once
namespace combi
{
    struct subset
    {
        short n;
        short sn;
        short* sset;
        unsigned __int64 mask;

        subset(short n = 1);
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned __int64 count();
        void reset();
    };
    struct comb
    {
        short n;
        short k;
        short* sset;
        comb(short n, short k);
        short getfirst();
        short getnext();
        short ntx(short i);
        unsigned __int64 count();
        void reset();
    };

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
};
