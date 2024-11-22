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
using System.Linq;
using static Puma.Lexer;
using static Puma.Parser;

namespace Puma
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parse_StartSection()
        {
            // Setup
            var lexer = new Lexer();
            var parser = new Parser();

            var source = @"
                start
                end
            ";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);

            // call the method under test
            var node = parser.Parse(tokens);

            // Assert
            Assert.AreNotEqual(null, node);
            Assert.AreNotEqual(null, node.TokenText);
            Assert.AreEqual("", node.TokenText);
            Assert.AreEqual(NodeCategory.File, node.Category);
            Assert.AreEqual(null, node.PreviousNode);
            Assert.AreNotEqual(null, node.FirstNode);

            var sectionNode = node.FirstNode;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("start", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("end", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("start", sectionNode?.PreviousNode?.TokenText);
        }

        [TestMethod]
        public void Parse_AllSection()
        {
            // Setup
            var lexer = new Lexer();
            var parser = new Parser();

            var source = @"
                use
                module
                enums
                properties
                start
                finalize
                functions
                end
            ";

            // call the method under test
            List<LexerTokens> tokens = lexer.Tokenize(source);

            // call the method under test
            var node = parser.Parse(tokens);

            // Assert
            Assert.AreNotEqual(null, node);
            Assert.AreNotEqual(null, node.TokenText);
            Assert.AreEqual("", node.TokenText);
            Assert.AreEqual(NodeCategory.File, node.Category);
            Assert.AreEqual(null, node.PreviousNode);
            Assert.AreNotEqual(null, node.FirstNode);

            var sectionNode = node.FirstNode;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("use", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("module", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("use", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("enums", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("module", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("properties", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("enums", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("start", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("properties", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("finalize", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("start", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("functions", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("finalize", sectionNode?.PreviousNode?.TokenText);

            sectionNode = sectionNode.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("end", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("functions", sectionNode?.PreviousNode?.TokenText);
        }

        [TestMethod]
        public void Parse_StartAssignment()
        {
            // Setup
            var lexer = new Lexer();
            var parser = new Parser();

            var source = @"
                start
                    a = 1
                    b = 2
                end
            ";

            // call the methods under test
            List<LexerTokens> tokens = lexer.Tokenize(source);
            var node = parser.Parse(tokens);

            // Assert
            Assert.AreNotEqual(null, node);
            Assert.AreNotEqual(null, node.TokenText);
            Assert.AreEqual("", node.TokenText);
            Assert.AreEqual(NodeCategory.File, node.Category);
            Assert.AreEqual(null, node.PreviousNode);
            Assert.AreNotEqual(null, node.FirstNode);

            var sectionNode = node.FirstNode;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("start", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.File, sectionNode?.PreviousNode?.Category);

            var statementBlockNode = sectionNode?.StatementBlock;
            Assert.AreNotEqual(null, statementBlockNode);
            Assert.AreNotEqual(null, statementBlockNode?.TokenText);
            Assert.AreEqual("", statementBlockNode?.TokenText);
            Assert.AreEqual(NodeCategory.StatementBlock, statementBlockNode?.Category);
            Assert.AreNotEqual(null, statementBlockNode?.PreviousNode);
            Assert.AreNotEqual(null, statementBlockNode?.PreviousNode?.TokenText);
            Assert.AreEqual("start", statementBlockNode?.PreviousNode?.TokenText);

            var statementNode = statementBlockNode?.LeftNode;
            Assert.AreNotEqual(null, statementNode);
            Assert.AreNotEqual(null, statementNode?.TokenText);
            Assert.AreEqual("=", statementNode?.TokenText);
            Assert.AreEqual(NodeCategory.Operator, statementNode?.Category);
            Assert.AreNotEqual(null, statementNode?.PreviousNode);
            Assert.AreNotEqual(null, statementNode?.PreviousNode?.TokenText);
            Assert.AreEqual("", statementNode?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.StatementBlock, statementNode?.PreviousNode?.Category);

            var statementNodeLeft = statementNode?.LeftNode;
            Assert.AreNotEqual(null, statementNodeLeft);
            Assert.AreNotEqual(null, statementNodeLeft?.TokenText);
            Assert.AreEqual("a", statementNodeLeft?.TokenText);
            Assert.AreEqual(NodeCategory.Identifier, statementNodeLeft?.Category);
            Assert.AreNotEqual(null, statementNodeLeft?.PreviousNode);
            Assert.AreNotEqual(null, statementNodeLeft?.PreviousNode?.TokenText);
            Assert.AreEqual("=", statementNodeLeft?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.Operator, statementNodeLeft?.PreviousNode?.Category);

            var statementNodeRight = statementNode?.RightNode;
            Assert.AreNotEqual(null, statementNodeRight);
            Assert.AreNotEqual(null, statementNodeRight?.TokenText);
            Assert.AreEqual("1", statementNodeRight?.TokenText);
            Assert.AreEqual(NodeCategory.NumericLiteral, statementNodeRight?.Category);
            Assert.AreNotEqual(null, statementNodeRight?.PreviousNode);
            Assert.AreNotEqual(null, statementNodeRight?.PreviousNode?.TokenText);
            Assert.AreEqual("=", statementNodeRight?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.Operator, statementNodeRight?.PreviousNode?.Category);

            statementBlockNode = statementBlockNode?.RightNode;
            Assert.AreNotEqual(null, statementBlockNode);
            Assert.AreNotEqual(null, statementBlockNode?.TokenText);
            Assert.AreEqual("", statementBlockNode?.TokenText);
            Assert.AreEqual(NodeCategory.StatementBlock, statementBlockNode?.Category);
            Assert.AreNotEqual(null, statementBlockNode?.PreviousNode);
            Assert.AreNotEqual(null, statementBlockNode?.PreviousNode?.TokenText);
            Assert.AreEqual("", statementBlockNode?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.StatementBlock, statementBlockNode?.PreviousNode?.Category);

            statementNode = statementBlockNode?.LeftNode;
            Assert.AreNotEqual(null, statementNode);
            Assert.AreNotEqual(null, statementNode?.TokenText);
            Assert.AreEqual("=", statementNode?.TokenText);
            Assert.AreEqual(NodeCategory.Operator, statementNode?.Category);
            Assert.AreNotEqual(null, statementNode?.PreviousNode);
            Assert.AreNotEqual(null, statementNode?.PreviousNode?.TokenText);
            Assert.AreEqual("", statementNode?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.StatementBlock, statementNode?.PreviousNode?.Category);

            statementNodeLeft = statementNode?.LeftNode;
            Assert.AreNotEqual(null, statementNodeLeft);
            Assert.AreNotEqual(null, statementNodeLeft?.TokenText);
            Assert.AreEqual("b", statementNodeLeft?.TokenText);
            Assert.AreEqual(NodeCategory.Identifier, statementNodeLeft?.Category);
            Assert.AreNotEqual(null, statementNodeLeft?.PreviousNode);
            Assert.AreNotEqual(null, statementNodeLeft?.PreviousNode?.TokenText);
            Assert.AreEqual("=", statementNodeLeft?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.Operator, statementNodeLeft?.PreviousNode?.Category);

            statementNodeRight = statementNode?.RightNode;
            Assert.AreNotEqual(null, statementNodeRight);
            Assert.AreNotEqual(null, statementNodeRight?.TokenText);
            Assert.AreEqual("2", statementNodeRight?.TokenText);
            Assert.AreEqual(NodeCategory.NumericLiteral, statementNodeRight?.Category);
            Assert.AreNotEqual(null, statementNodeRight?.PreviousNode);
            Assert.AreNotEqual(null, statementNodeRight?.PreviousNode?.TokenText);
            Assert.AreEqual("=", statementNodeRight?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.Operator, statementNodeRight?.PreviousNode?.Category);

            sectionNode = sectionNode?.NextSection;
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreEqual("end", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreEqual("start", sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.PreviousNode?.Category);

        }
    }
}

