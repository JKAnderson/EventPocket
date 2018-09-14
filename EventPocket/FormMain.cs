using Octokit;
using Semver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EventPocket
{
    public partial class FormMain : Form
    {
        private static Properties.Settings settings = Properties.Settings.Default;

        private DSProcess dsProcess;

        public FormMain()
        {
            InitializeComponent();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            Text = "EventPocket " + System.Windows.Forms.Application.ProductVersion;
            Location = settings.WindowLocation;
            if (settings.WindowSize.Width >= MinimumSize.Width && settings.WindowSize.Height >= MinimumSize.Height)
                Size = settings.WindowSize;
            if (settings.WindowMaximized)
                WindowState = FormWindowState.Maximized;

            nudHertz.Value = settings.Hertz;

            foreach (string idStr in settings.EventFlags.Split(','))
            {
                if (int.TryParse(idStr, out int id))
                    dgvFlags.Rows.Add(id, "");
            }

            foreach (string valStr in settings.EventValues.Split(','))
            {
                Match match = Regex.Match(valStr, @"\s*(\d+)\:(\d+)\s*");
                if (match.Success && int.TryParse(match.Groups[1].Value, out int id)
                    && int.TryParse(match.Groups[2].Value, out int width))
                {
                    dgvValues.Rows.Add(id, width, 0);
                }
            }

            GitHubClient gitHubClient = new GitHubClient(new ProductHeaderValue("EventPocket"));
            try
            {
                Release release = await gitHubClient.Repository.Release.GetLatest("JKAnderson", "EventPocket");
                if (SemVersion.Parse(release.TagName) > System.Windows.Forms.Application.ProductVersion)
                {
                    LinkLabel.Link link = new LinkLabel.Link();
                    link.LinkData = release.HtmlUrl;
                    llbUpdate.Links.Add(link);
                    llbUpdate.Visible = true;
                }
            }
            catch { }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.WindowMaximized = WindowState == FormWindowState.Maximized;
            if (WindowState == FormWindowState.Normal)
            {
                settings.WindowLocation = Location;
                settings.WindowSize = Size;
            }
            else
            {
                settings.WindowLocation = RestoreBounds.Location;
                settings.WindowSize = RestoreBounds.Size;
            }

            settings.Hertz = nudHertz.Value;

            var str = new List<string>();
            foreach (DataGridViewRow row in dgvFlags.Rows)
            {
                if (int.TryParse(row.Cells[0].Value?.ToString(), out int id))
                    str.Add($"{id}");
            }
            settings.EventFlags = string.Join(",", str);

            str = new List<string>();
            foreach (DataGridViewRow row in dgvValues.Rows)
            {
                if (int.TryParse(row.Cells[0].Value?.ToString(), out int id)
                    && int.TryParse(row.Cells[1].Value?.ToString(), out int width))
                {
                    str.Add($"{id}:{width}");
                }
            }
            settings.EventValues = string.Join(",", str);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (dsProcess == null)
            {
                if (DSProcess.GetProcess(out dsProcess))
                {
                    lblAttachedValue.Text = dsProcess.Remastered ? "DSR" : "PTDE";
                    tmrUpdate.Interval = (int)(1000.0 / (double)nudHertz.Value);
                }
            }
            else
            {
                if (dsProcess.Alive())
                {
                    updateDataGrids();
                }
                else
                {
                    dsProcess.Close();
                    dsProcess = null;
                    lblAttachedValue.Text = "None";
                    tmrUpdate.Interval = 1000;
                }
            }
        }

        private void updateDataGrids()
        {
            foreach (DataGridViewRow row in dgvFlags.Rows)
            {
                if (int.TryParse(row.Cells[0].Value?.ToString(), out int id))
                {
                    bool flag = dsProcess.ReadEventFlag(id);
                    row.Cells[1].Value = flag ? "On" : "Off";
                    row.Cells[1].Style.BackColor = flag ? Color.LightGreen : Color.Pink;
                }
            }

            foreach (DataGridViewRow row in dgvValues.Rows)
            {
                if (int.TryParse(row.Cells[0].Value?.ToString(), out int id)
                    && int.TryParse(row.Cells[1].Value?.ToString(), out int width))
                {
                    int value = dsProcess.ReadEventValue(id, width);
                    row.Cells[2].Value = value;
                }
            }
        }

        private void nudHertz_ValueChanged(object sender, EventArgs e)
        {
            if (dsProcess != null)
                tmrUpdate.Interval = (int)(1000.0 / (double)nudHertz.Value);
        }
    }
}
