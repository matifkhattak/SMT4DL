#https://machinelearningmastery.com/image-augmentation-deep-learning-keras/

from keras.preprocessing.image import ImageDataGenerator, array_to_img, img_to_array, load_img
from PIL import Image
import glob

from PIL import Image
from PIL import ImageOps
from PIL import ImageFilter
from PIL import ImageEnhance
from PIL import ImageDraw
from pathlib import Path


import random
import cv2


#affine transformations on image i.e., rotation, translation,scaling, shear etc.

# Rotation
def generateRotatedImages():

    ######Train Data#############
    print("Rotation: Processing Train NORMAL Data")
    for filename in glob.glob('./chest_xray/train/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image: # Open Origional image
            print("Processing Train NORMAL => ", filename)
            # Rotate Image By 180 Degree
            rotated_image = original_Image.rotate(180) #rotation by 180 degrees
            #rotated_image1.show()
            imageName = "./TransformedDatasets/Rotation/train/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing Train PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/train/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Train PNEUMONIA => ", filename)
            # Rotate Image By 180 Degree
            rotated_image = original_Image.rotate(180)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Rotation/train/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    ######Test Data#############
    print("Processing Test NORMAL Data")
    for filename in glob.glob('./chest_xray/test/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test NORMAL => ", filename)
            # Rotate Image By 180 Degree
            rotated_image = original_Image.rotate(180)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Rotation/test/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing Test PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/test/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test PNEUMONIA => ", filename)
            # Rotate Image By 180 Degree
            rotated_image = original_Image.rotate(180)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Rotation/test/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing val NORMAL Data")
    for filename in glob.glob('./chest_xray/val/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val NORMAL => ", filename)
            # Rotate Image By 180 Degree
            rotated_image = original_Image.rotate(180)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Rotation/val/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

    print("Processing val PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/val/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val PNEUMONIA => ", filename)
            # Rotate Image By 180 Degree
            rotated_image = original_Image.rotate(180)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Rotation/val/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

# Flipping
def generateFlippedImages():
    #https://pythonexamples.org/python-pillow-flip-image-vertical-horizontal/

    ######Train Data#############
    print("Flipped: Processing Train NORMAL Data")
    for filename in glob.glob('./chest_xray/train/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image: # Open Origional image
            print("Processing Train NORMAL => ", filename)
            # Flip Image Horizontally
            # Note:  original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM), FLIP_Left_Right is similar to mirroring horizontally
            rotated_image = original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM) # Flip Image w.r.t. Horizontal Axis
            #rotated_image1.show()
            imageName = "./TransformedDatasets/Flip/train/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing Train PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/train/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Train PNEUMONIA => ", filename)
            # Flip Image Horizontally
            rotated_image = original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Flip/train/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    ######Test Data#############
    print("Processing Test NORMAL Data")
    for filename in glob.glob('./chest_xray/test/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test NORMAL => ", filename)
            # Flip Image Horizontally
            rotated_image = original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Flip/test/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing Test PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/test/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test PNEUMONIA => ", filename)
            # Flip Image Horizontally
            rotated_image = original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Flip/test/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing val NORMAL Data")
    for filename in glob.glob('./chest_xray/val/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val NORMAL => ", filename)
            # Flip Image Horizontally
            rotated_image = original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Flip/val/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing val PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/val/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val PNEUMONIA => ", filename)
            # Flip Image Horizontally
            rotated_image = original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Flip/val/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


# Mirroring
def generateMirroredImages():

    ######Train Data#############
    print("Mirror: Processing Train NORMAL Data")
    for filename in glob.glob('./chest_xray/train/NORMAL/*.jpeg'):  #

        with Image.open(filename) as original_Image: # Open Origional image
            print("Processing Train NORMAL => ", filename)
            # Mirror Image
            # Note:  original_Image.transpose(Image.Transpose.FLIP_TOP_BOTTOM), FLIP_Left_Right is similar to mirroring horizontally
            rotated_image = ImageOps.mirror(original_Image) # Mirror Image horizontally:
            imageName = "./TransformedDatasets/Mirror/train/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing Train PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/train/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Train PNEUMONIA => ", filename)
            # Mirror Image
            rotated_image = ImageOps.mirror(original_Image)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Mirror/train/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)



    ######Test Data#############
    print("Processing Test NORMAL Data")
    for filename in glob.glob('./chest_xray/test/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test NORMAL => ", filename)
            # Mirror Image
            rotated_image = ImageOps.mirror(original_Image)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Mirror/test/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing Test PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/test/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test PNEUMONIA => ", filename)
            # Mirror Image
            rotated_image = ImageOps.mirror(original_Image)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Mirror/test/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing val NORMAL Data")
    for filename in glob.glob('./chest_xray/val/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val NORMAL => ", filename)
            # Mirror Image
            rotated_image = ImageOps.mirror(original_Image)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Mirror/val/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing val PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/val/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val PNEUMONIA => ", filename)
            # Mirror Image
            rotated_image = ImageOps.mirror(original_Image)
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Mirror/val/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

#  image smooting: Blur
def generateBlurredImages():
    #https://www.tutorialspoint.com/python_pillow/python_pillow_blur_an_image.htm
    ######Train Data#############
    print("Blur: Processing Train NORMAL Data")
    for filename in glob.glob('./chest_xray/train/NORMAL/*.jpeg'):  #

        with Image.open(filename) as original_Image: # Open Origional image
            print("Processing Train NORMAL => ", filename)
            # Blur Image
            rotated_image = original_Image.filter(ImageFilter.GaussianBlur(1)) # Blur Image
            imageName = "./TransformedDatasets/Blur1/train/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)



    print("Processing Train PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/train/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Train PNEUMONIA => ", filename)
            # Blur Image
            rotated_image = original_Image.filter(ImageFilter.GaussianBlur(1))
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Blur1/train/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)




    ######Test Data#############
    print("Processing Test NORMAL Data")
    for filename in glob.glob('./chest_xray/test/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test NORMAL => ", filename)
            # Blur Image
            rotated_image = original_Image.filter(ImageFilter.GaussianBlur(1))
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Blur1/test/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)



    print("Processing Test PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/test/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test PNEUMONIA => ", filename)
            # Blur Image
            rotated_image = original_Image.filter(ImageFilter.GaussianBlur(1))
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Blur1/test/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)



    print("Processing val NORMAL Data")
    for filename in glob.glob('./chest_xray/val/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val NORMAL => ", filename)
            # Blur Image
            rotated_image = original_Image.filter(ImageFilter.GaussianBlur(1))
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Blur1/val/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)



    print("Processing val PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/val/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val PNEUMONIA => ", filename)
            # Blur Image
            rotated_image = original_Image.filter(ImageFilter.GaussianBlur(1))
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Blur1/val/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

#  image sharpening
def generateSharpImages():
    ######Train Data#############
    print("Sharp: Processing Train NORMAL Data")
    for filename in glob.glob('./chest_xray/train/NORMAL/*.jpeg'):  #

        with Image.open(filename) as original_Image: # Open Origional image
            print("Processing Train NORMAL => ", filename)
            # Sharpen Image
            enhancer = ImageEnhance.Sharpness(original_Image)
            factor = 3
            rotated_image = enhancer.enhance(factor) # Sharpen Image
            imageName = "./TransformedDatasets/Sharp/train/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

    print("Processing Train PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/train/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Train PNEUMONIA => ", filename)
            # Sharpen Image
            enhancer = ImageEnhance.Sharpness(original_Image)
            factor = 3
            rotated_image = enhancer.enhance(factor)  # Sharpen Image
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Sharp/train/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    ######Test Data#############
    print("Processing Test NORMAL Data")
    for filename in glob.glob('./chest_xray/test/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test NORMAL => ", filename)
            # Sharpen Image
            enhancer = ImageEnhance.Sharpness(original_Image)
            factor = 3
            rotated_image = enhancer.enhance(factor)  # Sharpen Image
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Sharp/test/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

    print("Processing Test PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/test/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test PNEUMONIA => ", filename)
            # Sharpen Image
            enhancer = ImageEnhance.Sharpness(original_Image)
            factor = 3
            rotated_image = enhancer.enhance(factor)  # Sharpen Image
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Sharp/test/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)


    print("Processing val NORMAL Data")
    for filename in glob.glob('./chest_xray/val/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val NORMAL => ", filename)
            # Sharpen Image
            enhancer = ImageEnhance.Sharpness(original_Image)
            factor = 3
            rotated_image = enhancer.enhance(factor)  # Sharpen Image
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Sharp/val/NORMAL/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

    print("Processing val PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/val/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val PNEUMONIA => ", filename)
            # Sharpen Image
            enhancer = ImageEnhance.Sharpness(original_Image)
            factor = 3
            rotated_image = enhancer.enhance(factor)  # Sharpen Image
            # rotated_image1.show()
            imageName = "./TransformedDatasets/Sharp/val/PNEUMONIA/" + Path(filename).name
            # save the newly generated image
            rotated_image.save(imageName)

#  adding rectangle (outside of region of focus (rof): simulating as some part of the camera is covered
def generateImagesWithRectangleAdded():
    ######Test Data#############
    ######Train Data#############
    print("Rectange: Processing Train NORMAL Data")
    for filename in glob.glob('./chest_xray/train/NORMAL/*.jpeg'):  #

        with Image.open(filename) as original_Image: # Open Origional image
            print("Processing Train NORMAL => ", filename)
            # Adding rectangle
            draw = ImageDraw.Draw(original_Image)
            draw.rectangle((100, 100, 150, 150), fill="black", outline="black")
            original_Image.save("./TransformedDatasets/Rectangle/train/NORMAL/" + Path(filename).name)

    print("Processing Train PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/train/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Train PNEUMONIA => ", filename)
            # Adding rectangle
            draw = ImageDraw.Draw(original_Image)
            draw.rectangle((100, 100, 150, 150), fill="black", outline="black")
            original_Image.save("./TransformedDatasets/Rectangle/train/PNEUMONIA/" + Path(filename).name)


    ######Test Data#############
    print("Processing Test NORMAL Data")
    for filename in glob.glob('./chest_xray/test/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test NORMAL => ", filename)
            # Adding rectangle
            draw = ImageDraw.Draw(original_Image)
            draw.rectangle((100, 100, 150, 150), fill="black", outline="black")
            original_Image.save("./TransformedDatasets/Rectangle/test/NORMAL/" + Path(filename).name)

    print("Processing Test PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/test/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing Test PNEUMONIA => ", filename)
            # Adding rectangle
            draw = ImageDraw.Draw(original_Image)
            draw.rectangle((100, 100, 150, 150), fill="black", outline="black")
            original_Image.save("./TransformedDatasets/Rectangle/test/PNEUMONIA/" + Path(filename).name)


    print("Processing val NORMAL Data")
    for filename in glob.glob('./chest_xray/val/NORMAL/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val NORMAL => ", filename)
            # Adding rectangle
            draw = ImageDraw.Draw(original_Image)
            draw.rectangle((100, 100, 150, 150), fill="black", outline="black")
            original_Image.save("./TransformedDatasets/Rectangle/val/NORMAL/" + Path(filename).name)

    print("Processing val PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/val/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset

        with Image.open(filename) as original_Image:  # Open Origional image
            print("Processing val PNEUMONIA => ", filename)
            # Adding rectangle
            draw = ImageDraw.Draw(original_Image)
            draw.rectangle((100, 100, 150, 150), fill="black", outline="black")
            original_Image.save("./TransformedDatasets/Rectangle/val/PNEUMONIA/" + Path(filename).name)

#  adding/spil random dots (salt and pepper noise, which is only applicable to gray scale images): simulating dust
def generateImagesWithSaltAndPepperAdded():
    ######Train Data#############
    print("Salt and Pepper noise: Processing Train NORMAL Data")
    for filename in glob.glob('./chest_xray/train/NORMAL/*.jpeg'):   # iterate over all images in training dataset
        print("Processing Train NORMAL => ", filename)
        original_Image = cv2.imread(filename, cv2.IMREAD_GRAYSCALE)
        # Saving the image
        cv2.imwrite("./TransformedDatasets/ScatteredDots/train/NORMAL/" + Path(filename).name,
                        add_noise(original_Image))

    print("Processing Train PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/train/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset
        print("Processing Train PNEUMONIA => ", filename)
        original_Image = cv2.imread(filename, cv2.IMREAD_GRAYSCALE)
        # Saving the image
        cv2.imwrite("./TransformedDatasets/ScatteredDots/train/PNEUMONIA/" + Path(filename).name,
                    add_noise(original_Image))


    ######Test Data#############
    print("Processing Test NORMAL Data")
    for filename in glob.glob('./chest_xray/test/NORMAL/*.jpeg'):  # iterate over all images in training dataset
        print("Processing Test NORMAL => ", filename)
        original_Image = cv2.imread(filename, cv2.IMREAD_GRAYSCALE)
        # Saving the image
        cv2.imwrite("./TransformedDatasets/ScatteredDots/test/NORMAL/" + Path(filename).name,
                    add_noise(original_Image))

    print("Processing Test PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/test/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset
        print("Processing Test PNEUMONIA => ", filename)
        original_Image = cv2.imread(filename, cv2.IMREAD_GRAYSCALE)
        # Saving the image
        cv2.imwrite("./TransformedDatasets/ScatteredDots/test/PNEUMONIA/" + Path(filename).name,
                    add_noise(original_Image))


    print("Processing val NORMAL Data")
    for filename in glob.glob('./chest_xray/val/NORMAL/*.jpeg'):  # iterate over all images in training dataset
        print("Processing val NORMAL => ", filename)
        original_Image = cv2.imread(filename, cv2.IMREAD_GRAYSCALE)
        # Saving the image
        cv2.imwrite("./TransformedDatasets/ScatteredDots/val/NORMAL/" + Path(filename).name,
                    add_noise(original_Image))

    print("Processing val PNEUMONIA Data")
    for filename in glob.glob('./chest_xray/val/PNEUMONIA/*.jpeg'):  # iterate over all images in training dataset
        print("Processing val PNEUMONIA => ", filename)
        original_Image = cv2.imread(filename, cv2.IMREAD_GRAYSCALE)
        # Saving the image
        cv2.imwrite("./TransformedDatasets/ScatteredDots/val/PNEUMONIA/" + Path(filename).name,
                    add_noise(original_Image))


def add_noise(img):
    #https://www.geeksforgeeks.org/add-a-salt-and-pepper-noise-to-an-image-with-python/
    # Getting the dimensions of the image
    row, col = img.shape

    # Randomly pick some pixels in the
    # image for coloring them white
    # Pick a random number between 300 and 10000
    number_of_pixels = random.randint(300, 10000)
    for i in range(number_of_pixels):
        # Pick a random y coordinate
        y_coord = random.randint(0, row - 1)

        # Pick a random x coordinate
        x_coord = random.randint(0, col - 1)

        # Color that pixel to white
        img[y_coord][x_coord] = 255

    # Randomly pick some pixels in
    # the image for coloring them black
    # Pick a random number between 300 and 10000
    number_of_pixels = random.randint(300, 10000)
    for i in range(number_of_pixels):
        # Pick a random y coordinate
        y_coord = random.randint(0, row - 1)

        # Pick a random x coordinate
        x_coord = random.randint(0, col - 1)

        # Color that pixel to black
        img[y_coord][x_coord] = 0

    return img

if __name__ == '__main__':
    #generateImagesWithSaltAndPepperAdded()
    print("")


