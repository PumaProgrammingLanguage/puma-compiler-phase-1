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

using static Puma.Parser;

namespace Puma
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
        internal string Generate(FileNode fileNode)
        {
            // intermediate language source code
            // add the header file stdint.h because specific size integers are built-in types in Puma
            string il = "#include \"stdint.h\"\n";
            string sectionTerminatingCode = "";

            // get the next section node
            SectionNode sectionNode = fileNode.FirstNode;

            do
            {
                if (sectionNode.Category == NodeCategory.Section)
                {
                    int returnValue = 0;
                    switch (sectionNode.TokenText)
                    {
                        case "start":
                            // generate the intermediate language source code for the section
                            il += "\n// start section\n";
                            il += "int main(void)\n{\n";
                            // add the terminating code for this section
                            sectionTerminatingCode += $"return {returnValue};\n}}\n";
                            break;

                        case "end":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            // add the terminating code for the section
                            il += "\n// end section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "use":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// use section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "module":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// module section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "type":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// type section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "trait":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// trait section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "enums":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// enums section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "properties":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// properties section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "initialize":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// initialize section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "finalize":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// finalize section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        case "functions":
                            // generate the intermediate language source code for the section
                            // add the terminating code for the section
                            il += sectionTerminatingCode;
                            il += "\n// functions section\n";
                            // reset the terminating code for the section
                            sectionTerminatingCode = "";
                            break;

                        default:
                            throw new System.NotImplementedException();
                    }
                }

                sectionNode = sectionNode.NextSection;
            } while (sectionNode != null);

            // return the intermediate language source code
            return il;
        }
    }
}