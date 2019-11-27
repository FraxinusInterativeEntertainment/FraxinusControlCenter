using System.Collections;
using UnityEngine;
using PureMVC.Patterns;

public class StartupCommand : MacroCommand
{
    protected override void InitializeMacroCommand()
    {
        AddSubCommand(typeof(ModelPreCommand));
    }
}
