# Advent of Code 2015

One possible implementation for [Advent of Code 2015](https://adventofcode.com/2015) in python3.

The input is read from data files with the names `day01.txt` .. `day25.txt`. By default this will be read from the directory '2015/input', but another folder can be specified as argument to `main()`.

The examples are used as input for simple unit tests, to be executed with pytest.

## Setup

- From the 2015 folder:
- Create a virtual environment: `python3 -m venv venv`
- Activate the virtual environment: `venv\Scripts\activate.bat`
- Install the requirements: `pip install -r requirements.txt`
- Validate that the tests pass: `pytest`
- Run it, either as module:
  - From the folder above `2015`: `py -m 2015`
- Or as script:
  - From the folder `2015`: `py .`
