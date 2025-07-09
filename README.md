The Puma Programming Language Project

The goal of this project is to write an LLVM compiler front-end for the Puma Programming Language. Puma is a new programming language that is safe, organized, maintainable, readable, reliable and efficient.

Goals:
The current goal of this project is to write a compiler front-end for the Puma Programming Language in three phases.

Phase 1:
Write a Puma to C/C++ translator that calls the clang compiler to finished building the application. This translator does not have to be fast at building the app but the executable should be approximately as fast as C/C++.

Phase 2:
Refactor the Phase A compiler by replacing the current language code with the Puma Programming Language itself.  At the end of this phase, the Puma compiler shall be able to build itself.  This compiler should be faster at building Puma apps than the Phase A compiler and the executables shall be as fast.

Phase 3:
Refactor the Phase A compiler to translate to the LLVM Intermediate language instead of C/C++.  The LLVM backend will finish the build.  This compiler should be faster at building Puma apps compared to Phase B.  The clang compiler will be used to learn how to write efficient Intermedeate language code.  

Standard Library:
Write a standard library that is convenient and easy to use.  Advance features may be added but defaults must keep the components of the library convenient and easy to use.

Documents:
The specification and code of conduct documents are found in the doc folder.  


Programming Language Developed
In in 2023, the foundation was laid for an exciting new programming language with the creation of the Puma Programming Language — a language designed with safety, organization, and maintainability at its core without compromising on performance. However, Puma isn’t just another new language. It’s a practical tool for writing cleaner code and fostering greater consistency across software development teams.

Key Features of Puma Programming Language
• Clean, simplified syntax with features to organize the code
• Support for both object-oriented and procedural paradigms, giving developers flexibility
• HTML window rendering through expressive library calls
• Efficient handling of UTF-8 Unicode characters and strings for globalized applications
• A thoughtfully designed, developer-friendly standard library
• Ownership-based memory management model for safety without a garbage collector
• Dynamic generics that adapt to your needs without sacrificing organization
Advanced Capabilities
• Enforces one type definition per file for better project organization
• Single-type with multi-trait inheritance structure for composability
• Base types provide default behavior increasing maintainability
• Non-nullable references by default to eliminate null-pointer issues (nullable when desired)
• Direct access to low-level bit manipulation
• Native support for fixed-point and floating-point precision
• Full range of primitives: integers, Booleans, and more
• Support for mutable and immutable variables for safe concurrency
• A exception handling system that catches all exception

Compiler Development	
In 2024, development began on the Puma compiler — a three-phase quest to bring the language to life. Phase one focuses on building a translator that converts Puma code into C/C++, enabling rapid prototyping and integration. In phase two, the compiler becomes self-hosting —rewritten entirely in the Puma language. The final phase replaces C/C++ as an intermediate language with direct generation of LLVM IR for streamlined performance and advanced tooling integration. The Puma compiler will evolve from here.

Standard Library
A comprehensive standard library will accompany the compiler, designed for both power and simplicity. With an emphasis on ease of use, the library will feature intuitive APIs and smart defaults—streamlining the most common use cases. Whether you're building tools or applications, Puma’s standard library will help you do more with less effort.
