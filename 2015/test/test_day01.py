from days.day01 import solve, solve2

def test_day01():
    assert solve('(())') == 0
    assert solve('()()') == 0
    assert solve('(((') == 3
    assert solve('(()(()(') == 3
    assert solve('))(((((') == 3
    assert solve('())') == -1
    assert solve('))(') == -1
    assert solve(')))') == -3
    assert solve(')())())') == -3

def test_day01_part2():
    assert solve2(')') == 1
    assert solve2('()())') == 5
