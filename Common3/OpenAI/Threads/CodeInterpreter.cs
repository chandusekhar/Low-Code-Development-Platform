using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class CodeInterpreter
    {
        /// <summary>
        /// The input to the Code Interpreter tool call.
        /// </summary>
        
        [JsonProperty("input")]
        public string Input { get; private set; }

        /// <summary>
        /// The outputs from the Code Interpreter tool call.
        /// Code Interpreter can output one or more items, including text (logs) or images (image).
        /// Each of these are represented by a different object type.
        /// </summary>
        
        [JsonProperty("outputs")]
        public IReadOnlyList<CodeInterpreterOutputs> Outputs { get; private set; }
    }
}