using DataStructers.Tests;
using DataStructers.Tests.Interfaces;
using DataStructures.Lib;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace DataStructers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ITests[] tests = { new ListTests(), new LinkedListTests() };
            //foreach (var t in tests)
            //{
            //    t.Run();
            //}

            //Tests();

            //Tests2();

            //Tests3();

            //Tests4();

            //Tests5();
        }

        static void Tests2()
        {
            #region BinaryTree
            var binaryTree = new BinaryTree<int>();
            binaryTree.Add(100);
            binaryTree.Add(58);
            binaryTree.Add(2);
            binaryTree.Add(12);
            binaryTree.Add(33);
            binaryTree.Add(0);

            binaryTree.Contains(12);
            binaryTree.Contains(14);
            binaryTree.ToArray();

            Console.WriteLine(binaryTree.Count);
            Console.WriteLine(binaryTree.Root);
            #endregion

            #region LinkedList
            var linkedList = new DataStructures.Lib.LinkedList<int>();
            linkedList.Add(100);
            linkedList.Add(21);
            linkedList.AddFirst(11);
            linkedList.Insert(2, 32);
            linkedList.Contains(21);
            linkedList.Contains(210);
            linkedList.ToArray();

            Console.WriteLine(linkedList.First);
            Console.WriteLine(linkedList.Last);
            Console.WriteLine(linkedList.Count);
            #endregion

            #region DoubleLinkedList
            var doubleLinkedList = new DoubleLinkedList<int>();
            doubleLinkedList.Add(100);
            doubleLinkedList.Add(21);
            doubleLinkedList.Add(1);
            doubleLinkedList.AddFirst(11);
            doubleLinkedList.Remove(21);
            doubleLinkedList.RemoveFirst();
            doubleLinkedList.RemoveLast();
            doubleLinkedList.Contains(21);
            doubleLinkedList.Contains(210);
            doubleLinkedList.ToArray();

            Console.WriteLine(doubleLinkedList.First);
            Console.WriteLine(doubleLinkedList.Last);
            Console.WriteLine(doubleLinkedList.Count);
            #endregion

            #region Queue
            var queue = new DataStructures.Lib.Queue<int>();
            queue.Enqueue(100);
            queue.Enqueue(145);
            queue.Enqueue(12);
            queue.Dequeue();
            queue.Contains(12);
            queue.Contains(2);
            queue.ToArray();

            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Count);
            #endregion

            #region Stack
            var stack = new DataStructures.Lib.Stack<int>();
            stack.Push(32);
            stack.Push(12);
            stack.Push(43);
            stack.Pop();
            stack.Contains(12);
            stack.Contains(2);
            stack.ToArray();

            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Count);
            #endregion
            Console.ReadLine();
        }

        static void Tests3()
        {
            //var myList = new DataStructures.Lib.List<int>();
            //Console.WriteLine("List created!");

            //myList.Add(1);
            //myList.Add(2);
            //myList.Add(3);
            //Console.WriteLine("List after adding elements:");

            //PrintList(myList);

            //myList.Insert(1, 4);
            //Console.WriteLine("List after inserting 4 at index 1:");

            //PrintList(myList);

            //myList.Remove(3);
            //Console.WriteLine("Contains 3: " + myList.Contains(3));

            //PrintList(myList);

            //myList.Reverse();
            //Console.WriteLine("List after reversing:");

            //PrintList(myList);

            //Console.WriteLine("Element at index 1: " + myList[1]);
            //Console.WriteLine("List count: " + myList.Count);

            //myList.Clear();
            //Console.WriteLine("List after clearing:");
            //Console.WriteLine("List count after clearing: " + myList.Count);

            var binaryTree = new BinaryTree<int>();

            binaryTree.Add(1);
            binaryTree.Add(3);
            binaryTree.Add(4);
            binaryTree.Add(5);
            binaryTree.Add(6);
            binaryTree.Add(7);
            binaryTree.Add(8);

            Console.WriteLine("BinaryTree Contains 3: " + binaryTree.Contains(3));
            Console.WriteLine("BinaryTree Contains 2: " + binaryTree.Contains(9));

            Console.WriteLine("Count: " + binaryTree.Count);

            binaryTree.Clear();
            Console.WriteLine("BinaryTree count elements after clearing: " + binaryTree.Count);

            binaryTree.Add(10);
            binaryTree.Add(15);
            binaryTree.Add(5);
            binaryTree.Add(3);
            binaryTree.Add(1);

            var array = binaryTree.ToArray();
            Console.WriteLine("Array: " + string.Join(", ", array));

            var myQueue = new DataStructures.Lib.Queue<int>();

            myQueue.Enqueue(1);
            myQueue.Enqueue(2);
            myQueue.Enqueue(3);

            Console.WriteLine($"Queue count: {myQueue.Count}");

            Console.WriteLine("Queue elements:");
            foreach (var item in myQueue.ToArray())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Peek: {myQueue.Peek()}");

            Console.WriteLine($"Queue contains 1: {myQueue.Contains(1)}");
            Console.WriteLine($"Queue contains 5: {myQueue.Contains(5)}");

            Console.WriteLine("Dequeue elements:");
            while (myQueue.Count > 0)
            {
                Console.WriteLine(myQueue.Dequeue());
            }

            Console.WriteLine($"Count after dequeue: {myQueue.Count}");

            var stack = new DataStructures.Lib.Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Console.WriteLine($"Stack count: {stack.Count}");

            Console.WriteLine($"Stack contains 2: {stack.Contains(2)}");

            Console.WriteLine($"Peek: {stack.Peek()}");

            Console.WriteLine($"Pop: {stack.Pop()}");

            Console.WriteLine($"Peek after Pop: {stack.Peek()}");

            var arraySt = stack.ToArray();
            Console.WriteLine("Stack as array:");
            foreach (var item in arraySt)
            {
                Console.WriteLine(item);
            }

            stack.Clear();

            Console.WriteLine($"Stack count after Clear: {stack.Count}");
            var list = new DataStructures.Lib.LinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);

            Console.WriteLine("List elements:");
            PrintList(list);

            list.AddFirst(0);

            list.Insert(2, 100);

            Console.WriteLine("List elements after modifications:");
            PrintList(list);

            Console.WriteLine("List contains 100: " + list.Contains(100));
            Console.WriteLine("List contains 5: " + list.Contains(5));

            Console.ReadLine();
        }

        static void PrintList<T>(DataStructures.Lib.Interfaces.ICollection<T> list)
        {
            T?[] array = list.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        static void Tests5()
        {
            TestQueue();
            TestStack();
            TestSinglyLinkedList();
            TestBinarySearchTree();
            TestLinkedList();

            Console.ReadLine();
        }

        static void TestQueue()
        {
            Console.WriteLine("Testing Queue:");

            var queue = new DataStructures.Lib.Queue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Console.WriteLine($"Count: {queue.Count}");

            Console.WriteLine("Dequeue: " + queue.Dequeue());
            Console.WriteLine("Peek: " + queue.Peek());

            Console.WriteLine($"Count after Dequeue and Peek: {queue.Count}");

            Console.WriteLine("Clearing the queue...");
            queue.Clear();
            Console.WriteLine($"Count after Clear: {queue.Count}");

            Console.WriteLine();
        }

        static void TestStack()
        {
            Console.WriteLine("Testing Stack:");

            var stack = new DataStructures.Lib.Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Console.WriteLine($"Count: {stack.Count}");

            Console.WriteLine("Pop: " + stack.Pop());
            Console.WriteLine("Peek: " + stack.Peek());

            Console.WriteLine($"Count after Pop and Peek: {stack.Count}");

            Console.WriteLine("Clearing the stack...");
            stack.Clear();
            Console.WriteLine($"Count after Clear: {stack.Count}");

            Console.WriteLine();
        }

        static void TestSinglyLinkedList()
        {
            Console.WriteLine("Testing Singly Linked List:");

            var singlyLinkedList = new DataStructures.Lib.LinkedList<int>();

            singlyLinkedList.Add(1);
            singlyLinkedList.Add(2);
            singlyLinkedList.Add(3);

            Console.WriteLine($"Count: {singlyLinkedList.Count}");

            Console.WriteLine("Clearing the list...");
            singlyLinkedList.Clear();
            Console.WriteLine($"Count after Clear: {singlyLinkedList.Count}");

            Console.WriteLine();
        }

        static void TestBinarySearchTree()
        {
            Console.WriteLine("Testing Binary Search Tree:");

            var binarySearchTree = new BinaryTree<int>();

            binarySearchTree.Add(2);
            binarySearchTree.Add(1);
            binarySearchTree.Add(3);

            Console.WriteLine($"Count: {binarySearchTree.Count}");
            Console.WriteLine("Contains 2: " + binarySearchTree.Contains(2));
            Console.WriteLine("Contains 4: " + binarySearchTree.Contains(4));

            Console.WriteLine("Clearing the tree...");
            binarySearchTree.Clear();
            Console.WriteLine($"Count after Clear: {binarySearchTree.Count}");

            Console.WriteLine();
        }

        static void TestLinkedList()
        {
            Console.WriteLine("Testing Linked List:");

            var linkedList = new DataStructures.Lib.LinkedList<int>();

            linkedList.Add(1);
            linkedList.Add(2);
            linkedList.Add(3);

            Console.WriteLine($"Count: {linkedList.Count}");
            Console.WriteLine("Contains 2: " + linkedList.Contains(2));
            Console.WriteLine("Contains 4: " + linkedList.Contains(4));

            Console.WriteLine("Clearing the list...");
            linkedList.Clear();
            Console.WriteLine($"Count after Clear: {linkedList.Count}");

            Console.WriteLine();
        }
    }
}
