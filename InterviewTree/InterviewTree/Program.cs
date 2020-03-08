using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTree
{
    class Program
    {
        static void Main(string[] args)
        {
           
            TreeNode tree = CreateTree();
            TreeNode manual = new TreeNode 
            { Id = 0, Content = "Im the root",Children = new List<TreeNode> { new TreeNode {Id = 4 } ,new TreeNode {Id = 1, Content = "child with id 1",
                Children = new List<TreeNode> { new TreeNode {Id = 2, isExpanded = true, 
                    Children = new List<TreeNode> { new TreeNode {Id = 3, isExpanded = true } } } } } } };

           

            
            #region tree builder
            int autoId; //an int for automatic id

            TreeNode CreateTree()
            {
                autoId = 1;
                //Random randomBranch = new Random();
                //int branches = randomBranch.Next(0, 4); //generate a random number, if you want to randomize
                TreeNode newTree = new TreeNode(); //id 0 will be the root
                newTree.Id = 0;
                newTree.Content = "I'm the rootnode";
                newTree.ParentId = null; //it does not have a parent       

                //add children with some content, id and parent id
                for (int i = 0; i < 3; i++) //add the int branches like i < branches
                {
                    //Random randomChild = new Random();
                    //int children = randomChild.Next(0, 5);
                    //theese are the children of the root, so all of their parent is 0
                    newTree.Children.Add(new TreeNode { Id = autoId, Content =$"I'm a node and my id is {autoId}", ParentId = 0});
                    autoId++;
                    AddChild(newTree.Children[i], 3, autoId -1); //add some children to this node, id -1 gives us the parent, you can also replace the 3 with the random                 
                }
                return newTree;
            }

            void AddChild(TreeNode ParentNode, int childrenPerBranch, int parent)
            {
                for (int i = 0; i < childrenPerBranch; i++)
                {
                    ParentNode.Children.Add(new TreeNode { Id = autoId, Content = $"I'm a node and my id is {autoId}", ParentId = parent});
                    autoId++; //also increase the id here, otherwise the next parent will have the same id as this
                }
            }
            #endregion


            #region examples
            //lets make a list of nodes the user can see, by default i will add the root
            List<TreeNode> visible = new List<TreeNode>();
            visible.Add(tree);
            //we can show the content of the visible list of the user to select nodes from
            //the user can expand items from this list, which is only the root now
            TreeNode.ExpandNode(0, tree); //lets expand the root
            //you can use LINQ to find a node
            var current = tree.GetNodes().Where(node => node.Id == 0).FirstOrDefault(); 
            var expand = current.GetChildren(); //you can get its children
            visible.AddRange(expand); //add the children to the list

            foreach (var item in visible)
            {
                Console.WriteLine(item.Content);
            }

            Console.WriteLine("-------------");

            TreeNode.CollapseNode(0, tree); //now lets collapse it          
            current = tree.GetNodes().Where(node => node.Id == 0).FirstOrDefault();
            var collapse = current.GetChildren();
            visible = visible.Except(collapse).ToList(); //also remove from the visible list

            
            foreach (var item in visible)
            {
                Console.WriteLine(item.Content);
            }

            //the select / deselect function work the same way, let user select from the visible list and call the functions 
            //you can select anything in code, to prevent the user from selecting stuff they shouldnt, add it to the visible list and only present that
            //lets select some nodes, and print out their states, the demo creates a tree with nodes from 0-12
            
            TreeNode.SelectNode(9, tree);
            TreeNode.ExpandNode(5, tree);
            TreeNode.SelectNode(5, tree);

            var selectedNodes = tree.GetNodes().Where(node => node.isSelected == true); //to get all the selected nodes
            var recurSelected = tree.GetNodes().Where(node => node.isRecursivelySelected == true); //all the recursively selected nodes
            var expanded = tree.GetNodes().Where(node => node.isExpanded == true); //all the expanded nodes

            foreach (var item in selectedNodes)
            {
                Console.WriteLine(item.Content + " " + "i am selected");
            }

            Console.WriteLine("--------------");

            foreach (var item in recurSelected)
            {
                Console.WriteLine(item.Content + " " + "i was recursively selected");
            }

            Console.WriteLine("--------------");

            foreach (var item in expanded)
            {
                Console.WriteLine(item.Content + " " + "i am expanded");
            }
            #endregion
        }
    }
}
