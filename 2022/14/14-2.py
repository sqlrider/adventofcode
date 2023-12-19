# Advent of Code 2022 - Puzzle 14-2

import numpy
import time

puzzleinput = []

with open("input.txt", 'r') as file:
    for line in file:
        line = line.strip('\n')
        puzzleinput.append(line.split(' -> '))

def PrintRange(arr, y_start, y_end, x_start, x_end):
    for i in range(y_start, y_end):
        for j in range(x_start, x_end):
            print(arr[i][j],end='')
        print()
    print()

# Get min/max x and y values
x_min = 999999
x_max = 0
y_max = 0

for line in puzzleinput:
    for coord in line:
        coord = coord.split(',')
        if int(coord[0]) > x_max:
            x_max = int(coord[0])
        if int(coord[0]) < x_min:
            x_min = int(coord[0])
        if int(coord[1]) > y_max:
            y_max = int(coord[1])

# Add to range to cover sand spilling to sides
x_max += 301
y_max += 3

print(x_min)
print(x_max)
print(y_max)

# Initialise grid - extra 2 horizontal rows for Part 2
grid = numpy.chararray((y_max, x_max),unicode=True)
grid[:] = '-'
for i in range(0,x_max):
    grid[y_max-1][i] = '#'

# Add lines
for line in puzzleinput:
    for x in range(0, len(line)-1):
        coord = line[x].split(',')
        next_coord = line[x+1].split(',')
        current_x = int(coord[0])
        current_y = int(coord[1])
        next_x = int(next_coord[0])
        next_y = int(next_coord[1])

        # Set initial rock
        grid[current_y][current_x] = '#'

        # Get vector
        if next_x > current_x:
            for dx in range(0, abs(current_x - next_x) + 1):
                grid[current_y][current_x + dx] = '#'
        elif next_x < current_x:
            for dx in range(0, abs(current_x - next_x) + 1):
                grid[current_y][current_x - dx] = '#'
        elif next_y > current_y:
            for dy in range(0, abs(current_y - next_y) + 1):
                grid[current_y + dy][current_x] = '#'
        elif next_y < current_y:
            for dy in range(0, abs(current_y - next_y) + 1):
                grid[current_y - dy][current_x] = '#'
        else:
            print("ERROR")


# Sandfall
sand_at_rest = 0
sand_blocked = False

while(not sand_blocked):
    
    sand_x = 500
    sand_y = 0
    grid[sand_y][sand_x] = '+'
    #PrintRange(grid,0,y_max,x_min,x_max)
    
    current_sand_at_rest = False

    while(not current_sand_at_rest):

        if grid[sand_y + 1][sand_x] == '-':
            grid[sand_y + 1][sand_x] = '+'
            grid[sand_y][sand_x] = '-'
            sand_y += 1
        elif grid[sand_y + 1][sand_x - 1] == '-':
            grid[sand_y + 1][sand_x - 1] = '+'
            grid[sand_y][sand_x] = '-'
            sand_y += 1
            sand_x -= 1
        elif grid[sand_y + 1][sand_x + 1] == '-':
            grid[sand_y + 1][sand_x + 1] = '+'
            grid[sand_y][sand_x] = '-'
            sand_y += 1
            sand_x += 1
        else:
            current_sand_at_rest = True

        #if not current_sand_at_rest:
        #    PrintRange(grid,0,y_max,x_min-10,x_max)

    sand_at_rest += 1

    if grid[1][499] == '+' and grid[1][500] == '+' and grid[1][501] == '+':
        sand_blocked = True

PrintRange(grid,0,y_max,x_min-10,x_max)

print("Total sand at rest = ",sand_at_rest + 1)






