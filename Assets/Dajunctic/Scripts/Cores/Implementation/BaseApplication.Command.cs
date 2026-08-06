using System;
using System.Collections.Generic;

namespace Dajunctic
{
    public partial class BaseApplication
    {
        public CommandResult SendCommand(ICommand command)
        {
            return command.Execute();   
        }
    }
}