using System;
using System.ComponentModel.Composition;

namespace ArcadeBot
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BotIdAttribute : ExportAttribute, IBotMetadata
    {
        public BotIdAttribute(string id, string version)
            : base(typeof(Bot))
        {
            Id = id;
            Version = version;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; private set; }
    }
}

