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

using System.Diagnostics;

namespace Puma
{
    internal partial class Parser
    {
        /// <summary>
        /// 
        /// </summary>
        public class SectionNode : ASTNode
        {
            public StatementNode? StatementBlock = null;

            public void AddNodeToTree(StatementNode currentBlockNode)
            {
                Debug.Assert(currentBlockNode != null);

                // check if first statement in section
                if (StatementBlock == null)
                {
                    // Set the previous node
                    currentBlockNode.PreviousNode = this;
                    // Add the current node to the tree
                    StatementBlock = currentBlockNode;
                }
                else
                {
                    // find the last statement in the block
                    StatementNode lastStatement = StatementBlock;
                    while (lastStatement.RightNode != null)
                    {
                        lastStatement = lastStatement.RightNode;
                    }
                    // Set the previous node
                    currentBlockNode.PreviousNode = lastStatement;
                    // Add the current node to the tree
                    lastStatement.RightNode = currentBlockNode;
                }
            }
        }
    }
}