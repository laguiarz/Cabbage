using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUG.Packages.KBCodeReview
{
    static class ArcHelper
    {
        public static void ArcDiff()
        {
            ExecuteCommand.ExecuteArc("arc diff");
            ArrayList lines = new ArrayList();
            lines.Add("Executing Arc Diff");
            GxConsoleHandler.GitConsoleWriter(lines, "KBCodeReview - Execute Arc diff", true);
        }

        public static void ArcLand()
        {
            ExecuteCommand.ExecuteArc("arc land");
            ArrayList lines = new ArrayList();
            lines.Add("Executing Arc Land");
            GxConsoleHandler.GitConsoleWriter(lines, "KBCodeReview - Execute Arc land", true);
        }
    }
}
