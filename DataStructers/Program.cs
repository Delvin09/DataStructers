using DataStructers.Tests.Interfaces;
using DataStructures.Lib;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DataStructers
{
    internal class Program
    {
        class MyException : Exception
        {

        }

        class Person
        {
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
                return $"Id: {Id}, Name: {Name}, Age: {Age}";
            }
        }

        class Order
        {
            public int Id { get; set; }
            public Person Customer { get; set; }
            public DataStructures.Lib.List<Product> Products { get; set; }
        }

        class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ProductCategory Category { get; set; }
            public decimal Price { get; set; }
            public int Count { get; set; }
        }

        class ProductCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
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

        class UserOrderResult
        {
            public IEnumerable<Order> Orders { get; set; }
            public Person User { get; set; }
        }

        class DisObj : IDisposable
        {
            //static DisObj()
            //{
            //    throw new NotImplementedException();
            //}

            ~DisObj()
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
            }
        }

        static void Main(string[] args)
        {
            var users = new System.Collections.Generic.List<Person>
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

            var otherUsers = new DataStructures.Lib.List<Person>
            {
                new Person { Id = 107, Name = "Ed", Age = 19 },
                new Person { Id = 106, Name = "Ed", Age = 23 },
                new Person { Id = 100, Name = "Sam", Age = 30 },
                new Person { Id = 5, Name = "Ted", Age = 43 },
                new Person { Id = 8, Name = "Alex", Age = 18 },
            };

            var ids = new System.Collections.Generic.List<int> {
                107, 8, 106, 5, 100
            };

            var categories = new DataStructures.Lib.List<ProductCategory>()
            {
                new ProductCategory{ Id = 1, Name = "SmartPhone" },
                new ProductCategory{ Id = 2, Name = "Notebook" },
                new ProductCategory{ Id = 3, Name = "VideoCard" }
            };

            var products = new DataStructures.Lib.List<Product>()
            {
                new Product{ Id = 1, Name = "IPhone 10", Count = 100, Price = 799.99M, Category = categories[0] },
                new Product{ Id = 2, Name = "IPhone 11", Count = 10, Price = 859.89M, Category = categories[0] },
                new Product{ Id = 3, Name = "Samsung", Count = 50, Price = 669.19M, Category = categories[0] },

                new Product{ Id = 4, Name = "Apple Air", Count = 6, Price = 1669.19M, Category = categories[1] },
                new Product{ Id = 5, Name = "Asus Zenbook", Count = 21, Price = 1249.20M, Category = categories[1] },
                new Product{ Id = 6, Name = "Lenovo", Count = 44, Price = 595.55M, Category = categories[1] },

                new Product{ Id = 7, Name = "NVidia 3070", Count = 25, Price = 1200M, Category = categories[2] },
                new Product{ Id = 8, Name = "NVidia 4090", Count = 9, Price = 2200.50M, Category = categories[2] },
                new Product{ Id = 9, Name = "AMD Radeon", Count = 32, Price = 1100M, Category = categories[2] },
            };

            var orders = new DataStructures.Lib.List<Order>
            {
                new Order{ Id = 1, Customer = users[0], Products = new DataStructures.Lib.List<Product>
                    {
                        products[0], products[1], products[2]
                    }
                },
                new Order{ Id = 2, Customer = users[0], Products = new DataStructures.Lib.List<Product>
                    {
                        products[1], products[2], products[3]
                    }
                },
                new Order{ Id = 3, Customer = users[1], Products = new DataStructures.Lib.List<Product>
                    {
                        products[4]
                    }
                },
                new Order{ Id = 4, Customer = users[2], Products = new DataStructures.Lib.List<Product>
                    {
                        products[4], products[5]
                    }
                },
                new Order{ Id = 5, Customer = users[2], Products = new DataStructures.Lib.List<Product>
                    {
                        products[6]
                    }
                },
                new Order{ Id = 4, Customer = users[3], Products = new DataStructures.Lib.List<Product>
                    {
                        products[6]
                    }
                },
                new Order{ Id = 4, Customer = users[5], Products = new DataStructures.Lib.List<Product>
                    {
                        products[7], products[8]
                    }
                },
            };

            //foreach (var item in users.GroupJoin(orders, u => u.Id, o => o.Customer.Id, (user, order) => new UserOrderResult { Orders = order, User = user }))
            //{
            //    Console.WriteLine($"{item.User}");
            //    foreach(var order in item.Orders)
            //    {
            //        Console.WriteLine($"{order.Id}");
            //    }
            //}

            Console.WriteLine("Before Do");
            try
            {
                DisObj a = new DisObj();
                Do(users);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {

                }
            }
            Console.WriteLine("After Do");

            //var query = otherUsers
            //    .DistinctBy(p => p.Id)
            //    .OrderBy(p => p.Name).ThenByDescending(p => p.Age)
            //    .Intersect(users)
            //    ;
            //.Select(p => new Customer() { Id = p.Id, Age = p.Age, Name = p.Name, Oreders = new System.Collections.Generic.List<Oreder>() });
            // list.Add(new Person { Id = 8, Name = "Alex", Age = 18 });

            //foreach (var item in query)
            //{
            //    Console.WriteLine(item);
            //}
        }


        static void Do(System.Collections.Generic.List<Person> users)
        {
            Console.WriteLine("Before in Do");
            foreach (var user in users)
            {
                try
                {
                    var clonable = TryCast(user);
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Something goes wrong!");
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("Null found!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error!");
                    throw;
                }
                finally
                {
                    Console.WriteLine("After in Do");
                    throw new Exception("Finally ex");
                }
            }
        }

        static ICloneable TryCast(Person person)
        {
            Console.WriteLine("Before TryCast");

            try
            {
                if (person.Age < 100)
                    throw new MyException();
                return person as ICloneable;
            }
            finally
            {
                Console.WriteLine("After TryCast");
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
