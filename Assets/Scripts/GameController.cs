using UnityEngine;

namespace GameScene.GameStatus
{
    public class GameController { 
        public enum Status
        {
            Start,
            Playing,
            GameOver,
            Clear
        }

        Status status = Status.Start;


    }
}
