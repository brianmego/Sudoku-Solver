using HtmlAgilityPack;
using System.Collections.Generic;
using System;

namespace Sudoku_Solver.Models
{
    class WebHarvester
    {
        internal List<string> GetSamplePuzzle()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = htmlWeb.Load("http://show.websudoku.com/?level=1");
            HtmlNodeCollection allInputs = document.DocumentNode.SelectNodes("//input");
            List<string> allSudokuSlots = new List<string>();
            foreach (HtmlNode node in allInputs)
            {
                if (node.Id.StartsWith("f"))
                {
                    allSudokuSlots.Add(node.GetAttributeValue("value", ""));
                }
            }
            return allSudokuSlots;
        }
    }
}
