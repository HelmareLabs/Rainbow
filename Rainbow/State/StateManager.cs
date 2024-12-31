using System;
using System.Collections.Generic;

namespace HelmareLabs.Rainbow.State
{
    /// <summary>
    ///     Manages multiple game states.
    /// </summary>
    public class StateManager
    {
        private List<Type> States { get; } = [];

        /// <summary>
        ///     Gets the current game state.
        /// </summary>
        public RainbowState? CurrentState { get; private set; }

        /// <summary>
        ///     <para>Adds a new state.</para>
        ///     <em>Note: Using after content is loaded could yield unexpected results.</em>
        /// </summary>
        /// <typeparam name="T">The type of state.</typeparam>
        /// <returns>The id of the added state.</returns>
        public int AddState<T>()
            where T : RainbowState
        {
            States.Add(typeof(T));
            return States.Count - 1;
        }

        /// <summary>
        ///     Leaves the current state and enters the specified
        ///     state.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="RainbowStateException"></exception>
        public void Goto(int id)
        {
            RainbowState? from = CurrentState;
            RainbowState to = CreateState(id);

            from?.Leave(to);
            CurrentState = to;
            to.Enter(from);
        }

        private RainbowState CreateState(int id)
        {
            RainbowState? state =
                (RainbowState?)Activator.CreateInstance(States[id], this, id)
                ?? throw new RainbowStateException($"Failed to create state: {id}", null);
            return state;
        }
    }
}
