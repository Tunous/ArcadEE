using ArcadeBot;
using BotBits;
using BotCake;

namespace ArcadeBot
{
    /// <summary>
    /// Base for bot playgrounds.
    /// </summary>
    public abstract class PlaygroundBase : BotBase
    {
        /// <summary>
        /// Gets the position at which the playground was created.
        /// </summary>
        /// <value>The position.</value>
        public Point Position { get; private set; }

        private bool _enabled = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ArcadeBot.PlaygroundBase"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;

                if (_enabled) {
                    OnEnable();
                } else {
                    OnDisable();
                }
            }
        }

        protected PlaygroundBase(Point position)
        {
            Position = position;
        }

        /// <summary>
        /// Called when playground is enabled.
        /// </summary>
        protected abstract void OnEnable();

        /// <summary>
        /// Called when playground is disabled.
        /// </summary>
        protected abstract void OnDisable();

        /// <summary>
        /// Determines whether the specified point is inside of this playground.
        /// </summary>
        /// <returns><c>true</c> if the specified point is inside of this playground; otherwise, <c>false</c>.</returns>
        /// <param name="point">Point.</param>
        public abstract bool Contains(Point point);
    }
}

