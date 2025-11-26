using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker
{
    internal class BugReport
    {
        int bugID;
        String title, description, priority, status, assignedTo;
        public bool isActive { get; set; }

        public BugReport(int bugID, string title, string description, string priority, string status, string assignedTo)
        {
            this.bugID = bugID;
            this.title = title;
            this.description = description;
            this.priority = priority;
            this.status = status;
            this.assignedTo = assignedTo;
            isActive = true;
        }

        public int getBugID() { return bugID; }

        public string getTitle() { return title; }

        public string getDescription() { return description; }

        public string getPriority() { return priority; }

        public string getStatus() { return status; }

        public string getAssignedTo() { return assignedTo; }

        public void setStatus(string newStatus)
        {
            status = newStatus;
        }

        public void setAssignedTo(string newAssignee)
        {
            assignedTo = newAssignee;
        }

        public void displayBug()
        {
            Console.WriteLine("Bug ID: " + bugID);
            Console.WriteLine("Title: " + title);
            Console.WriteLine("Description: " + description);
            Console.WriteLine("Priority: " + priority);
            Console.WriteLine("Status: " + status);
            Console.WriteLine("Assigned To: " + assignedTo);
        }

    }
}
