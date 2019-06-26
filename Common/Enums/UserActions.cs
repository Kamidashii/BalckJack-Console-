using System;

namespace Common.Enums
{
    public class UserActions
    {
        public enum ActionType
        {
            Invalid = 0,
            Take = 1,
            Enough = 2,
            Surrender = 3,
            ShowCards = 4,
            Finished = 5
        }
    }
}
