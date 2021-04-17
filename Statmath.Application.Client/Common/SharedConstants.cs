namespace Statmath.Application.Client.Common
{
    internal class SharedConstants
    { 
        public const string ConsolePrefix = "$ ";
        public const string UnknownCommand = "Unkown command. For more information enter: help";

        // commands
        public const string CommandExit = "exit";
        public const string CommandHelp = "help";
        public const string CommandClear = "clr";

        // read command
        public const string CommandRead = "read";

        // create command
        public const string CommandCreate = "create";
        public const string CommandCreateUnkown = "Invalid create command. For more information enter: create help";
        public const string CommandCreateFileNotFound = "The file you entered is not existing";
        public const string CommandCreateUnreadable = "The file you entered is not readable";
        public const string CommandCreateInvalid = "The file you entered is not valid";

        // command arfs
        public const string CmdArgAll = "--all";
        public const string CmdArgDate = "--date";
        public const string CmdArgDateEnd = "--end";
        public const string CmdArgDateStart = "--start";
        public const string CmdArgJob = "--job";
        public const string CmdArgMachine = "--machine";

        public const string HorizontalLine = "-------------------------------------------------------------------";

        public const char CommandDelimiter = ' ';
    }
}
