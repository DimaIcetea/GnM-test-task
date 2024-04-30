# LinkedIn Automation

This is a simple automation script for LinkedIn written in C# using Selenium WebDriver. The script allows you to login to LinkedIn, navigate to a profile, and download the profile background and profile picture.

## Requirements

- .NET Core 3.1 or higher
- Chrome browser installed
- ChromeDriver executable (placed in the specified directory)

## Installation

1. Clone the repository:

git clone https://github.com/yourusername/linkedin-automation.git

2. Navigate to the project directory:


3. Restore dependencies:


4. Build the project:

dotnet build

## Usage

1. Run the program:


2. On the first run, the program will prompt you to enter your LinkedIn credentials. The credentials will be saved for future use.

3. The program will then login to LinkedIn, navigate to a profile, and download the profile background and profile picture.

## Configuration

- The ChromeDriver executable path is hardcoded in the `Main` method of the `Program` class. Make sure to update it according to your system.
- The credentials are stored in a `credentials.json` file. If you need to update your credentials, you can manually edit this file.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
