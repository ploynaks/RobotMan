using RobotMan.Enums;

namespace RobotMan;

public class RobotPosition
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction FacingDirection { get; set; }

    public override string ToString()
    {
        return $"{X}, {Y}, {FacingDirection.ToString()}";
    }
}