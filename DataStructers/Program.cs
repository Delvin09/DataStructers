using DataStructers.Tests.Interfaces;
using DataStructures.Lib;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace DataStructers
{
    internal class Program
    {
        class Person
        {
            public int Mark { get; set; } = 1;

            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Name, Age);
            }

            public override bool Equals(object? obj)
            {
                if (obj == null) return false;
                if (!(obj is Person)) return false;
                if (ReferenceEquals(this, obj)) return true;

                var other = obj as Person;
                return Id == other.Id
                    && Name.Equals(other.Name)
                    && Age == other.Age;
            }

            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Age: {Age}. {Mark}";
            }
        }

        class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            public System.Collections.Generic.List<Order> Orders { get; set; }

            public override string ToString()
            {
                return $"Id: {Id}, Name: {Name}, Age: {Age} [CUSTOMER]";
            }
        }

        class Order
        {

        }

        class EqCompPerson : IEqualityComparer<Person>
        {
            public bool Equals(Person? x, Person? y)
            {
                if (x == y) return true;
                return x.Id == y.Id
                    && x.Name.Equals(y.Name)
                    && x.Age == y.Age;
            }

            public int GetHashCode([DisallowNull] Person obj)
            {
                return HashCode.Combine(obj.Id, obj.Name, obj.Age);
            }
        }

        static void Main(string[] args)
        {
            var list = new System.Collections.Generic.List<Person>
            {
                new Person { Id = 9, Name = "Alex", Age = 19 },
                new Person { Id = 8, Name = "Alex", Age = 18 },
                
                new Person { Id = 1, Name = "Sam", Age = 21 },
                new Person { Id = 2, Name = "Bill", Age = 23 },
                new Person { Id = 3, Name = "Samuel", Age = 31 },
                new Person { Id = 4, Name = "John", Age = 18 },
                new Person { Id = 5, Name = "Ted", Age = 43 },
                new Person { Id = 6, Name = "Ed", Age = 63 },
                new Person { Id = 7, Name = "Zed", Age = 33 }
            };

            var list2 = new System.Collections.Generic.List<Person>
            {
                new Person { Id = 107, Name = "Ed", Age = 19, Mark = 100 },

                new Person { Id = 8, Name = "Alex", Age = 18, Mark = 100 },

                new Person { Id = 106, Name = "Ed", Age = 23, Mark = 100 },

                new Person { Id = 5, Name = "Ted", Age = 43, Mark = 100 },
                
                new Person { Id = 100, Name = "Sam", Age = 30, Mark = 100 }
            };

            var ids = new System.Collections.Generic.List<int> {
                107,8,106,5,100
            };

            var query = list2
                .DistinctBy(p => p.Id)
                .OrderBy(p => p.Name).ThenByDescending(p => p.Age)
                .Intersect(list);
                //.Select(p => new Customer() { Id = p.Id, Age = p.Age, Name = p.Name, Oreders = new System.Collections.Generic.List<Oreder>() });
           // list.Add(new Person { Id = 8, Name = "Alex", Age = 18 });

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        static void RunTests()
        {
            var testGroup = new ListTestsWithEvents();
            var testRenderer = new ConsoleTestRenderer(new ITestsGroup[] { testGroup });

            //testGroup.Subscribe(testRenderer);

            testRenderer.Show();
            testGroup.Run();

            //====================================================================

            //ITests[] tests = { new ListTests(), new LinkedListTests() };
            //foreach (var t in tests)
            //{
            //    t.Run();
            //}
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
