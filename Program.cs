using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BugIDIndexList indexList = new BugIDIndexList();
            BugDatabase bugDB = new BugDatabase();

            bugDB.buildIndex(indexList);

            int choice = menuOptions();

            menuFuntionality(choice, bugDB, indexList);

            Console.ReadLine();
        }

        static int menuOptions()
        {
            Console.WriteLine("Bug Tracker Menu");
            Console.WriteLine("1. Display All Bug Reports");
            Console.WriteLine("2. Add New Bug Report");
            Console.WriteLine("3. Search And Display Details For A Bug");
            Console.WriteLine("4. Assign An Employee To A Bug");
            Console.WriteLine("5. Update Bug Status");
            Console.WriteLine("6. Delete Bug Report");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your option: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            return choice;
        }

        static void menuFunctionality(int choice, BugDatabase bugDB, BugIDIndexList indexList)
        {
            while (true)
            {
                switch (choice)
                {
                    case 1:
                        bugDB.displayAllDetails();
                        break;
                    case 2:
                        addNewBugReport(bugDB, indexList);
                        Console.WriteLine();
                        break;
                    case 3:
                        searchAndDisplayBug(bugDB, indexList);
                        Console.WriteLine();
                        break;
                    case 4:
                        assignBug(bugDB, indexList);
                        Console.WriteLine();
                        break;
                    case 5:
                        updateBugStatus(bugDB, indexList);
                        Console.WriteLine();
                        break;
                    case 6:
                        deleteBugReport(bugDB, indexList);
                        Console.WriteLine();
                        break;
                    case 7:
                        bugDB.close();
                        Console.WriteLine("Exiting program..");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.WriteLine();
                        break;
                }
                choice = menuOptions();
            }
        }

        static void addNewBugReport(BugDatabase bugDB, BugIDIndexList indexList)
        {
            Console.Write("Enter Bug ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bugID))
            {
                Console.WriteLine("Invalid Bug ID input. Enter a numeric value.");
                return;
            }
            if (indexList.findBug(bugID) > 0)
            {
                Console.WriteLine("Bug ID already exists. Cannot add duplicate.");
                return;
            }
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
            if (title.Contains(","))
            {
                // no commas since our file is comma delimited
                Console.WriteLine("Title cannot have a comma");
                return;
            }
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
            if (description.Contains(","))
            {
                // no commas since our file is comma delimited
                Console.WriteLine("Title cannot have a comma");
                return;
            }
            Console.Write("Enter Priority (Low, Medium or High): ");
            string priority = Console.ReadLine().ToLower();
            if (!(priority == "low" || priority == "medium" || priority == "high"))
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            Console.Write("Enter Status (Open or Closed): ");
            string status = Console.ReadLine().ToLower();
            if (!(status == "open" || status == "closed"))
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            Console.Write("Assigned To: ");
            string assignedTo = Console.ReadLine();

            BugReport newBug = new BugReport(bugID, title, description, priority, status, assignedTo);
            bugDB.addNewOne(newBug, indexList);
            Console.WriteLine("New bug report added successfully.");
        }

        static void searchAndDisplayBug(BugDatabase bugDB, BugIDIndexList indexList)
        {
            Console.Write("Enter Bug ID to search: ");
            if (int.TryParse(Console.ReadLine(), out int bugID))
            {
                int indexPosition = indexList.findBug(bugID);
                if (indexPosition >= 1)
                {
                    Console.WriteLine("Bug found at index position: " + indexPosition);
                    BugIDIndex index = indexList.getIndexInfo(indexPosition - 1);
                    int dataBasePosition = index.getPosition();
                    BugReport bug = bugDB.getBugInfo(dataBasePosition);
                    bug.displayBug();
                }
                else
                {
                    Console.WriteLine("Bug ID not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Bug ID input.");
            }
        }

        static void assignBug(BugDatabase bugDB, BugIDIndexList indexList)
        {
            Console.Write("Enter Bug ID to assign: ");
            if (int.TryParse(Console.ReadLine(), out int bugID))
            {
                int indexPosition = indexList.findBug(bugID);
                if (indexPosition >= 1)
                {
                    Console.Write("Enter Employee Name to assign: ");
                    string assignee = Console.ReadLine();
                    BugIDIndex index = indexList.getIndexInfo(indexPosition - 1);
                    int bugDatabasePosition = index.getPosition();
                    BugReport bug = bugDB.getBugInfo(bugDatabasePosition);
                    bug.setAssignedTo(assignee);
                    Console.WriteLine("Bug assigned successfully.");
                }
                else
                {
                    Console.WriteLine("Bug ID not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Bug ID input.");
            }
        }
        static void updateBugStatus(BugDatabase bugDB, BugIDIndexList indexList)
        {
            Console.Write("Enter Bug ID to update status: ");
            if (int.TryParse(Console.ReadLine(), out int bugID))
            {
                int indexPosition = indexList.findBug(bugID);
                if (indexPosition >= 1)
                {
                    Console.Write("Enter new status: ");
                    string newStatus = Console.ReadLine();
                    BugIDIndex index = indexList.getIndexInfo(indexPosition - 1);
                    int bugDataBasePosition = index.getPosition();
                    BugReport bug = bugDB.getBugInfo(bugDataBasePosition); 
                    bug.setStatus(newStatus);
                    Console.WriteLine("Bug status updated successfully.");
                }
                else
                {
                    Console.WriteLine("Bug ID not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Bug ID input.");
            }
        }
        static void deleteBugReport(BugDatabase bugDB, BugIDIndexList indexList)
        {
            Console.Write("Enter Bug ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int bugID))
            {
                int indexPosition = indexList.findBug(bugID);
                if (indexPosition >= 1)
                {
                    BugIDIndex wantedIndex = indexList.getIndexInfo(indexPosition - 1);
                    int bugDatabasePosition = wantedIndex.getPosition();
                    bugDB.deleteOne(bugDatabasePosition);
                    indexList.deleteIndex(indexPosition - 1);
                    Console.WriteLine("Bug report deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Bug ID not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Bug ID input.");
            }
        }
    }
}

