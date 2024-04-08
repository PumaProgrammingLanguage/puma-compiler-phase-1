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

using static PumaToCpp.Lexer;

namespace PumaToCpp
{
    /// <summary>
    /// 
    /// </summary>
    internal class Parser
    {
        /// <summary>
        /// 
        /// </summary>
        public class ASTNode
        {
            public string Token;
            public NodeCategory Category;
            public ASTNode PreBranchNode;
            public ASTNode PostBranchNode;
        }

        enum Position
        {
            PreBranchNode,
            PostBranchNode,
            BaseNode,
        }

        public enum NodeCategory
        {
            Unknown,
            StringLiteral,
            CharLiteral,
            NumericLiteral,
            Comment,
            Identifier,
            Whitespace,
            Delimiter,
            Punctuation,
            Operator,
            Section,
            Statement,
            Keyword,
        }

        string[] keywords =
        [
            "using",
            "as",
            "type",
            "trait",
            "is",
            "has",
            "are",
            "value",
            "object",
            "enums",
            "base",
            "properties",
            "functions",
            "start",
            "initialize",
            "finalize",
            "return",
            "yield",
            "public",
            "private",
            "global",
            "Internal",
            "var",
            "const",
            "readonly",
            "readwrite",
            "int",
            "i64",
            "i32",
            "i16",
            "i8",
            "uint",
            "u64",
            "u32",
            "u16",
            "u8",
            "float",
            "f64",
            "f32",
            "fixed",
            "fx64",
            "fx32",
            "char",
            "str",
            "bool",
            "implicit",
            "explicit",
            "operator",
            "get",
            "set",
            "with",
            "if",
            "elseif",
            "else",
            "and",
            "or",
            "not",
            "for",
            "in",
            "while",
            "Loop",
            "begin",
            "end",
            "break",
            "continue",
            "match",
            "with",
            "multithread",
            "multiprocess"
        ];

        string[] sections =
        [
            "using", // using / import
            "type", "trait", "namespace", // file type
            "enums", // enums
            "properties", // properties
            "initialize", "start", // initialize
            "finalize", // finalize / cleanup
            "functions", // functions
        ];

        public ASTNode Parse(List<LexerTokens> tokens)
        {
            var rootNode = new ASTNode()
            {
                Token = "root"
            };

            ASTNode baseNode = rootNode;
            Position position = Position.BaseNode;
            Position subPosition = Position.PreBranchNode;

            foreach (LexerTokens token in tokens)
            {
                var currentNode = new ASTNode()
                {
                    Token = token.Token
                };

                AssignNodeCategory(token, currentNode);

                baseNode = AddNodeToTree(currentNode, baseNode, position, subPosition);
            }

            return rootNode;
        }

        private void AssignNodeCategory(LexerTokens token, ASTNode currentNode)
        {
            // State machine to parse the token
            switch (token.Category)
            {
                case Lexer.TokenCategory.StringLiteral:
                    // Handle string literal token
                    currentNode.Category = NodeCategory.StringLiteral;
                    break;

                case Lexer.TokenCategory.CharLiteral:
                    // Handle char literal token
                    currentNode.Category = NodeCategory.CharLiteral;
                    break;

                case Lexer.TokenCategory.NumericLiteral:
                    // Handle numeric literal token
                    currentNode.Category = NodeCategory.NumericLiteral;
                    break;

                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    currentNode.Category = NodeCategory.Comment;
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (keywords.Contains(token.Token))
                    {
                        // Handle keyword token
                        currentNode.Category = NodeCategory.Keyword;
                    }
                    else
                    {
                        // Handle identifier token
                        currentNode.Category = NodeCategory.Identifier;
                    }
                    break;

                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    currentNode.Category = NodeCategory.Whitespace;
                    break;

                case Lexer.TokenCategory.Delimiter:
                    // Handle delimiter token
                    currentNode.Category = NodeCategory.Delimiter;
                    break;

                case Lexer.TokenCategory.Punctuation:
                    // Handle punctuation token
                    currentNode.Category = NodeCategory.Punctuation;
                    break;

                case Lexer.TokenCategory.Operator:
                    // Handle operator token
                    currentNode.Category = NodeCategory.Operator;
                    break;

                default:
                    // Handle unknown token
                    currentNode.Category = NodeCategory.Unknown;
                    break;
            }
        }

        private static ASTNode AddNodeToTree(ASTNode currentNode, ASTNode baseNode, Position position, Position subPosition = Position.PreBranchNode)
        {
            // Add the current node to the tree
            switch (position)
            {
                case Position.PreBranchNode:
                    baseNode.PreBranchNode = currentNode;
                    break;

                case Position.PostBranchNode:
                    baseNode.PostBranchNode = currentNode;
                    break;

                case Position.BaseNode:
                    // current is the base node
                    ASTNode savedNode = baseNode;
                    baseNode = currentNode;
                    switch (subPosition)
                    {
                        case Position.PreBranchNode:
                            baseNode.PreBranchNode = savedNode;
                            break;

                        case Position.PostBranchNode:
                            baseNode.PostBranchNode = savedNode;
                            break;

                        default:
                            break;
                    }
                    break;
            }

            return baseNode;
		}
    }
}