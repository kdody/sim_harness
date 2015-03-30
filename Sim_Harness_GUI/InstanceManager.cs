using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Script.Serialization;

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
		var jss = new JavaScriptSerializer();
		var dict = jss.Deserialize<dynamic>(_scenario);

		List<Dictionary<dynamic>> houses = dict["houses"];
		List<Dictionary<dynamic>> apps = dict["users"];

		foreach(Dictionary<dynamic> entry in houses)
		{
			string args = String.Concat("--house ", entry["name"]);
			args = String.Concat(" --scenario ", _scenario);
			var process = new Process();
			process.StartInfo.Filename = _house_path;
			process.StartInfo.Arguments = args;
			_houses.Add(process);
		}

		foreach(Dictionary<dynamic> entry in devices)
		{
			string args = String.Concat("--user ", entry["name"]);
			args = String.Concat(" --scenario ", _scenario);
			var process = new Process();
			process.StartInfo.Filename = _app_path;
			process.StartInfo.Arguments = args;
			_apps.Add(process);
		}
	}
}
}

