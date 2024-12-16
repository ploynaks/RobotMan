using RobotMan.Enums;

namespace RobotMan;

public class RobotMoveService
{
    public int FirstIndex = 0;
    public int LastIndex = 4;

    public RobotPosition Execute(RobotPosition currentPosition, Command command)
    {
        return command switch
        {
            Command.Move => Move(currentPosition),
            Command.Left => new()
            {
                X = currentPosition.X, Y = currentPosition.Y,
                FacingDirection = Rotate(currentPosition.FacingDirection, RotateDirection.Left)
            },
            Command.Right => new()
            {
                X = currentPosition.X, Y = currentPosition.Y,
                FacingDirection = Rotate(currentPosition.FacingDirection, RotateDirection.Right)
            },
            Command.Report => currentPosition,
            _ => throw new Exception("Command is unknown.")
        };
    }

    public RobotPosition Move(RobotPosition position)
    {
        if (position.X == FirstIndex && position.FacingDirection == Direction.West) return position;
        if (position.Y == FirstIndex && position.FacingDirection == Direction.South) return position;
        if (position.X == LastIndex && position.FacingDirection == Direction.East) return position;
        if (position.Y == LastIndex && position.FacingDirection == Direction.North) return position;

        var newPosition = new RobotPosition()
            { X = position.X, Y = position.Y, FacingDirection = position.FacingDirection };

        switch (position.FacingDirection)
        {
            case Direction.North:
                newPosition.Y += 1;
                break;
            case Direction.East:
                newPosition.X += 1;
                break;
            case Direction.South:
                newPosition.Y -= 1;
                break;
            case Direction.West:
                newPosition.X -= 1;
                break;
            default:
                throw new Exception("The facing direction is unknown.");
        }
        return newPosition;
    }
    
    public Direction Rotate(Direction currrentDirection, RotateDirection rotateDirection)
    {
        return currrentDirection switch
        {
            Direction.North => rotateDirection == RotateDirection.Left ? Direction.West : Direction.East,
            Direction.East => rotateDirection == RotateDirection.Left ? Direction.North : Direction.South,
            Direction.South => rotateDirection == RotateDirection.Left ? Direction.East : Direction.West,
            Direction.West => rotateDirection == RotateDirection.Left ? Direction.South : Direction.North,
            _ => throw new Exception("Current direction is unknown.")
        };
    }
}