from pathlib import Path
from importlib import import_module

def main(input_dir: Path):
    package = __package__
    if package:
        package += '.'
    for day in [f'day{num:02}' for num in range(1, 26)]:
        try:
            # If we are running as module, this will be '2015.days.day01',
            # otherwise it will be 'days.day01'
            day_mod = import_module(f'{package}days.{day}')
        except ImportError as e:
            continue
        print('===========================')
        print(f'{day}:')
        try:
            input_file = input_dir / f'{day}.txt'
            data = input_file.read_text()
        except FileNotFoundError:
            print(' - Missing data file:', input_file)
            continue
        solve = getattr(day_mod, 'solve', None)
        if solve:
            print(' - part1:', solve(data))
        else:
            print(' - part1: Missing "solve" function')
        solve2 = getattr(day_mod, 'solve2', None)
        if solve2:
            print(' - part2:', solve2(data))
        else:
            print(' - part2: Missing "solve2" function')
    print('===========================')

if __name__ == '__main__':
    main(Path(__file__).absolute().parent / 'input')
