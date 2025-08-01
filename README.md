# GEMS (Gym Exercise Monitoring System)

GEMS is a browser-based fitness assistant that helps users perform exercises safely and effectively using only a webcam and no extra hardware is needed. GEMS helps people by using pose estimation to classify exercises, count correct and incorrect repetitions, and provide real-time feedback. [Click here to watch the demo on YouTube](https://youtu.be/DDTzKsheceo)

## Features

- Works in the browser using a regular webcam.
- Uses MediaPipe to extract pose keypoints from live video.
- Classifies exercises and detects form errors using a deep learning model (LSTM).
- Uses rule-based logic and deep learning models (GRU) to count repetitions and correct mistakes.
- Saves user profiles and performance history securely.
- Integrated AI coach powered by LLaMA3 (via Groq API) to give tips based on history & stats and .


## Achievements

- 97.57% accuracy in exercise classification.
- 96–99% accuracy in deep learning-based form correction.
- 83–99% accuracy in rule-based form correction.
- 89-93% accuracy in deep learning based repetition counting.
- ±1 rep error in repetition counting.


## Technologies Used
- .NET C# for the front-end client app and user interface logic.
- Flask (Python) for back-end pose analysis, deep learning inference, and Mediapipe processing.
- MediaPipe for real-time human pose estimation from webcam video.
- PyTorch / Keras for LSTM and GRU-based deep learning models.
- JavaScript + HTML/CSS for browser interactivity and live feedback rendering.
- Groq API (LLaMA3) for natural language feedback and coaching.

## Limitations

- Requires proper webcam placement for accurate results.
- Can only detect a limited set of errors based on available training data.

## Creators 

- [Ahmed Galal](https://github.com/1AhmedGalal)
- [Ahmed Ayman](https://github.com/AhmedAymanMo)
- [Ahmed Reda](https://github.com/ahmedredaooooo)
- [Abdelrahman Warraky](https://github.com/0Abdelrahman1)
- [Osama Khaled](https://github.com)
- [Eslam Raed](https://github.com)
