

# JARVIS: Console-Based Chatbot

> “I am JARVIS, your AI assistant. I specialize in cybersecurity and South African general knowledge.”
>
> * 🧠 Voice-powered, ASCII-styled, interactive console chatbot.

---

## 📜 Overview

**JARVIS** is a console-based chatbot created in **C#** that blends **cybersecurity education**, **South African general knowledge**, and some light-hearted small talk. The chatbot features:

* 🔊 **Text-to-Speech responses**
* 🎨 **ASCII art image display**
* 🎧 **Custom audio intro**
* 💬 Conversational **chat loop with smart topic recognition**
* 🧠 Built-in knowledge base and follow-up logic

---

## ⚙️ Requirements

* Windows OS (for **System.Speech** & Console features)
* .NET Framework (Console App - typically .NET 4.x)
* `eddie.jpg` (ASCII art image file)
* `greeting.wav` (Intro audio file)

---

## 📦 Project Structure

```
📁 JARVIS_Chatbot
├── eddie.jpg              # Image used for ASCII art display
├── greeting.wav           # Optional audio intro
├── Program.cs             # Main entry point
├── JARVIS.cs              # Core chatbot logic
├── AsciiArtDisplay.cs     # Handles image to ASCII rendering
├── ChatKnowledge.cs       # Knowledge base
```

---

## 🚀 Getting Started

### 1. Clone or Download the Repository

```bash
git clone https://github.com/your-username/JARVIS_Chatbot.git
cd JARVIS_Chatbot
```

### 2. Add Required Files

Place these files in the **root directory** of the project:

* ✅ `eddie.jpg` – Used for displaying ASCII art at startup.
* ✅ `greeting.wav` – Optional voice greeting (can be replaced or skipped).

### 3. Build and Run the Project

Open in Visual Studio and run the project (`F5`) or use:

```bash
dotnet build
dotnet run
```

---

## 🧠 How to Use JARVIS

Once the program starts, here's what happens:

### 🌟 Intro

* `greeting.wav` plays
* ASCII art of `eddie.jpg` is displayed
* A short bio introduces JARVIS
* You are asked for your name (used in further messages)

---

### 💬 Chatting with JARVIS

You’ll see a prompt like:

```
Ask me something (or type 'bye' to exit):
```

You can ask about:

#### 🔐 Cybersecurity Topics

* `phishing`
* `password`
* `malware`
* `ransomware`
* `safe online`
* `firewall`

➡ JARVIS will reply with a smart answer and **may ask follow-up questions**. You can reply with `yes` or `no` to continue the topic.

#### 🇿🇦 South African Knowledge

* `apartheid`
* `mandela`
* `bafana bafana`
* `loadshedding`

➡ No follow-up questions, just fun facts and general knowledge.

#### 👋 Small Talk

* `how are you`, `howzit`, `what’s up`, `who is jarvis`, etc.

➡ JARVIS responds with casual replies and compliments.

---

## 🛑 Exit the Chat

To end the conversation, type `bye` and JARVIS will sign off with a friendly reminder to stay safe.

---

## 🔊 Notes

* JARVIS uses **`System.Speech.Synthesis`** to speak responses out loud.
* You can **mute** by commenting out `Speak()` calls if needed.
* Works best with **ASCII-friendly terminal fonts** and **default 80x25 console window size**.

---

## 🛠️ Customization

* 🌐 **Add more topics**: Modify the `Topics` dictionary in `ChatKnowledge.cs`.
* 🧵 **Change ASCII size**: Edit `new Size(80, 60)` in `AsciiArtDisplay.cs`.
* 🎶 **New intro**: Replace `greeting.wav` with your own `.wav` file.

---

## 🤖 Example Interaction

```
Hey there! What's your name? Tiisetso

Hi, Tiisetso! You're now chatting with JARVIS.  
Here's what you can ask about:  
...  

Ask me something (or type 'bye' to exit): phishing  
Phishing is an attack where hackers trick users into revealing sensitive information. Have you ever received a suspicious email? (yes/no)

> yes  
Be careful! Never click on suspicious links or enter personal info in emails. Would you like to learn how to report phishing attempts? (yes/no)
```

---

## 🧠 Author

Made with ❤️ and code. Inspired by Marvel's JARVIS.
Perfect for beginner chatbot projects, class demos, or basic AI interaction fun.

---

