using System;
using HelmareLabs.Rainbow.Graphics;
using Microsoft.Xna.Framework;

namespace HelmareLabs.Rainbow.State
{
    /// <summary>
    ///     A class which represents a game state and attempts to mirror
    ///     the main Game class.
    /// </summary>
    public abstract class RainbowState(StateManager states, int id)
    {
        /// <summary>
        ///     Gets the world-space camera.
        /// </summary>
        public Camera2D Camera { get; } = new Camera2D();

        /// <summary>
        ///     Gets or sets the state manager for this state.
        /// </summary>
        public StateManager States { get; } = states;

        /// <summary>
        ///     Gets or sets the ID for this state in the
        ///     state manager.
        /// </summary>
        public int ID { get; } = id;

        /// <summary>
        ///     This is called when the state manager
        ///     enters the state. The instance will be a
        ///     fresh instance.
        /// </summary>
        public abstract void Enter(RainbowState? from);

        /// <summary>
        ///     This is called when the state manager
        ///     leaves the state. The instance is destoried
        ///     afterwords.
        /// </summary>
        public virtual void Leave(RainbowState? to) { }

        /// <summary>
        ///     Updates the game state.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime) { }

        public virtual bool PreDraw() => true;

        public virtual void PostDraw() { }

        /// <summary>
        ///     Draws the world.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="ctx">The world-space render context.</param>
        public virtual void Draw(GameTime gameTime, RenderContext ctx) { }

        /// <summary>
        ///     Draws the UI.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="ctx">The screen-space render context.</param>
        public virtual void DrawUI(GameTime gameTime, RenderContext ctx) { }

        /// <summary>
        ///     Called right before Microsoft.Xna.Framework.Game.Draw(Microsoft.Xna.Framework.GameTime)
        ///     is normally called. Can return false to let the game loop not call
        ///     Microsoft.Xna.Framework.Game.Draw(Microsoft.Xna.Framework.GameTime).
        /// </summary>
        /// <returns></returns>
        public virtual bool BeginDraw() => true;

        /// <summary>
        ///     Called right after Microsoft.Xna.Framework.Game.Draw(Microsoft.Xna.Framework.GameTime)
        ///     is normally called.
        /// </summary>
        public virtual void EndDraw() { }

        /// <summary>
        ///     Called when the app gains focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void OnActivated(object? sender, EventArgs args) { }

        /// <summary>
        ///     Called when the app loses focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void OnDeactivated(object? sender, EventArgs args) { }

        /// <summary>
        ///     Called when the app is exiting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void OnExiting(object? sender, ExitingEventArgs args) { }
    }
}
