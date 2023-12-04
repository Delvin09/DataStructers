﻿using DataStructures.Lib.Interfaces;

namespace DataStructures.Lib
{
    public class Node<T> : INode<T>
    {
        public T? Key { get; }

        public INode<T>? Left { get; set; }

        public INode<T>? Right { get; set; }

        public Node(T? value)
        {
            Key = value;
            Left = Right = null;
        }

        public override string? ToString()
        {
            return Key?.ToString();
        }
    }

    public class BinaryTree<T> : IBinaryTree<T> where T : IComparable<T>
    {
        public INode<T>? Root { get; private set; }

        public int Count { get; private set; }

        public void Add(T? value)
        {
            Root = Add(Root, value);
        }

        private INode<T> Add(INode<T>? root, T? value)
        {
            if (root == null)
            {
                root = new Node<T>(value);
                Count++;
                return root;
            }

            var compResult = value?.CompareTo(root.Key);
            if (compResult < 0)
            {
                root.Left = Add(root.Left, value);
            }
            else if (compResult > 0)
            {
                root.Right = Add(root.Right, value);
            }

            return root;
        }

        public bool Contains(T? value)
        {
            return Contains(Root, value);
        }

        private bool Contains(INode<T>? root, T? value)
        {
            if (root == null || root.Key?.CompareTo(value) == 0)
            {
                return root != null;
            }

            if (value?.CompareTo(root.Key) < 0)
            {
                return Contains(root.Left, value);
            }
            else
            {
                return Contains(root.Right, value);
            }
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public T?[] ToArray()
        {
            T?[] result = new T?[Count];
            int index = 0;

            void InorderToArray(INode<T>? node)
            {
                if (node != null)
                {
                    InorderToArray(node.Left);
                    result[index++] = node.Key;
                    InorderToArray(node.Right);
                }
            }

            InorderToArray(Root);
            return result;
        }
    }
}
