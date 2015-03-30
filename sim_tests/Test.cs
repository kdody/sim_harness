using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Hats.device_commands;
using Hats.SimWeather;
using Hats.Time;
using NUnit.Framework;

namespace sim_tests
{
[TestFixture ()]
public class Test
{
	[Test ()]
	public void SettingTimeFrame()
	{
		var frame = new TimeFrame();
		Assert.True(TimeInterface.setTimeFrameFromString (frame, ""));
	}

	[Test ()]
	public void CreatingTimeFrame()
	{
		var frame = new TimeFrame();
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

	public void TimeRate()
	{
		const Double SimRate = 2.0;
		var doubleTime = new TimeFrame();

		var then = DateTime.Now;
		var diff = Math.Abs((then - doubleTime.time(then)).TotalSeconds);
		Assert.Less(diff, 1e-3);

		doubleTime.setTimeFrame(rate: SimRate);

		then = DateTime.Now;
		Thread.Sleep(2000);
		var now = DateTime.Now;

		var simSpan = doubleTime.time(now) - doubleTime.time(then);
		var realSpan = now - then;

		var computedRate = simSpan.TotalSeconds / realSpan.TotalSeconds;
		diff = Math.Abs(computedRate - SimRate); 
		Assert.Less(diff, 0.01);
	}

	[Test ()]
	public void LinearWeather()
	{
		var frame = new TimeFrame();
		var linear = new LinearWeather(frame);

		Assert.IsNaN(linear.Temperature());

		const Double StartingTemp = 75.0;
		TemperatureSetPoint temp_a = new TemperatureSetPoint(DateTime.Now, StartingTemp);
		linear.Add(temp_a);

		Assert.AreEqual(StartingTemp, linear.Temperature());
	}
}
}
