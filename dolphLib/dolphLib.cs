﻿using System;

namespace dolphLib
{
    public static class Configurator
    {
        // The method goes through the department configuration and returns a completed bypass sheet
        public static int[] RuleParser(int[] departmentRules, int[] rulesElse)
        {
            var bypassSheet = new int[departmentRules.Length]; // bypass sheet

            for (int i = 0; i < departmentRules.Length; i++)
            {
                switch (departmentRules[i])
                {
                    case 1: case 4:
                        if (bypassSheet[i] is 0) // if stamp in bypass sheet is 0 write stamp
                        {
                            bypassSheet[i] = i + 1;
                        }
                        // Checking Else block of rules 
                        i = ReturnRuleElse(rulesElse, bypassSheet, i);
                        break;
                    
                    case 2: case 5:
                        if (bypassSheet[i] >= 0) // If the bypass sheet has a stamp i, the parser crosses it out
                        {
                            bypassSheet[i] = -1;
                        }
                        i = ReturnRuleElse(rulesElse, bypassSheet, i);
                        
                        break;
                    
                    case 3: case 6:
                        Console.WriteLine("\n Enter goto department parameter");
                        var temporaryIterator = Checker.ParseIntOrReturn0(Console.ReadLine()); // Temporary iterator for switching to a given department 
                        
                        // The user sets the department number to which he wants to move the pointer, the ternary operator checks the entered rule using the ReturnParameterOrZero method
                        i = rulesElse[i] < 7 ? ReturnParameterOrZero(temporaryIterator, departmentRules) : ReturnRuleElse(rulesElse, bypassSheet, i);
                        
                        var temporaryBypass = bypassSheet; // temporary bypass sheet for checking loop
                        Console.WriteLine(Checker.IsLoop(bypassSheet, temporaryBypass));
                        break;
                }
            }
            // Console output of differences in the bypass sheet
            Console.WriteLine("\n Differences in bypass sheet: ");
            foreach (var t in bypassSheet)
            {
                Console.Write($"| {t} ");
            }
            return bypassSheet;
        }
        
        // A method that returns the value of the rule from the Else block
        private static int ReturnRuleElse(int[] rulesElse, int[] bypassSheet, int i)
        {
            switch (rulesElse[i])
            {
                case 7:
                    if (bypassSheet[i] is 0) // if stamp in bypass sheet is 0 write stamp
                    {
                        Console.WriteLine("\n Enter stamp number");
                        var temporaryStamp = Checker.ParseIntOrReturn0(Console.ReadLine());
                        bypassSheet[ReturnParameterOrZero(temporaryStamp, rulesElse)] = ReturnParameterOrZero(temporaryStamp, rulesElse);
                    }
                    break;
                
                case 8:
                    Console.WriteLine("\n Enter cross out number of stamp");
                    var temporaryDep = Checker.ParseIntOrReturn0(Console.ReadLine()); // temporary stamp number for cross out 
                    if (bypassSheet[i] == i + 1) // If the bypass sheet has a stamp i, the parser crosses it out
                    {
                        bypassSheet[ReturnParameterOrZero(temporaryDep, rulesElse)] = ReturnParameterOrZero(temporaryDep, rulesElse);
                    }
                    break;
                
                case 9:
                    Console.WriteLine("\n Enter goto department parameter"); 
                    var tempEnum = Checker.ParseIntOrReturn0(Console.ReadLine()); // Temporary iterator for switching to a given department 
                    i = ReturnParameterOrZero(tempEnum, rulesElse); // Write to iterator temporary enumerator fr goto department
                    break;
            }
            return i;
        }

        private static int ReturnParameterOrZero(int tempParam, int[] rules)
        {
            // check the parameter so that it does not go beyond the bypass sheet
            return tempParam <= rules.Length ? tempParam : 0;
        }
    }
}