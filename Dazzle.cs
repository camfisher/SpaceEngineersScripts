public bool on = false;
public int displacement = 1;
public int count = 0;
// CurrentPosition
public int horizontal = 0;
public int vertical = 0;
public int forward = 0;

// Make These Match CurrentPosition
public int horizontalhold = 0;
public int verticalhold = 0;
public int forwardhold = 0;

public Program()
{
  Runtime.UpdateFrequency = UpdateFrequency.Update1;
}

public void Main(string args)
{
  IMyBlockGroup group = GridTerminalSystem.GetBlockGroupWithName("Dazzle - Projectors");
  if (group == null)
  {
    Echo("Group not found");
    return;
  }
  List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
  group.GetBlocks(blocks);
  if (args.ToLower() == "run" && on == false)
  {
    foreach (var block in blocks)
    {
      Echo($"- {block.CustomName}");
      var proj = block as IMyProjector;
      proj.ApplyAction("OnOff_On");
      on = true;
    }
  }
  if (args.ToLower() == "stop" && on == true)
  {
    foreach (var block in blocks)
    {
      Echo($"- {block.CustomName}");
      var proj = block as IMyProjector;
      // Reset Horizontal
      while (horizontalhold > horizontal)
      {
        proj.ApplyAction("DecreaseX");
        horizontalhold--;
      }
      while (horizontalhold < horizontal)
      {
        proj.ApplyAction("IncreaseX");
        horizontalhold++;
      }

      // Reset Vertical
      while (verticalhold > vertical)
      {
        proj.ApplyAction("DecreaseZ");
        verticalhold--;
      }
      while (verticalhold < vertical)
      {
        proj.ApplyAction("IncreaseZ");
        verticalhold++;
      }
      proj.ApplyAction("OnOff_Off");
      on = false;
    }
  }
  if (on == false){Echo("Status: Paused");}
  else if (on == true)
  {
    if (count >= 0 && count < 5)
    {
      Echo("Status: ***||***");
      count++;
    }
    else if (count >= 5 && count < 10)
    {
      Echo("Status: **|**|**");
      count++;
    }
    else if (count >= 10 && count < 15)
    {
      Echo("Status: *|****|*");
      count++;
    }
    else if (count >= 15 && count < 20)
    {
      Echo("Status: |******|");
      count = 0;
    }
    foreach (var block in blocks)
    {
      Echo($"- {block.CustomName}");
      var proj = block as IMyProjector;

      Random rnd = new Random();
      int newx = rnd.Next(3);
      if (newx == 2) {newx = -1;}
      Echo($"X - {newx}");
      if (horizontalhold < (horizontal + displacement) && newx == 1)
      {
        horizontalhold += newx;
        proj.ApplyAction("IncreaseX");
      }
      else if (horizontalhold > (horizontal - displacement) && newx == -1)
      {
        horizontalhold += newx;
        proj.ApplyAction("DecreaseX");
      }
      int newy = rnd.Next(3);
      if (newy == 2) {newy = -1;}
      Echo($"Y - {newy}");
      if (forwardhold < (forward + displacement) && newy == 1)
      {
        forwardhold += newy;
        proj.ApplyAction("IncreaseY");
      }
      else if (forwardhold > (forward - displacement) && newy == -1)
      {
        forwardhold += newy;
        proj.ApplyAction("DecreaseY");
      }
      int newz = rnd.Next(3);
      if (newz == 2) {newz = -1;}
      Echo($"Z - {newz}");
      if (verticalhold < (vertical + displacement) && newz == 1)
      {
        verticalhold += newz;
        proj.ApplyAction("IncreaseZ");
      }
      else if (verticalhold > (vertical - displacement) && newz == -1)
      {
        verticalhold += newz;
        proj.ApplyAction("DecreaseZ");
      }
    }
  }
}
