using System;

namespace ArcadeBot
{
    public interface IBotMetadata
    {
        string Id { get; }
        string Version { get; }
    }
}

