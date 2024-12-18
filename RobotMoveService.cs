using RobotMan.Enums;

namespace RobotMan;

public class RobotMoveService
{
    public RobotPosition Execute(RobotPosition currentPosition, Command command)
    {
        if ((currentPosition.X < Constants.FirstIndex || currentPosition.X > Constants.LastIndex)
            || (currentPosition.Y < Constants.FirstIndex || currentPosition.Y > Constants.LastIndex))
            throw new Exception($"The robot is not on the table. Position ({currentPosition.X},{currentPosition.Y})");
        
        var newPosition = new RobotPosition()
            { X = currentPosition.X, Y = currentPosition.Y, FacingDirection = currentPosition.FacingDirection };
        switch (command)
        {
            case Command.MOVE:
                newPosition = Move(currentPosition);
                break;
            case Command.LEFT:
                newPosition.FacingDirection = Rotate(currentPosition.FacingDirection, Command.LEFT);
                break;
            case Command.RIGHT:
                newPosition.FacingDirection = Rotate(currentPosition.FacingDirection, Command.RIGHT);
                break;
            case Command.REPORT:
                Console.WriteLine($"OUTPUT: {currentPosition}");
                break;
            default:
                throw new Exception("Command is unknown.");
        }
        return newPosition;
    }

    public RobotPosition Move(RobotPosition position)
    {
        if (position.X == Constants.FirstIndex && position.FacingDirection == Direction.WEST) return position;
        if (position.Y == Constants.FirstIndex && position.FacingDirection == Direction.SOUTH) return position;
        if (position.X == Constants.LastIndex && position.FacingDirection == Direction.EAST) return position;
        if (position.Y == Constants.LastIndex && position.FacingDirection == Direction.NORTH) return position;

        var newPosition = new RobotPosition()
            { X = position.X, Y = position.Y, FacingDirection = position.FacingDirection };

        switch (position.FacingDirection)
        {
            case Direction.NORTH:
                newPosition.Y += 1;
                break;
            case Direction.EAST:
                newPosition.X += 1;
                break;
            case Direction.SOUTH:
                newPosition.Y -= 1;
                break;
            case Direction.WEST:
                newPosition.X -= 1;
                break;
            default:
                throw new Exception("The facing direction is unknown.");
        }
        return newPosition;
    }
    
    public Direction Rotate(Direction currrentDirection, Command rotateDirection)
    {
        return currrentDirection switch
        {
            Direction.NORTH => rotateDirection == Command.LEFT ? Direction.WEST : Direction.EAST,
            Direction.EAST => rotateDirection == Command.LEFT ? Direction.NORTH : Direction.SOUTH,
            Direction.SOUTH => rotateDirection == Command.LEFT ? Direction.EAST : Direction.WEST,
            Direction.WEST => rotateDirection == Command.LEFT ? Direction.SOUTH : Direction.NORTH,
            _ => throw new Exception("Current direction is unknown.")
        };
    }
}