int cycle = 0;

float rotationspeed = 0.5f;
float frontlength = 50f;

public Program()
{
  Runtime.UpdateFrequency = UpdateFrequency.Update10;
}

void Main(string arg)
{
  string ERR_TXT = "";
  bool front = false;

  //Setup Sensor
  List<IMyTerminalBlock> sense = new List<IMyTerminalBlock>();
  IMySensorBlock sensor = null;
  GridTerminalSystem.GetBlocksOfType<IMySensorBlock>(sense);
  if (sense.Count == 0)
  {
    ERR_TXT += "No Sensor Block Found\n";
  }
  else
  {
    for (int i = 0; i < sense.Count; i++)
    {
      if (sense[i].CustomName == "Front Sensor" && front == false)
      {
        sensor = (IMySensorBlock)sense[i];
        front = true;
      }
      else
      {
        ERR_TXT += "Multiple Front Sensors Found\n";
      }
    }
    if (sensor == null)
    {
      ERR_TXT += "No Sensor Block Named \"Front Sensor\" Found\n";
    }
  }

  //Setup Gyros
  List<IMyTerminalBlock> gyro = new List<IMyTerminalBlock>();
  GridTerminalSystem.GetBlocksOfType<IMyGyro>(gyro);
  if(gyro.Count == 0) {
    ERR_TXT += "no Gyroscope blocks found\n";
  }

  if (ERR_TXT != "")
  {
    Echo("Scrip Errors:\n" + ERR_TXT + "(Make Sure Block Ownership is Set Correctly)");
    return;
  }
  else {Echo("");}

  if (cycle == 0 || cycle == 1) //Top
  {
    sensor.LeftExtend = 10f;
    sensor.RightExtend = 1f;
    sensor.BottomExtend = 1f;
    sensor.TopExtend = 10f;
    sensor.BackExtend = 1f;
    sensor.FrontExtend = frontlength;
    cycle++;
  }
  else if (cycle == 2 || cycle == 3)//Left
  {
    sensor.LeftExtend = 10f;
    sensor.RightExtend = 1f;
    sensor.BottomExtend = 10f;
    sensor.TopExtend = 1f;
    sensor.BackExtend = 1f;
    sensor.FrontExtend = frontlength;
    cycle++;
  }
  else if (cycle == 4 || cycle == 5)//Bottom
  {
    sensor.LeftExtend = 1f;
    sensor.RightExtend = 10f;
    sensor.BottomExtend = 10f;
    sensor.TopExtend = 1f;
    sensor.BackExtend = 1f;
    sensor.FrontExtend = frontlength;
    cycle++;
  }
  else if (cycle == 6 || cycle == 7)//Right
  {
    sensor.LeftExtend = 1f;
    sensor.RightExtend = 10f;
    sensor.BottomExtend = 1f;
    sensor.TopExtend = 10f;
    sensor.BackExtend = 1f;
    sensor.FrontExtend = frontlength;
    cycle++;
  }
  else if (cycle == 8 || cycle == 9)//Center
  {
    sensor.LeftExtend = 1f;
    sensor.RightExtend = 1f;
    sensor.BottomExtend = 1f;
    sensor.TopExtend = 1f;
    sensor.BackExtend = 1f;
    sensor.FrontExtend = frontlength;
    cycle = 0;
  }

  //Check for Enemies
  if (cycle == 0 && sensor.IsActive == true)
  {
    for (int i = 0; i < gyro.Count; i++)
    {
      gyro[i].GyroOverride = true;
      gyro[i].Yaw = rotationspeed;
    }
    Echo("Top");
    Echo($"{sensor.IsActive}");
  }
  else if (cycle == 1 && sensor.IsActive == true)
  {
    for (int i = 0; i < gyro.Count; i++)
    {
      gyro[i].GyroOverride = true;
      gyro[i].Pitch = rotationspeed;
    }
    Echo("Left");
    Echo($"{sensor.IsActive}");
  }
  else if (cycle == 2 && sensor.IsActive == true)
  {
    for (int i = 0; i < gyro.Count; i++)
    {
      gyro[i].GyroOverride = true;
      gyro[i].Yaw = rotationspeed * -1f;
    }
    Echo("Bottom");
    Echo($"{sensor.IsActive}");
  }
  else if (cycle == 3 && sensor.IsActive == true)
  {
    for (int i = 0; i < gyro.Count; i++)
    {
      gyro[i].GyroOverride = true;
      gyro[i].Pitch = rotationspeed * -1f;
    }
    Echo("Right");
    Echo($"{sensor.IsActive}");
  }
  else
  {
    gyro.Pitch = 0;
    gyro.Yaw = 0;
  }
}
