using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Contracts
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
