from days.day02 import solve, solve2

def test_day02():
    assert solve('2x3x4') == 58
    assert solve('1x1x10') == 43
    assert solve('2x3x4\n1x1x10') == 58 + 43
    assert solve('') == 0

def test_day02_part2():
    assert solve2('2x3x4') == 34
    assert solve2('1x1x10') == 14
    assert solve2('2x3x4\n1x1x10') == 34 + 14
    assert solve2('') == 0
