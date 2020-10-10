import uuid, requests, os
from zipfile import ZipFile

def load_map():
    index = 0
    rectIndexX = 0
    rectIndexY = 0
    latitude = 51.633433071807616
    longitude = 19.144253803513553
    width = 0.11569976806
    height = 0.07186519543

    guid = uuid.uuid1()
    path = 'D:/Unity Projects/ARcardGame/MapGenerator/Maps/%s' % guid

    try:
        os.mkdir(path)
    except OSError:
        print("Creation of the directory %s failed" % path)
    else:
        print("Successfully created the directory %s " % path)

    for intertionY in range(0, 4):
        for iterationX in range(0, 4):
            get_map(latitude, longitude, width, height, rectIndexX, rectIndexY, guid, index)

            longitude += width
            rectIndexX += 1
            index += 1

        latitude += height
        rectIndexX  = 0
        rectIndexY += 1
        index += 1

def get_map(latitude, longitude, width, height, rectIndexX, rectIndexY, guid, index):
    print(f'https://terrain.party/api/export?box={longitude},{latitude},{longitude+width},{latitude+height}&name=Rect-{rectIndexX}{rectIndexY}')
    receive = requests.get(f'https://terrain.party/api/export?box={longitude},{latitude},{longitude+width},{latitude+height}&name=Rect-{rectIndexY}{rectIndexX}')
    with open(fr'D:\Unity Projects\ARcardGame\MapGenerator\Maps\{guid}\{index}.zip', 'wb') as f:
        f.write(receive.content)
        with ZipFile(fr'D:\Unity Projects\ARcardGame\MapGenerator\Maps\{guid}\{index}.zip', 'r') as zipObj:
            listOfFileNames = zipObj.namelist()
            for fileName in listOfFileNames:
                if fileName.startswith(f'Rect-{rectIndexY}{rectIndexX} Height Map (Merged).png'):
                    zipObj.extract(fileName, fr'D:\Unity Projects\ARcardGame\MapGenerator\Maps\{guid}')
                    print(f'Extracted Rect-{rectIndexY}{rectIndexX}.png')
    os.remove(fr'D:\Unity Projects\ARcardGame\MapGenerator\Maps\{guid}\{index}.zip')

if __name__ == '__main__':
    load_map()
