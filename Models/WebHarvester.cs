using HtmlAgilityPack;
using System.Collections.Generic;
using System;

namespace Sudoku_Solver.Models
{
    class WebHarvester
    {
        internal List<string> GetPuzzle(int level)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = htmlWeb.Load("http://show.websudoku.com/?level=" + level);
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

        internal List<string> GetEasyPuzzle()
        {
            return GetPuzzle(1);
        }

        internal List<string> GetMediumPuzzle()
        {
            return GetPuzzle(2);
        }

        internal List<string> GetHardPuzzle()
        {
            return GetPuzzle(3);
        }

        internal List<string> GetEvilPuzzle()
        {
            return GetPuzzle(4);
        }
    }
}
