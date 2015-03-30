using System;
using System.Collections.Generic;
using Hats.SimWeather;
using Hats.Time;

namespace Hats.SimWeather
{

public class TemperatureSetPoint : IComparable
{

	public TemperatureSetPoint()
	{
	}

	public TemperatureSetPoint(DateTime time, Double temp)
	{
		Time = time;
		Temp = temp;
	}

	/**
	 * Time at which this temperature should be valid
	 */
	public DateTime Time { get; set; }
	/**
	 * Temperature, in Celsius, which this set point represents.
	 */
	public Double Temp { get; set; }

	public int CompareTo(object obj)
	{
		return Time.CompareTo((obj as TemperatureSetPoint).Time);
	}
}

/**
 * Class which simulates weather as a linear interpolation between a sequence of
 * temperature set points.
 */
public class LinearWeather : IWeather
{
	protected TimeFrame _frame;
	protected List<TemperatureSetPoint> _temps;

	public LinearWeather()
	{
		_frame = new TimeFrame();
		_temps = new List<TemperatureSetPoint>();
	}

	public LinearWeather(TimeFrame frame)
	{
		_frame = frame;
		_temps = new List<TemperatureSetPoint>();
	}

	public LinearWeather(TimeFrame frame, List<TemperatureSetPoint> temps)
	{
		_frame = frame;
		_temps = temps;
		_temps.Sort();
	}

	/**
	 * Adds a TemperatureSetPoint to the Weather Simulation.
	 */
	public void Add(TemperatureSetPoint temp)
	{
		int idx = _temps.BinarySearch(temp);

		if(idx == 0)
		{
			_temps.Add(temp);
			return;
		}
		else if(idx < 0)
		{
			idx = ~idx;
		}
		_temps.Insert(idx, temp);
	}

	/**
	 * Gets the temperature at the current time, according to the given TimeFrame.
	 * \param[out] Current temperature in Celsius, NaN if no setpoints exist
	 */
	public Double Temperature()
	{
		if(_temps.Count == 0)
		{
			return Double.NaN;
		}
		DateTime now = _frame.now();
		var search_point = new TemperatureSetPoint(now, 0.0);
		int idx = _temps.BinarySearch(search_point);

		Double temp;

		if(idx >= 0)
		{
			temp = _temps[idx].Temp;
		}
		else
		{
			idx = ~idx;

			if(idx == _temps.Count)
			{
				temp = _temps[idx - 1].Temp;
			}
			else
			{
				int low_idx = idx - 1;

				DateTime low_time = _temps[low_idx].Time;
				Double now_dt = (now - low_time).TotalSeconds;
				Double all_dt = (_temps[idx].Time - low_time).TotalSeconds;
				Double dTemp = _temps[idx].Temp - _temps[low_idx].Temp;
				temp = _temps[low_idx].Temp + dTemp * (now_dt / all_dt);
			}
		}

		return temp;
	}
}

}
