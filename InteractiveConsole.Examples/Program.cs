using System;
using InteractiveConsole;

View.Text("Here are all console colors:", newLine: true);
View.ValuesOf<ConsoleColor>();

using (ForeColor.Begin(ConsoleColor.Blue))
using (BackColor.Begin(ConsoleColor.Gray))
{
    if (Question.Ask("Is blue ok?").IsYes())
    {
        View.Text("Okay", newLine: true);
        return;
    }
    else
    {
        using (ForeColor.Ask("Choose new fore color:"))
        using (BackColor.Ask("Choose new back color:"))
        {
            View.Text("Great!", newLine: true);   
        }
    }
    
    View.Text("Back to blue.", newLine: true);
}
            
View.Text("Back to previous colors.");
