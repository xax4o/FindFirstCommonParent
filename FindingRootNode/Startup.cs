namespace FindingRootNode
{
    using System;
    using System.Collections.Generic;

    public class Startup
    {
        public static IDictionary<string, Node> nodes = new Dictionary<string, Node>();

        public static void Main()
        {
            ReadInput();
            
            //Reading searched tree nodes
            Console.Write("Enter the two nodes in the format /FirstNode SecondNode/: ");
            var searchedNodes = Console.ReadLine().Split(' ');

            var firstNode = searchedNodes[0];
            var secondNode = searchedNodes[1];

            var result = GetFirstCommonParent(firstNode, secondNode);

            Console.WriteLine("The Root parrent of {0} and {1} is {2} ", firstNode, secondNode, result.Name);
        }

        public static void ReadInput()
        {
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
        }

        public static Node GetFirstCommonParent(string firstNodeToSearch, string secondNodeToSearch)
        {
            var pathForFirstNode = GeneratePath(firstNodeToSearch);

            //Searching for the first parant node from second node to root node
            if (nodes.ContainsKey(secondNodeToSearch))
            {
                var currentNode = nodes[secondNodeToSearch];

                // The global parent has no parent. It's parent is null.
                while (currentNode.Parent != null)
                {
                    if (pathForFirstNode.Contains(currentNode))
                    {
                        break;
                    }

                    currentNode = currentNode.Parent;
                }

                return currentNode;
            }
            else
            {
                throw new ArgumentException("No such node " + secondNodeToSearch);
            }
        }

        public static ICollection<Node> GeneratePath(string node)
        {
            var nodesPath = new HashSet<Node>();

            //Generating a path from given node to root node.
            if (nodes.ContainsKey(node))
            {
                var currentNode = nodes[node];

                // The global parent has no parent. It's parent is null.
                while (currentNode.Parent != null)
                {
                    nodesPath.Add(currentNode);
                    currentNode = currentNode.Parent;
                }

                nodesPath.Add(currentNode);

                return nodesPath;
            }
            else
            {
                throw new ArgumentException("No such node " + node);
            }
        }
    }
}
