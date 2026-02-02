using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace Keane.CH.Framework.Apps.UI.Web.AjaxResponse
{
    /// <summary>
    /// Extends the System.Web.UI.Control so as to enable 
    /// controls to simply be added to the client response.
    /// </summary>
    public static class AjaxResponseDataControlExtension
    {
        /// <summary>
        /// Extension method to add a control to a client response.
        /// </summary>
        /// <param name="control">The asp.net control to be added to the response.</param>
        public static void AddToResponse(
            this Control control, 
            AjaxResponseData response)
        {
            // Defensively code.
            if (control == null)
                throw new ArgumentNullException("control");

            // Text boxes.
            TextBox textBox = (control as TextBox);
            if (textBox != null)
            {
                response.AddDomUpdate(
                    textBox.ClientID, textBox.Text, DomUpdateTargetType.AspNetTextBox);
                return;
            }

            // Labels.
            Label label = (control as Label);
            if (label != null)
            {
                response.AddDomUpdate(
                    label.ClientID, label.Text, DomUpdateTargetType.AspNetLabel);
                return;
            }

            // Html input controls.
            HtmlInputControl htmlInputControl = control as HtmlInputControl;
            if (htmlInputControl != null)
            {
                response.AddDomUpdate(
                    htmlInputControl.ClientID, htmlInputControl.Value, DomUpdateTargetType.HtmlInput);
                return;
            }

            // Unspecified.
            throw new ApplicationException("The control type is unsupported.");
        }
    }
}
