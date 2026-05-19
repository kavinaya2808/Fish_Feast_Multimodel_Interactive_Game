import cv2
import mediapipe as mp
import socket
import time


HOST = 'localhost'  
PORT = 5005

def send_command(cmd):
    try:
        s = socket.socket()
        s.connect((HOST, PORT))
        s.sendall(cmd.encode())
        s.close()
    except ConnectionRefusedError:
        print("⚠️ Could not connect to Unity (Is it running and listening?)")


mp_face_mesh = mp.solutions.face_mesh
face_mesh = mp_face_mesh.FaceMesh(static_image_mode=False, max_num_faces=1, refine_landmarks=True)

mp_hands = mp.solutions.hands
hands = mp_hands.Hands(static_image_mode=False, max_num_hands=1, min_detection_confidence=0.7)

mp_drawing = mp.solutions.drawing_utils

cap = cv2.VideoCapture(0)

MOUTH_OPEN_THRESHOLD = 0.10  
cooldown = 1.5  
last_bite = 0
last_block = 0

def get_mouth_aspect_ratio(landmarks):
    # Use multiple points for top and bottom lips
    top_lip = (landmarks[13].y + landmarks[312].y + landmarks[82].y) / 3
    bottom_lip = (landmarks[14].y + landmarks[317].y + landmarks[87].y) / 3
    left = landmarks[78].x
    right = landmarks[308].x
    return (bottom_lip - top_lip) / (right - left)


def is_hand_open(hand_landmarks):
    fingers_open = []

    # Thumb
    fingers_open.append(hand_landmarks.landmark[4].x < hand_landmarks.landmark[3].x)

    # Index, Middle, Ring, Pinky
    for tip_id in [8, 12, 16, 20]:
        finger_open = hand_landmarks.landmark[tip_id].y < hand_landmarks.landmark[tip_id - 2].y
        fingers_open.append(finger_open)

    return all(fingers_open)


while True:
    ret, frame = cap.read()
    if not ret:
        print("❌ Failed to read frame from webcam")
        continue

    frame = cv2.flip(frame, 1)
    frame = cv2.resize(frame, (480, 480))  
    rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    face_result = face_mesh.process(rgb)
    hands_result = hands.process(rgb)

    hand_detected = False
    if hands_result.multi_hand_landmarks:
        for hand in hands_result.multi_hand_landmarks:
            if is_hand_open(hand) and (time.time() - last_block) > cooldown:
                print("🖐️ Stop Hand Detected!")
                send_command("block")
                last_block = time.time()
                hand_detected = True
            mp_drawing.draw_landmarks(frame, hand, mp_hands.HAND_CONNECTIONS)

    if face_result.multi_face_landmarks:
        for face in face_result.multi_face_landmarks:
            mar = get_mouth_aspect_ratio(face.landmark)
            if mar > MOUTH_OPEN_THRESHOLD and (time.time() - last_bite) > cooldown:
                print("👄 Mouth Open Detected!")
                send_command("bite")
                last_bite = time.time()
            mp_drawing.draw_landmarks(frame, face, mp_face_mesh.FACEMESH_CONTOURS)

    cv2.imshow("Mouth + Hand Detection", frame)
    if cv2.waitKey(5) & 0xFF == 27:  
        break

cap.release()
cv2.destroyAllWindows()
