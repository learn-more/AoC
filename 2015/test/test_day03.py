from days.day03 import solve, solve2

def test_day03():
    assert solve('>') == 2
    assert solve('^>v<') == 4
    assert solve('^v^v^v^v^v') == 2
    assert solve('') == 1

def test_day03_part2():
    assert solve2('^v') == 3
    assert solve2('^>v<') == 3
    assert solve2('^v^v^v^v^v') == 11
