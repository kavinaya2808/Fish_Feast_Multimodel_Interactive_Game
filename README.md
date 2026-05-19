# Voice and Gesture Controlled Cooperative Shark Game
This project showcases a multimodal user interface that enables two players to control a shark character in a game using two distinct input modalities: voice and gesture. This project explores how asymmetric input roles can enhance cooperation and user engagement in shared experiences.

<img width="800" height="500" alt="Screenshot 2025-05-23 at 2 18 36 AM" src="https://github.com/user-attachments/assets/1f779f46-b7d0-4bbb-85ea-94e000331e76" />


# What Is This Project About?

We built a 2-player cooperative game where:

🗣️ Player 1 controls the movement of the shark (up/down) using voice commands.

✋ Player 2 controls actions (eat fish and block bombs) using facial gestures and hand gestures.
The goal is to collaboratively navigate the shark to eat fish (score points) and avoid bombs (avoid losing points).

<img width="352" height="175" alt="Screenshot 2025-05-23 at 2 19 39 AM" src="https://github.com/user-attachments/assets/df328b88-dd4d-44c2-9bea-96deaa97a313" />
<img width="352" height="175" alt="Screenshot 2025-05-23 at 2 18 41 AM" src="https://github.com/user-attachments/assets/a8c2f395-a0a4-44f0-96ab-f79193c06a53" />

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

<img width="400" height="250" alt="Screenshot 2025-05-22 at 11 23 08 PM" src="https://github.com/user-attachments/assets/44e01105-6001-440a-b4e8-f4cff4aaf765" />
<img width="400" height="250" alt="FER2" src="https://github.com/user-attachments/assets/cb9e226e-c1a2-4d0a-944f-cb8db6ddadaa" />


# CASE/CARE Multimodal Framework
Our design follows multimodal principles:

Assignment: Each player is responsible for specific input types
Complementarity: Voice handles direction; gestures handle actions
Equivalence: Players can switch roles (modality independence)

