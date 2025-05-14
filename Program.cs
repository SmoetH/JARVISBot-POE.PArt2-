using System;
using System.Collections.Generic;
using System.Media;
using System.Speech.Synthesis;
using System.Threading;

class JARVIS
{
    private string _username = "Stranger";
    private string _interest = "";
    private string _lastTopic = "";
    private SpeechSynthesizer _synth = new SpeechSynthesizer();
    private Random _rand = new Random();

    private Dictionary<string, List<string>> _topicResponses = new Dictionary<string, List<string>>()
    {
        { "phishing", new List<string> {
            "Phishing is a trick to steal your info. Ever got a weird email asking for your login? (yes/no)",
            "Phishing attacks often look like real messages. Seen one recently? (yes/no)"
        }},
        { "password", new List<string> {
            "Use at least 12 characters, including symbols and numbers! Use a password manager? (yes/no)",
            "Never reuse passwords. Curious about manager tools? (yes/no)"
        }},
        { "malware", new List<string> {
            "Malware is like digital poison for your system. Want removal tips? (yes/no)",
            "It hides in shady links and downloads. Want to learn how to spot it? (yes/no)"
        }},
        { "privacy", new List<string> {
            "Always check your app permissions and avoid oversharing. Need more advice? (yes/no)",
            "Online privacy matters. Use encrypted apps like Signal. Curious to learn more? (yes/no)"
        }},
        { "scam", new List<string> {
            "Scams come in all forms—calls, texts, emails. Want red flags to watch for? (yes/no)",
            "If it sounds too good to be true—it usually is. Want to learn how to report scams? (yes/no)"
        }},
        { "safe online", new List<string> {
            "Use 2FA, strong passwords, and avoid public Wi-Fi. Want a full guide? (yes/no)",
            "Update your software and avoid suspicious links. Need more tips? (yes/no)"
        }},
        { "nelson mandela", new List<string> {
            "He was South Africa’s first black president. Want to hear about his early life? (yes/no)",
            "Mandela was imprisoned for 27 years. Curious about that journey? (yes/no)"
        }},
        { "rand", new List<string> {
            "The Rand replaced the pound in 1961. Want to know more about its history? (yes/no)",
            "Did you know the Rand was once stronger than the dollar? Want to know why? (yes/no)"
        }},
        { "table mountain", new List<string> {
            "Table Mountain is a natural wonder in Cape Town. Ever visited? (yes/no)",
            "It's one of the world’s oldest mountains. Want cool facts? (yes/no)"
        }},
    };

    // Track progress in topic questions
    private Dictionary<string, int> _questionIndex = new Dictionary<string, int>();

    public void Start()
    {
        PlayAudioIntro();
        RenderAsciiLogo();
        DisplayBio();
        GreetUser();
        ChatLoop();
    }

    private void PlayAudioIntro()
    {
        try
        {
            if (System.IO.File.Exists("greeting.wav"))
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.PlaySync();
            }
            else
            {
                Console.WriteLine("[Audio intro not found]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Audio error: {ex.Message}]");
        }
    }

    private void RenderAsciiLogo()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
      ██████╗  █████╗ ██████╗ ██╗   ██╗██╗███████╗
     ██╔════╝ ██╔══██╗██╔══██╗██║   ██║██║██╔════╝
     ██║  ███╗███████║██████╔╝██║   ██║██║█████╗  
     ██║   ██║██╔══██║██╔═══╝ ██║   ██║██║██╔══╝  
     ╚██████╔╝██║  ██║██║     ╚██████╔╝██║███████╗
      ╚═════╝ ╚═╝  ╚═╝╚═╝      ╚═════╝ ╚═╝╚══════╝

          J.A.R.V.I.S – Cybersecurity & SA Guide
");
        Console.ResetColor();
    }

    private void DisplayBio()
    {
        string bio = "I am J.A.R.V.I.S., your assistant for cybersecurity and South African knowledge.";
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(bio);
        Console.ResetColor();
        Speak(bio);
    }

    private void GreetUser()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("👋 Hey! What’s your name? ");
        Console.ResetColor();

        string nameInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nameInput))
            _username = nameInput.Trim();

        string greet = $"Hi {_username}, ready to explore cybersecurity and more with me?";
        Console.WriteLine(greet);
        Speak(greet);
    }

    private void ChatLoop()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n💬 Ask me something (or type 'bye' to exit): ");
            Console.ResetColor();

            string input = Console.ReadLine()?.ToLower().Trim();
            if (string.IsNullOrEmpty(input)) continue;
            if (input == "bye") break;

            string response = GetResponse(input);
            SimulateTyping(response);
            Speak(response);
        }

        Console.WriteLine("\n👋 Goodbye! Stay cyber-safe.");
        Speak("Goodbye! Stay cyber-safe.");
    }

    private string GetResponse(string input)
    {
        if (input.Contains("worried"))
            return "No worries, " + _username + "! I'm here to help you stay safe. What's bothering you?";
        if (input.Contains("how’s life") || input.Contains("how are you"))
            return "I'm doing great, thanks for asking! How about you?";
        if (input.Contains("curious"))
            return "Curiosity is good! Ask away, I’ve got answers.";
        if (input.Contains("frustrated"))
            return "I understand—it can be confusing. Let’s make it easier.";

        // Handle spelling or misinterpretation of keywords
        input = CorrectSpelling(input);

        // Follow-up handler for yes/no
        if (input == "yes" || input == "no")
            return HandleFollowUp(input);

        // Set interest & reset index
        foreach (var topic in _topicResponses.Keys)
        {
            if (input.Contains(topic))
            {
                _lastTopic = topic;
                _interest = topic;

                if (!_questionIndex.ContainsKey(topic))
                    _questionIndex[topic] = 0;

                int index = _questionIndex[topic];
                if (index < _topicResponses[topic].Count)
                    return _topicResponses[topic][index];
                else
                    return $"You've gone through all the questions I have on {_lastTopic}. Want to explore another topic?";
            }
        }

        if (input.Contains("interested") && input.Contains("in"))
        {
            int index = input.IndexOf("in") + 3;
            string interest = input.Substring(index).Trim();
            _interest = interest;
            return $"Great! I'll remember that you're interested in {_interest}. It's a crucial part of staying safe online.";
        }

        if (input.Contains("remind me") && !string.IsNullOrEmpty(_interest))
        {
            return $"As someone interested in {_interest}, you might want to explore more on that. I can help!";
        }

        return "Hmm... I didn't catch that. Ask me about cybersecurity or South African facts!";
    }

    private string HandleFollowUp(string answer)
    {
        if (string.IsNullOrEmpty(_lastTopic))
            return "Could you ask a new question? I'm listening.";

        if (!_questionIndex.ContainsKey(_lastTopic))
            _questionIndex[_lastTopic] = 0;

        if (answer == "yes")
        {
            _questionIndex[_lastTopic]++;

            if (_questionIndex[_lastTopic] < _topicResponses[_lastTopic].Count)
            {
                return _topicResponses[_lastTopic][_questionIndex[_lastTopic]];
            }
            else
            {
                return $"That's all I have on {_lastTopic}. Ask about something else you're curious about!";
            }
        }
        else
        {
            return "No problem. Let me know when you're ready to dive into something else.";
        }
    }

    private void Speak(string text)
    {
        try
        {
            _synth.SpeakAsyncCancelAll();
            _synth.SpeakAsync(text);
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Speech error: " + ex.Message + "]");
        }
    }

    private void SimulateTyping(string msg)
    {
        foreach (char c in msg)
        {
            Console.Write(c);
            Thread.Sleep(15);
        }
        Console.WriteLine();
    }

    // Basic spelling correction function for known keywords
    private string CorrectSpelling(string input)
    {
        // Correct some common misinterpretations or misspellings here
        Dictionary<string, string> corrections = new Dictionary<string, string>
        {
            { "password", "password" },
            { "phising", "phishing" },
            { "securoty", "security" }
        };

        foreach (var correction in corrections)
        {
            if (input.Contains(correction.Key))
                input = input.Replace(correction.Key, correction.Value);
        }

        return input;
    }
}
