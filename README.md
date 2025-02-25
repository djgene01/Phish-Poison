# PhishPoisoner

An open-source tool to combat phishing by flooding malicious endpoints with fake credentials. This project helps security enthusiasts and researchers disrupt phishing campaigns by overwhelming their data collection systems with useless, randomly generated email addresses and passwords.

## Features
- **Customizable Phishing Endpoint URL**: Target any phishing form submission endpoint.
- **Optional Proxy Support**: Hide your IP address using a proxy server.
- **Adjustable Number of Fake Entries**: Control how many fake submissions to send.
- **Terminal-based UI**: Built with Terminal.Gui for an intuitive text-based interface.
- **Automatic Endpoint Detection**: Paste phishing HTML to extract the form submission endpoint.

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/PhishPoisoner.git
   cd PhishPoisoner


How It Works
PhishPoisoner sends HTTP POST requests with fake email and password data to a phishing form’s submission endpoint. Here’s the process:
Identify the Endpoint:
Phishing pages typically use an HTML <form> tag to collect user input (e.g., email and password).
The action attribute of the <form> tag specifies the URL where the data is sent (e.g., https://malicious.com/submit.php).
PhishPoisoner targets this URL to flood it with fake submissions.
Generate Fake Data:
Uses the Bogus library to create realistic email addresses (e.g., john.doe@example.com) and strong passwords (e.g., K7$mPx9!qL2j).
Send Requests:
Simulates form submissions by sending POST requests with the fake data.
Includes random User-Agent headers and delays (1–5 seconds) to mimic human behavior and avoid detection.
Proxy Support:
Optionally routes requests through a proxy to anonymize your IP address.
Logging:
Displays real-time progress and errors in the UI, including HTTP status codes or network failures.
Finding the Endpoint in HTML
To use PhishPoisoner effectively, you need the phishing form’s submission endpoint. Here’s how to find it manually:
Obtain the Phishing Page:
Open the phishing email or link in a safe environment (e.g., a virtual machine or sandboxed browser).
Right-click and select "View Page Source" (or use Developer Tools in your browser with Ctrl+U or F12).
Locate the <form> Tag:
Search (Ctrl+F) for <form in the HTML source.
Look for the action attribute, which specifies the endpoint. For example:
html
<form action="https://yanadai.top/submit.php" method="post">
The URL in the action attribute (here, https://yanadai.top/submit.php) is the endpoint.
Input Fields:
Note the name attributes of <input> tags within the form (e.g., email and pswd). PhishPoisoner uses these names (email and pswd) by default, but most phishing forms follow similar conventions.
Alternative:
If you’re unsure, paste the HTML into PhishPoisoner’s "Detect Endpoint" feature (see Usage below), and it will attempt to extract the endpoint automatically.
Usage
Launch the tool:
bash
dotnet run
Manual Entry:
Enter the phishing endpoint URL (e.g., https://malicious.com/phish.php).
(Optional) Provide a proxy (e.g., http://proxy:8080).
Set the number of fake entries to send (e.g., 100).
Click "Start Poisoning" to begin.
Automatic Detection:
Click "Detect Endpoint" (or press the shortcut key if assigned).
Paste the phishing page’s HTML into the dialog box.
PhishPoisoner will extract the action URL and populate the endpoint field if found.
Monitor Progress:
Watch the log window for submission statuses (e.g., "Sent: john.doe@example.com | Status: 200") or errors.
Legal Notice
This tool is for educational and research purposes only. Use it responsibly and only against endpoints you own or have explicit permission to test. Flooding live phishing sites without authorization may violate laws in your jurisdiction.
Contributing
We welcome contributions! Fork the repo, make improvements (e.g., proxy rotation, concurrency, better endpoint detection), and submit a pull request.
License
MIT License - see LICENSE for details.
