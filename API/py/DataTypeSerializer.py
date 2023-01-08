import sys

def LOCKER_RETURN_VALUE():
    if len(sys.argv) == 1:
        return None
    MAIN_VAR = sys.argv[1]
    
    match MAIN_VAR:
        case "null": return None
        case "nil": return None
        case "true": return True
        case "false": return False
        case _:
            if MAIN_VAR.isnumeric():
                _INT_VAR = int(MAIN_VAR)
                if _INT_VAR != None:
                    return _INT_VAR
                _FLOAT_VAR = float(MAIN_VAR)
                if _FLOAT_VAR != None:
                    return type(_FLOAT_VAR)

    return MAIN_VAR

if __name__ == "__main__":
    print(LOCKER_RETURN_VALUE())