using DataStructers.Tests;
using DataStructers.Tests.Interfaces;
using DataStructures.Lib;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace DataStructers
{
    static class CollectionHelpers
    {
        private class FileterIterable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> collection;
            private readonly Predicate<T> predicate;

            public FileterIterable(IEnumerable<T> collection, Predicate<T> predicate)
            {
                this.collection = collection;
                this.predicate = predicate;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new FilterIterator<T>(collection, predicate);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class FilterIterator<T> : IEnumerator<T>
        {
            private readonly IEnumerable<T> _collection;
            private readonly Predicate<T> _predicate;

            private IEnumerator<T> _iterator;

            public T? Current => _iterator.Current;

            object IEnumerator.Current => Current;

            public FilterIterator(IEnumerable<T> collection, Predicate<T> predicate)
            {
                this._collection = collection;
                this._predicate = predicate;
            }

            public bool MoveNext()
            {
                if (_iterator == null) _iterator = _collection.GetEnumerator();

                bool result = false;
                do
                {
                    result = _iterator.MoveNext();
                    if (result && _predicate(_iterator.Current))
                    {
                        return true;
                    }
                } while (result);

                return false;
            }

            public void Reset()
            {
            }

            public void Dispose()
            {
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> func)
        {
            return new FileterIterable<T>(list, item => func(item));
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> iterable, int count)
        {
            int current = 0;
            foreach (var item in iterable) {

                if (current >= count) yield break;

                yield return item;
                current++;
            }
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> iterable, Func<T, bool> takeWhen)
        {
            int current = 0;
            foreach (var item in iterable)
            {
                if (!takeWhen(item)) yield break;

                yield return item;
                current++;
            }
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> iterable, int count)
        {
            int current = 0;
            foreach(var item in iterable)
            {
                if (current >= count)
                {
                    yield return item;
                }
                else
                    current++;
            }
        }
    }

    internal class Program
    {
        class Person
        {
            private int _id;


            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            //public override int GetHashCode()
            //{
            //    return HashCode.Combine(Id, Name, Age);
            //}

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
            var list = new DataStructures.Lib.List<Person>
            {
                new Person { Id = 1, Name = "Sam", Age = 21 },
                new Person { Id = 2, Name = "Bill", Age = 23 },
                new Person { Id = 3, Name = "Samuel", Age = 31 },
                new Person { Id = 4, Name = "John", Age = 19 },
                new Person { Id = 5, Name = "Ted", Age = 43 },
                new Person { Id = 6, Name = "Ed", Age = 63 },
                new Person { Id = 7, Name = "Zed", Age = 33 }
            };

            foreach (var item in list.Skip(2).Take(i => i.Age < 40))
            {
                Console.WriteLine(item.Name);
            }

            var dic = new System.Collections.Generic.Dictionary<Person, string>(new EqCompPerson());
            var p = new Person { Id = 1, Name = "Sam", Age = 21 };
            dic[p] = "Hello";
            dic[p] = "Sam";

            //--------------
            var p2 = new Person { Id = 1, Name = "Sam", Age = 21 };
            var value = dic[p2];
            dic[p2] = "Sam2";
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
