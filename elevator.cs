//0.0
//4.2
//8.5


public void Main(string args)
{
  IMyBlockGroup group = GridTerminalSystem.GetBlockGroupWithName("Elevator Pistons");
  if (group == null)
  {
    Echo("Group not found");
    return;
  }
  Echo($"{group.Name}:");
  List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
  group.GetBlocks(blocks);
  if (args.ToLower() == "1st")
  {
    foreach (var block in blocks)
    {
      Echo($"- {block.CustomName}");
      var piston = block as IMyPistonBase;
      if (piston.CurrentPosition > 0.0)
      {
        piston.MinLimit = 0.0f;
        piston.MaxLimit = 0.0f;
        piston.ApplyAction("Retract");
      }
    }
  }
  if (args.ToLower() == "2nd")
  {
    foreach (var block in blocks)
    {
      Echo($"- {block.CustomName}");
      var piston = block as IMyPistonBase;
      if (piston.CurrentPosition > 4.2)
      {
        piston.MinLimit = 4.2f;
        piston.MaxLimit = 4.2f;
        piston.ApplyAction("Retract");
        }
        if (piston.CurrentPosition < 4.2)
        {
          piston.MinLimit = 4.2f;
          piston.MaxLimit = 4.2f;
          piston.ApplyAction("Extend");
        }
      }
    }
    if (args.ToLower() == "3rd")
    {
      foreach (var block in blocks)
      {
        Echo($"- {block.CustomName}");
        var piston = block as IMyPistonBase;
        if (piston.CurrentPosition > 8.5)
        {
          piston.MinLimit = 8.5f;
          piston.MaxLimit = 8.5f;
          piston.ApplyAction("Retract");
        }
        if (piston.CurrentPosition < 8.5)
        {
          piston.MinLimit = 8.5f;
          piston.MaxLimit = 8.5f;
          piston.ApplyAction("Extend");
        }
      }
    }
}
