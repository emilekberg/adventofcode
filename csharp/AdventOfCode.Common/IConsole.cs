using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common;

public interface IConsole
{
    void Write(string output);
    void WriteLine(string line);
    Task<MenuSelectionResult> Menu(List<string> options);

}
