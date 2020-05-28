using System.Drawing;
using System.Threading.Tasks;
using Asteroids.Engine.Enums;

namespace Asteroids.Engine.Interfaces
{
    /// <summary>
    /// Asteroids game engine that will calculate the lines and polygons and react to control key events.
    /// </summary>
    public interface IBaseGameController
    {
        /// <summary>
        /// Start the game engine.
        /// </summary>
        /// <param name="container">Primary <see cref="IGraphicContainer"/> to draw on.</param>
        /// <param name="frameRectangle">Initial game <see cref="Rectangle"/> dimensions to base calculations one.</param>
        Task Initialize(IGraphicContainer container, Rectangle frameRectangle);

        /// <summary>
        /// Current state of the game.
        /// </summary>
        GameMode GameStatus { get; }

        /// <summary>
        /// Resize the game controller calculation rectangle.
        /// </summary>
        /// <param name="frameRectangle">New game <see cref="Rectangle"/> dimensions to base calculations one.</param>
        void ResizeGame(Rectangle frameRectangle);

        /// <summary>
        /// Apply a key-down event to the game.
        /// </summary>
        /// <param name="key"><see cref="PlayKey"/> to apply.</param>
        void KeyDown(PlayKey key);

        /// <summary>
        /// Apply a key-up event to the game.
        /// </summary>
        /// <param name="key"><see cref="PlayKey"/> to apply.</param>
        void KeyUp(PlayKey key);

        /// <summary>
        /// Shutdown the game.
        /// </summary>
        void Dispose();
    }
}
