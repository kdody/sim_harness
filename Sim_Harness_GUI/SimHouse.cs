using System;
using System.Diagnostics;


/**
 * SimHouse Class
 * Instantiate simulated houses
 * \author: Nate Hughes <njh2986@vt.edu>
 */
namespace Hats.Sim
{
public class SimHouse
{	
	/**
 	 * Instantiates a SimHouse object - a simulated house
 	 * \param[in] scenarioConfig String containing the scenario config in JSON format
 	 */
	public SimHouse(String scenarioConfig, string app_path, string house_id)
	{
		string args = String.Concat("--house ", house_id);
		args = String.Concat(" --scenario ", scenarioConfig);
		_process = new Process();
		_process.StartInfo.FileName = app_path;
		_process.StartInfo.Arguments = args;
	}

	public bool Start()
	{
		return _process.Start();
	}

	public void Kill()
	{
		_process.Kill();
		_process.WaitForExit();
	}

	protected Process _process;
	// NOTE: the ready signal is gotten via the server, not directly from the sim house
}
}