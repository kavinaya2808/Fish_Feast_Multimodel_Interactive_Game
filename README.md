# Voice and Gesture Controlled Cooperative Shark Game
This project showcases a multimodal user interface that enables two players to control a shark character in a game using two distinct input modalities: voice and gesture. This project explores how asymmetric input roles can enhance cooperation and user engagement in shared experiences.

# What Is This Project About?

We built a 2-player cooperative game where:

🗣️ Player 1 controls the movement of the shark (up/down) using voice commands.

✋ Player 2 controls actions (eat fish and block bombs) using facial gestures and hand gestures.
The goal is to collaboratively navigate the shark to eat fish (score points) and avoid bombs (avoid losing points).

# Modalities Used

1. Voice Modality (Player 1)
Uses Vosk speech recognition in Python
Recognizes simple commands: "up" and "down"
Sent to the game engine via TCP socket
2. Gesture Modality (Player 2)
Uses MediaPipe for real-time gesture detection:
Mouth open → eat fish
Palm detection → block bomb
Communicates with Unity via socket connection

# CASE/CARE Multimodal Framework
Our design follows multimodal principles:

Assignment: Each player is responsible for specific input types
Complementarity: Voice handles direction; gestures handle actions
Equivalence: Players can switch roles (modality independence)

