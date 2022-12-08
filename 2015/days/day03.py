"""
Implementation for https://adventofcode.com/2015/day/3
"""

def _get_visited(input):
    visited = set([ '0x0' ])
    x, y = 0, 0
    for char in input:
        if char in '<>':
            x += -1 if char == '<' else 1
        elif char in '^v':
            y += -1 if char == '^' else 1
        else:
            assert False, char
        visited.add(f'{x}x{y}')
    return visited

def solve(input: str) -> int:
    visited = _get_visited(input)
    return len(visited)

def solve2(input: str) -> int:
    santa = _get_visited(input[::2])
    robot = _get_visited(input[1::2])
    return len(santa.union(robot))

