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
using static PumaToCpp.Parser;

namespace PumaToCpp
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
            Assert.AreEqual("root", node.TokenText);
            Assert.AreEqual(NodeCategory.Root, node.Category);
            Assert.AreEqual(null, node.PreviousNode);
            Assert.AreNotEqual(null, node.SectionBranch);
            var sectionNode = node.SectionBranch.ElementAt<SectionNode>(0);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("start", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            // this needs to pass
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(1);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("end", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
        }

        [TestMethod]
        public void Parse_AllSection()
        {
            // Setup
            var lexer = new Lexer();
            var parser = new Parser();

            var source = @"
                using
                namespace
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
            Assert.AreEqual("root", node.TokenText);
            Assert.AreEqual(NodeCategory.Root, node.Category);
            Assert.AreEqual(null, node.PreviousNode);
            Assert.AreNotEqual(null, node.SectionBranch);
            var sectionNode = node.SectionBranch.ElementAt<SectionNode>(0);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("using", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(1);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("namespace", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(2);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("enums", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(3);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("properties", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(4);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("start", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(5);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("finalize", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(6);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("functions", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
            sectionNode = node.SectionBranch.ElementAt<SectionNode>(7);
            Assert.AreNotEqual(null, sectionNode);
            Assert.AreNotEqual(null, sectionNode?.TokenText);
            Assert.AreEqual("end", sectionNode?.TokenText);
            Assert.AreEqual(NodeCategory.Section, sectionNode?.Category);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode);
            Assert.AreNotEqual(null, sectionNode?.PreviousNode?.TokenText);
            Assert.AreEqual("root", sectionNode?.PreviousNode?.TokenText);
        }
    }
}
