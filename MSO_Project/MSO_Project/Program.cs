using System;
using Gtk;

public class GtkHelloWorld {

    public static void Main() {
        Application.Init();

        //Create the Window
        Window myWin = new Window("Nice Window");
        myWin.Resize(200,200);

        //Create a label and put some text in it.
        Label myLabel = new Label();
        myLabel.Text = "Hello World!!!!";

        //Add the label to the form
        myWin.Add(myLabel);

        //Show Everything
        myWin.ShowAll();

        Application.Run();
    }
}