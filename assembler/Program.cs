using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

if (args.Length < 2)
    return;

var file = args[0];
var output = args[1];
var lines = File.ReadAllLines(file);

var sb = new StringBuilder();
sb.AppendLine("v2.0 raw");

Dictionary<string, int> labels = new();
List<string> finalLines = new List<string>();
for (int i = 0; i < lines.Length; i++)
{
    var crr = lines[i];
    if (crr.Contains(':'))
    {
        labels.Add(crr.Replace(":", "").Trim(), 
            finalLines.Count
        );
        continue;
    }
    
    finalLines.Add(crr);
}

foreach (var line in finalLines)
{
    var opt = StringSplitOptions.RemoveEmptyEntries;
    var data = line.Split(' ', opt);
    
    if (data.Length == 0)
        continue;
    var inst = data[0];

    switch (inst)
    {
        case "nop":
            sb.Append("00");
            break;
        
        case "mov":
            if (data.Length == 2)
            {
                sb.Append('7');
                sb.Append(getData(byte.Parse(data[1])));
            }
            else if (data.Length == 3)
            {
                sb.Append('6');
                sb.Append(getData((byte)(
                    4 * int.Parse(data[1].Replace("$", "")) +
                    int.Parse(data[2].Replace("$", "")))
                ));
            }
            break;
        
        case "add":
            sb.Append('1');
            sb.Append(getData((byte)
                (4 * byte.Parse(data[1].Replace("$", "")) +
                byte.Parse(data[2].Replace("$", "")))
            ));
            break;
        
        case "mul":
            sb.Append('2');
            sb.Append(getData((byte)
                (4 * byte.Parse(data[1].Replace("$", "")) +
                byte.Parse(data[2].Replace("$", "")))
            ));
            break;
        
        case "and":
            sb.Append('3');
            sb.Append(getData((byte)
                (4 * byte.Parse(data[1].Replace("$", "")) +
                byte.Parse(data[2].Replace("$", "")))
            ));
            break;
        
        case "or":
            sb.Append('4');
            sb.Append(getData((byte)
                (4 * byte.Parse(data[1].Replace("$", "")) +
                byte.Parse(data[2].Replace("$", "")))
            ));
            break;
        
        case "cmp":
            sb.Append('5');
            sb.Append(getData((byte)
                (4 * byte.Parse(data[1].Replace("$", "")) +
                byte.Parse(data[2].Replace("$", "")))
            ));
            break;
        
        case "str":
            sb.Append('7');
            sb.Append(getData((byte)
                (4 + int.Parse(data[1].Replace("$", ""))))
            );
            break;
        
        case "ldr":
            sb.Append("78");
            break;
        
        case "ret":
            sb.Append("7E");
            break;
        
        case "jg":
            var jump = labels[data[1]];
            sb.Append(getData((byte)
                (8 + jump / 16)
            ));
            sb.Append(getData((byte)
                (jump % 16)
            ));
            break;
    }
    sb.Append(' ');
}

File.WriteAllText(output, sb.ToString());

string getData(byte b) 
    => Convert.ToHexString(new byte[] {
        b
    }).Substring(1);