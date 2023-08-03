using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowHolder
{
    public class TemplateBase : Form
    {
        public virtual string CommandLine { get; set; }

        protected string AppendOption(ref string strSpace, string strOption, CheckBox cb, TextBox tb)
        {
            return AppendOption(ref strSpace, strOption, cb.Checked, tb.Text, false);
        }

        protected string AppendOption(ref string strSpace, string strOption, CheckBox cb, RadioButton rb)
        {
            return AppendOption(ref strSpace, strOption, cb.Checked && rb.Checked, null, false);
        }

        protected string AppendOption(ref string strSpace, string strOption, RadioButton cb, TextBox tb)
        {
            return AppendOption(ref strSpace, strOption, cb.Checked, tb.Text, false);
        }

        protected string AppendOption(ref string strSpace, string strOption, CheckBox cb)
        {
            return AppendOption(ref strSpace, strOption, cb.Checked, null, false);
        }

        protected string AppendOption(ref string strSpace, string strOption, bool bEnable, string strArgument, bool bOverrideArgSpace)
        {
            if (!bEnable)
                return "";

            // null means "no argument to this option" but an empty string means "no value for this option"
            // -- which in this case translates to "don't store this option"
            if (strArgument == "")
                return "";

            if (strArgument != null)
            {
                strArgument = strArgument.Replace("\"", "\\\"");
                bool bContainsWhitespace = false;
                foreach (char c in strArgument)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        bContainsWhitespace = true;
                        break;
                    }
                }

                if (bContainsWhitespace)
                    strArgument = "\"" + strArgument + "\"";
            }

            string strSpaceAfterOption = "";
            if (strArgument != null)
            {
                if (!strOption.EndsWith(":") || bOverrideArgSpace)
                    strSpaceAfterOption = " ";
            }

            string strResult = strSpace + strOption + (strArgument == null ? "" : strSpaceAfterOption + strArgument);
            strSpace = " ";

            return strResult;
        }
    }
}
