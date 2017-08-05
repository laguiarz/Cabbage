using Artech.Architecture.Common.Services;
using System.Collections;

namespace GUG.Packages.KBCodeReview
{
    static class GxConsoleHandler
    {
        private static string OutputId = "General";

        public static bool WriteOutput(string msg)
        {
            string consoleMsg = '[' + Resources.AppName + "]:" + msg;
            IOutputService output = InitializeGXOutput();
            output.AddLine(consoleMsg);
            return true;
        }

        // THIS MUST BE SEPARATED FROM HERE
        public static void GitConsoleWriter(ArrayList lines,string title,bool success)
        {
            IOutputService output = InitializeGXOutput();
            GXWrtStartConsole(output, title);
            foreach (string ln in lines)
            {
                GXWrtLineConsole(output, ln);
            }
            GXEndOutputSection(output, title, success);
        }


        public static void GitAlreadyInitiated(string result) {
            IOutputService output = InitializeGXOutput();
            GXWrtLineConsole(output, result);
        }

        //------------------------------------------------------------------------------

        private static IOutputService InitializeGXOutput()
        {
            IOutputService output = CommonServices.Output;
            output.SelectOutput(OutputId);
            return output;
        }

        private static void GXWrtStartConsole(IOutputService output,string text)
        {
            output.StartSection(text);
        }

        public static void GXWrtLineConsole(IOutputService output, string text)
        {
            if (output == null)
            {
                output = CommonServices.Output;
            }
            output.AddLine(text);
        }

        public static void GXEndOutputSection(IOutputService output,string title,bool success)
        {
            output.EndSection(title, success);
            output.UnselectOutput(OutputId);
        }


        

    }
}
