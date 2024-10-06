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
using System.Diagnostics;

namespace Puma
{
    internal partial class Parser
    {
        /// <summary>
        /// 
        /// </summary>
        public class StatementNode : ASTNode
        {
            public StatementNode? LeftNode = null;
            public StatementNode? RightNode = null;

            public void AddNodeToTree(StatementNode currentStatementNode, Parser.Position position)
            {
                Debug.Assert(currentStatementNode != null);

                // Add the current node to the tree
                switch (position)
                {
                    case Parser.Position.LeftNode:
                        LeftNode = currentStatementNode;
                        currentStatementNode.PreviousNode = this;
                        break;
                    case Parser.Position.RightNode:
                        RightNode = currentStatementNode;
                        currentStatementNode.PreviousNode = this;
                        break;
                    case Parser.Position.PreviousLeftNode:
                        // swap current node with this node
                        // check if this is on the left or right of previous node and replace it with current node
                        if (((StatementNode)PreviousNode).LeftNode == this)
                        {
                            ((StatementNode)PreviousNode).LeftNode = currentStatementNode;
                        }
                        else if (((StatementNode)PreviousNode).RightNode == this)
                        {
                            ((StatementNode)PreviousNode).LeftNode = currentStatementNode;
                        }
                        // Set the previous node
                        currentStatementNode.PreviousNode = PreviousNode;
                        // now put this on the left of the new current node
                        currentStatementNode.LeftNode = this;
                        PreviousNode = currentStatementNode;
                        break;
                    case Parser.Position.PreviousRightNode:
                        // swap current node with this node
                        // check if this is on the left or right of previous node and replace it with current node
                        if (((StatementNode)PreviousNode).LeftNode == this)
                        {
                            ((StatementNode)PreviousNode).LeftNode = currentStatementNode;
                        }
                        else if (((StatementNode)PreviousNode).RightNode == this)
                        {
                            ((StatementNode)PreviousNode).LeftNode = currentStatementNode;
                        }
                        // Set the previous node
                        currentStatementNode.PreviousNode = PreviousNode;
                        // now put this on the right of the new current node
                        currentStatementNode.RightNode = this;
                        PreviousNode = currentStatementNode;
                        break;
                }
            }
        }
    }
}