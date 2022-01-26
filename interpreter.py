from dataclasses import replace
import sys
from time import sleep

from calculator import calculate

SPACE = ' '
args = sys.argv

def parseDictionary(lines):
    sourceMap = {}

    for line in lines:
        if line != "" and not line.startswith("#"):
            tokens = line.split(' ')
            label = tokens[0]
            command = SPACE.join(tokens[1:])
            sourceMap[label] = command
    return sourceMap

def getNextKey(currentKey, sourceMap):
    keys = list(sourceMap.keys())
    index = keys.index(currentKey)

    if index == len(keys) - 1:
        return "END"
    else:
        return keys[index + 1]

def execute(sourceMap):
    CURRENT_KEY = ''
    CURRENT_COMMAND = ''
    CONTINUE_EXECUTION = True
    NEXT_KEY = ''
    
    REGISTER_MAP = {}
    FILE_MAP = {}

    while (CONTINUE_EXECUTION):
        if (CURRENT_KEY == ''):
            CURRENT_KEY = next(iter(sourceMap)) #Gets the first key in the source map
        elif (NEXT_KEY == ''):
            CURRENT_KEY = getNextKey(CURRENT_KEY, sourceMap)
        else:
            CURRENT_KEY = NEXT_KEY
            NEXT_KEY = ''
        
        if CURRENT_KEY == "END":
            break

        CURRENT_COMMAND = sourceMap[CURRENT_KEY]
        tokens = CURRENT_COMMAND.split(' ')
        keyword = tokens[0]
        args = tokens[1:]

        argIndex = 0
        for arg in args:
            if (arg.startswith('$')):
                varName = arg.replace('$', '')
                if (varName in REGISTER_MAP):
                    args[argIndex] = REGISTER_MAP[varName]
            argIndex = argIndex + 1

        if keyword == "PRINT":
            print(SPACE.join(args).replace('"', ''))
        elif keyword == "GOTO":
            NEXT_KEY = args[0]
        elif keyword == "STORE":
            register = args[0]
            value = SPACE.join(args[1:])
            REGISTER_MAP[register] = value
        elif keyword == "INPUT":
            register = args[0]
            inputText = str(input(SPACE.join(args[1:]).replace('"', '')))
            REGISTER_MAP[register] = inputText
        elif keyword == "CALC":
            register = args[0]
            expression = SPACE.join(args[1:])
            result = str(calculate(expression))
            REGISTER_MAP[register] = result
        elif keyword == "ASSIGN":
            register = args[0]
            file = args[1]
            textFile = open(file)
            FILE_MAP[register] = textFile
        elif keyword == "READ":
            file = args[0]
            register = args[1]
            textFile = FILE_MAP[file]
            value = textFile.readline().replace("\n", "")
            if not value:
                value = "EOF"
            REGISTER_MAP[register] = value
        elif keyword == "COMPARE":
            label = args[0]
            param1 = args[1]
            mode = args[2]
            param2 = args[3]

            if mode == "EQ":
                if (param1 == param2):
                    NEXT_KEY = label
            elif mode == "LT":
                if (int(param1) < int(param2)):
                    NEXT_KEY = label
            elif mode == "GT":
                if (int(param1) > int(param2)):
                    NEXT_KEY = label
            elif mode == "NE":
                if (param1 != param2):
                    NEXT_KEY = label
            else:
                raise SyntaxError("Invalid comparison mode '" + mode + "'")
        else:
            raise SyntaxError("Invalid command word '" + keyword + "'")

if len(args) == 1:
    print("No input file specified")
else:
    fileName = args[1]
    with open(fileName, 'r') as sourceFile:
        lines = sourceFile.read().split('\n')
        sourceMap = parseDictionary(lines)
        execute(sourceMap)
