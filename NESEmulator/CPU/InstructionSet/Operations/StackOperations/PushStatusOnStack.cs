﻿using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PushStatusOnStack : IOperation
    {
        //It writes the status register to the stack pointer with the B flag set (and the U)
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            var previousBreakFlagStatus = registers.GetFlag(StatusRegisterFlags.BRKCommand);
            //First, it sets the U & B flags
            registers.SetFlag(StatusRegisterFlags.BRKCommand, true);
            registers.SetFlag(StatusRegisterFlags.Unused, true); // in theory, this is always 1 anyway
            //and then it saves the status un the stack, which is decreased
            bus.CPUWrite((ushort)(0x0100 + registers.GetStackPointer()), registers.GetStatus());
            registers.DecrementStackPointer();
            //and it resets the previous B flag
            registers.SetFlag(StatusRegisterFlags.BRKCommand, previousBreakFlagStatus);

            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}