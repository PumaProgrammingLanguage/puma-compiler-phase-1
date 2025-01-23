The Puma Programming Language Project

The goal of this project is to write an LLVM compiler for the Puma Programming Language. Puma is a new programming language that is organized, maintainable, safe, efficient, reliable and readable.

Goals
The current goal of this project is to write a compiler for the Puma Programming Language in three phases.

Phase A
Write a Puma to C/C++ translator that calls the clang compiler to finished building the application. This translator does not have to be fast building the app but the executable should be approximately as fast as C/C++.

Phase B
Refactor the Phase A compiler by replacing the current language that the compiler is written in the Puma Programming Language itself.  At the end of this phase, the Puma compiler shall be able to build itself.  This compiler should be faster at building Puma apps as the Phase A compiler and the executables should be as fast.

Phase C
Refactor the Phase A compiler to translate to the LLVM Intermediate language instead of C/C++.  The LLVM backend will finish the build.  This compiler should be faster at building Puma apps compared to Phase B but the executable may be slower due to the fact that it may not be as optimized as the clang compiler. Future version of this compiler should generate an executable that approach the speed of the Phase B compiler.

Standard Library
Write a standard library that is convenient to use.  Advance features may be added but defaults must keep the components of the library convenient.

Documents
The specification and code of conduct documents are found in the doc folder.  


