#region SearchAThing.Patterns, Copyright(C) 2016 Lorenzo Delana, License under MIT
/*
* The MIT License(MIT)
* Copyright(c) 2016 Lorenzo Delana, https://searchathing.com
*
* Permission is hereby granted, free of charge, to any person obtaining a
* copy of this software and associated documentation files (the "Software"),
* to deal in the Software without restriction, including without limitation
* the rights to use, copy, modify, merge, publish, distribute, sublicense,
* and/or sell copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
* DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace XlsMacroEval
{
    class Program
    {
        static void Main(string[] args)
        {
            var xlsmPathfilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Book1.xlsm");

            try
            {
                var xl = new Excel.Application();
                xl.Visible = true;

                var wb = xl.Workbooks.Add(xlsmPathfilename);
                // first sheet
                var ws = (Excel.Worksheet)wb.Sheets.Item[1];

                ws.Cells[1, 1] = 10;
                ws.Cells[1, 2] = 20;
                ws.Cells[1, 3] = 30;

                var row = 2;
                var col = 1;

                var rngres = (Excel.Range)ws.Cells[row, col];

                Console.WriteLine($"cell value={rngres.Value2}");

                // don't save changes
                var saveChanges = false;

                wb.Close(saveChanges);

                xl.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error [{ex.Message}]");
            }
        }
    }
}
