// https://adventofcode.com/2024/day/17

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

foreach(string s in puzzleinput)
{
    Console.WriteLine(s);
}

int reg_a = Convert.ToInt32(puzzleinput[0].Split(':')[1].Trim());
int reg_b = Convert.ToInt32(puzzleinput[1].Split(':')[1].Trim());
int reg_c = Convert.ToInt32(puzzleinput[2].Split(':')[1].Trim());

string[] program_string = puzzleinput[4].Split(':')[1].Trim().Split(',');
int[] program = new int[program_string.Length];
for(int i = 0; i < program_string.Length; i++)
    program[i] = Convert.ToInt32(program_string[i]);

Console.WriteLine($"Register A: {reg_a}");
Console.WriteLine($"Register B: {reg_b}");
Console.WriteLine($"Register C: {reg_c}");

int instr_ptr = 0;

List<int> outputs = new List<int>();

while(true)
{
    // The adv instruction (opcode 0) performs division. The numerator is the value in the A register
    // The denominator is found by raising 2 to the power of the instruction's combo operand.
    // (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.)
    // The result of the division operation is truncated to an integer and then written to the A register.
    if(program[instr_ptr] == 0)
    {
        double combo = 0;
        switch(program[instr_ptr+1])
        {
            case 0: combo = 0; break;
            case 1: combo = 1; break;
            case 2: combo = 2; break;
            case 3: combo = 3; break;
            case 4: combo = reg_a; break;
            case 5: combo = reg_b; break;
            case 6: combo = reg_c; break;
            default: break;
        }

        reg_a = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(reg_a / Math.Pow(2,combo))));

        instr_ptr += 2;
    }

    // The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.
    else if(program[instr_ptr] == 1)
    {
        reg_b = reg_b ^ program[instr_ptr+1];
        instr_ptr += 2;
    }

    // The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value to the B register.
    else if(program[instr_ptr] == 2)
    {
        int combo = 0;
        switch(program[instr_ptr+1])
        {
            case 0: combo = 0; break;
            case 1: combo = 1; break;
            case 2: combo = 2; break;
            case 3: combo = 3; break;
            case 4: combo = reg_a; break;
            case 5: combo = reg_b; break;
            case 6: combo = reg_c; break;
            default: break;
        }

        reg_b = combo % 8;

        instr_ptr += 2;
    }

    // The jnz instruction (opcode 3) does nothing if the A register is 0. 
    // However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand;
    // if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
    else if(program[instr_ptr] == 3)
    {
        if(reg_a != 0)
            instr_ptr = program[instr_ptr + 1];
        else
            instr_ptr += 2;
    }

    // The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C,
    // then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)
    else if(program[instr_ptr] == 4)
    {
        reg_b = reg_b ^ reg_c;
        instr_ptr += 2;
    }

    // The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value.
    // (If a program outputs multiple values, they are separated by commas.)
    else if(program[instr_ptr] == 5)
    {
        int combo = 0;
        switch(program[instr_ptr+1])
        {
            case 0: combo = 0; break;
            case 1: combo = 1; break;
            case 2: combo = 2; break;
            case 3: combo = 3; break;
            case 4: combo = reg_a; break;
            case 5: combo = reg_b; break;
            case 6: combo = reg_c; break;
            default: break;
        }

        outputs.Add(combo % 8);

        instr_ptr += 2;
    }

    // The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register.
    // (The numerator is still read from the A register.)
    else if(program[instr_ptr] == 6)
    {
        double combo = 0;
        switch(program[instr_ptr+1])
        {
            case 0: combo = 0; break;
            case 1: combo = 1; break;
            case 2: combo = 2; break;
            case 3: combo = 3; break;
            case 4: combo = reg_a; break;
            case 5: combo = reg_b; break;
            case 6: combo = reg_c; break;
            default: break;
        }

        reg_b = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(reg_a / Math.Pow(2,combo))));

        instr_ptr += 2;
    }

    // The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register.
    // (The numerator is still read from the A register.)
    else if(program[instr_ptr] == 7)
    {
        double combo = 0;
        switch(program[instr_ptr+1])
        {
            case 0: combo = 0; break;
            case 1: combo = 1; break;
            case 2: combo = 2; break;
            case 3: combo = 3; break;
            case 4: combo = reg_a; break;
            case 5: combo = reg_b; break;
            case 6: combo = reg_c; break;
            default: break;
        }

        reg_c = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(reg_a / Math.Pow(2,combo))));

        instr_ptr += 2;
    }

    if(instr_ptr >= program.Length)
        break;
}


for(int i = 0; i < outputs.Count; i++)
{
    if(i == outputs.Count - 1)
        Console.Write(outputs[i]);
    else
        Console.Write($"{outputs[i]},");
}
