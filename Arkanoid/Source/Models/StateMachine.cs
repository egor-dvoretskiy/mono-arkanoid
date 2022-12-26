using Arkanoid.Source.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Source.Models
{
    public class StateMachine
    {
        private readonly int _maxStateNumber;

        public StateMachine()
        {
            CurrentState = GameState.MainMenu;

            _maxStateNumber = (int)Enum.GetValues(typeof(GameState)).Cast<GameState>().Max();
        }

        public GameState CurrentState { get; private set; }

        public void ProceedNextStage()
        {
            int nextNumber = (int)this.CurrentState + 1 > this._maxStateNumber ? 0 : (int)this.CurrentState + 1;

            CurrentState = (GameState)nextNumber;
        }
    }
}
