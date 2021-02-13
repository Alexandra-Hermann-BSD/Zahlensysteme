using System;
using Gtk;

public partial class MainWindow: Window
{
	public MainWindow()
		: base(WindowType.Toplevel)
	{
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
	protected void OnQuitActionActivated(object sender, EventArgs e)
	{
		Application.Quit();
	}
}
