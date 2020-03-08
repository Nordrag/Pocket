using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewTree
{
    public class TreeNode
    {
        //the content of the node
        public string Content;
        //is it expanded
        public bool isExpanded;
        //is it selected
        public bool isSelected;
        //is it selected via recursion
        public bool isRecursivelySelected;
        //unique id
        public int Id;
        //the parent id of the node, ended up not using it
        public int? ParentId;
        //the children
        public List<TreeNode> Children;

        public TreeNode()
        {
            Children = new List<TreeNode>();
        }

        /// <summary>
        /// expands a node in a tree by id
        /// </summary>
        /// <param name="id">the id of the node</param>
        /// <param name="Tree">the tree we are looking in</param>
        public static void ExpandNode(int id, TreeNode Tree)
        {
            //is it this node
            if (Tree.Id == id)
            {
                Tree.isExpanded = true;
            }
            else
            {
                //if not look in the children
                foreach (var item in Tree.Children)
                {
                    ExpandNode(id, item);
                }
            }          
        }
      
        /// <summary>
        /// collapses a node by id
        /// </summary>
        /// <param name="id">the id</param>
        /// <param name="Tree">the tree we are looking in</param>       
        public static void CollapseNode(int id, TreeNode Tree)
        {
            if (Tree.Id == id)
            {
                Tree.isExpanded = false;
            }
            else
            {
                foreach (var item in Tree.Children)
                {
                    CollapseChildren(item.Children);
                }
            }         
        }

        //if we collapse an upper level node, we should probably collapse the children as well
        private static void CollapseChildren(List<TreeNode> Children)
        {
            foreach (var item in Children)
            {
                item.isExpanded = false;
                CollapseChildren(item.Children);
            }
        }

        /// <summary>
        /// selects a node in a tree by id
        /// </summary>
        /// <param name="id">the id of the node</param>
        /// <param name="Tree">the tree</param>
        public static void SelectNode(int id, TreeNode Tree)
        {            

            if (Tree.Id == id)
            {              
                Tree.isSelected = true;

                if (!Tree.isExpanded)
                {
                    SelectRecursively(Tree.Children);                 
                }               
            }
            else
            {
                foreach (var item in Tree.Children)
                {
                    SelectNode(id,item);                  
                }                              
            }
           
        }

        /// <summary>
        /// deselects a node in a tree by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Tree"></param>
        public static void DeselectNode(int id, TreeNode Tree)
        {
            if (Tree.Id == id)
            {
                Tree.isSelected = false;
                if (!Tree.isExpanded)
                {
                    for (int i = 0; i < Tree.Children.Count; i++)
                    {
                        DeselectRecursively(Tree.Children);
                    }
                }
            }
            else
            {
                foreach (var item in Tree.Children)
                {
                    DeselectNode(id, item);
                }
            }
        }

        /// <summary>
        /// use this with linq to get nodes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Tree"></param>
        /// <returns></returns>
        public IEnumerable<TreeNode> GetNodes()
        {
            return new[] { this }.Concat(Children.SelectMany(child => child.GetNodes()));
        }    

        /// <summary>
        /// gets the children of this node
        /// </summary>
        /// <returns></returns>
        public List<TreeNode> GetChildren()
        {
            return Children;
        }

        //helper for setting the recursively if the node was collapsed
        static void SelectRecursively(List<TreeNode> Nodes)
        {
            foreach (var item in Nodes)
            {
                item.isRecursivelySelected = true;
                item.isSelected = false;
                SelectRecursively(item.Children);              
            }
        }

        //same for deselecting
        static void DeselectRecursively(List<TreeNode> Nodes)
        {
            foreach (var item in Nodes)
            {
                item.isRecursivelySelected = false;
                item.isSelected = false;
                SelectRecursively(item.Children);
            }
        }

       
    }
}
