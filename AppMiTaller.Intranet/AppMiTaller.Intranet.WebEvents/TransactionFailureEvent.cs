using System;
using System.Web.Management;

namespace AppMiTaller.Intranet.WebEvents
{
    public class TransactionFailureEvent : WebFailureAuditEvent
    {
        string customMessage;

        public TransactionFailureEvent(object sender, String usuarioID, String message)
            : base(String.Format("Usuario: {0}. - Message: {1};", usuarioID, message), sender, WebEventCodes.WebExtendedBase)
        {
            this.customMessage = String.Format("Usuario: {0}. - Message: {1};", usuarioID, message);
        }

        public override void FormatCustomEventDetails(WebEventFormatter formatter)
        {
            base.FormatCustomEventDetails(formatter);
            formatter.AppendLine(this.customMessage);
        }
    }
}
