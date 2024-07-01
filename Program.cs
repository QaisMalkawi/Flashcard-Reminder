using FlashcardReminder.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp1.Forms;

namespace FlashcardReminder
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeTRayIcon();

            Application.Run(new CardsView());
        }

        static void InitializeTRayIcon()
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon(Path.Combine(Application.StartupPath, "Icon.ico"));
            notifyIcon.Text = "Cards";
            notifyIcon.Visible = true;

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem settingsItem = new ToolStripMenuItem("Settings");
            ToolStripMenuItem nextCardItem = new ToolStripMenuItem("Next Card");
            ToolStripMenuItem cardsDirectoryItem = new ToolStripMenuItem("Cards Directory");
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");

            Globals.LoadDecks();

            settingsItem.Click += (o, ea)=>
            {
                if (Globals.settingsForm != null) return;

                Globals.LoadDecks();
                Globals.settingsForm = new SettingsWindow();
                Globals.settingsForm.ShowDialog();

            };
            nextCardItem.Click += (o, ea)=>
            {
                if (Globals.cardViewForm == null) return;

                Globals.cardViewForm.ShowCard(o, ea);

            };
            cardsDirectoryItem.Click += (o, ea)=>
            {
                if (Directory.Exists(Globals.DecksPath))
                {
                    Process.Start("explorer.exe", Globals.DecksPath);
                }
            };
            exitItem.Click += (o, ea) =>
            {
                Globals.SaveDecks();
                Application.Exit();
            };

            contextMenu.Items.Add(settingsItem);
            contextMenu.Items.Add(nextCardItem);
            contextMenu.Items.Add(cardsDirectoryItem);
            contextMenu.Items.Add(exitItem);

            notifyIcon.ContextMenuStrip = contextMenu;
        }
    }
}
