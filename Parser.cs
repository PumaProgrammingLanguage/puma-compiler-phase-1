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

using static Puma.Lexer;

namespace Puma
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class Parser
    {
        /// <summary>
        /// State of the parser
        /// </summary>
        private enum State
        {
            Invalid,
            File,
            Import,
            Type,
            Trait,
            Module,
            Enums,
            Properties,
            Initialize,
            Start,
            Finalize,
            Functions,
            Function,
            StatementBlock,
            Statement,
            Parameter,
            end,
        }

        /// <summary>
        /// Category of the nodes in the tree
        /// </summary>
        public enum NodeCategory
        {
            Unknown,
            Invalid,
            Root,
            StringLiteral,
            CharLiteral,
            NumericLiteral,
            Comment,
            Identifier,
            Delimiter,
            Punctuation,
            Operator,
            Keyword,
            Section,
            Function,
            StatementBlock,
        }

        /// <summary>
        /// Names of the sections of Puma files
        /// </summary>
        readonly string[] Sections =
        [
            // use
            "use",
            // type
            "type",
            "trait",
            "module",
            // enums
            "enums",
            // properties
            "properties",
            // initialize
            "initialize",
            "start",
            // finalize
            "finalize",
            // functions
            "functions",
            // end
            "end",
        ];

        /// <summary>
        /// Sections of Puma files
        /// </summary>
        public enum Section
        {
            File,
            Import,
            Type,
            Trait,
            Module,
            Enums,
            Properties,
            Initialize,
            Start,
            Finalize,
            Functions,
            Invalid,
            end,
        }

        //readonly string[] Keywords =
        //[
        //    "use",
        //    "as",
        //    "type",
        //    "trait",
        //    "is",
        //    "has",
        //    "are",
        //    "value",
        //    "object",
        //    "enums",
        //    "base",
        //    "properties",
        //    "functions",
        //    "start",
        //    "initialize",
        //    "finalize",
        //    "return",
        //    "yield",
        //    "public",
        //    "private",
        //    "global",
        //    "internal",
        //    "var",
        //    "const",
        //    "readonly",
        //    "readwrite",
        //    "int",
        //    "i64",
        //    "i32",
        //    "i16",
        //    "i8",
        //    "uint",
        //    "u64",
        //    "u32",
        //    "u16",
        //    "u8",
        //    "float",
        //    "f64",
        //    "f32",
        //    "fixed",
        //    "fx64",
        //    "fx32",
        //    "char",
        //    "str",
        //    "bool",
        //    "implicit",
        //    "explicit",
        //    "operator",
        //    "get",
        //    "set",
        //    "with",
        //    "if",
        //    "elseif",
        //    "else",
        //    "and",
        //    "or",
        //    "not",
        //    "for",
        //    "in",
        //    "while",
        //    "Repeat",
        //    "begin",
        //    "end",
        //    "break",
        //    "continue",
        //    "match",
        //    "with",
        //    "multithread",
        //    "multiprocess"
        //];

        /// <summary>
        /// Position of the node in the tree in respect to the current node
        /// </summary>
        enum Position
        {
            None = 0,
            LeftBranchNode,
            RightBranchNode,
            PreviousNode,       // bidirectional pointer list
        }

        /// <summary>
        /// fields of the parser
        /// </summary>
        private readonly RootNode CurrentRootNode = new()
        {
            TokenText = "root",
            Category = NodeCategory.Root
        };
        private ASTNode? LastNode = null;
        private State CurrentParserState = State.File;
        private Section CurrentSection = Section.File;

        /// <summary>
        /// constructor of the parser
        /// </summary>
        public Parser()
        {
            LastNode = CurrentRootNode;
        }

        /// <summary>
        /// Main parser method
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public RootNode Parse(List<LexerTokens> tokens)
        {
            foreach (LexerTokens token in tokens)
            {
                switch (CurrentParserState)
                {
                    case State.File:
                        ParseFile(token);
                        break;

                    case State.Import:
                        ParseImport(token);
                        break;

                    case State.Type:
                        ParseType(token);
                        break;

                    case State.Trait:
                        ParseTrait(token);
                        break;

                    case State.Module:
                        ParseModule(token);
                        break;

                    case State.Enums:
                        ParseEnums(token);
                        break;

                    case State.Properties:
                        ParseProperties(token);
                        break;

                    case State.Initialize:
                        ParseInitialize(token);
                        break;

                    case State.Start:
                        ParseStart(token);
                        break;

                    case State.Finalize:
                        ParseFinalize(token);
                        break;

                    case State.Functions:
                        ParseFunctions(token);
                        break;

                    case State.Function:
                        ParseFunction(token);
                        break;

                    case State.StatementBlock:
                        ParseStatementBlock(token);
                        break;

                    case State.Statement:
                        ParseStatement(token);
                        break;

                    case State.Parameter:
                        ParseParameters(token);
                        break;

                    case State.end:
                        // end of the file
                        ParseEnd(token);
                        break;

                    case State.Invalid:
                    default:
                        break;
                }
            }

            return CurrentRootNode;
        }

        /// <summary>
        /// Parse the file
        /// </summary>
        /// <param name="token"></param>
        private void ParseFile(LexerTokens token)
        {
            // State machine to parse outside of the sections
            // should only be comments, statement identifiers, and whitespace
            switch (token.Category)
            {
                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.Comment:
                // Handle comment token
                case Lexer.TokenCategory.EndOfLine:
                // Handle end of line token
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token

                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    // don't add the current node to the tree
                    break;
            }
        }

        /// <summary>
        /// Parse the using section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseImport(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the module section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseModule(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the type section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseType(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the trait section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseTrait(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the enums section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseEnums(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the properties section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseProperties(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the start section
        /// </summary>
        /// <param name="token"></param>
        private void ParseStart(LexerTokens token)
        {
            // state machine to parse the start section header
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the initialize section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseInitialize(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the finalize section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseFinalize(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the functions section
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseFunctions(LexerTokens token)
        {
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                    // Handle comment token
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Identifier:
                    // Handle identifier token
                    if (Sections.Contains(token.TokenText))
                    {
                        // Handle section token
                        var sectionNode = new SectionNode
                        {
                            TokenText = token.TokenText,
                            Category = NodeCategory.Section
                        };
                        // Add the current node to the tree
                        sectionNode.AddNodeToTree(CurrentRootNode);
                        // Set the next state
                        SetNextState(token.TokenText);
                    }
                    else
                    {
                        // Handle invalid token outside of a section
                        // Not needed in the AST
                    }
                    break;

                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Handle whitespace token
                    // Not needed in the AST
                    break;

                // Invalid token
                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the end of the file
        /// </summary>
        /// <param name="token"></param>
        private void ParseEnd(LexerTokens token)
        {
            // state machine to parse the end of the sections
            // search for comments, whitespace, and end of line tokens
            switch (token.Category)
            {
                case Lexer.TokenCategory.Comment:
                case Lexer.TokenCategory.EndOfLine:
                case Lexer.TokenCategory.Whitespace:
                    // Not needed in the AST
                    break;

                case Lexer.TokenCategory.Delimiter:
                case Lexer.TokenCategory.Identifier:
                case Lexer.TokenCategory.Punctuation:
                case Lexer.TokenCategory.Operator:
                case Lexer.TokenCategory.StringLiteral:
                case Lexer.TokenCategory.CharLiteral:
                case Lexer.TokenCategory.NumericLiteral:
                default:
                    // Handle invalid token outside of a section
                    CurrentParserState = State.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Set the next state of the parser
        /// </summary>
        /// <param name="tokenText"></param>
        private void SetNextState(string? tokenText)
        {
            switch (tokenText)
            {
                case "use":
                    CurrentParserState = State.Import;
                    CurrentSection = Section.Import;
                    break;

                case "type":
                    CurrentParserState = State.Type;
                    CurrentSection = Section.Type;
                    break;

                case "trait":
                    CurrentParserState = State.Trait;
                    CurrentSection = Section.Trait;
                    break;

                case "module":
                    CurrentParserState = State.Module;
                    CurrentSection = Section.Module;
                    break;

                case "enums":
                    CurrentParserState = State.Enums;
                    CurrentSection = Section.Enums;
                    break;

                case "properties":
                    CurrentParserState = State.Properties;
                    CurrentSection = Section.Properties;
                    break;

                case "initialize":
                    CurrentParserState = State.Initialize;
                    CurrentSection = Section.Initialize;
                    break;

                case "start":
                    CurrentParserState = State.Start;
                    CurrentSection = Section.Start;
                    break;

                case "finalize":
                    CurrentParserState = State.Finalize;
                    CurrentSection = Section.Finalize;
                    break;

                case "functions":
                    CurrentParserState = State.Functions;
                    CurrentSection = Section.Functions;
                    break;

                case "end":
                    CurrentParserState = State.end;
                    CurrentSection = Section.end;
                    break;

                default:
                    // invalid section
                    CurrentSection = Section.Invalid;
                    break;
            }
        }

        /// <summary>
        /// Parse the current function
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseFunction(LexerTokens token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parse the parameters of a function
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseParameters(LexerTokens token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parse the current statement block
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseStatementBlock(LexerTokens token)
        {
            throw new NotImplementedException();
            //// if found end token
            //if (token.Category == Lexer.TokenCategory.Identifier && token.TokenText == "end")
            //{
            //    // Handle end token
            //    CurrentParserState = State.end;
            //}
            //// if found a section token
            //else if (Sections.Contains(token.TokenText))
            //{
            //    // Set the next state
            //    SetNextState(token.TokenText);
            //}
            //else
            //{
            //    // parse the statement
            //    ParseStatement(token);
            //}
            //return;
        }

        /// <summary>
        /// Parse the current statement
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ParseStatement(LexerTokens token)
        {
            throw new NotImplementedException();
        }
    }
}