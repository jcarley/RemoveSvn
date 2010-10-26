using System;
using System.Windows.Forms;
using System.IO;

namespace RemoveSvn
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtRootDirectory.Text = folderBrowserDialog.SelectedPath;

                SearchDirectory();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDirectory();
        }

        private void SearchDirectory()
        {
            Cursor originalCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (!string.IsNullOrEmpty(txtRootDirectory.Text) && Directory.Exists(txtRootDirectory.Text))
                {
                    chkDirectoryList.Items.Clear();

                    DirectorySearcher searcher = new DirectorySearcher { Pattern = "*svn", RootDirectory = txtRootDirectory.Text };

                    searcher.Match += new EventHandler<DirectoryEventArgs>(Directory_Match);

                    searcher.Search();

                    MessageBox.Show(this, "Search Complete.", "Search", MessageBoxButtons.OK);
                }
            }
            finally
            {
                this.Cursor = originalCursor;
            }
        }

        private void Directory_Match(object sender, DirectoryEventArgs args)
        {
            DirectoryMatch match = new DirectoryMatch(args.Directory);
            chkDirectoryList.Items.Add(match, true);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "This action will remove all checked directories.  Continue?", "Remove all directories", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.No)
                return;

            Cursor originalCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            try
            {
                foreach (object item in chkDirectoryList.CheckedItems)
                {
                    DirectoryMatch match = item as DirectoryMatch;

                    if (match != null)
                    {
                        match.Remove();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor = originalCursor;
            }

            SearchDirectory();
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < chkDirectoryList.Items.Count; index++)
            {
                chkDirectoryList.SetItemChecked(index, true);
            }
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < chkDirectoryList.Items.Count; index++)
            {
                chkDirectoryList.SetItemChecked(index, false);
            }
        }

    }
}
