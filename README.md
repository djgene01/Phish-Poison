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
