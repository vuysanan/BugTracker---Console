using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker
{
    internal class BugDatabase
    {
        ArrayList MainList;

        public BugDatabase()
        {
            MainList = new ArrayList();
            readData();
        }

        public void addNewOne(BugReport newBug, BugIDIndexList list)
        {
            MainList.Add(newBug);
            list.addIndex(new BugIDIndex(newBug.getBugID(), MainList.Count - 1));
            list.sortIDAsc();
        }

        public void deleteOne(int position)
        {
            // Mark the bug as inactive, rather than removing it from the list so that database maintains records
            BugReport bug = MainList[position] as BugReport;
            if (bug != null)
            {
                bug.isActive = false;
            }
        }

        private void readData()
        {
            if (!File.Exists("BugData.txt"))
            {
                File.Create("BugData.txt").Close();
                return;
            }
            const char DELIMITER = ',';
            String inputLine;
            String[] fields;

            using (StreamReader sr = new StreamReader("BugData.txt"))
            {
                while (!sr.EndOfStream)
                {
                    inputLine = sr.ReadLine();
                    fields = inputLine.Split(DELIMITER);
                    BugReport newBug = new BugReport(Convert.ToInt32(fields[0]), fields[1], fields[2], fields[3].ToLower(), fields[4].ToLower(), fields[5]);
                    MainList.Add(newBug);
                }
            }
        }

        public BugReport getBugInfo(int position)
        {
            if (position >= 0 && position < MainList.Count)
            {
                return MainList[position] as BugReport;
            }
            else
            {
                return null;
            }
        }

        public void displayAllDetails()
        {
            foreach (BugReport bug in MainList)
            {
                if (bug.isActive)
                {
                    bug.displayBug();
                    Console.WriteLine();
                }
            }
        }

        public void buildIndex(BugIDIndexList list)
        {
            for (int i = 0; i < MainList.Count; i++)
            {
                BugReport bug = MainList[i] as BugReport;
                list.addIndex(new BugIDIndex(bug.getBugID(), i));
            }
            list.sortIDAsc();
        }

        public void close()
        {
            saveData();
        }

        private void saveData()
        {
            using (StreamWriter sw = new StreamWriter("BugData.txt"))
            {
                foreach (BugReport bug in MainList)
                {
                    if (bug.isActive)
                    {
                        String line = $"{bug.getBugID()}, {bug.getTitle()}, {bug.getDescription()}, {bug.getPriority()}, {bug.getStatus()}, {bug.getAssignedTo()}";
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
