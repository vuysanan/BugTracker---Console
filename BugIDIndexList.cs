using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker
{
    internal class BugIDIndexList
    {
        ArrayList IDIndexList;
        int sortedState = 0;

        public BugIDIndexList()
        {
            IDIndexList = new ArrayList();
        }

        public void addIndex(BugIDIndex newID)
        {
            if (sortedState == 1)
            {
                int last = IDIndexList.Count - 1;
                insertSorted(newID, last);
            }
            else
            {
                IDIndexList.Add(newID);
            }
        }

        private void insertSorted(BugIDIndex newID, int last)
        {
            BugIDIndex previousID = IDIndexList[last] as BugIDIndex;
            while ((last != -1) && (newID.getID().CompareTo(previousID.getID()) < 0))
            {
                last--;
                if (last != -1)
                    previousID = IDIndexList[last] as BugIDIndex;
            }
            IDIndexList.Insert(last + 1, newID);
        }

        public void sortIDAsc()
        {
            //bubbleSortIDAsc();
            if (IDIndexList.Count > 1)
                quickSortIDAsc(0, IDIndexList.Count - 1);
            sortedState = 1;
        }

        /*private void bubbleSortIDAsc()
        {
            for (int pass = 1; pass < IDIndexList.Count; pass++)
            {
                for (int compare = 1; compare < IDIndexList.Count - pass; compare++)
                {
                    BugIDIndex first = IDIndexList[compare - 1] as BugIDIndex;
                    BugIDIndex second = IDIndexList[compare] as BugIDIndex;
                    if (first.getID().CompareTo(second.getID()) > 0)
                        swap(compare - 1, compare);
                }
            }
        }*/

        private void quickSortIDAsc(int left, int right)
        {
            int i = left;
            int j = right;
            BugIDIndex pivot = IDIndexList[(left + right) / 2] as BugIDIndex;
            while (i <= j)
            {
                while ((IDIndexList[i] as BugIDIndex).getID().CompareTo(pivot.getID()) < 0)
                    i++;
                while ((IDIndexList[j] as BugIDIndex).getID().CompareTo(pivot.getID()) > 0)
                    j--;
                if (i <= j)
                {
                    swap(i, j);
                    i++;
                    j--;
                }
            }
            if (left < j)
                quickSortIDAsc(left, j);
            if (i < right)
                quickSortIDAsc(i, right);
        }

        private void swap(int left, int right)
        {
            BugIDIndex temp = IDIndexList[right] as BugIDIndex;
            IDIndexList[right] = IDIndexList[left];
            IDIndexList[left] = temp;
        }

        public BugIDIndex getIndexInfo(int position)
        {
            if (position >= 0 && position < IDIndexList.Count)
            {
                return IDIndexList[position] as BugIDIndex;
            }
            else return null;
        }

        public void deleteIndex(int position)
        {
            if (position >= 0 && position < IDIndexList.Count)
            {
                IDIndexList.RemoveAt(position);
            }
        }

        public int findBug(int bugID)
        {
            return binarySearchID(bugID) + 1;
        }

        private int binarySearchID(int bugID)
        {
            int first = 0;
            int last = IDIndexList.Count - 1;

            while (first <= last)
            {
                int middle = (first + last) / 2;
                BugIDIndex current = IDIndexList[middle] as BugIDIndex;
                if (current.getID() == bugID)
                    return middle;
                else if (current.getID().CompareTo(bugID) > 0)
                    last = middle - 1;
                else first = middle + 1;
            }
            return -1;
        }

        public void displayIDInfo()
        {
            foreach (BugIDIndex ID in IDIndexList)
            {
                ID.displayIndex();
                Console.WriteLine();
            }
        }
    }
}
