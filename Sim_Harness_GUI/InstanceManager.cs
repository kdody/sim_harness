using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Sim_Harness_GUI
{
public class InstanceManager
{
	protected string _scenario;
	protected List<Process> _houses;
	protected List<Process> _apps;
	protected string _house_path;
	protected string _app_path;
	public InstanceManager(string scenario, string house_path, string app_path)
	{
		_scenario = scenario;
	}

	public bool Launch()
	{
		//TODO: Add validation that everything is ready to launch
		//TODO: Add backing store in case of crash
		foreach(Process proc in _houses)
		{
			proc.Start();
		}

		foreach(Process proc in _apps)
		{
			proc.Start();
		}

		return true;
	}

	public void End()
	{
		foreach(Process proc in _houses)
		{
			proc.Kill();
		}
		foreach(Process proc in _apps)
		{
			proc.Kill();
		}
	}

	public void BuildLists()
	{
		Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(_scenario);
		List<Dictionary<string, string>> house_dict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(values["houses"]);
		List<Dictionary<string, string>> app_dict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(values["users"]);

		foreach(Dictionary<string, string> entry in house_dict)
		{
			string args = String.Concat("--house ", entry["name"]);
			args = String.Concat(" --scenario ", _scenario);
			var process = new Process();
			process.StartInfo.FileName = _house_path;
			process.StartInfo.Arguments = args;
			_houses.Add(process);
		}

		foreach(Dictionary<string, string> entry in app_dict)
		{
			string args = String.Concat("--user ", entry["name"]);
			args = String.Concat(" --scenario ", _scenario);
			var process = new Process();
			process.StartInfo.FileName = _app_path;
			process.StartInfo.Arguments = args;
			_apps.Add(process);
		}
	}
}
}

