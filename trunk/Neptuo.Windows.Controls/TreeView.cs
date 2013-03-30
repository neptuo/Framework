using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Neptuo.Windows.Controls
{
    public class TreeView : System.Windows.Controls.TreeView
    {
        public TreeView()
        {
            this.MouseDown += TreeView_MouseUp;
        }

        private void TreeView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem selectedItem = (ItemContainerGenerator.ContainerFromItem(SelectedItem) as TreeViewItem);
            //if((sender as TreeViewItem) == null && 
            if (selectedItem != null)
                selectedItem.IsSelected = false;

        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return TreeViewItem.CreateTreeViewItem();
        }
    }
}
