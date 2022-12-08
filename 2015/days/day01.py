
def solve(input: str) -> int:
    floor = 0
    for char in input:
        assert char in '()'
        floor += 1 if char == '(' else -1
    return floor

def solve2(input: str) -> int:
    floor = 0
    for idx, char in enumerate(input):
        assert char in '()'
        floor += 1 if char == '(' else -1
        if floor < 0:
            return idx + 1
    return floor

