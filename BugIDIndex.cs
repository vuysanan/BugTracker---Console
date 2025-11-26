using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker
{
    internal class BugIDIndex
    {
        int ID, position;

        public BugIDIndex(int ID, int position)
        {
            this.ID = ID;
            this.position = position;
        }

        public int getID() { return ID; }
        public int getPosition() { return position; }

        public void displayIndex()
        {
            Console.WriteLine("Bug ID: " + ID + "\nPosition: " + position);
        }
    }
}
