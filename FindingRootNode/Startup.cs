namespace FindingRootNode
{
    using System;
    using System.Collections.Generic;

    public class FindRootNode
    {
        public static void Main()
        {
            var nodes = new Dictionary<string, Node>();

            //Reading input tree nodes
            Console.Write("Enter number of tree nodes: ");
            var treeNodesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < treeNodesCount; i++)
            {
                Console.Write("Enter tree node {0} in the format /Parrent Child/: ", i + 1);
                var currentNodePair = Console.ReadLine().Split(' ');
                var parent = currentNodePair[0];
                var child = currentNodePair[1];

                if (nodes.Count == 0) // If nodes dictionary is empty than add first two nodes a parent and a child.
                {
                    var parentNode = new Node();
                    parentNode.Name = parent;
                    nodes.Add(parent, parentNode);

                    var childNode = new Node();
                    childNode.Name = child;
                    childNode.Parent = parentNode;
                    nodes.Add(child, childNode);
                }
                else if (nodes.ContainsKey(parent)) // If nodes dictionary contains a parent than adding a child to it.
                {
                    var childNode = new Node();
                    childNode.Name = child;
                    childNode.Parent = nodes[parent];
                    nodes.Add(child, childNode);
                }
                else if (nodes.ContainsKey(child)) // If a child node is set to a global parent.
                {
                    if (nodes[child].Parent == null)
                    {
                        var parentNode = new Node();
                        parentNode.Name = parent;
                        nodes[child].Parent = parentNode;
                        nodes.Add(parent, parentNode);
                    }
                }
            }

            //Reading searched tree nodes
            Console.Write("Enter the two nodes in the format /FirstNode SecondNode/: ");
            var searchedNodes = Console.ReadLine().Split(' ');

            var firstNodeToSearch = searchedNodes[0];
            var secondNodeToSearch = searchedNodes[1];

            var firstNodesParrents = new HashSet<Node>();

            //Generating a path from first node to root node.
            if (nodes.ContainsKey(firstNodeToSearch))
            {
                var currentNode = nodes[firstNodeToSearch];

                // The global parent has no parent. It's parent is null.
                while (currentNode.Parent != null)
                {
                    firstNodesParrents.Add(currentNode);
                    currentNode = currentNode.Parent;
                }

                firstNodesParrents.Add(currentNode);
            }
            else
            {
                Console.WriteLine("No such node {0}!", firstNodeToSearch);
            }

            //Searching for the first parant node from second node to root node
            if (nodes.ContainsKey(secondNodeToSearch))
            {
                var currentNode = nodes[secondNodeToSearch];

                // The global parent has no parent. It's parent is null.
                while (currentNode.Parent != null)
                {
                    if (firstNodesParrents.Contains(currentNode))
                    {
                        break;
                    }

                    currentNode = currentNode.Parent;
                }

                Console.WriteLine("The Root parrent of {0} and {1} is {2} ", firstNodeToSearch, secondNodeToSearch, currentNode.Name);
            }
            else
            {
                Console.WriteLine("No such node {0}!", secondNodeToSearch);
            }
        }
    }
}
