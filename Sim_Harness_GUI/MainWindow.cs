using System;
using Gtk;
using Sim_Harness_GUI;

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

	protected void OnStartTestButtonClicked (object sender, EventArgs e)
	{
		this.testSenarioComboBox.AppendText("Hello");
	}

	protected void OnNewSenarioCreateButtonClicked (object sender, EventArgs e)
	{
		Dialog  newSenario = new New_Senario();
		int response = newSenario.Run();
		if(response == (int) Gtk.ResponseType.Ok)
		{
			Console.WriteLine("EXIT OKAY");
		} 
		else
		{
			Console.WriteLine("EXIT CANCEL");
		}

		newSenario.Destroy();
	}
}
