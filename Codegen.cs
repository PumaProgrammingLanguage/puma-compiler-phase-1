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

using static PumaToCpp.Parser;

namespace PumaToCpp
{
    /// <summary>
    /// 
    /// </summary>
    internal class Codegen
    {
        /// <summary>
        /// 
        /// </summary>
        public Codegen()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ast"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal string Generate(RootNode ast)
        {
            // intermediate language source code
            string il = "";
            string sectionTerminatingCode = "";

            foreach (var sectionNode in ast.SectionBranch)
            {
                if (sectionNode.Category == NodeCategory.Section)
                {
                    switch (sectionNode.TokenText)
                    {
                        case "start":
                            // generate the intermediate language source code for the section
                            il += "void main()\n{\n";
                            sectionTerminatingCode += "}\n";

                            // put section code here


                            break;
                        case "end":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;
                    }
                }
            }

            // return the intermediate language source code
            return il;
        }
    }
}