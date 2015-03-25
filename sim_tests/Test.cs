using NUnit.Framework;
using System;
using Hats.Time;
using Hats.device_commands;
using System.Net.Http;


namespace sim_tests
{
[TestFixture ()]
public class Test
{
	[Test ()]
	public void SettingTimeFrame()
	{
		TimeFrame frame = new TimeFrame();
		Assert.True(TimeInterface.setTimeFrameFromString (frame, ""));
	}

	[Test ()]
	public void CreatingTimeFrame()
	{
		TimeFrame frame = new TimeFrame();
		Assert.Throws<ArgumentException>(() => frame.setTimeFrame(rate: -1.0));
	}

	[Test ()]
	public void CreateDevice()
	{
		int houseID = 0;
		int roomID = 0;
		HttpClient newClient = new HttpClient();
		int newID = DeviceCommands.createDevice("devClass", "devType", "devName", houseID, roomID, newClient);
		Assert.True(newID == 0);
	}

	[Test ()]
	public void RemoveDevice()
	{
		int houseID = 0;
		int roomID = 0;
		HttpClient newClient = new HttpClient();
		int newID = DeviceCommands.createDevice("devClass", "devType", "devName", houseID, roomID, newClient);
		bool removed = DeviceCommands.removeDevice(houseID, roomID, newID, newClient);
		Assert.True(removed == false);
	}
	[Test ()]
	public void ChangeDeviceName()
	{
		int houseID = 0;
		int roomID = 0;
		HttpClient newClient = new HttpClient();
		int newID = DeviceCommands.createDevice("devClass", "devType", "devName", houseID, roomID, newClient);
		bool nameChanged = DeviceCommands.changeDeviceName(houseID, roomID, newID, "devName", newClient);
		Assert.True(nameChanged == false);
	}
}
}

