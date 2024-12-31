using System;
using HelmareLabs.Rainbow.Graphics;
using HelmareLabs.Rainbow.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HelmareLabs.Rainbow
{
    /// <summary>
    ///     A static class that contains the latest RainbowGame instance.
    /// </summary>
    public static class Rainbow
    {
        internal static RainbowGame? _instance;

        /// <summary>
        ///     Gets the latest RainbowGame instance.
        /// </summary>
        public static RainbowGame Instance
        {
            get =>
                _instance
                ?? throw new NullReferenceException("No RainbowGame instance has been set.");
        }

        /// <summary>
        ///     Gets the instance as a specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static T InstanceAs<T>()
            where T : RainbowGame
        {
            return Instance as T
                ?? throw new InvalidCastException($"Instance is not of type {typeof(T).FullName}");
        }

        /// <summary>
        ///     Gets whether the game is active.
        /// </summary>
        public static bool IsActive => Instance.IsActive;

        /// <summary>
        ///     Gets the game's content manager.
        /// </summary>
        public static ContentManager Content => Instance.Content;

        /// <summary>
        ///     Gets the game's graphics device.
        /// </summary>
        public static GraphicsDevice GraphicsDevice => Instance.GraphicsDevice;

        /// <summary>
        ///     Gets the game's grpahics manager.
        /// </summary>
        public static GraphicsDeviceManager Graphics => Instance.Graphics;

        /// <summary>
        ///     Gets the game's window.
        /// </summary>
        public static GameWindow Window => Instance.Window;

        /// <summary>
        ///     Gets the game's StateManager.
        /// </summary>
        public static StateManager States => Instance.States;

        /// <summary>
        ///     Gets the current state.
        /// </summary>
        public static RainbowState? State => Instance.States.CurrentState;

        /// <summary>
        ///     Gets the current state as a specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static T? StateAs<T>()
            where T : RainbowState
        {
            RainbowState? state = State;
            if (state == null)
            {
                return null;
            }
            else
            {
                return state as T ?? throw new InvalidCastException();
            }
        }

        /// <summary>
        ///     Gets the current world-space camera.
        /// </summary>
        public static Camera2D? Camera => State?.Camera;

        /// <summary>
        ///     Gets the game's render pipeline.
        /// </summary>
        public static RenderPipeline Renderer => Instance.Renderer;

        /// <summary>
        ///     Exits the game.
        /// </summary>
        public static void Exit() => Instance.Exit();
    }
}
