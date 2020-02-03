public void Main(string args, UpdateType updateSource)
{
  IMyBlockGroup group = GridTerminalSystem.GetBlockGroupWithName("Solar Rotors");
  var izy = GridTerminalSystem.GetBlockWithName("Solar") as IMyProgrammableBlock;

  if (group == null)
  {
    Echo("Group not found");
    return;
  }
  Echo($"{group.Name}:");
  List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
  group.GetBlocks(blocks);
  foreach (var block in blocks)
  {
    Echo($"- {block.CustomName}");
    var rotor = block as IMyMotorAdvancedStator;
    if (rotor.Angle > 0.0 && args.ToLower() == "retract")
    {
      rotor.TargetVelocityRPM = -5.0f;
      rotor.LowerLimitDeg = 0.0f;
      rotor.UpperLimitDeg = 0.0f;
      izy.ApplyAction("OnOff_Off");
    }
    if (rotor.Angle < 0.0 && args.ToLower() == "retract")
    {
      rotor.TargetVelocityRPM = 5.0f;
      rotor.LowerLimitDeg = 0.0f;
      rotor.UpperLimitDeg = 0.0f;
      izy.ApplyAction("OnOff_Off");
    }
    if (args.ToLower() == "extend")
    {
      izy.ApplyAction("OnOff_On");
      rotor.LowerLimitDeg = -361f;
      rotor.UpperLimitDeg = 361f;
    }
  }

  IMyBlockGroup group1 = GridTerminalSystem.GetBlockGroupWithName("Solar Pistons");
  if (group == null)
  {
    Echo("Group not found");
    return;
  }
  Echo($"{group1.Name}:");
  List<IMyTerminalBlock> blockss = new List<IMyTerminalBlock>();
  group1.GetBlocks(blockss);
  if (args.ToLower() == "retract")
  {
    foreach (var block in blockss)
    {
      var piston = block as IMyPistonBase;
      Echo($"- {piston.CustomName}");
      piston.Velocity = 0.25f;
      piston.ApplyAction("Retract");
    }
  }
  if (args.ToLower() == "extend")
  {
    foreach (var block in blockss)
    {
      var piston = block as IMyPistonBase;
      Echo($"- {piston.CustomName}");
      piston.Velocity = 1f;
      piston.ApplyAction("Extend");
    }
  }
}
