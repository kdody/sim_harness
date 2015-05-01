﻿using System;
using Gtk;
using Sim_Harness_GUI;
using System.IO;

using System;
using Gtk;
//using Hats.Time;
using Newtonsoft.Json;
using Sim_Harness_GUI;
using System.IO;

using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;

using System.Net.Http;
using System.Threading.Tasks;

public partial class MainWindow: Gtk.Window
{
	protected InstanceManager _instances;
	protected string urlserver;

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Console.WriteLine("Build");
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Console.WriteLine("Deleted");
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnStartTestButtonClicked(object sender, EventArgs e)
	{
		String jsonStartString = buildStartString();
		currentTestTextview.Buffer.Text = jsonStartString;
		//_instances = new InstanceManager(scenarioDirectoryText.Text, houseSimLocationEntry.Text, appSimLocationEntry.Text);
		//_instances.Start();
		startTestButton.Sensitive = false;
		endTestButton.Sensitive = true;



	}

	protected void OnLoadScenarioButton(object sender, EventArgs e)
	{
		var item = new Gtk.TreeIter();
		this.testScenarioComboBox.GetActiveIter(out item);

		//TODO: Read in file, prep for launch here
		Console.WriteLine(this.testScenarioComboBox.Model.GetValue(item, 1));
	}

	protected void OnAppSimulatorChooseFileButtonClicked(object sender, EventArgs e)
	{
		this.appSimLocationEntry.Text = this.selectFile();
	}

	protected void OnHouseSimLocationButtonClicked(object sender, EventArgs e)
	{
		this.houseSimLocationEntry.Text = this.selectFile();
	}

	protected String selectFile()
	{
		String returnText = "";
		Gtk.FileChooserDialog filechooser =
			new Gtk.FileChooserDialog("Choose the file to select",
				this,
				FileChooserAction.Open,
				"Cancel", ResponseType.Cancel,
				"Select", ResponseType.Accept);

		if(filechooser.Run() == (int)ResponseType.Accept)
		{
			returnText = filechooser.Filename;
		}

		filechooser.Destroy();
		return returnText;
	}

	protected void OnScenarioDirectoryLoad(object sender, EventArgs e)
	{
		Gtk.FileChooserDialog chooser = new Gtk.FileChooserDialog("Select Scenario Directory",
			                                this,
			                                FileChooserAction.SelectFolder,
			                                "Cancel", ResponseType.Cancel,
			                                "Select", ResponseType.Accept);

		if(chooser.Run() == (int)ResponseType.Accept)
		{
			this.buildScenarioList(chooser.Filename);
			this.scenarioDirectoryText.Text = chooser.Filename;
		}

		chooser.Destroy();
	}

	protected void buildScenarioList(String dir)
	{
		var newList = new ListStore(typeof(string), typeof(string));
		foreach(string file in Directory.EnumerateFiles(dir, "*.json"))
		{
			string scenario = System.IO.Path.GetFileNameWithoutExtension(file);
			Console.WriteLine("Adding " + scenario);
			newList.AppendValues(scenario, file);
		}
		this.testScenarioComboBox.Model = newList;
		this.testScenarioComboBox.Active = 0;
	}

	protected void OnScenarioDirectoryTextChanged (object sender, EventArgs e)
	{
		changeStartButton();
	}

	protected void OnAppSimLocationEntryChanged (object sender, EventArgs e)
	{
		changeStartButton();
	}

	protected void OnHouseSimLocationEntryChanged (object sender, EventArgs e)
	{
		changeStartButton();
	}

	/**
	 * Changes the start button to clickable if the files are valid
	 */ 
	private void changeStartButton()
	{
		if(checkFiles())
		{
			startTestButton.Sensitive = true;
		}
		else
		{
			startTestButton.Sensitive = false;
		}
	}

	/**
	 * Makes sure the three files selected are 
	 */
	private bool checkFiles()
	{
		if(File.Exists(scenarioDirectoryText.Text + "/" + testScenarioComboBox.ActiveText + ".json") && File.Exists(appSimLocationEntry.Text) && File.Exists(houseSimLocationEntry.Text))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private string buildStartString()
	{
		string jsonString = "{\n\t\"TimeFrame\": {" +
			"\n\t\t\"wall\": \"" + DateTime.Now.ToString("o") + "\"," +
			"\n\t\t\"sim\": \"" + DateTime.Now.ToString("o") + "\"," + 
			"\n\t\t\"rate\": " + timeFrameSpeedSpinbutton.Text +
			"\n\t}\n}";
		return jsonString;
	}

	protected void OnEndTestButtonClicked (object sender, EventArgs e)
	{
		_instances.Kill();
		endTestButton.Sensitive = false;
		startTestButton.Sensitive = true;
	}

	public void postTimeFrame(string time){
		/*WebRequest request = WebRequest.CreateHttp("https://posttestserver.com/post.php");
		request.Method = "POST";
		request.ContentType = "application/json";
		byte[] byteArray = Encoding.UTF8.GetBytes(time);
		Stream data = request.GetRequestStream();
		request.ContentLength = byteArray.Length; //byteArray
		data.Write(byteArray, 0, byteArray.Length);
		data.Close();*/


		currentTestTextview.Buffer.Text = "Make request to server:\n\n\t" + time + "\n\n\tServer: http://requestb.in/1ehzgva1\n\n";

		var task = MakeRequest(time);
		currentTestTextview.Buffer.Text += "\t Waiting...\n\n";
		task.Wait();


		var response = task.Result;

		currentTestTextview.Buffer.Text += "\tResponse: " + response + "\n\n----------------------------------\n\n";


		var body = response.Content.ReadAsStringAsync().Result;


	}
	private static async Task<HttpResponseMessage> MakeRequest(string time)
	{
		var httpClient = new HttpClient();
		await httpClient.GetAsync(new Uri("http://requestb.in/1ehzgva1"));

		var stringContent = new StringContent(time);

		var response= await httpClient.PostAsync("http://requestb.in/1ehzgva1", stringContent);	
		return response;
	}

	protected void OnServerURLEntryChanged (object sender, EventArgs e)
	{
		urlserver = serverURLEntry.Text;
		//throw new NotImplementedException ();
	}
}
