using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace passgen
{
    public partial class MainForm : Form
    {
        private EncryptionMachine.Machine machine;
        private EncryptionMachine.Machine.Context machineContext;

        private class InputPlaceholder
        {
            public static void GotFocus(object sender, EventArgs e)
            {
                if (sender is Control textbox)
                {
                    if (textbox.Tag is string tag)
                        if (textbox.Text == tag)
                        {
                            textbox.Text = string.Empty;
                            textbox.ForeColor = SystemColors.WindowText;
                        }
                }
            }

            public static void LostFocus(object sender, EventArgs e)
            {
                if (sender is Control textbox)
                {
                    if (textbox.Tag is string tag && textbox.Text.Length == 0)
                    {
                        textbox.Text = tag;
                        textbox.ForeColor = SystemColors.GrayText;
                    }
                }
            }
        }

        private readonly string[] servicesList = new string[]
        {
            "VK", "Google", "Yandex", "Mail.ru", "Twitter", "OK", "Facebook",
            "Instagram", "Wikipedia", "Twitch", "Amazon", "Reddit", "Aliexpress",
            "Avito", "Github", "Gitlab", "Trello", "Rambler"
        };

        public MainForm()
        {
            machine = new EncryptionMachine.Machine();
            if (!machine.Compile(ExampleCode(), out machineContext))
                return;

            InitializeComponent();

            serviceSelector.Items.AddRange(servicesList);

            Array.ForEach(new Control[] { masterInput, loginInput, serviceSelector, algoSelector }, (w) =>
            {
                w.TextChanged += Input_TextChanged;
                w.GotFocus += InputPlaceholder.GotFocus;
                w.LostFocus += InputPlaceholder.LostFocus;
                InputPlaceholder.LostFocus(w, null);
            });

            masterInput.GotFocus += MasterInput_GotFocus;
            masterInput.LostFocus += MasterInput_LostFocus;

            //this.Load += MainForm_Load;
        }

        private void MasterInput_GotFocus(object sender, EventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (textbox.Tag is string tag)
                    if (textbox.Text != tag)
                    {
                        textbox.PasswordChar = '\u25CF';
                    }
            }
        }

        private void MasterInput_LostFocus(object sender, EventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (textbox.Tag is string tag)
                    if (textbox.Text == tag)
                    {
                        textbox.PasswordChar = (char)0;
                    }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            masterInput.Text = "MyMasterPassword";
            loginInput.Text = "MyLogin";
            serviceSelector.Text = "MyService";
            algoSelector.SelectedIndex = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100_000; i++)
            {
                Input_TextChanged(null, null);
            }
            stopwatch.Stop();
            Console.WriteLine($"100 000 iterations time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        private void Input_TextChanged(object sender, EventArgs e)
        {
            var result = string.Empty;

            do
            {
                if ((masterInput.Text.Length > 0) && (masterInput.Text != masterInput.Tag as string))
                    machineContext.variables["master"] = masterInput.Text;
                else break;

                if ((serviceSelector.Text.Length > 0) && (serviceSelector.Text != serviceSelector.Tag as string))
                    machineContext.variables["service"] = serviceSelector.Text;
                else break;

                if ((loginInput.Text.Length > 0) && (loginInput.Text != loginInput.Tag as string))
                    machineContext.variables["account"] = loginInput.Text;
                else break;

                result = machine.Run(machineContext);
            } while (false);

            hashOutput.Text = result;
        }

        private string ExampleCode()
        {
            StringBuilder code = new StringBuilder();
            code.AppendLine("use master");
            code.AppendLine("indexes");
            code.AppendLine("save master.indexes");

            code.AppendLine("use master");
            code.AppendLine("ato1");
            code.AppendLine("mul master.indexes");
            code.AppendLine("sum");
            code.AppendLine("save masterKey");

            code.AppendLine("use service");
            code.AppendLine("ato1");
            code.AppendLine("sum");
            code.AppendLine("mul masterKey");
            code.AppendLine("save serviceKey");

            code.AppendLine("use account");
            code.AppendLine("ato1");
            code.AppendLine("sum");
            code.AppendLine("mul masterKey");
            code.AppendLine("save accountKey");

            code.AppendLine("use service");
            code.AppendLine("substr 1 1");
            code.AppendLine("upper");
            code.AppendLine("print");

            code.AppendLine("use account");
            code.AppendLine("substr 1 1");
            code.AppendLine("upper");
            code.AppendLine("print");

            code.AppendLine("use serviceKey");
            code.AppendLine("print");

            code.AppendLine("use account");
            code.AppendLine("substr 1 1");
            code.AppendLine("lower");
            code.AppendLine("print");

            code.AppendLine("use accountKey");
            code.AppendLine("print");

            code.AppendLine("use service");
            code.AppendLine("substr 1 1");
            code.AppendLine("lower");
            code.AppendLine("print");

            return code.ToString();
        }

        private void AlgoSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox selector)
            {
                if (selector.SelectedIndex == 4)
                {
                    new CodeEditor().Show();
                }
            }
        }
    }
}
