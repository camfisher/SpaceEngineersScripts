public void Main(string arg)
{
   var out1 = GridTerminalSystem.GetBlockWithName("Airlock1ExteriorDoor") as IMyDoor;
   var in1= GridTerminalSystem.GetBlockWithName("Airlock1InteriorDoor") as IMyDoor;
   var air1= GridTerminalSystem.GetBlockWithName("Airlock1AirVent") as IMyAirVent;
   if (arg.ToLower() == "cycle")
   {
     if (in1.Status == DoorStatus.Closed)
     {
       out1.ApplyAction("OnOff_On");
       out1.ApplyAction("Open_Off");
       air1.ApplyAction("Depressurize_Off");
       for(int i = 0; i < 10000; i++)
       {
         Echo(":" + air1.GetOxygenLevel());
         if (air1.GetOxygenLevel() > 0.0)
         {
           in1.ApplyAction("Open_On");
           out1.ApplyAction("OnOff_Off");
         }
       }
     }
     if (out1.Status == DoorStatus.Closed)
     {
       in1.ApplyAction("OnOff_On");
       in1.ApplyAction("Open_Off");
       air1.ApplyAction("Depressurize_On");
       for(int i = 0; i < 10000; i++)
       {
         Echo(":" + air1.GetOxygenLevel());
         if (air1.GetOxygenLevel() < 1)
         {
           out1.ApplyAction("Open_On");
           in1.ApplyAction("OnOff_Off");
         }
       }
     }
   }
}
