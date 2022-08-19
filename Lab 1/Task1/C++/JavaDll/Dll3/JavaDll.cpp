#include "pch.h"
#include <cstdio>

extern "C"
{

    __declspec (dllexport) void __stdcall Java_CMyClass_helloThere()
    {

        printf("General Kenobi");

    }

}