# USAGE
# python classify.py --model pokedex.model --labelbin lb.pickle --image examples/charmander_counter.png

# import the necessary packages
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

class Connection:
    image_name = ""
    classification_result = ""

    @staticmethod
    def server():
        context = zmq.Context()
        socket = context.socket(zmq.REP)
        socket.bind("tcp://*:5555")

        while True:

            #  Wait for next request from client
            message = socket.recv()
            Connection.image_name = message.decode("utf-8")

            if Connection.image_name != "":
                image_classifier()

            Connection.image_name = ""

            result_to_unity = Connection.classification_result.encode()

            #  In the real world usage, you just need to replace time.sleep() with
            #  whatever work you want python to do.
            time.sleep(1)

            #  Send reply back to client
            #  In the real world usage, after you finish your work, send your output here
            socket.send(result_to_unity)

def image_classifier():
    model_path = "pokedex.model"
    label_binarizer_path = "lb.pickle"      
    image_path = "images/" + Connection.image_name

    # load the image
    image = cv2.imread(image_path)
    output = image.copy()
    
    # pre-process the image for classification
    image = cv2.resize(image, (96, 96))

    #cv2.imshow("RESIZED IMAGE", image)

    image = image.astype("float") / 255.0
    image = img_to_array(image)
    image = np.expand_dims(image, axis=0)
    
    # load the trained convolutional neural network and the label
    # binarizer
    print("[INFO] loading network...")
    model = load_model(model_path)
    lb = pickle.loads(open(label_binarizer_path, "rb").read())

    # classify the input image
    print("[INFO] classifying image...")
    proba = model.predict(image)[0]
    idx = np.argmax(proba)
    label = lb.classes_[idx]

    # we'll mark our prediction as "correct" of the input image filename
    # contains the predicted label text (obviously this makes the
    # assumption that you have named your testing image files this way)
    filename = image_path
    correct = "correct" if filename.rfind(label) != -1 else "incorrect"

    # build the label and draw the label on the image
    label = "{}: {:.2f}% ({})".format(label, proba[idx] * 100, correct)
    output = imutils.resize(output, width=400)
    cv2.putText(output, label, (10, 25),  cv2.FONT_HERSHEY_SIMPLEX,
        0.7, (0, 255, 0), 2)

    Connection.classification_result = label

    # show the output image
    print("[INFO] {}".format(label))
    #cv2.imshow("Output", output)
    #cv2.waitKey(0)

if __name__ == "__main__":
    Connection.server()