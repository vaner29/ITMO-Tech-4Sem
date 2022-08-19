#include "pch.h"
#include <cstdio>
extern "C"
{

    __declspec (dllexport) void __stdcall helloThere()
    {

        printf("General Kenobi");

    }

}