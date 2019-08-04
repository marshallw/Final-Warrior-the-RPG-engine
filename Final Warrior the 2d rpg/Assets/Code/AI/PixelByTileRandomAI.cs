using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementActions
{
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4,
    Stop  = 5
}

public class PixelByTileRandomAI : AI
{
    private static int Cooldown = 100;
    private int CooldownRemaining = 0;
    public override Vector3 GetNextAction()
    {
        Vector3 result = Vector3.zero;
        if (CooldownRemaining == 0)
        {
            MovementActions nextAction = (MovementActions)Random.Range((int)MovementActions.Up, (int)MovementActions.Stop);

            switch (nextAction)
            {
                case MovementActions.Up:
                    result.y = 1;
                    break;
                case MovementActions.Right:
                    result.x = 1;
                    break;
                case MovementActions.Down:
                    result.y = -1;
                    break;
                case MovementActions.Left:
                    result.x = -1;
                    break;
            }

            CooldownRemaining = Cooldown + Random.Range(0, Cooldown/3);
        }
        else
        {
            CooldownRemaining--;
        }
        return result;
    }
}
