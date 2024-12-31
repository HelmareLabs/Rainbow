using System;
using System.Dynamic;
using HelmareLabs.Rainbow.Graphics;
using HelmareLabs.Rainbow.State;
using Microsoft.Xna.Framework;

namespace HelmareLabs.Rainbow
{
    public abstract class RainbowGame : Game
    {
        private RenderPipeline? _renderer;
        private Camera2D _uiCamera = new Camera2D { CenterOrigin = false };

        /// <summary>
        ///     Gets the state manager.
        /// </summary>
        public StateManager States { get; }

        /// <summary>
        ///     Gets the graphics device manager.
        /// </summary>
        public GraphicsDeviceManager Graphics { get; }

        /// <summary>
        ///     Gets the global renderpipelie.
        /// </summary>
        public RenderPipeline Renderer
        {
            get => _renderer ?? throw new NullReferenceException();
            protected set
            {
                if (_renderer == null)
                {
                    _renderer = value;
                }
                else
                {
                    throw new InvalidOperationException("Cannot assign another RenderPipeline.");
                }
            }
        }

        public RainbowGame()
        {
            Rainbow._instance = this;
            States = new StateManager();
            Graphics = new GraphicsDeviceManager(this);
        }

        protected override void LoadContent()
        {
            // Initialize the render pipeline.
            _renderer = new RenderPipeline(GraphicsDevice);
            _renderer.AddContext("world");
            _renderer.AddContext("ui");

            // Goto first state.
            States.Goto(0);
        }

        protected override void UnloadContent()
        {
            // Unload content
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            States.CurrentState?.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (States.CurrentState != null)
            {
                // Render world view.
                Renderer.ContextID = 0;

                States.CurrentState.Camera.UpdateMatrix(GraphicsDevice.Viewport);
                Renderer.Context.TransformMatrix = States.CurrentState.Camera.TransformMatrix;
                Renderer.Context.MinimumDepth = States.CurrentState.Camera.Transform.Z;

                States.CurrentState.Draw(gameTime, Renderer.Context);

                // Render UI view.
                Renderer.ContextID = 1;

                _uiCamera.VirtualHeight = States.CurrentState.Camera.VirtualHeight;
                _uiCamera.UpdateMatrix(GraphicsDevice.Viewport);
                Renderer.Context.TransformMatrix = _uiCamera.TransformMatrix;

                States.CurrentState.DrawUI(gameTime, Renderer.Context);

                GraphicsDevice.Clear(States.CurrentState.Camera.ClearColor);
                Renderer.FlushAll();
            }
            base.Draw(gameTime);
        }

        protected override bool BeginDraw()
        {
            return States.CurrentState?.BeginDraw() ?? true;
        }

        protected override void EndDraw()
        {
            States.CurrentState?.EndDraw();
            base.EndDraw();
        }

        protected override void OnActivated(object? sender, EventArgs args)
        {
            States.CurrentState?.OnActivated(sender, args);
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object? sender, EventArgs args)
        {
            States.CurrentState?.OnDeactivated(sender, args);
            base.OnActivated(sender, args);
        }

        protected override void OnExiting(object? sender, ExitingEventArgs args)
        {
            States.CurrentState?.OnExiting(sender, args);
            base.OnExiting(sender, args);
        }
    }
}
