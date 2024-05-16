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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using static PumaToCpp.Lexer;

namespace PumaToCpp
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void Tokenize_ShouldReturnTokens()
        {
            // Setup
            var lexer = new Lexer();
            var source = @"
                // Puma Hello, World! program
                start
                    print(""Hello, World!"")
                end
            ";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(12, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[0].Category);
            Assert.AreEqual("\n", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Comment, tokens[1].Category);
            Assert.AreEqual("// Puma Hello, World! program", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[2].Category);
            Assert.AreEqual("\n", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[3].Category);
            Assert.AreEqual("start", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[4].Category);
            Assert.AreEqual("\n", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[5].Category);
            Assert.AreEqual("print", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Delimiter, tokens[6].Category);
            Assert.AreEqual("(", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.StringLiteral, tokens[7].Category);
            Assert.AreEqual("\"Hello, World!\"", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Delimiter, tokens[8].Category);
            Assert.AreEqual(")", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[9].Category);
            Assert.AreEqual("\n", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("end", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[11].Category);
            Assert.AreEqual("\n", tokens[11].TokenText);
        }

        [TestMethod]
        // unit test for operators
        public void Tokenize_ShouldReturnTokensForOperators1()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "a=1\nb=2\nc=a+b\nd=a-b\ne=a*b\n";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(26, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("a", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[2].Category);
            Assert.AreEqual("1", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[3].Category);
            Assert.AreEqual("\n", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("b", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[5].Category);
            Assert.AreEqual("=", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[6].Category);
            Assert.AreEqual("2", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[7].Category);
            Assert.AreEqual("\n", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("c", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[9].Category);
            Assert.AreEqual("=", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("a", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[11].Category);
            Assert.AreEqual("+", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[12].Category);
            Assert.AreEqual("b", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[13].Category);
            Assert.AreEqual("\n", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[14].Category);
            Assert.AreEqual("d", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[15].Category);
            Assert.AreEqual("=", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[16].Category);
            Assert.AreEqual("a", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[17].Category);
            Assert.AreEqual("-", tokens[17].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[18].Category);
            Assert.AreEqual("b", tokens[18].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[19].Category);
            Assert.AreEqual("\n", tokens[19].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[20].Category);
            Assert.AreEqual("e", tokens[20].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[21].Category);
            Assert.AreEqual("=", tokens[21].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[22].Category);
            Assert.AreEqual("a", tokens[22].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[23].Category);
            Assert.AreEqual("*", tokens[23].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[24].Category);
            Assert.AreEqual("b", tokens[24].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[25].Category);
            Assert.AreEqual("\n", tokens[25].TokenText);
        }

        [TestMethod]
        // unit test for operators
        public void Tokenize_ShouldReturnTokensForOperators2()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "f=a/b\ng=a%b\nh=a^b\ni=a&b\nj=a|b\n";

            // call the method under test
            var tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(30, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("f", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[2].Category);
            Assert.AreEqual("a", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[3].Category);
            Assert.AreEqual("/", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("b", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[5].Category);
            Assert.AreEqual("\n", tokens[5].TokenText);
            
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[6].Category);
            Assert.AreEqual("g", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[7].Category);
            Assert.AreEqual("=", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("a", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[9].Category);
            Assert.AreEqual("%", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("b", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[11].Category);
            Assert.AreEqual("\n", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[12].Category);
            Assert.AreEqual("h", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[13].Category);
            Assert.AreEqual("=", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[14].Category);
            Assert.AreEqual("a", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[15].Category);
            Assert.AreEqual("^", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[16].Category);
            Assert.AreEqual("b", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[17].Category);
            Assert.AreEqual("\n", tokens[17].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[18].Category);
            Assert.AreEqual("i", tokens[18].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[19].Category);
            Assert.AreEqual("=", tokens[19].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[20].Category);
            Assert.AreEqual("a", tokens[20].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[21].Category);
            Assert.AreEqual("&", tokens[21].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[22].Category);
            Assert.AreEqual("b", tokens[22].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[23].Category);
            Assert.AreEqual("\n", tokens[23].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[24].Category);
            Assert.AreEqual("j", tokens[24].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[25].Category);
            Assert.AreEqual("=", tokens[25].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[26].Category);
            Assert.AreEqual("a", tokens[26].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[27].Category);
            Assert.AreEqual("|", tokens[27].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[28].Category);
            Assert.AreEqual("b", tokens[28].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[29].Category);
            Assert.AreEqual("\n", tokens[29].TokenText);
        }

        [TestMethod]
        // unit test for operators
        public void Tokenize_ShouldReturnTokensForOperators3()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "k=a<<b\nl=a>>b\nm=a==b\nn=a!=b\no=a<b\n";

            // call the method under test
            var tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(30, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("k", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[2].Category);
            Assert.AreEqual("a", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[3].Category);
            Assert.AreEqual("<<", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("b", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[5].Category);
            Assert.AreEqual("\n", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[6].Category);
            Assert.AreEqual("l", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[7].Category);
            Assert.AreEqual("=", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("a", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[9].Category);
            Assert.AreEqual(">>", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("b", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[11].Category);
            Assert.AreEqual("\n", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[12].Category);
            Assert.AreEqual("m", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[13].Category);
            Assert.AreEqual("=", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[14].Category);
            Assert.AreEqual("a", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[15].Category);
            Assert.AreEqual("==", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[16].Category);
            Assert.AreEqual("b", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[17].Category);
            Assert.AreEqual("\n", tokens[17].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[18].Category);
            Assert.AreEqual("n", tokens[18].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[19].Category);
            Assert.AreEqual("=", tokens[19].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[20].Category);
            Assert.AreEqual("a", tokens[20].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[21].Category);
            Assert.AreEqual("!=", tokens[21].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[22].Category);
            Assert.AreEqual("b", tokens[22].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[23].Category);
            Assert.AreEqual("\n", tokens[23].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[24].Category);
            Assert.AreEqual("o", tokens[24].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[25].Category);
            Assert.AreEqual("=", tokens[25].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[26].Category);
            Assert.AreEqual("a", tokens[26].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[27].Category);
            Assert.AreEqual("<", tokens[27].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[28].Category);
            Assert.AreEqual("b", tokens[28].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[29].Category);
            Assert.AreEqual("\n", tokens[29].TokenText);
        }

        [TestMethod]
        // unit test for operators
        public void Tokenize_ShouldReturnTokensForOperators4()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "p=a<=b\nq=a>b\nr=a>=b\n";

            // call the method under test
            var tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(18, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("p", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[2].Category);
            Assert.AreEqual("a", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[3].Category);
            Assert.AreEqual("<=", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("b", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[5].Category);
            Assert.AreEqual("\n", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[6].Category);
            Assert.AreEqual("q", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[7].Category);
            Assert.AreEqual("=", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("a", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[9].Category);
            Assert.AreEqual(">", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("b", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[11].Category);
            Assert.AreEqual("\n", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[12].Category);
            Assert.AreEqual("r", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[13].Category);
            Assert.AreEqual("=", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[14].Category);
            Assert.AreEqual("a", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[15].Category);
            Assert.AreEqual(">=", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[16].Category);
            Assert.AreEqual("b", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[17].Category);
            Assert.AreEqual("\n", tokens[17].TokenText);
        }

        [TestMethod]
        // unit test for operators
        public void Tokenize_ShouldReturnTokensForOperators5()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "u= not a\nv=~a\nw+=b\nx-=b\ny*=b\n";

            // call the method under test
            var tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(22, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("u", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[2].Category);
            Assert.AreEqual("not", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[3].Category);
            Assert.AreEqual("a", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[4].Category);
            Assert.AreEqual("\n", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[5].Category);
            Assert.AreEqual("v", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[6].Category);
            Assert.AreEqual("=", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[7].Category);
            Assert.AreEqual("~", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("a", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[9].Category);
            Assert.AreEqual("\n", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("w", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[11].Category);
            Assert.AreEqual("+=", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[12].Category);
            Assert.AreEqual("b", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[13].Category);
            Assert.AreEqual("\n", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[14].Category);
            Assert.AreEqual("x", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[15].Category);
            Assert.AreEqual("-=", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[16].Category);
            Assert.AreEqual("b", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[17].Category);
            Assert.AreEqual("\n", tokens[17].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[18].Category);
            Assert.AreEqual("y", tokens[18].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[19].Category);
            Assert.AreEqual("*=", tokens[19].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[20].Category);
            Assert.AreEqual("b", tokens[20].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[21].Category);
            Assert.AreEqual("\n", tokens[21].TokenText);
        }

        [TestMethod]
        // unit test for operators
        public void Tokenize_ShouldReturnTokensForOperators6()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "z/=b\naa%=b\nab^=b\nac&=b\nad|=b\n";

            // call the method under test
            var tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(20, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("z", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("/=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[2].Category);
            Assert.AreEqual("b", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[3].Category);
            Assert.AreEqual("\n", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("aa", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[5].Category);
            Assert.AreEqual("%=", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[6].Category);
            Assert.AreEqual("b", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[7].Category);
            Assert.AreEqual("\n", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("ab", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[9].Category);
            Assert.AreEqual("^=", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("b", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[11].Category);
            Assert.AreEqual("\n", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[12].Category);
            Assert.AreEqual("ac", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[13].Category);
            Assert.AreEqual("&=", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[14].Category);
            Assert.AreEqual("b", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[15].Category);
            Assert.AreEqual("\n", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[16].Category);
            Assert.AreEqual("ad", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[17].Category);
            Assert.AreEqual("|=", tokens[17].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[18].Category);
            Assert.AreEqual("b", tokens[18].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[19].Category);
            Assert.AreEqual("\n", tokens[19].TokenText);
        }

        [TestMethod]
        // unit test for operators
        public void Tokenize_ShouldReturnTokensForOperators7()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "ae<<=b\naf>>=b\n";

            // call the method under test
            var tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(8, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("ae", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("<<=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[2].Category);
            Assert.AreEqual("b", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[3].Category);
            Assert.AreEqual("\n", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("af", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[5].Category);
            Assert.AreEqual(">>=", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[6].Category);
            Assert.AreEqual("b", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[7].Category);
            Assert.AreEqual("\n", tokens[7].TokenText);
        }

        [TestMethod]
        // unit test for all of the chars
        public void Tokenize_ShouldReturnTokensForChars()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "a='1'\nb='+'\nc='\n'\n";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(12, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("a", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.CharLiteral, tokens[2].Category);
            Assert.AreEqual("'1'", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[3].Category);
            Assert.AreEqual("\n", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("b", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[5].Category);
            Assert.AreEqual("=", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.CharLiteral, tokens[6].Category);
            Assert.AreEqual("'+'", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[7].Category);
            Assert.AreEqual("\n", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("c", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[9].Category);
            Assert.AreEqual("=", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.CharLiteral, tokens[10].Category);
            Assert.AreEqual("'\n'", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[11].Category);
            Assert.AreEqual("\n", tokens[11].TokenText);
        }

        [TestMethod]
        // unit test for strings
        public void Tokenize_ShouldReturnTokensForStrings()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "a=\"1\"\nb=\"+\"\nc=\"\n\"\n";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(12, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("a", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.StringLiteral, tokens[2].Category);
            Assert.AreEqual("\"1\"", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[3].Category);
            Assert.AreEqual("\n", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("b", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[5].Category);
            Assert.AreEqual("=", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.StringLiteral, tokens[6].Category);
            Assert.AreEqual("\"+\"", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[7].Category);
            Assert.AreEqual("\n", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("c", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[9].Category);
            Assert.AreEqual("=", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.StringLiteral, tokens[10].Category);
            Assert.AreEqual("\"\n\"", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[11].Category);
            Assert.AreEqual("\n", tokens[11].TokenText);
        }

        [TestMethod]
        // unit test for strings
        public void Tokenize_ShouldReturnTokensForNumericLiterals()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "a=1\nb=-1\nc=+1\nd=1.0\ne=1.0e3\nf=1.0e-3\ng=1.0E-3\nh=1.0E+3\n";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(34, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("a", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[2].Category);
            Assert.AreEqual("1", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[3].Category);
            Assert.AreEqual("\n", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[4].Category);
            Assert.AreEqual("b", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[5].Category);
            Assert.AreEqual("=", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[6].Category);
            Assert.AreEqual("-", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[7].Category);
            Assert.AreEqual("1", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[8].Category);
            Assert.AreEqual("\n", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[9].Category);
            Assert.AreEqual("c", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[10].Category);
            Assert.AreEqual("=", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[11].Category);
            Assert.AreEqual("+", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[12].Category);
            Assert.AreEqual("1", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[13].Category);
            Assert.AreEqual("\n", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[14].Category);
            Assert.AreEqual("d", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[15].Category);
            Assert.AreEqual("=", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[16].Category);
            Assert.AreEqual("1.0", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[17].Category);
            Assert.AreEqual("\n", tokens[17].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[18].Category);
            Assert.AreEqual("e", tokens[18].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[19].Category);
            Assert.AreEqual("=", tokens[19].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[20].Category);
            Assert.AreEqual("1.0e3", tokens[20].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[21].Category);
            Assert.AreEqual("\n", tokens[21].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[22].Category);
            Assert.AreEqual("f", tokens[22].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[23].Category);
            Assert.AreEqual("=", tokens[23].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[24].Category);
            Assert.AreEqual("1.0e-3", tokens[24].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[25].Category);
            Assert.AreEqual("\n", tokens[25].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[26].Category);
            Assert.AreEqual("g", tokens[26].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[27].Category);
            Assert.AreEqual("=", tokens[27].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[28].Category);
            Assert.AreEqual("1.0E-3", tokens[28].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[29].Category);
            Assert.AreEqual("\n", tokens[29].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[30].Category);
            Assert.AreEqual("h", tokens[30].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[31].Category);
            Assert.AreEqual("=", tokens[31].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[32].Category);
            Assert.AreEqual("1.0E+3", tokens[32].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[33].Category);
            Assert.AreEqual("\n", tokens[33].TokenText);
        }

        [TestMethod]
        // unit test for strings
        public void Tokenize_ShouldReturnTokensForHexAndOctalLiterals()
        {
            // Setup
            var lexer = new Lexer();

            // test these operators
            var source = "i=77 o\nj=0ff h\nk=0FF h\nl=0FFh\nm=0ffh\nn=77o\n";

            // call the method under test
            var tokens = lexer.Tokenize(source);

            // Assert
            Assert.AreNotEqual(null, tokens);
            Assert.AreEqual(30, tokens.Count);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[0].Category);
            Assert.AreEqual("i", tokens[0].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[1].Category);
            Assert.AreEqual("=", tokens[1].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[2].Category);
            Assert.AreEqual("77", tokens[2].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[3].Category);
            Assert.AreEqual("o", tokens[3].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[4].Category);
            Assert.AreEqual("\n", tokens[4].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[5].Category);
            Assert.AreEqual("j", tokens[5].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[6].Category);
            Assert.AreEqual("=", tokens[6].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[7].Category);
            Assert.AreEqual("0ff", tokens[7].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[8].Category);
            Assert.AreEqual("h", tokens[8].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[9].Category);
            Assert.AreEqual("\n", tokens[9].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[10].Category);
            Assert.AreEqual("k", tokens[10].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[11].Category);
            Assert.AreEqual("=", tokens[11].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[12].Category);
            Assert.AreEqual("0FF", tokens[12].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[13].Category);
            Assert.AreEqual("h", tokens[13].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[14].Category);
            Assert.AreEqual("\n", tokens[14].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[15].Category);
            Assert.AreEqual("l", tokens[15].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[16].Category);
            Assert.AreEqual("=", tokens[16].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[17].Category);
            Assert.AreEqual("0FF", tokens[17].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[18].Category);
            Assert.AreEqual("h", tokens[18].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[19].Category);
            Assert.AreEqual("\n", tokens[19].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[20].Category);
            Assert.AreEqual("m", tokens[20].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[21].Category);
            Assert.AreEqual("=", tokens[21].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[22].Category);
            Assert.AreEqual("0ff", tokens[22].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[23].Category);
            Assert.AreEqual("h", tokens[23].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[24].Category);
            Assert.AreEqual("\n", tokens[24].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[25].Category);
            Assert.AreEqual("n", tokens[25].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Operator, tokens[26].Category);
            Assert.AreEqual("=", tokens[26].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.NumericLiteral, tokens[27].Category);
            Assert.AreEqual("77", tokens[27].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.Identifier, tokens[28].Category);
            Assert.AreEqual("o", tokens[28].TokenText);
            Assert.AreEqual(Lexer.TokenCategory.EndOfLine, tokens[29].Category);
            Assert.AreEqual("\n", tokens[29].TokenText);

        }
    }
}