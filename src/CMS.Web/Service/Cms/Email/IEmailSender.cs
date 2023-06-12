using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Web.Service.Cms;

public interface IEmailSender 
{
    /// <summary>
    /// Send an Email.
    /// </summary>
    /// <param name="message"></param>
    void Send(MimeMessage message);

    /// <summary>
    /// Sends an Email.
    /// </summary>
    /// <param name="message">Mail to be sent</param>
    /// <param name="cancellationToken"></param>
    Task SendAsync(MimeMessage message, CancellationToken cancellationToken = default);
}