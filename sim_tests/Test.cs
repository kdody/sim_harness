using NUnit.Framework;
using System;
using Hats.Time;

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
}
}

