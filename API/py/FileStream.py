import sys

def LOCKER_FILE_SAVE():
    with open(sys.argv[2], 'w') as FILESTREAM_MAIN:
        FILESTREAM_MAIN.write(sys.argv[3])
        FILESTREAM_MAIN.close()

def LOCKER_FILE_READ():
    FILESTREAM_MAIN = open(sys.argv[2], 'a')
    FILESTREAM_RESULT = FILESTREAM_MAIN.read()
    FILESTREAM_RESULT = FILESTREAM_RESULT.split('\n')
    return FILESTREAM_RESULT

if __name__ == '__main__':
    match sys.argv[1]:
        case 's': LOCKER_FILE_SAVE()
        case 'r': LOCKER_FILE_READ()
