using BotBits;
using BotCake;

namespace ArcadeBot
{
    public abstract class Bot : BotBase
    {
        /// <summary>
        /// Tries the create playground at specified position.
        /// </summary>
        /// <returns>Whether playground was created.</returns>
        /// <param name="point">Point.</param>
        /// <param name="playground">Playground.</param>
        public abstract bool TryCreatePlaygroundAt(Point point, out PlaygroundBase playground);
    }
}

