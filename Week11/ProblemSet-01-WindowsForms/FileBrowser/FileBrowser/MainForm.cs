using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileBrowser
{
    public partial class MainForm : Form
    {
        Dictionary<string, DirectoryInfo> directories;
        Dictionary<string, TreeNode> treeNodes;
        HashSet<string> expandedNodes;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            directories = new Dictionary<string, DirectoryInfo>();
            treeNodes = new Dictionary<string, TreeNode>();
            expandedNodes = new HashSet<string>();

            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                DirectoryInfo driveDir = new DirectoryInfo(drive.Name);

                directories.Add(driveDir.FullName, driveDir);
                TreeNode root = folderTreeView.Nodes.Add(driveDir.FullName, driveDir.Name);
                treeNodes.Add(driveDir.FullName, root);

                AddSubFolders(driveDir);
            }
        }

        private void AddSubFolders(DirectoryInfo curDir)
        {
            try
            {
                var subFolders = curDir.GetDirectories();
                TreeNode rootOfCurDir = treeNodes[curDir.FullName];
                foreach (var folder in subFolders)
                {
                    directories.Add(folder.FullName, folder);
                    TreeNode child = rootOfCurDir.Nodes.Add(folder.FullName, folder.Name);
                    treeNodes.Add(folder.FullName, child);
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }

        private void folderTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode expandedNode = e.Node;
            if (!expandedNodes.Contains(expandedNode.Name))
            {
                foreach (TreeNode childNode in expandedNode.Nodes)
                {
                    DirectoryInfo curDir = directories[childNode.Name];
                    AddSubFolders(curDir);
                }
                expandedNodes.Add(expandedNode.Name);
            }
        }

        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            directoryListView.Items.Clear();

            try
            {
                var selectedNode = e.Node;
                var selectedDir = directories[selectedNode.Name];
                var files = selectedDir.GetFiles();
                foreach (var file in files)
                {
                    var item = new ListViewItem(new string[] { file.Name,
                                                           file.CreationTime.ToString(),
                                                           GetSize(file.Length) });

                    item.Name = file.FullName;

                    directoryListView.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }

        private string GetSize(long bytes)
        {
            if (bytes < 1024)
            {
                return (bytes.ToString() + " B").PadLeft(10);
            }
            if (bytes >> 10 < 1024)
            {
                return (((double)bytes / (1 << 10)).ToString("N2") + " KB").PadLeft(10);
            }
            if (bytes >> 20 < 1024)
            {
                return (((double)bytes / (1 << 20)).ToString("N2") + " MB").PadLeft(10);
            }
            if (bytes >> 30 < 1024)
            {
                return (((double)bytes / (1 << 30)).ToString("N2") + " GB").PadLeft(10);
            }

            return (((double)bytes / (1 << 40)).ToString("N2") + " TB").PadLeft(10);
        }

        private void directoryListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;
            if (listView != null)
            {
                var clickedItem = listView.HitTest(e.Location).Item;
                if (clickedItem != null)
                {
                    System.Diagnostics.Process.Start(clickedItem.Name);
                }
            }
        }
    }
}
