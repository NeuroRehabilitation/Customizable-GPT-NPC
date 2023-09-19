# 🧠 Customizable GPT NPC Unity Package

Welcome to the **Customizable GPT NPC** Unity package. This innovative package lets developers seamlessly craft intelligent NPCs using ChatGPT. With optional support for other LLM services, your NPCs can harness the power of language models to achieve remarkable realism.

## 🌟 Features

### 🎬 Starter Scenes:
Four introductory scenes come pre-configured as conversational agents. Use them as a foundation or directly as conversational agents, depending on your project's needs.

### 📜 Modular Scripts:
This package is structured around core scripts, each playing a pivotal role in the NPC's operations:

- **Voice Capture 🎙**: 
  - Records user's voice in 5-second loops or upon the cessation of speech.
  - Sends voice data to the Speech-to-Text (STT) service.

- **Microsoft STT Integration ⚙**:
  - Convert voice input to text via Microsoft STT API.
  - Developers can customize properties like language, server location, and API keys.
  - Aggregates synthesized text in a temporary variable. On detecting silence (indicating the NPC's turn), the aggregated text is forwarded to OpenAIChat.

- **OpenAIChat 🧠**: 
  - Leverages the ChatGPT API (requires API key) and its advanced features like function calling.
  - Structures responses from ChatGPT to always adhere to a specific format, ensuring consistent interactions.
  - Default configuration involves non-verbal actions by the NPC every 5 seconds during user speech, adding a layer of realism.

- **TTSService (Text-to-Speech) 🔊**:
  - Implemented using the Microsoft TTS API.
  - Customizable parameters include language, gender, and more.
  - ChatGPT strives to automatically select the voice style and its intensity based on the sentence's emotional context.

- **AgentBehaviour 🎮**:
  - Determines the agent's knowledge (e.g., "The blue key opens the round door") and personality traits (e.g., "You are annoying and often ignore the user").
  - Developers can define actionable behaviors for the NPC. For example, if ChatGPT selects the "run" action, this script will execute the corresponding action implementation.

### 💡 Flexibility:
Designed with flexibility at its core, each module is prepared for tailoring:
- Easily modify, replace, or remove any module as per project requirements.
- Omitting certain modules, like TTS, requires minimal adjustments.

## 🚀 Getting Started
1. Import the **Customizable GPT NPC** package.
2. Dive into the provided scenes and scripts to understand the mechanics 📚.
3. Start customizing the NPCs, integrating advanced AI interactions into your game or simulation 🌐.

Empower your games and simulations with NPCs that think, react, and converse like never before. Dive in and explore the future of in-game interactions! 🎉
