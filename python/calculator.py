import re
from collections import namedtuple
import math

opinfo = namedtuple('Operator', 'precedence associativity')
operator_info = {
    "+": opinfo(0, "L"),
    "-": opinfo(0, "L"),
    "/": opinfo(1, "L"),
    "*": opinfo(1, "L"),
    "!": opinfo(2, "L"),
    "^": opinfo(2, "R"),
}

def tokenize(input_string):
    cleaned = re.sub(r'\s+', "", input_string)
    chars = list(cleaned)
    output = []
    state = ""
    buffer = ""

    while len(chars) != 0:
        char = chars.pop(0)
        if char.isdigit() or char == ".":
            if state != "num":
                output.append(buffer) if buffer != "" else False
                buffer = ""
            state = "num"
            buffer += char
        elif char in operator_info.keys() or char in ["(", ")"]:
            output.append(buffer) if buffer != "" else False
            buffer = ""
            output.append(char)
        else:
            if state != "func":
                output.append(buffer) if buffer != "" else False
                buffer = ""
            state = "func"
            buffer += char
    output.append(buffer) if buffer != "" else False
    return output

def shunt(tokens):
    tokens += ['end']
    operators = []
    output = []

    while len(tokens) != 1:
        current_token = tokens.pop(0)
        if current_token.isdigit():
            output.append(current_token)
        elif current_token in operator_info.keys():
            while True:
                if len(operators) == 0:
                    break
                satisfied = False
                if operators[-1].isalpha():
                    satisfied = True
                if operators[-1] not in ["(", ")"]:
                    if operator_info[operators[-1]].precedence > operator_info[current_token].precedence:
                        satisfied = True
                    elif operator_info[operators[-1]].precedence == operator_info[current_token].precedence:
                        if operator_info[operators[-1]].associativity == "left":
                            satisfied = True
                satisfied = satisfied and operators[-1] != "("
                if not satisfied:
                    break
                output.append(operators.pop())
            operators.append(current_token)
        elif current_token == "(":
            operators.append(current_token)
        elif current_token == ")":
            while True:
                if len(operators) == 0:
                    break
                if operators[-1] == "(":
                    break
                output.append(operators.pop())
            if len(operators) != 0 and operators[-1] == "(":
                operators.pop()
        else:
            operators.append(current_token)
    output.extend(operators[::-1])
    return output

def evaluate(tokens):
    stack = []
    for token in tokens:
        if (token.isdigit()):
            stack.insert(0, token)
        else:
            if (token == "+"):
                op1 = int(stack.pop())
                op2 = int(stack.pop())
                stack.insert(0, op1 + op2)
            elif(token == "-"):
                op1 = int(stack.pop())
                op2 = int(stack.pop())
                stack.insert(0, op1 - op2)
            elif(token == "/"):
                op1 = int(stack.pop())
                op2 = int(stack.pop())
                stack.insert(0, op1 / op2)
            elif(token == "*"):
                op1 = int(stack.pop())
                op2 = int(stack.pop())
                stack.insert(0, op1 * op2)
            elif(token == "!"):
                op1 = int(stack.pop())
                stack.insert(0, math.factorial(op1))
            elif(token == "^"):
                op1 = int(stack.pop())
                op2 = int(stack.pop())
                stack.insert(0, math.pow(op1, op2))
    
    return stack.pop()

def calculate(expression):
    return evaluate(shunt(tokenize(expression)))