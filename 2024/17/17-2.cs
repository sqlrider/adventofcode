// https://adventofcode.com/2024/day/17#part2

using System.Text;

string[] puzzleinput = File.ReadAllLines(@"..\input.txt");

long reg_a = Convert.ToInt32(puzzleinput[0].Split(':')[1].Trim());
long reg_b = Convert.ToInt32(puzzleinput[1].Split(':')[1].Trim());
long reg_c = Convert.ToInt32(puzzleinput[2].Split(':')[1].Trim());

string program_line = puzzleinput[4].Split(':')[1].Trim();
string[] program_string = puzzleinput[4].Split(':')[1].Trim().Split(',');
int[] program = new int[program_string.Length];
for(int i = 0; i < program_string.Length; i++)
    program[i] = Convert.ToInt32(program_string[i]);

// The computer knows eight instructions, each identified by a 3-bit number (called the instruction's opcode).
// Each instruction also reads the 3-bit number after it as an input; this is called its operand

// A number called the instruction pointer identifies the position in the program from which the next opcode will be read; it starts at 0, pointing at the first 3-bit number in the program.
// Except for jump instructions, the instruction pointer increases by 2 after each instruction is processed (to move past the instruction's opcode and its operand

/*  Combo operands 0 through 3 represent literal values 0 through 3.
    Combo operand 4 represents the value of register A.
    Combo operand 5 represents the value of register B.
    Combo operand 6 represents the value of register C.
    Combo operand 7 is reserved and will not appear in valid programs. */

int instr_ptr;

List<int> outputs;

long a = 0;
while(true)
{
    instr_ptr = 0;
    reg_a = a;
    outputs = new List<int>();

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

            reg_a = Convert.ToInt64(Math.Truncate(Convert.ToDecimal(reg_a / Math.Pow(2,combo))));

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

            reg_b = Convert.ToInt64(combo) % 8;

            instr_ptr += 2;
        }

        // The jnz instruction (opcode 3) does nothing if the A register is 0. 
        // However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand;
        // if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
        else if(program[instr_ptr] == 3)
        {
            if(reg_a != 0)
            {
                instr_ptr = program[instr_ptr + 1];
            }
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

            outputs.Add(Convert.ToInt32(Convert.ToInt64(combo % 8)));

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

            reg_c = Convert.ToInt64(Math.Truncate(Convert.ToDecimal(reg_a / Math.Pow(2,combo))));

            instr_ptr += 2;
        }

        /*
        Console.WriteLine($"Register A: {reg_a}");
        Console.WriteLine($"Register B: {reg_b}");
        Console.WriteLine($"Register C: {reg_c}");
        */

        if(instr_ptr >= program.Length)
            break;
    }

    StringBuilder sb = new StringBuilder();

    for(int i = 0; i < outputs.Count; i++)
    {
        if(i == outputs.Count - 1)
            sb.Append(outputs[i]);
        else
            sb.Append($"{outputs[i]},");
    }

    Console.WriteLine(a);
    Console.WriteLine($"{a} - {program_line} vs {sb.ToString()}");

    if(sb.ToString() == program_line)
    {
        Console.WriteLine($"Found correct A value = {a}");
        break;
    }

    a+= 1000000000;
}
