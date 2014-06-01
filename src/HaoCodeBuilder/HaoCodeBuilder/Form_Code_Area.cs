using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Actions;

namespace HaoCodeBuilder
{
    public partial class Form_Code_Area : Form_Base
    {
        public Form_Code_Area(string text, string title)
        {
            InitializeComponent();
            this.Text = title;
            this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            this.textEditorControl1.Encoding = System.Text.Encoding.Default;
            this.textEditorControl1.Text = text;
        }

        private void Form_Code_Area_Load(object sender, EventArgs e)
        {

        }

        private void DoEditAction(ICSharpCode.TextEditor.Actions.IEditAction action)
        {
            TextEditorControl editor = this.textEditorControl1;
            if (editor != null && action != null)
            {
                TextArea area = editor.ActiveTextAreaControl.TextArea;
                editor.BeginUpdate();
                try
                {
                    lock (editor.Document)
                    {
                        action.Execute(area);
                    }
                }
                finally
                {
                    editor.EndUpdate();
                    area.Caret.UpdateCaretPosition();
                }
            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoEditAction(new ICSharpCode.TextEditor.Actions.SelectWholeDocument());
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoEditAction(new ICSharpCode.TextEditor.Actions.Paste());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoEditAction(new ICSharpCode.TextEditor.Actions.SelectWholeDocument());
            DoEditAction(new ICSharpCode.TextEditor.Actions.Copy());
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoEditAction(new ICSharpCode.TextEditor.Actions.Copy());
        }


    }
}
