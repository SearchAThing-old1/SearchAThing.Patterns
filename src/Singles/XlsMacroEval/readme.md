# XlsMacroEval

*SearchAThing.Patterns*

---

## Introduction

- we have an xlsm excel file with values computed over other cells using formulas that calls VBA macros

- we want to change data cell value and retrieve results of cells

## Example

- file [Book1.xlsm](https://github.com/devel0/SearchAThing.Patterns/blob/master/src/Singles/XlsMacroEval/XlsMacroEval/Book1.xlsm) contains a sheet like the following

| 1 | 2 | 3 |
| 6 | | |

where 60 is the result of a formula containing a macro : "=Macro1(A1:C1)"

and the macro is defined as follow VBA code

```visualbasic
Function Macro1(rng As Range)
    Dim val As Double
    val = 0
    For Each cell In rng
        val = val + cell.Value
    Next cell
    Macro1 = val
End Function
```

In the sample code we'll change 1, 2, 3 with 10, 20, 30 expectating 60 as result.

> **Tip:** Run the code from VS using CTRL+F5 in order to see the console before it close

> Note: to see the macro, from excel may you need to enable the Developer tab ( File -> Options -> Custom Ribbon -> Developer )

## Pattern

- add nuget package `Microsoft.Office.Interop.Excel`

- see the rest in the [code](https://github.com/devel0/SearchAThing.Patterns/blob/master/src/Singles/XlsMacroEval/XlsMacroEval/Program.cs)