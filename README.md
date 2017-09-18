# Maxotek.EmailUtils

Quickly send email using an HTML template. Supports parameters which can be passed dynamically.

## Example

```csharp
/* Constants */
var subjectTemplate = "Feedback from ${Sender}";
var bodyTemplate = @"
	<body>
		<div>
			Feedback from ${Sender} (${SenderEmail})<br/>
			<p>${Body}</p>
		</div>
	</body>
";

var smtpHost = "smtp.gmail.com";
var smtpPort = 587;
var smtpUser = "john.doe";
var smtpPass = "whoami?";
var feedbackTo = "john@thedoes.com";

/* Parameters - Plug these externally */
var name = "Jane Doe";
var email = "jane@thedoes.com";
var feedback = "Thou art a nobody";

/* Usage */
var emailSender = new EmailSender(smtpHost, smtpPort, smtpUser, smtpPass);

var subjectParameters = new Dictionary<string, string>
{
    {"Sender", name}
};

var feedbackSubject = EmailSender.ReplaceVars(subjectTemplate, subjectParameters);

var bodyParameters = new Dictionary<string, string>
{
    {"Sender", name},
    {"SenderEmail", email},
    {"Body", feedback}
};

var replyTo = new MailAddress(email, name);

emailSender.SendMail(feedbackSubject, feedbackTo, email, bodyTemplate, bodyParameters, replyTo);
```

On a side note, check out my site [Tool Slick](https://toolslick.com) which makes use of this utility library for the site feedback feature.
