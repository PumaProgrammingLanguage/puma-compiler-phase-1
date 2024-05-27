// LLVM Compiler for the Puma programming language
//   as defined in the document "The Puma Programming Language Specification"
//   available at https://github.com/ThePumaProgrammingLanguage
//
// Copyright � 2024 by Darryl Anthony Burchfield
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static PumaToCpp.Lexer;
using static PumaToCpp.Parser;

namespace PumaToCpp.Tests
{
    [TestClass]
    public class CodegenTests
    {
        [TestMethod]
        public void Codegen_MainFunction()
        {
            // Setup
            var lexer = new Lexer();
            var parser = new Parser();
            var codegen = new Codegen();

            var source = @"
                start
                end
            ";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);
            var node = parser.Parse(tokens);
            var il = codegen.Generate(node);

            // Assert
            Assert.AreEqual("void main()\n{\n}\n", il);
        }
    }
}
