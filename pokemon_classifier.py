# Import the necessary packages
from keras.preprocessing.image import img_to_array
from keras.models import load_model
import numpy as np
import argparse
import imutils
import pickle
import cv2
import os
import time
import zmq

# Server class to establish TCP communication with Unity
class Connection:
    image_name = ""
    classification_result = ""

    @staticmethod
    def server():
        context = zmq.Context()
        socket = context.socket(zmq.REP)                # Server socket
        socket.bind("tcp://*:5555")                     # Port number 

        while True:
            # Wait for next request from client
            message = socket.recv()

            # Decode the received message from bytes to string 
            Connection.image_name = message.decode("utf-8")

            # Run the image classification algorithm
            if Connection.image_name != "":
                image_classifier()

            # Prevent the classification algorithm from continuously running
            # by resetting received message
            Connection.image_name = ""

            # Encode the result of classification from string to bytes
            result_to_unity = Connection.classification_result.encode()

            # The connection is set to sleep in each cycle of the loop
            time.sleep(1)

            # Send the classification result back to client
            socket.send(result_to_unity)


# Function with an algorithm to analyze and classify images of pokemon
# It outputs name, % and correct/incorrect tag
def image_classifier():
    model_path = "pokedex.model"                        # path to the trained model
    label_binarizer_path = "lb.pickle"                  # path to the label binarizer file      
    image_path = "images/" + Connection.image_name      # input image file path

    # Load the image
    image = cv2.imread(image_path)
    output = image.copy()
    
    # Pre-process the image for classification
    image = cv2.resize(image, (96, 96))
    image = image.astype("float") / 255.0
    image = img_to_array(image)
    image = np.expand_dims(image, axis=0)
    
    # Load the trained convolutional neural network and the label
    # binarizer
    print("[INFO] loading network...")
    model = load_model(model_path)
    lb = pickle.loads(open(label_binarizer_path, "rb").read())

    # Classify the input image
    print("[INFO] classifying image...")
    proba = model.predict(image)[0]
    idx = np.argmax(proba)
    label = lb.classes_[idx]

    # The prediction is marked as "correct" if the input image filename
    # contains the predicted label text
    filename = image_path
    correct = "correct" if filename.rfind(label) != -1 else "incorrect"

    # Build the label and draw the label on the image
    label = "{}: {:.2f}% ({})".format(label, proba[idx] * 100, correct)
    output = imutils.resize(output, width=400)
    cv2.putText(output, label, (10, 25),  cv2.FONT_HERSHEY_SIMPLEX,
        0.7, (0, 255, 0), 2)

    # Save the output result in a variable
    Connection.classification_result = label

    print("[INFO] {}".format(label))


# Run the server
if __name__ == "__main__":
    Connection.server()