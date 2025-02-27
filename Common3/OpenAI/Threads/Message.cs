using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class Message
    {
        public static implicit operator Message(string content) { return new Message(content); }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">
        /// The content of the message.
        /// </param>
        /// <param name="fileIds">
        /// A list of File IDs that the message should use.
        /// There can be a maximum of 10 files attached to a message.
        /// Useful for tools like 'retrieval' and 'code_interpreter' that can access and use files.</param>
        /// <param name="metadata">
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </param>
        public Message(string content, IEnumerable<string> fileIds = null, IReadOnlyDictionary<string, string> metadata = null)
        {
            Role = Role.User;
            Content = content;
            FileIds = fileIds == null ? null : fileIds.ToList();
            Metadata = metadata;
        }

        /// <summary>
        /// The role of the entity that is creating the message.
        /// Currently only user is supported.
        /// </summary>
        [JsonProperty("role")]
        public Role Role { get; private set; }

        /// <summary>
        /// The content of the message.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; private set; }

        /// <summary>
        /// A list of File IDs that the message should use.
        /// There can be a maximum of 10 files attached to a message.
        /// Useful for tools like 'retrieval' and 'code_interpreter' that can access and use files.
        /// </summary>
        [JsonProperty("file_ids")]
        public IReadOnlyList<string> FileIds { get; private set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonProperty("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; private set; }
    }
}