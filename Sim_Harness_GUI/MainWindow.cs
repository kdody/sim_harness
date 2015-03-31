using System;
using Gtk;
using Sim_Harness_GUI;
using System.IO;

public partial class MainWindow: Gtk.Window
{
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
		//this.testSenarioComboBox.AppendText("Hello");
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

	protected InstanceManager _instances;
}
