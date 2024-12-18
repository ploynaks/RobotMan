// See https://aka.ms/new-console-template for more information

using RobotMan;
using RobotMan.Enums;


if (!Enum.TryParse<Command>(args[0], out var placeCommand))
{
    Console.WriteLine("Command is not recognised.");
    return;
}

if (placeCommand != Command.PLACE)
{
    Console.WriteLine("You need to place your robot first.");
    return;
}
    
var splitInput = args[1].Split(",");
if (splitInput.Length != 3)
{
    Console.WriteLine("Invalid arguments.");
    return;
}

if (int.TryParse(splitInput[0], out var x)
    && int.TryParse(splitInput[1], out var y)
    && Enum.TryParse<Direction>(splitInput[2], out var direction))
{
    if ((x < Constants.FirstIndex || x > Constants.LastIndex)
        && (y < Constants.FirstIndex || y > Constants.LastIndex))
    {
        Console.WriteLine("Invalid positions.");
        return;
    }

}
else
{
    Console.WriteLine("Invalid arguments.");
    return;
}

var commands = new List<Command>();
foreach (var arg in args.Skip(2))
{
    if (Enum.TryParse<Command>(arg, out var result))
        commands.Add(result);
}

var position = new RobotPosition() { X = x, Y = y, FacingDirection = direction };
RobotMoveService service = new RobotMoveService();
foreach (var command in commands)
{
    position = service.Execute(position, command);
}
