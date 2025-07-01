import socket
from vosk import Model, KaldiRecognizer
import pyaudio

HOST = 'localhost'
PORT = 5005

def send_command(cmd):
    try:
        s = socket.socket()
        s.connect((HOST, PORT))
        s.sendall(cmd.encode())
        s.close()
    except Exception as e:
        print(f"Socket error: {e}")

# Initialize Vosk model
model = Model(lang="en-us")  # You can download models from https://alphacephei.com/vosk/models
recognizer = KaldiRecognizer(model, 16000)

# Initialize microphone
p = pyaudio.PyAudio()
stream = p.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=8192)
stream.start_stream()

print("Listening for voice commands: 'up', 'down'")

while True:
    data = stream.read(4096, exception_on_overflow=False)
    
    if recognizer.AcceptWaveform(data):
        result = recognizer.Result()
        command = result[14:-3].lower()  # Extract text from JSON result
        print(f"Recognized: {command}")

        # Only send first valid command found
        for word in ["up", "down"]:
            if word in command:
                send_command(word)
                break
    else:
        partial_result = recognizer.PartialResult()
        # You can optionally print partial results if you want
        # print(partial_result)
