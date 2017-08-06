using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUG.Packages.KBCodeReview
{
    static class GitHelper
    {

        public static bool GitInit()
        {
            string command = "git init ";
            return GitExecute(command);
        }

        public static bool GitClone(string url)
        {
            string command = "git clone " + url + " .";
            return GitExecute(command);
        }

        public static bool SetNotepadAsEditor()
        {
            string notepad = "'C:\\Program Files (x86)\\Notepad++\\notepad++.exe'";
            char quote = '"';
            string commandName = "git config core.editor " + quote + notepad + " - multiInst -notabbar -nosession -noPlugin" + quote;

            return GitExecute(commandName);
        }

        public static bool GitPull()
        {
            string commandName = "git pull";
            return GitExecute(commandName);
        }

        public static bool CommitPendingChanges()
        {
            string commandName = "git add .";
            bool resultOk = GitExecute(commandName);
            if (resultOk)
            {
                commandName = "git commit";
                resultOk = GitExecute(commandName);
            }

            return resultOk;
        }

        public static bool GitCheckout(string branchName, bool createFlag)
        {
            string commandName = "";
            if (createFlag)
            {
                commandName = "git checkout -b " + branchName;
            }
            else
            {
                 commandName = "git checkout " + branchName;
            }
            return GitExecute(commandName);
        }

        public static bool GitPush(string branchHame)
        {
            string commandName = "git push origin " + branchHame;
            return GitExecute(commandName);
        }

        public static bool GitPullRequest()
        {
            string commandName = "hub pull-request";
            return GitExecute(commandName);
        }

        public static bool HasPendingMerge(string branchName)
        {
            //When i'm about to start a new cycle i try to delete the local branch ;)
            //it will fail always... if there are pending merges it will say so
            //otherwise it will error out due to 
            // error: Cannot delete branch <branchname> checked out at '<path>'

            string commandName = "git branch -d " + branchName;
            string commandOutput;
            bool resultOk = GitExecute(commandName, out commandOutput);

            if (!resultOk && commandOutput.Contains("not found"))
            {
                //we are ok as the issue is that there is no branch het
                return false;
            }
            else
            {
                if (!resultOk && commandOutput.Contains("checked out at"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            //return false;
        }
    

        public static bool GitDeleteBranch(string branchName)
        {
            //first delete branch at server
            string commandName = "git push origin --delete" + branchName;
            string commandOutput;
            bool resultOk = GitExecute(commandName, out commandOutput);

            if (!resultOk)
            {
                //we could not delete the branch at the server, so don't continue...
                return false;
            }
            else
            {
                //delete local branch
                commandName = "git branch -d " + branchName;
                resultOk = GitExecute(commandName);
            }
            return resultOk;
        }



        private static bool GitExecute(string commandName, out string result)
        {
            bool success;
            ExecuteCommand.Execute(commandName, out result, out success);
            ArrayList lines = new ArrayList();
            lines.Add(result);
            GxConsoleHandler.GitConsoleWriter(lines, "[" + Resources.AppName + "]: - Execute " + commandName, true);

            return success;
        }

        private static bool GitExecute(string commandName)
        {
            string result;
            bool success;
            ExecuteCommand.Execute(commandName, out result, out success);
            ArrayList lines = new ArrayList();
            lines.Add(result);
            GxConsoleHandler.GitConsoleWriter(lines, "[" + Resources.AppName + "]: - Execute " + commandName, true);

            return success;
        }

    }
}
