using System;
using Gtk;
using Sim_Harness_GUI;
using System.IO;
using System.Messaging; //message queues
using System.Diagnostics; //processes


public partial class MainWindow: Gtk.Window
{
	protected InstanceManager _instances;
	ProcessStartInfo appGenerator_info = new ProcessStartInfo();
	ProcessStartInfo houseGenerator_info = new ProcessStartInfo();
	Process appGenerator, houseGenerator;
	MessageQueue appQueue_r, appQueue_w, houseQueue_r, houseQueue_w;
	string appQueueName_r = @".\private$\appQueueRead";
	string appQueueName_w = @".\private$\appQueueWrite";
	string houseQueueName_r = @".\private$\houseQueueRead";
	string houseQueueName_w = @".\private$\houseQueueWrite";
	//NOTE: names are from the parent's (this program's) perspective

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

		//open the message queues
		openMessageQueue(ref appQueue_r, ref appQueueName_r);
		openMessageQueue(ref appQueue_w, ref appQueueName_w);
		openMessageQueue(ref houseQueue_r, ref houseQueueName_r);
		openMessageQueue(ref houseQueue_w, ref houseQueueName_w);

		//set process settings
		appGenerator_info.FileName = appSimLocationEntry.Text;
		houseGenerator_info.FileName = houseSimLocationEntry.Text;
		appGenerator_info.Arguments = string.Concat(appQueueName_r, " ", appQueueName_w);
		houseGenerator_info.Arguments = string.Concat(houseQueueName_r, " ", houseQueueName_w);

		//start the processes
		currentTestTextview.Buffer.Text += "App: ";
		currentTestTextview.Buffer.Text += startProcess(ref appGenerator, ref appGenerator_info);
		currentTestTextview.Buffer.Text += "House: ";
		currentTestTextview.Buffer.Text += startProcess(ref houseGenerator, ref houseGenerator_info);

		//send the first JSON string through the queue
		sendMessage(ref appQueue_w, ref jsonStartString);
		sendMessage(ref houseQueue_w, ref jsonStartString);

		//receive confirmation messages
		currentTestTextview.Buffer.Text += "App: ";
		currentTestTextview.Buffer.Text += receiveMessage(ref appQueue_r);
		currentTestTextview.Buffer.Text += "\n";
		currentTestTextview.Buffer.Text += "House: ";
		currentTestTextview.Buffer.Text += receiveMessage(ref houseQueue_r);
		currentTestTextview.Buffer.Text += "\n";

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
		currentTestTextview.Buffer.Text += ("App: ");
		currentTestTextview.Buffer.Text += (killProcess(ref appGenerator));
		currentTestTextview.Buffer.Text += ("House: ");
		currentTestTextview.Buffer.Text += (killProcess(ref houseGenerator));
		endTestButton.Sensitive = false;
		startTestButton.Sensitive = true;
	}


	//  helper functions //

	private string startProcess(ref Process p, ref ProcessStartInfo ps) {
		string output = "";
		try {
			p = Process.Start(ps);
			output = string.Concat(output,"Process started successfully\n");
		} catch (Exception ex) {
			output = string.Concat(output, "Unable to start process from location: ");
			output = string.Concat(output, ps.FileName);
			output = string.Concat(output, "\n");
		}
		return output;
	}

	private string killProcess(ref Process p){
		string output = "";
		try {
			p.Kill();
			output = string.Concat(output,"Process killed successfully\n");
		} catch (InvalidOperationException ex) {
			output = string.Concat(output, "InvalidOperationException thrown - the process has already exited\n");
		} catch (Exception ex) {
			output = string.Concat(output, "Exception thrown while trying to kill the process\n");
			output = string.Concat(output, ex.Message);
			output = string.Concat(output, "\n");
		}
		return output;
	}

	private void openMessageQueue(ref MessageQueue mQueue, ref string mQueue_name){
		if (!MessageQueue.Exists(mQueue_name))
			mQueue = MessageQueue.Create(mQueue_name); //create the queue if it doesn't exist
		else {
			mQueue = new MessageQueue(mQueue_name); //connect to the existing queue
			mQueue.Purge(); //delete any messages that might be in the existing queue
		}
	}

	private void sendMessage(ref MessageQueue mQueue, ref string message) {
		System.Messaging.Message messagetosend = new System.Messaging.Message();
		messagetosend.Body = message;
		mQueue.Send(messagetosend);
	}

	private string receiveMessage(ref MessageQueue mQueue) {
		string output = "";
		System.Messaging.Message received_message = new System.Messaging.Message();
		received_message = mQueue.Receive(); //blocking
		received_message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
		output = (string)received_message.Body;
		return output;
	}
}

