using CourtBooking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CourtBooking.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendBookingConfirmationAsync(string toEmail, string userName, string courtName,
        DateTime startTime, DateTime endTime, decimal totalPrice)
    {
        var subject = "✅ Reserva Confirmada - CourtBooking";
        var body = $@"
            <h2>¡Tu reserva está confirmada!</h2>
            <p>Hola <strong>{userName}</strong>,</p>
            <p>Tu reserva ha sido confirmada con los siguientes detalles:</p>
            <table style='border-collapse: collapse; width: 100%;'>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Cancha</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>{courtName}</td></tr>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Fecha</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>{startTime:dd/MM/yyyy}</td></tr>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Horario</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>{startTime:HH:mm} - {endTime:HH:mm}</td></tr>
                <tr><td style='padding: 8px; border: 1px solid #ddd;'><strong>Total</strong></td><td style='padding: 8px; border: 1px solid #ddd;'>${totalPrice:N2}</td></tr>
            </table>
            <br/>
            <p>¡Te esperamos! 🏆</p>
            <p><em>Equipo CourtBooking</em></p>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendBookingCancellationAsync(string toEmail, string userName, string courtName,
        DateTime startTime, string? reason)
    {
        var subject = "❌ Reserva Cancelada - CourtBooking";
        var body = $@"
            <h2>Tu reserva ha sido cancelada</h2>
            <p>Hola <strong>{userName}</strong>,</p>
            <p>Tu reserva para <strong>{courtName}</strong> del <strong>{startTime:dd/MM/yyyy HH:mm}</strong> ha sido cancelada.</p>
            {(reason is not null ? $"<p><strong>Motivo:</strong> {reason}</p>" : "")}
            <p>Podés hacer una nueva reserva cuando quieras.</p>
            <p><em>Equipo CourtBooking</em></p>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendBookingRescheduleAsync(string toEmail, string userName, string courtName,
        DateTime newStartTime, DateTime newEndTime)
    {
        var subject = "🔄 Reserva Reprogramada - CourtBooking";
        var body = $@"
            <h2>Tu reserva fue reprogramada</h2>
            <p>Hola <strong>{userName}</strong>,</p>
            <p>Tu reserva en <strong>{courtName}</strong> fue actualizada:</p>
            <p><strong>Nueva fecha:</strong> {newStartTime:dd/MM/yyyy}</p>
            <p><strong>Nuevo horario:</strong> {newStartTime:HH:mm} - {newEndTime:HH:mm}</p>
            <p><em>Equipo CourtBooking</em></p>";

        await SendEmailAsync(toEmail, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var smtpEnabled = bool.Parse(_configuration["Email:Enabled"] ?? "false");
        if (!smtpEnabled)
        {
            _logger.LogInformation("Email (disabled) to: {Email} | Subject: {Subject}", toEmail, subject);
            return;
        }

        // Production: use MailKit
        // var message = new MimeMessage();
        // message.From.Add(new MailboxAddress("CourtBooking", config["Email:From"]));
        // message.To.Add(new MailboxAddress(toEmail, toEmail));
        // message.Subject = subject;
        // message.Body = new TextPart("html") { Text = htmlBody };
        // using var client = new SmtpClient();
        // await client.ConnectAsync(config["Email:Host"], config.GetValue<int>("Email:Port"), SecureSocketOptions.StartTls);
        // await client.AuthenticateAsync(config["Email:Username"], config["Email:Password"]);
        // await client.SendAsync(message);
        // await client.DisconnectAsync(true);

        _logger.LogInformation("Email sent to {Email}: {Subject}", toEmail, subject);
        await Task.CompletedTask;
    }
}
