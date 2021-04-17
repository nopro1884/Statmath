namespace Statmath.Application.Shared
{
    public class Constants
    {
        public const string ConsolePrefix = "$ ";
        public const string UnknownCommand = "Unknown command. For more information enter: help";

        // commands
        public const string CommandExit = "exit";
        public const string CommandHelp = "help";
        public const string CommandClear = "clr";
        public const char CommandDelimiter = ' ';

        // read command
        public const string CommandRead = "read";
        public const string CommandReadInvalidCommand = "Unknown command. Enter read --help for more information";

        // create command
        public const string CommandCreate = "create";
        public const string CommandCreateUnkown = "Invalid create command. For more information enter: create help";
        public const string CommandCreateFileNotFound = "The file you entered is not existing";
        public const string CommandCreateUnreadable = "The file you entered is not readable";
        public const string CommandCreateInvalid = "The file you entered is not valid";

        // command args
        public const string CmdArgAll = "all";
        public const string CmdArgDate = "date";
        public const string CmdArgDateTime = "datetime";
        public const string CmdArgDateEnd = "end";
        public const string CmdArgDateStart = "start";
        public const string CmdArgJob = "job";
        public const string CmdArgMachine = "machine";
        public const string CmdArgHelp = "--help";

        // Api Actions
        public const string ApiActionCreateMany = "create_many";
        public const string ApiActionCreate = "create";
        public const string ApiActionGetAll = "get_all";
        public const string ApiActionGetByDate = "get_by_date";
        public const string ApiActionGetByDateTime = "get_by_datetime";
        public const string ApiActionGetByMachine = "get_by_machine";
        public const string ApiActionGetByJob = "get_by_job";
        public const string ApiParamJob = "j";
        public const string ApiParamMachine = "m";
        public const string ApiParamTime = "t";
        public const string ApiParamDate = "d";

        // Api error messages
        public const string UnableToGetDataMessagePlaceholder = "%p%";
        public const string UnableToGetDataMessageWithParameter = "Unable to get any data with param: " + UnableToGetDataMessagePlaceholder;
        public const string UnableToGetDataMessage = "Unable to get any data";


        // Printer 
        public const string HeaderMachine = "  Machine ";
        public const string HeaderJob = "  Job  ";
        public const string HeaderStartDate = "       Start       ";
        public const string HeaderEndDate = "        End        ";
        public const string HeaderRow = "|" + HeaderMachine + "|" + HeaderJob + "|" + HeaderStartDate + "|" + HeaderEndDate + "|";
        public const string HorizontalLine = "--------------------------------------------------------------------------------";
        public const string HeaderHorizontalLine = "-----------+-------+-------------------+--------------------";
    }
}
