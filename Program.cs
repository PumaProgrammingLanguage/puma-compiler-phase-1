// LLVM Compiler for the Puma programming language
//   as defined in the document "The Puma Programming Language Specification"
//   available at https://github.com/ThePumaProgrammingLanguage
//
// Copyright © 2024 by Darryl Anthony Burchfield
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Puma
{
    internal class Program
    {
        //
        private static void Main(string[] args)
        {
            // Create instances of the lexer, parser, and codegen classes.
            var lexer = new Lexer();
            var parser = new Parser();
            var codegen = new Codegen();
            // Create variables to store the command-line arguments.
            string clangArguments = "";
            bool verbose = false;
            bool help = false;
            bool version = false;
            bool emitC = false;
            bool output = false;
            string sourceFileName = "";
            string outputFileName = "";
            // Create a variable to store the source code.
            string source;

            // Parse the command-line arguments.
            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-v":
                    case "--verbose":
                        // Print verbose output.
                        verbose = true;
                        // output file name won't follow this flag.
                        output = false;
                        break;

                    case "-h":
                    case "--help":
                        // Print the help message. Return without compiling the source file.
                        help = true;
                        // output file name won't follow this flag.
                        output = false;
                        break;

                    case "-V":
                    case "--version":
                        // Print the version of the Puma compiler. Return without compiling the source file.
                        version = true;
                        // output file name won't follow this flag.
                        output = false;
                        break;

                    case "-emit-c":
                        // Emit the generated C code to a file. Return without compiling the source file.
                        emitC = true;
                        // output file name won't follow this flag.
                        output = false;
                        break;

                    case "-o":
                    case "--output":
                        // The next argument after this flag is the output file name.
                        output = true;
                        break;

                    default:
                        if (output)
                        {
                            // If the output flag is set, the argument is the output file name.
                            outputFileName = arg;
                            output = false;
                            break;
                        }
                        // If the argument ends with ".puma", it is the source file name.
                        else if (arg.EndsWith(".puma"))
                        {
                            sourceFileName = arg;
                        }
                        else
                        {
                            // Otherwise, add the argument to the clang arguments string.
                            clangArguments += " " + args;
                        }
                        break;
                }
            }

            var cSourceFileName = sourceFileName.Replace(".puma", ".c");

            if (help)
            {
                // Print the help message and return.
                Console.WriteLine("Usage: puma [options] file.puma");
                Console.WriteLine("Options:");
                Console.WriteLine("  -v, --verbose  Print verbose output.");
                Console.WriteLine("  -h, --help     Print this help message and exit.");
                Console.WriteLine("  -V, --version  Print the version of the Puma compiler and exit.");
                Console.WriteLine("  -emit-c        Emit the generated C code to a file and exit.");
                Console.WriteLine("  -o, --output   Specify the output file name.");
                Console.WriteLine("  <clang flag>   other clang flags.");
                return;
            }

            if (version)
            {
                var versionNumber = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
                Console.WriteLine($"Puma Compiler version {versionNumber}");
                return;
            }

            // If the source file name is empty, print an error message and return.  Otherwise, read the source file.
            if (sourceFileName != "")
            {
                source = File.ReadAllText(sourceFileName);
            }
            else
            {
                Console.WriteLine("Error: No source file specified.");
                return;
            }

            // Tokenize and parse the source string above.
            // Generate C code from the syntax tree generated by the parser.

            var tokens = lexer.Tokenize(source);
            var ast = parser.Parse(tokens);
            var cCode = codegen.Generate(ast);

            if (verbose)
            {
                // Print the C code to the console if the verbose flag is set.
                Console.WriteLine("C language IR:");
                Console.WriteLine(cCode);
            }

            if (emitC)
            {
                // Write the generated C code to a file with the same name as the source file, but with the .c extension.
                File.WriteAllText(cSourceFileName, cCode);
                return;
            }

            if (outputFileName != "")
            {
                clangArguments = $"{cSourceFileName} -o {outputFileName} {clangArguments}"; // add the arguments after the source file name.
            }
            else
            {
                clangArguments = $"{cSourceFileName} {clangArguments}"; // add the arguments after the source file name.
            }

            // Compile the generated C code using clang.
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "clang",
                    Arguments = clangArguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }

            };
            // run the clang compiler by calling the WaitForExit method.
            process.Start();
            process.WaitForExit();

            // run the clang compiler by a command line call.
            
        }
    }
}