"""
Implementation for https://adventofcode.com/2015/day/2
"""
from math import prod

def solve(input: str) -> int:
    total = 0
    for line in input.splitlines():
        l, w, h = [int(val) for val in line.split('x')]
        sides = [l*w, w*h, h*l]
        total += min(sides)
        total += sum([side * 2 for side in sides])
    return total

def solve2(input: str) -> int:
    total = 0
    for line in input.splitlines():
        sizes = [int(val) for val in line.split('x')]
        total += sum([elem * 2 for elem in sorted(sizes)[:2]])
        total += prod(sizes)
    return total
